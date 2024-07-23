using System.Management.Automation;
using Ominous.Constants;

namespace Ominous.Tests;

public class ConvertToCodeTest : AbstractPSCmdletTest
{
    public ConvertToCodeTest() : base(VerbsData.ConvertTo, Nouns.Code, inputParameterName: "Value") { }
}