using System.Management.Automation;
using System.Text;
using Ominous.Constants;
using Ominous.Tests.Extensions;

namespace Ominous.Tests;

public class ConvertToDetailsTest : AbstractPSCmdletTest
{
    public ConvertToDetailsTest() : base(VerbsData.ConvertTo, Nouns.Details) { }

    [InlineData("This is a very detailed thing", "Value", "This is a very detailed thing")]
    [InlineData("This is a very detailed thing", "Value", "This is a very detailed thing", "Title", "Example")]
    [InlineData("This is a very detailed thing", "Value", "This is a very detailed thing", "Summary", "Examples")]
    [Theory]
    public void Should_ConvertTo_Details_ByParameter(string expected, params object[] args)
    {
        var _params = new Dictionary<string, object>();

        for (int i = 0; i < args.Length; i += 2)
            _params.Add(args[i].ToString()!, args[i + 1]);

        var result = InvokeCommandExpectSuccess(_params);

        string summary = (string)(_params.ContainsKey("Title") ? _params["Title"] : _params.ContainsKey("Summary") ? _params["Summary"] : "Details");
        Assert.Equal(GetDetails(expected, summary), result.Output.Join());
    }

    [Fact]
    public void Should_ConvertTo_Details_ByPosition()
    {
        var input = "This is a very detailed thing";
        var result = InvokeCommandExpectSuccess(new object[] { input });
        Assert.Equal(GetDetails(input), result.Output.Join());
    }

    [Fact]
    public void Should_ConvertTo_Details_ByPosition2()
    {
        var input = "This is a very detailed thing";
        var result = InvokeCommandExpectSuccess(new object[] { input, "Examplez" });
        Assert.Equal(GetDetails(input, "Examplez"), result.Output.Join());
    }

    [InlineData("This is a very detailed thing", "Details")]
    [InlineData("This is a very detailed thing", "ConvertTo-Collapse")]
    [InlineData("This is a very detailed thing", "Collapse")]
    [Theory]
    public void Regression_Should_Use_Invocation_Name(string expected, string alias)
    {
        var result = InvokeCommandExpectSuccess(alias, new object[] { expected });
        Assert.Equal(GetDetails(expected), result.Output.Join());
    }

    private static string GetDetails(string value, string summary = "Details")
    {
        StringBuilder sb = new();
        sb.AppendLine("<details>")
            .AppendLine("<summary>" + summary + "</summary>")
            .AppendLine(value)
            .AppendLine("</details>");
        return Environment.NewLine + sb.ToString() + Environment.NewLine;
    }
}