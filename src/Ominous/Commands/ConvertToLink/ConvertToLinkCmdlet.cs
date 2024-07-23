using System.Management.Automation;
using Ominous.Constants;
using Ominous.Model;

namespace Ominous.Commands;

[Cmdlet(
    VerbsData.ConvertTo,
    Nouns.Link,
    HelpUri = "https://github.com/soulshined/OMINOUS/blob/master/publish/generate-docs/cmdlets/ConvertTo-Link.md"
)]
[Alias(
    Nouns.Link
)]
[OutputType(
    typeof(string)
)]
public partial class ConvertToLinkCmdlet : AbstractValueCmdlet
{
    [Parameter(Position = 1, HelpMessage = "Display title of link (optional)")]
    [ValidateNotNullOrEmpty()]
    public string Title;

    [Parameter(HelpMessage = "Coerces a link to be an explicit HTML <a> tag with a _blank target attr value")]
    public SwitchParameter NewTab;

    protected override void ProcessRecord() =>
        WriteObject(Convert(Value, Title, NewTab.IsPresent, ref State).ToMarkdown(Preference.Flavor));

    internal static ConversionResult Convert(string value, string title, bool isNewTab, ref State state) =>
        new(value, ref state, title: title, isNewTab: isNewTab);
}