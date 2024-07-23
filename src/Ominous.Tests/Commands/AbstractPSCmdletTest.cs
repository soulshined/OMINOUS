using System.Runtime.CompilerServices;
using static Ominous.Tests.PSCmdletRunner;
using Ominous.Tests.Extensions;
using System.Diagnostics;

namespace Ominous.Tests;

public abstract class AbstractPSCmdletTest : AbstractTest
{

    private string Verb { get; }
    private string Noun { get; }

    private string InputParameterName { get; }

    protected AbstractPSCmdletTest(string verb, string noun, string inputParameterName = "InputObject", [CallerFilePath] string? path = null) : base(path)
    {
        if (path == null)
            throw new ArgumentNullException(path);

        Verb = verb;
        Noun = noun;
        InputParameterName = inputParameterName;
    }

    [Fact]
    public void TestCases_ShouldBeValid()
    {
        var cases = GetTestCases();

        foreach (var i in cases)
        {
            if (i.GivenConfig.IsDisabled) continue;

            RunnerResult result;

            if (i.GivenConfig.Type.Equals("expression"))
            {
                result = InvokeExpressionExpectSuccess(i.Given);
            }
            else
            {
                var io = InvokeExpression(i.Given);
                Assert.False(io.HadErrors, new List<object>(new string[] { $"Test case failed : {i.DirectoryName}/{i.FileName}{Environment.NewLine}", io.ErrorStream.Join() }).Join());

                var parameters = new Dictionary<string, object>(){
                    { InputParameterName, InputParameterName.Equals("Value") ? io.Output[0].BaseObject : io.Output }
                };

                if (!string.IsNullOrWhiteSpace(i.GivenConfig.Arguments))
                {
                    var argSplits = i.GivenConfig.Arguments.Split("-", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    foreach (var arg in argSplits)
                    {
                        var thisArgSplits = arg.Split(" ");
                        if (thisArgSplits.Length == 1)
                        {
                            parameters.Add(thisArgSplits[0], true);
                        }
                        else
                        {
                            parameters.Add(thisArgSplits[0], thisArgSplits[1]);
                        }
                    }
                }

                result = InvokeCommandExpectSuccess(parameters);
            }

            Assert.False(result.HadErrors, result.ErrorStream.Join());

            Debug.WriteLine(result.Output.Join());
            var first = i.Expected.Equals(result.Output.Join());
            Debug.WriteLine($"first => {first}");
            var second = Environment.NewLine + i.Expected;
            Debug.WriteLine($"second => {second}");
            var secondResult = second.Equals(result.Output.Join());
            Debug.WriteLine($"second result => {secondResult}");
            var output = result.Output.Join();
            if (!i.Expected.Equals(result.Output.Join()) && !(Environment.NewLine + i.Expected).Equals(result.Output.Join()))
            {
                Debug.WriteLine($"Test Case Failed => {i.DirectoryName}/{i.FileName}");
                Assert.Equal(i.Expected, result.Output.Join());
            }
        }
    }

    protected RunnerResult InvokeCommandExpectSuccess(Dictionary<string, object>? parameters)
    {
        return AbstractTest.InvokeCommandExpectSuccess(GetCommandIdentifier(), parameters);
    }

    protected static new RunnerResult InvokeCommandExpectSuccess(string alias, Dictionary<string, object>? parameters)
    {
        return AbstractTest.InvokeCommandExpectSuccess(alias, parameters ?? new Dictionary<string, object>());
    }

    protected RunnerResult InvokeCommandExpectSuccess(object[] args)
    {
        return AbstractTest.InvokeCommandExpectSuccess(GetCommandIdentifier(), args);
    }

    protected static new RunnerResult InvokeCommandExpectSuccess(string alias, params object[] args)
    {
        return AbstractTest.InvokeCommandExpectSuccess(alias, args);
    }

    private string GetCommandIdentifier()
    {
        return $"{Verb}-{Noun}";
    }

}