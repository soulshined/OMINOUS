using System.Management.Automation;
using Ominous.Constants;

namespace Ominous.Tests;

public class ConvertToListTest : AbstractPSCmdletTest
{
    public ConvertToListTest() : base(VerbsData.ConvertTo, Nouns.List) { }
    [Fact]
    public void Run()
    {
        TestCases_ShouldBeValid();
    }
}