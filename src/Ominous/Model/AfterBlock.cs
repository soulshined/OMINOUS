using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Ominous.Attributes;
using Ominous.Attributes.Markdown;
using Ominous.Attributes.Markdown.Style;

namespace Ominous.Model;

public sealed class AfterBlock
{
    internal ScriptBlock Delegate { get; }
    internal List<AbstractOrderedAttribute> OrderedAttributes { get; }
    internal List<StyleAttribute> Styles { get; } = new();

    public AfterBlock(ScriptBlock sb)
    {
        var attrs = new List<AbstractOrderedAttribute>(sb.Attributes.OfType<AbstractOrderedAttribute>()).Distinct().ToList();
        attrs.Sort();
        OrderedAttributes = attrs;

        Styles = new List<StyleAttribute>(OrderedAttributes.OfType<StyleAttribute>());
        Delegate = sb;
    }

    internal string Apply(string s)
    {
        var result = Delegate.Invoke(s);
        var applied = result.Count == 0 ? s : result[0].BaseObject?.ToString();

        var escapeAttrs = OrderedAttributes.OfType<EscapeAttribute>();
        if (escapeAttrs.Count() > 0)
            escapeAttrs.ElementAt(0).Escape(ref applied);

        return applied;
    }

    internal void Style(ref string s, bool isHTML)
    {
        foreach (StyleAttribute attr in Styles)
            s = attr.Style(s, isHTML);
    }

}