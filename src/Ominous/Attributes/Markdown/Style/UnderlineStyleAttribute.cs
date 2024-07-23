namespace Ominous.Attributes.Markdown.Style;

[OrderPrecedence(OrderPrecedenceAttribute.PROCESS_FIRST + 1)]
public sealed class UnderlineAttribute : StyleAttribute
{
    public UnderlineAttribute() : base(null, tagName: "u")
    {
        IsExclusivelyHTML = true;
    }
}