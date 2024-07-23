using System.Management.Automation;
using Ominous.Constants;

namespace Ominous.Tests;

public class ConvertToLinkTest : AbstractPSCmdletTest
{
    public ConvertToLinkTest() : base(VerbsData.ConvertTo, Nouns.Link, inputParameterName: "Value") { }
}