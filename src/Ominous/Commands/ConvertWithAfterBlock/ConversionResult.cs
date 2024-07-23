using System;
using Ominous.Constants;
using Ominous.Extensions;
using Ominous.Model;

namespace Ominous.Commands;

public partial class ConvertWithAfterBlockCmdlet
{

    internal sealed class ConversionResult : AbstractConversionResult
    {
        private string Value { get; }
        internal ConversionResult(string value, ref State state) : base(0, ref state)
        {
            Value = value.TrimNewLines();
        }

        public override string ToMarkdown(FlavorType flavor) => ExecAfterBlocks(Value).TrimNewLines() + EOL;

        public override string ToHtml(FlavorType flavor) => throw new NotImplementedException();
    }

}