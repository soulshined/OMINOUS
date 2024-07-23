using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using Ominous.Commands.Transformers;
using Ominous.Constants;
using Ominous.Extensions;
using Ominous.Model;
using Ominous.Model.Table;

namespace Ominous.Commands;

[Cmdlet(
    VerbsData.ConvertTo,
    Nouns.Table,
    HelpUri = "https://github.com/soulshined/OMINOUS/blob/master/publish/generate-docs/cmdlets/ConvertTo-Table.md"
)]
[Alias(
    Nouns.Table
)]
[OutputType(
    typeof(string)
)]
public partial class ConvertToTableCmdlet : AbstractPSObjectCmdlet
{
    [Parameter(Position = 1, HelpMessage = "Definitions of column alignment/labeling")]
    [ColumnDefinitionTransformation]
    public List<ColumnDefinition> ColumnDefinitions { get; set; }

    protected override void EndProcessing()
    {
        base.EndProcessing();

        var converted = Convert(Items, ColumnDefinitions ?? new(), ref State);
        var mkdn = converted.ToMarkdown(Preference.Flavor);
        WriteObject(mkdn);
    }

    internal static ConversionResult Convert(List<PSObject> items, List<ColumnDefinition> columnDefinitions, ref State state)
    {
        var entries = new List<TableEntry[]>();

        for (int i = 0; i < items.Count; i++)
        {
            var pso = items[i];
            var row = new List<TableEntry>();

            if (pso.BaseObject is string s)
            {
                row.Add(new TableEntry(s.Trim()));
            }
            else if (pso.IsDictionary())
            {
                var d = (IDictionary)pso.BaseObject;
                foreach (string k in d.Keys)
                {
                    var value = ObjectExtensions.ToPSObject(d[k]);

                    if (value.IsNull())
                    {
                        row.Add(new TableEntry("", k));
                    }
                    else if (value.IsDictionary())
                    {
                        var converted = Convert(new List<PSObject>(new PSObject[] { value }), new(), ref state);
                        row.Add(new TableEntry(converted.ToHtml(state.Flavor), k, true));
                    }
                    else if (value.IsIterable())
                    {
                        var it = (List<PSObject>)value.BaseObject;
                        if (it.IsAllSameAsFirstFoundType(typeof(IDictionary), typeof(PSObject), typeof(PSCustomObject)))
                        {
                            RowResult _rows = new();

                            foreach (var j in it)
                            {
                                var converted = Convert(new List<PSObject>(new PSObject[] { j }), new(), ref state);
                                _rows.Add(converted.Rows[0]);
                            }

                            var nestedRowsHTML = _rows.ToHtml(state.Flavor);
                            var nestedColsHTML = new ColumnResult(_rows, new()).ToHtml(state.Flavor);

                            var tbl = string.Format("<table><thead>{0}</thead><tbody>{1}</tbody></table>", nestedColsHTML, nestedRowsHTML);

                            row.Add(new TableEntry(tbl, k, true));
                        }
                        else
                        {
                            var toList = ConvertToListCmdlet.Convert(ListType.Unordered, it, ref state);
                            row.Add(new TableEntry(toList.ToHtml(state.Flavor).Trim(), k, true));
                        }
                    }
                    else
                    {
                        row.Add(new TableEntry(value.BaseObject.ToString().Trim(), k));
                    }
                }
            }
            else
            {
                row.Add(new TableEntry(pso.BaseObject?.ToString().Trim(), ""));
            }

            entries.Add(row.ToArray());
        }

        return new ConversionResult(entries, columnDefinitions, ref state);
    }
}