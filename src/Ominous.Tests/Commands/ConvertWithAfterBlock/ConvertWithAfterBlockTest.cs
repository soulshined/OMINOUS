using System.Management.Automation;
using Ominous.Constants;

namespace Ominous.Tests;

public class ConvertWithAfterBlockTest : AbstractPSCmdletTest
{
    public ConvertWithAfterBlockTest() : base(VerbsData.Convert, "With" + Nouns.AfterBlock, inputParameterName: "Value") { }
}