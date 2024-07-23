using System.Linq;
using System.Management.Automation;
using Ominous.Model;

namespace Ominous.Commands.Transformers;

internal sealed class AfterBlockTransformationAttribute : ArgumentTransformationAttribute
{
    public override object Transform(EngineIntrinsics engineIntrinsics, object inputData)
    {
        if (inputData is ScriptBlock sb)
            return new AfterBlock[] { new(sb) };
        else if (inputData is ScriptBlock[] sba)
            return ((ScriptBlock[])inputData).Select(i => new AfterBlock(i)).ToArray();

        return inputData;
    }
}