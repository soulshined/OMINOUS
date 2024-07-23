using Ominous.Constants;

namespace Ominous.Model;

internal interface IMkdnConvertible
{
    internal string ToMarkdown(FlavorType flavor);
    internal string ToHtml(FlavorType flavor);
}
