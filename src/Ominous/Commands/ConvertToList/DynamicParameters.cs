using System.Management.Automation;

namespace Ominous.Commands;

public partial class ConvertToListCmdlet
{
    private OrderedListDynamicParameters dynOl;

    public class OrderedListDynamicParameters
    {
        [Parameter(HelpMessage = "Starting ordered list number", ParameterSetName = "OrderedList")]
        [ValidateRange(minRange: 2, maxRange: int.MaxValue)]
        public int Start { get; set; } = 1;
    }

    public object GetDynamicParameters()
    {
        if (MyInvocation.InvocationName.ToLower().Equals("orderedlist"))
        {
            dynOl = new();
            return dynOl;
        }

        return null;
    }
}