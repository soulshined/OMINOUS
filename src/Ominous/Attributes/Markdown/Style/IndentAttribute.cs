using System.Text;
using Ominous.Extensions;

namespace Ominous.Attributes.Markdown.Style;

[OrderPrecedence(OrderPrecedenceAttribute.PROCESS_LAST - 1)]
public sealed class IndentAttribute : StyleAttribute
{
    private uint Depth { get; } = 1;
    public IndentAttribute() : base(null) { }
    public IndentAttribute(uint depth) : base(null)
    {
        Depth = depth;
    }

    public override string Style(string s, bool isHTML)
    {
        var sb = new StringBuilder();
        foreach (var line in s.Split('\n'))
            sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;".Repeat(Depth - 1)).AppendLine(line);
        return sb.ToString();
    }

}