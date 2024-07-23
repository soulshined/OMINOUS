using System;
using System.Collections.Generic;
using System.Linq;
using Ominous.Constants;
using Ominous.Extensions;
using Ominous.Model.List;

namespace Ominous.Model.Table;

internal sealed class ColumnResult : AbstractList<ColumnDefinition>, IMkdnConvertible
{

    internal ColumnResult(RowResult rows, List<ColumnDefinition> definitions) : base()
    {
        if (rows == null || rows.Count == 0) return;

        foreach (var row in rows)
        {
            foreach (var kvp in row)
            {
                int firstPositionalDefIndex = -1;
                int defIndex = -1;
                for (int i = 0; i < definitions.Count; i++)
                {
                    if (definitions[i].IsExclusivelyPositional)
                    {
                        if (firstPositionalDefIndex == -1) firstPositionalDefIndex = i;
                    }
                    else if (!string.IsNullOrWhiteSpace(definitions[i].Name) && definitions[i].Name.Equals(kvp.Key, StringComparison.CurrentCultureIgnoreCase))
                    {
                        defIndex = i;
                        break;
                    }
                }

                if (defIndex == -1 && firstPositionalDefIndex == -1 && !ContainsColumn(kvp.Key))
                {
                    Add(new ColumnDefinition(AlignmentType.Left, kvp.Key, kvp.Key));
                }
                else if (defIndex != -1)
                {
                    Add(definitions[defIndex]);
                }
                else if (firstPositionalDefIndex != -1)
                {
                    Add(new ColumnDefinition(definitions[firstPositionalDefIndex].Alignment, kvp.Key, kvp.Key));
                    definitions.RemoveAt(firstPositionalDefIndex);
                }
            }
        }
    }

    internal bool ContainsColumn(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return false;

        return this.Any(c => c.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
    }

    public string ToHtml(FlavorType flavor) => string.Format("<tr>{0}</tr>", this.Select(i => $"<th>{i.Label}</th>").Join(""));

    public string ToMarkdown(FlavorType flavor)
    {
        if (Count == 0) return "";

        var header = this.Select(i => i.Label).Join(" | ");
        var subheader = this.Select(i => i.SubHeader).Join(" | ");

        return string.Format("| {0} |{1}| {2} |", header, Environment.NewLine, subheader);
    }

}