using Ominous.Utils;

namespace Ominous.Attributes.Markdown;

[OrderPrecedence(OrderPrecedenceAttribute.PROCESS_FIRST)]
public sealed class EscapeAttribute : AbstractOrderedAttribute
{
    public readonly char[] Chars;
    public EscapeAttribute()
    {
        Chars = MarkdownUtils.ESCAPABLE_CHRS;
    }

    public EscapeAttribute(params char[] chars) => chars.CopyTo(Chars, 0);

    public void Escape(ref string s) => MarkdownUtils.Escape(ref s, Chars);
}