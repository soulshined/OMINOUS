using System.Runtime.CompilerServices;
using static Ominous.Tests.PSCmdletRunner;

namespace Ominous.Tests;


public abstract class AbstractTest
{

    protected string TestClassPath { get; private set; }
    protected string TestClassCasesDirectory { get; private set; }

    protected AbstractTest([CallerFilePath] string? path = null)
    {
        if (path == null)
            throw new ArgumentNullException(path);

        TestClassPath = Path.GetFullPath(path);
        TestClassCasesDirectory = Path.Join(Path.GetDirectoryName(TestClassPath), "cases");
    }

    protected static RunnerResult InvokeCommandExpectSuccess(string commandName, Dictionary<string, object>? parameters)
    {
        var result = InvokeCommand(commandName, parameters ?? new Dictionary<string, object>());
        HandleError(result);
        return result;
    }

    protected static RunnerResult InvokeCommandExpectSuccess(string commandName, params object[] args)
    {
        var result = InvokeCommand(commandName, args);
        HandleError(result);
        return result;
    }

    protected static RunnerResult InvokeExpressionExpectSuccess(string scriptblock)
    {
        var result = InvokeExpression(scriptblock);
        HandleError(result);
        return result;
    }

    protected TestCase[] GetTestCases()
    {
        return GetTestCases("*.test");
    }

    protected TestCase[] GetTestCases(string searchPattern = "*.test")
    {
        try
        {
            return Directory.GetFiles(TestClassCasesDirectory, searchPattern, SearchOption.AllDirectories).Select(i => new TestCase(i)).ToArray();
        }
        catch (DirectoryNotFoundException)
        {
            return Array.Empty<TestCase>();
        }
    }

    private static void HandleError(RunnerResult result)
    {
        string st = result.ErrorStream.Count > 0 ? result.ErrorStream[0].Exception.StackTrace ?? "" : "";
        Assert.False(result.HadErrors, string.Join(Environment.NewLine, result.ErrorStream) + Environment.NewLine + st);
        Assert.True(result.IsSuccess);
    }

}