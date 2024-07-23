using System;
using System.Text;
using Ominous.Constants;
using Ominous.Extensions;
using Ominous.Model;

namespace Ominous.Commands;

public partial class ConvertToHeadingCmdlet
{

    internal sealed class ConversionResult : AbstractConversionResult
    {
        private string Id { get; }
        private int Level { get; }
        private string Value { get; }

        internal ConversionResult(string value, int level, ref State state, string id = null, uint depth = 0) : base(depth, ref state)
        {
            Level = level;
            Value = value;
            Id = id;
        }

        public override string ToMarkdown(FlavorType flavor)
        {
            var sb = new StringBuilder();

            sb.Append('#'.Repeat(Level - 1)).Append(" ").Append(ExecAfterBlocks(Value.TrimNewLines()));
            if (!string.IsNullOrWhiteSpace(Id))
                sb.Append($" {{#{Id}}}");

            return EOL + sb.ToString().TrimNewLines() + EOL;
        }

        public override string ToHtml(FlavorType flavor) =>
            throw new NotImplementedException();
    }

}