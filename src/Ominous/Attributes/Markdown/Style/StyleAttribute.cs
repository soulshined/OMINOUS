using System;

namespace Ominous.Attributes.Markdown.Style;

[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
[OrderPrecedence(OrderPrecedenceAttribute.PROCESS_LAST - 2)]
public abstract class StyleAttribute : AbstractOrderedAttribute
{
    public readonly string Prefix;
    public readonly string Suffix;
    public readonly string TagName;
    public bool IsExclusivelyHTML { get; protected set; } = false;
    protected StyleAttribute(string prefix, string suffix, string tagName = null)
    {
        Prefix = prefix;
        Suffix = suffix;
        TagName = tagName;
    }
    protected StyleAttribute(string prefix, string tagName = null) : this(prefix, prefix, tagName) { }

    public virtual string Style(string s, bool isHTML)
    {
        isHTML = isHTML || IsExclusivelyHTML;
        return s.Insert(s.Length, isHTML ? $"</{TagName}>" : Suffix).Insert(0, isHTML ? $"<{TagName}>" : Prefix);
    }
}