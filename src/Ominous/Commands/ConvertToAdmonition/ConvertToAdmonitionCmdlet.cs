using System.Management.Automation;
using Ominous.Constants;
using Ominous.Model;

namespace Ominous.Commands;

[Cmdlet(
    VerbsData.ConvertTo,
    Nouns.Admonition,
    HelpUri = "https://github.com/soulshined/OMINOUS/blob/master/publish/generate-docs/cmdlets/ConvertTo-Admonition.md"
)]
[Alias(
    Nouns.Admonition,
    "Alert",
    "Tip",
    "Warning",
    "Caution",
    "Important"
)]
[OutputType(
    typeof(string)
)]
public partial class ConvertToAdmonitionCmdlet : AbstractValueCmdlet
{
    [Parameter(HelpMessage = "Synonymous to a 'callout' type (i.e. tip, note, warning etc)")]
    public AdmonitionType Type { get; set; } = AdmonitionType.NOTE;

    [Parameter(HelpMessage = "For flavorless markdown, use this to add a caption to the callout")]
    public SwitchParameter WithCaption { get; set; }

    protected override void BeginProcessing()
    {
        base.BeginProcessing();
        Type = MyInvocation.InvocationName.ToLower() switch
        {
            "tip" => AdmonitionType.TIP,
            "warning" => AdmonitionType.WARNING,
            "caution" => AdmonitionType.CAUTION,
            "important" => AdmonitionType.IMPORTANT,
            _ => Type
        };
    }

    protected override void ProcessRecord() =>
        WriteObject(Convert(Type, Value, ref State, WithCaption.IsPresent).ToMarkdown(Preference.Flavor));

    internal static ConversionResult Convert(AdmonitionType type, string value, ref State state, bool hasCaption = false) =>
        new(value, ref state, type, hasCaption);
}