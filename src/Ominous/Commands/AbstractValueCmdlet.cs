using System.Management.Automation;

namespace Ominous.Commands;

public abstract class AbstractValueCmdlet : AbstractPSCmdlet
{
    [Parameter(ValueFromPipeline = true, Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true, HelpMessage = "Nullable string to convert to markdown")]
    public string Value { get; set; }
}
