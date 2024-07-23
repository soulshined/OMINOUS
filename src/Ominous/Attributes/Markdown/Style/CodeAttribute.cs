namespace Ominous.Attributes.Markdown.Style;

public sealed class CodeAttribute : StyleAttribute
{
    public CodeAttribute() : base("`", "code") { }

    public override string Style(string s, bool isHTML)
    {
        var styled = base.Style(s, isHTML);
        if (s.Contains("`") && !s.Contains("``"))
            return "`" + styled + "`";

        return styled;
    }
}