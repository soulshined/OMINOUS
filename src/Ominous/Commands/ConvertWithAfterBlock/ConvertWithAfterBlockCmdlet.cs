using System.Management.Automation;
using Ominous.Commands.Transformers;
using Ominous.Constants;
using Ominous.Model;

namespace Ominous.Commands;

[CmdletBinding]
[Cmdlet(
    VerbsData.Convert,
    "With" + Nouns.AfterBlock,
    HelpUri = "https://github.com/soulshined/OMINOUS/blob/master/publish/generate-docs/cmdlets/Convert-WithAfterBlock.md"
)]
[Alias(
    Nouns.AfterBlock,
    "Block",
    "Markdown",
    "Mkdn"
)]
[OutputType(
    typeof(string)
)]
public partial class ConvertWithAfterBlockCmdlet : AbstractPSCmdlet
{
    [Parameter(ValueFromPipeline = true, Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = "Nullable string to convert to markdown")]
    [AllowNull]
    [AllowEmptyString]
    public string Value { get; set; }

    [Parameter(Position = 1, HelpMessage = @"An after block is a scriptblock with ominous attributes specified that the content provided to it will be converted to")]
    [Alias("After")]
    [AfterBlockTransformation]
    public override AfterBlock[] AfterBlock { get; set; }

    protected override void ProcessRecord()
    {
        if (!string.IsNullOrWhiteSpace(Value))
            WriteObject(Convert(Value, ref State).ToMarkdown(Preference.Flavor));
    }

    internal static ConversionResult Convert(string value, ref State state) =>
        new(value, ref state);
}