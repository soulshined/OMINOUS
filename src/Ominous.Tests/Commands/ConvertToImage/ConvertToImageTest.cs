using System.Management.Automation;
using Ominous.Constants;

namespace Ominous.Tests;

public class ConvertToImageTest : AbstractPSCmdletTest
{
    public ConvertToImageTest() : base(VerbsData.ConvertTo, Nouns.Image, inputParameterName: "Value") { }
}