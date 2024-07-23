using System.Linq;
using Ominous.Constants;
using Ominous.Extensions;
using Ominous.Model.List;

namespace Ominous.Model.Table;

internal sealed class RowResult : AbstractList<TableEntry[]>, IMkdnConvertible
{
    public string ToHtml(FlavorType flavor)
    {
        return this.Select(row =>
        {
            var tds = row.Select(td => string.Format("<{0}>{1}</{0}>", "td", td.Value));
            return string.Format("<{0}>{1}</{0}>", "tr", tds.Join(""));
        }).Join("");
    }

    public string ToMarkdown(FlavorType flavor)
    {
        return this.Select(row =>
        {
            var tds = row.Select(td => td.Value).Join(" | ");
            return string.Format("| {0} |", tds);
        }).Join();
    }
}
