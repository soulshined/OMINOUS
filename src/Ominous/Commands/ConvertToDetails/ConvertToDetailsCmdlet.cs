using System.Management.Automation;
using Ominous.Constants;
using Ominous.Model;

namespace Ominous.Commands;

[Cmdlet(
    VerbsData.ConvertTo,
    Nouns.Details,
    HelpUri = "https://github.com/soulshined/OMINOUS/blob/master/publish/generate-docs/cmdlets/ConvertTo-Details.md"
)]
[Alias(
    Nouns.Details,
    "ConvertTo-Collapse",
    "Collapse"
)]
[OutputType(
    typeof(string)
)]
public partial class ConvertToDetailsCmdlet : AbstractValueCmdlet
{
    [Parameter(Position = 1, HelpMessage = "The description of the drop down caption")]
    [ValidateNotNullOrEmpty()]
    [Alias("Title")]
    public string Summary { get; set; } = "Details";

    protected override void ProcessRecord() =>
        WriteObject(Convert(Value, Summary, ref State).ToHtml(Preference.Flavor));

    internal static ConversionResult Convert(string value, string summary, ref State state) =>
        new(value, summary, ref state);
}