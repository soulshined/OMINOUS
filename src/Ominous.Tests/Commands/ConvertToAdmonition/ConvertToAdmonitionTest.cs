using System.Management.Automation;
using Ominous.Constants;

namespace Ominous.Tests;

public class ConvertToAdmonitionTest : AbstractPSCmdletTest
{
    public ConvertToAdmonitionTest() : base(VerbsData.ConvertTo, Nouns.Admonition, inputParameterName: "Value") { }
}