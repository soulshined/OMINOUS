namespace Ominous.Attributes.Markdown.Style;

[OrderPrecedence(OrderPrecedenceAttribute.PROCESS_FIRST + 2)]
public sealed class CenterAttribute : StyleAttribute
{
    public CenterAttribute() : base(null, tagName: "center")
    {
        IsExclusivelyHTML = true;
    }
}