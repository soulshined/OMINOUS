using System;
using System.Collections.Generic;
using Ominous.Constants;
using Ominous.Extensions;
using Ominous.Model;
using Ominous.Model.Table;

namespace Ominous.Commands;

public partial class ConvertToTableCmdlet
{

    internal sealed class ConversionResult : AbstractConversionResult
    {
        ColumnResult Columns { get; }
        internal readonly RowResult Rows = new();

        internal ConversionResult(List<TableEntry[]> entries, List<ColumnDefinition> columnDefinitions, ref State state) : base(0, ref state)
        {
            if (entries == null) return;
            foreach (var row in entries)
            {
                List<TableEntry> thisRow = new();
                foreach (TableEntry cell in row)
                    thisRow.Add(new TableEntry(ExecAfterBlocks(cell.Value, cell.IsHTML), cell.Key, cell.IsHTML));

                Rows.Add(thisRow.ToArray());
            }
            Columns = new ColumnResult(Rows, columnDefinitions);
        }

        public override string ToMarkdown(FlavorType flavor) =>
            EOL + (Columns.ToMarkdown(flavor).TrimNewLines() + Environment.NewLine + Rows.ToMarkdown(flavor).TrimNewLines()).TrimNewLines() + EOL;

        public override string ToHtml(FlavorType flavor) =>
            string.Format("<table><thead>{0}</thead><tbody>{1}</tbody></table>", Columns.ToHtml(flavor), Rows.ToHtml(flavor));
    }

}