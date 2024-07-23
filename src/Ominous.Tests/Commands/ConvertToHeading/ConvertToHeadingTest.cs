using System.Management.Automation;
using Ominous.Constants;
using Ominous.Tests.Extensions;

namespace Ominous.Tests;

public class ConvertToHeadingTest : AbstractPSCmdletTest
{
    public ConvertToHeadingTest() : base(VerbsData.ConvertTo, Nouns.Heading) { }

    [InlineData("# Foobar", "Value", "Foobar")]
    [InlineData("# Foobar", "Value", "Foobar", "Level", 1)]
    [InlineData("## Foobar", "Value", "Foobar", "Level", 2)]
    [InlineData("### Foobar", "Value", "Foobar", "Level", 3)]
    [InlineData("#### Foobar", "Value", "Foobar", "Level", 4)]
    [InlineData("##### Foobar", "Value", "Foobar", "Level", 5)]
    [InlineData("###### Foobar", "Value", "Foobar", "Level", 6)]
    [InlineData("# Foobar {#MyHeader}", "Value", "Foobar", "Id", "MyHeader")]
    [InlineData("# Foobar {#MyHeader}", "Value", "Foobar", "Level", 1, "Id", "MyHeader")]
    [InlineData("## Foobar {#MyHeader}", "Value", "Foobar", "Level", 2, "Id", "MyHeader")]
    [InlineData("### Foobar {#MyHeader}", "Value", "Foobar", "Level", 3, "Id", "MyHeader")]
    [InlineData("#### Foobar {#MyHeader}", "Value", "Foobar", "Level", 4, "Id", "MyHeader")]
    [InlineData("##### Foobar {#MyHeader}", "Value", "Foobar", "Level", 5, "Id", "MyHeader")]
    [InlineData("###### Foobar {#MyHeader}", "Value", "Foobar", "Level", 6, "Id", "MyHeader")]
    [Theory]
    public void Should_ConvertTo_Heading_ByParameter(string expected, params object[] args)
    {
        var _params = new Dictionary<string, object>();

        for (int i = 0; i < args.Length; i += 2)
            _params.Add(args[i].ToString()!, args[i + 1]);

        var result = InvokeCommandExpectSuccess(_params);
        Assert.Equal(Environment.NewLine + expected + Environment.NewLine, result.Output.Join());
    }

    [InlineData("# Foobar", "Foobar")]
    [InlineData("# Foobar", "Foobar", 1)]
    [InlineData("## Foobar", "Foobar", 2)]
    [InlineData("### Foobar", "Foobar", 3)]
    [InlineData("#### Foobar", "Foobar", 4)]
    [InlineData("##### Foobar", "Foobar", 5)]
    [InlineData("###### Foobar", "Foobar", 6)]
    [InlineData("# Foobar {#MyHeader}", "Foobar", 1, "MyHeader")]
    [InlineData("## Foobar {#MyHeader}", "Foobar", 2, "MyHeader")]
    [InlineData("### Foobar {#MyHeader}", "Foobar", 3, "MyHeader")]
    [InlineData("#### Foobar {#MyHeader}", "Foobar", 4, "MyHeader")]
    [InlineData("##### Foobar {#MyHeader}", "Foobar", 5, "MyHeader")]
    [InlineData("###### Foobar {#MyHeader}", "Foobar", 6, "MyHeader")]
    [Theory]
    public void Should_ConvertTo_Heading_ByPosition(string expected, params object[] args)
    {
        var result = InvokeCommandExpectSuccess(args);
        Assert.Equal(Environment.NewLine + expected + Environment.NewLine, result.Output.Join());
    }

    [InlineData("# Foobar", "Heading", "Foobar")]
    [InlineData("# Foobar", "Header", "Foobar")]
    [InlineData("# Foobar", "H1", "Foobar")]
    [InlineData("## Foobar", "H2", "Foobar")]
    [InlineData("### Foobar", "H3", "Foobar")]
    [InlineData("#### Foobar", "H4", "Foobar")]
    [InlineData("##### Foobar", "H5", "Foobar")]
    [InlineData("###### Foobar", "H6", "Foobar")]
    [Theory]
    public void Regression_Should_Use_Invocation_Name(string expected, string alias, string input)
    {
        var result = InvokeCommandExpectSuccess(alias, input);
        Assert.Equal(Environment.NewLine + expected + Environment.NewLine, result.Output.Join());
    }
}