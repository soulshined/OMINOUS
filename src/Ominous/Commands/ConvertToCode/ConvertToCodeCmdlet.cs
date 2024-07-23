using System.Management.Automation;
using Ominous.Constants;
using Ominous.Model;

namespace Ominous.Commands;

[Cmdlet(
    VerbsData.ConvertTo,
    Nouns.Code,
    HelpUri = "https://github.com/soulshined/OMINOUS/blob/master/publish/generate-docs/cmdlets/ConvertTo-Code.md"
)]
[Alias(
    Nouns.Code
)]
[OutputType(
    typeof(string)
)]
public partial class ConvertToCodeCmdlet : AbstractValueCmdlet
{
    [Parameter(HelpMessage = "Specify the language/syntax for highlighting")]
    [ValidateNotNullOrEmpty()]
    [Alias("Syntax")]
    public string Language { get; set; }

    protected override void ProcessRecord() =>
        WriteObject(Convert(Value, ref State, Language).ToMarkdown(Preference.Flavor));

    internal static ConversionResult Convert(string value, ref State state, string lang = null) =>
        new(value, lang, ref state);
}