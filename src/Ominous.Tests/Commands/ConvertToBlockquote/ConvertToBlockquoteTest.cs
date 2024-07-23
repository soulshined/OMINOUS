using System.Management.Automation;
using Ominous.Constants;

namespace Ominous.Tests;

public class ConvertToBlockquoteTest : AbstractPSCmdletTest
{
    public ConvertToBlockquoteTest() : base(VerbsData.ConvertTo, Nouns.Blockquote) { }
}