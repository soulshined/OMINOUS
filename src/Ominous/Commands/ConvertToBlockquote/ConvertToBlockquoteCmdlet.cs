using System.Collections.Generic;
using System.Management.Automation;
using Ominous.Constants;
using Ominous.Model;

namespace Ominous.Commands;

[Cmdlet(
    VerbsData.ConvertTo,
    Nouns.Blockquote,
    HelpUri = "https://github.com/soulshined/OMINOUS/blob/master/publish/generate-docs/cmdlets/ConvertTo-Blockquote.md"
)]
[Alias(
    Nouns.Blockquote,
    "Quote"
)]
[OutputType(
    typeof(string)
)]
public partial class ConvertToBlockquoteCmdlet : AbstractPSObjectCmdlet
{
    protected override void EndProcessing()
    {
        base.EndProcessing();
        WriteObject(Convert(Items, ref State).ToMarkdown(Preference.Flavor));
    }

    internal static ConversionResult Convert(List<PSObject> items, ref State state) =>
        new(items, ref state);
}