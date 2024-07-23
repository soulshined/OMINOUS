using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using Ominous.Constants;
using Ominous.Extensions;
using Ominous.Model;

namespace Ominous.Commands;

public partial class ConvertToBlockquoteCmdlet
{

    internal sealed class ConversionResult : AbstractConversionResult
    {
        private List<PSObject> Items { get; }

        internal ConversionResult(List<PSObject> items, ref State state, uint depth = 0) : base(depth, ref state)
        {
            Items = items;
        }

        private string Normalize(string s)
        {
            s = ExecAfterBlocks(s);
            var prefix = '>'.Repeat(Depth);
            return s.Split('\n').Select(i => $"{prefix} {i.TrimStart()}").Join();
        }

        public override string ToMarkdown(FlavorType flavor)
        {
            StringBuilder sb = new();

            foreach (PSObject pso in Items)
            {
                if (pso.BaseObject is string s)
                {
                    sb.AppendLine(Normalize(s));
                }
                else if (pso.IsIterable() && !pso.IsDictionary())
                {
                    var it = (List<PSObject>)pso.BaseObject;
                    foreach (var i in it)
                    {
                        var convertedItem = new ConversionResult(new List<PSObject>(new PSObject[] { i }), ref State, Depth + 1);
                        sb.AppendLine(convertedItem.ToMarkdown(flavor).TrimNewLines());
                    }
                }
                else
                {
                    sb.Append(Normalize(pso.BaseObject?.ToString() ?? ""));
                }
            }

            return EOL + sb.ToString().TrimNewLines() + EOL;
        }

        public override string ToHtml(FlavorType flavor) =>
            throw new NotImplementedException();
    }

}