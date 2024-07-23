using System.Management.Automation;
using Ominous.Constants;
using Ominous.Model;

namespace Ominous.Commands;

[Cmdlet(
    VerbsData.ConvertTo,
    Nouns.Heading,
    HelpUri = "https://github.com/soulshined/OMINOUS/blob/master/publish/generate-docs/cmdlets/ConvertTo-Heading.md"
)]
[Alias(
    Nouns.Heading,
    "Header",
    "H1",
    "H2",
    "H3",
    "H4",
    "H5",
    "H6"
)]
[OutputType(
    typeof(string)
)]
public partial class ConvertToHeadingCmdlet : AbstractValueCmdlet
{
    [Parameter(Position = 1, HelpMessage = "The header level (1-6)")]
    [ValidateRange(minRange: 1, maxRange: 6)]
    [ValidateNotNull()]
    public int Level { get; set; } = 1;

    [Parameter(Position = 2, HelpMessage = "Specify a specific anchor name")]
    [ValidateNotNullOrEmpty()]
    public string Id { get; set; }

    protected override void BeginProcessing()
    {
        base.BeginProcessing();
        Level = MyInvocation.InvocationName.ToLower() switch
        {
            "h2" => 2,
            "h3" => 3,
            "h4" => 4,
            "h5" => 5,
            "h6" => 6,
            _ => Level
        };
    }

    protected override void ProcessRecord() =>
        WriteObject(Convert(Value, Level, ref State, Id).ToMarkdown(Preference.Flavor));

    internal static ConversionResult Convert(string value, int level, ref State state, string id = null) =>
        new(value, level, ref state, id);
}