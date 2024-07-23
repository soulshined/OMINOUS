using System;
using System.Text;
using Ominous.Constants;
using Ominous.Model;

namespace Ominous.Commands;

public partial class ConvertToDetailsCmdlet
{

    internal sealed class ConversionResult : AbstractConversionResult
    {
        private string Summary { get; }
        private string Value { get; }

        internal ConversionResult(string value, string summary, ref State state, uint depth = 0) : base(depth, ref state)
        {
            Summary = summary.Trim();
            Value = value.Trim();
        }

        public override string ToHtml(FlavorType flavor)
        {
            StringBuilder sb = new();
            sb.AppendLine("<details>")
                .AppendLine("<summary>" + Summary + "</summary>")
                .AppendLine(ExecAfterBlocks(Value, true))
                .AppendLine("</details>");
            return EOL + sb.ToString() + EOL;
        }

        public override string ToMarkdown(FlavorType flavor) => ToHtml(flavor);
    }

}