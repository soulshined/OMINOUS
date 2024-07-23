using System.Management.Automation;
using Ominous.Constants;
using Ominous.Model;

namespace Ominous.Commands;

[Cmdlet(
    VerbsData.ConvertTo,
    Nouns.Image,
    HelpUri = "https://github.com/soulshined/OMINOUS/blob/master/publish/generate-docs/cmdlets/ConvertTo-Image.md"
    )]
[Alias(
    Nouns.Image
)]
[OutputType(
    typeof(string)
)]
public partial class ConvertToImageCmdlet : AbstractValueCmdlet
{
    [Parameter(HelpMessage = "The title of an image link (optional): `![<alt?>](<url> \"<title?>\")`")]
    public string Title { get; set; }

    [Parameter(HelpMessage = "The url of an image link: `![<alt?>](<url> \"<title?>\")`")]
    public string Link { get; set; }

    [Parameter(HelpMessage = "The alt of an image link (defaults to title first, then url): `![<alt?>](<url> \"<title?>\")`")]
    public string Alt { get; set; }

    [Parameter(HelpMessage = "The caption applied under a link (optional)")]
    public string Caption { get; set; }

    protected override void ProcessRecord() =>
        WriteObject(Convert(Value, Alt, Title, Caption, Link, ref State).ToMarkdown(Preference.Flavor));

    internal static ConversionResult Convert(string value, string alt, string title, string caption, string link, ref State state) =>
        new(value, alt, title, caption, ref state, link);
}