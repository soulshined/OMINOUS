using static Ominous.Tests.PSCmdletRunner;
using Ominous.Tests.Extensions;
using System.Diagnostics;

namespace Ominous.Tests;

public class AfterBlockTest : AbstractTest
{
    public AfterBlockTest() : base()
    {
    }

    [Fact]
    public void TestCases_ShouldBeValid()
    {
        foreach (var tc in GetTestCases())
        {
            if (tc.GivenConfig.IsDisabled) continue;

            RunnerResult result = InvokeExpressionExpectSuccess(tc.Given);

            Assert.False(result.HadErrors, result.ErrorStream.Join());

            Debug.WriteLine(result.Output.Join());
            if (!tc.Expected.Equals(result.Output.Join()) && !(Environment.NewLine + tc.Expected).Equals(result.Output.Join()))
            {
                Debug.WriteLine($"Test Case Failed => {tc.DirectoryName}/{tc.FileName}");
                Assert.Equal(tc.Expected, result.Output.Join());
            }
        }
    }
}