using System.Management.Automation;
using Ominous.Constants;

namespace Ominous.Tests;

public class ConvertToTableTest : AbstractPSCmdletTest
{
    public ConvertToTableTest() : base(VerbsData.ConvertTo, Nouns.Table) { }

    [Fact]
    public void Should_ConvertTo_Table_ByPipelineInput()
    {
        var result = InvokeExpressionExpectSuccess(@"
$Mkdn = H2 ""Commands""

$Mkdn += (Get-Module OMINOUS).ExportedCmdlets.Values | Table -ColumnDefinitions Center, Center, Center

$Mkdn
");

        Assert.NotNull(result);
    }

}