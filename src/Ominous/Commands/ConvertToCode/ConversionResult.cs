using System;
using System.Text;
using Ominous.Constants;
using Ominous.Extensions;
using Ominous.Model;

namespace Ominous.Commands;

public partial class ConvertToCodeCmdlet
{
    internal static readonly string BACKTICKS = "```";

    internal sealed class ConversionResult : AbstractConversionResult
    {
        private string Value { get; }
        private string Language { get; }
        internal ConversionResult(string value, string lang, ref State state) : base(0, ref state)
        {
            Value = value.Trim();
            Language = lang?.Trim();
        }

        public override string ToMarkdown(FlavorType flavor)
        {
            var resolved = ExecAfterBlocks(Value);

            StringBuilder sb = new();

            sb.Append(BACKTICKS).AppendLine(Language ?? "")
                .AppendLine(resolved)
                .AppendLine(BACKTICKS);

            return EOL + sb.ToString().TrimNewLines() + EOL;
        }

        public override string ToHtml(FlavorType flavor) => throw new NotImplementedException();
    }

}