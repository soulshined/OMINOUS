using System;
using System.Collections.Generic;
using System.Management.Automation;
using Ominous.Constants;
using Ominous.Extensions;
using Ominous.Model;

namespace Ominous.Commands;

public partial class ConvertToAdmonitionCmdlet
{

    internal sealed class ConversionResult : AbstractConversionResult
    {
        private AdmonitionType Type { get; }
        private string Value { get; }
        private bool HasCaption { get; }
        internal ConversionResult(string value, ref State state, AdmonitionType type = AdmonitionType.NOTE, bool hasCaption = false) : base(0, ref state)
        {
            Value = value.Trim();
            Type = type;
            HasCaption = hasCaption;
        }

        public override string ToMarkdown(FlavorType flavor)
        {
            string mkdn = flavor switch
            {
                FlavorType.Github => string.Format("[!{0}]\n{1}", Type, Value.TrimStart()),
                _ => string.Format("{0}{1} {2}", GetTypeEmoji(), HasCaption ? $" **{Type}**: " : "", Value.TrimStart()),
            };

            var bq = ConvertToBlockquoteCmdlet.Convert(new List<PSObject>(new PSObject[] { mkdn.ToPSObject() }), ref State).ToMarkdown(flavor);
            return EOL + ExecAfterBlocks(bq).TrimNewLines() + EOL;
        }

        private string GetTypeEmoji()
        {
            return Type switch
            {
                AdmonitionType.NOTE => ":spiral_notepad:",
                AdmonitionType.CAUTION => ":heavy_exclamation_mark:",
                AdmonitionType.IMPORTANT => ":no_entry_sign:",
                AdmonitionType.TIP => ":bulb:",
                AdmonitionType.WARNING => ":warning:",
                _ => "",
            };
        }

        public override string ToHtml(FlavorType flavor) => throw new NotImplementedException();
    }

}