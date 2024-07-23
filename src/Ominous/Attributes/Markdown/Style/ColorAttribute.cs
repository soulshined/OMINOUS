namespace Ominous.Attributes.Markdown.Style;

[OrderPrecedence(OrderPrecedenceAttribute.PROCESS_FIRST + 1)]
public sealed class ColorAttribute : StyleAttribute
{
    public readonly string Color;
    public ColorAttribute(string color) : base(null, "span")
    {
        IsExclusivelyHTML = true;
        Color = color?.Trim() ?? "inherit";
    }

    public override string Style(string s, bool isHTML) => string.Format("<span style=\"color: {0};\">{1}</span>", Color, s);
}