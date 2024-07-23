using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Ominous.Constants;
using Ominous.Extensions;
using Ominous.Model;
using Ominous.Model.List;

namespace Ominous.Commands;

[Cmdlet(
    VerbsData.ConvertTo,
    Nouns.List,
    HelpUri = "https://github.com/soulshined/OMINOUS/blob/master/publish/generate-docs/cmdlets/ConvertTo-List.md"
)]
[Alias(
    Nouns.List,
    "UnorderedList",
    "OrderedList",
    "TaskList"
)]
[OutputType(
    typeof(string)
)]
public partial class ConvertToListCmdlet : AbstractPSObjectCmdlet, IDynamicParameters
{
    private ListType ListType { get; set; } = ListType.Unordered;

    protected override void BeginProcessing()
    {
        base.BeginProcessing();
        ListType = MyInvocation.InvocationName.ToLower() switch
        {
            "orderedlist" => ListType.Ordered,
            "tasklist" => ListType.Task,
            _ => ListType
        };
    }

    protected override void EndProcessing()
    {
        base.EndProcessing();

        int sn = ListType == ListType.Ordered ? dynOl.Start - 1 : 0;
        var converted = Convert(ListType, Items, ref State, 0, sn);
        WriteObject(converted.ToMarkdown(Preference.Flavor));
    }

    internal static ConversionResult Convert(ListType listType, List<PSObject> items, ref State state) =>
        Convert(listType, items, ref state, 0, 0);

    private static ConversionResult Convert(ListType listType, List<PSObject> items, ref State state, uint depth = 0, int onum = 0)
    {
        List<ListItem> rows = new();

        foreach (PSObject pso in items)
        {
            if (pso.IsDictionary())
            {
                IDictionary<string, PSObject> dict = (IDictionary<string, PSObject>)pso.BaseObject;
                if (listType == ListType.Task && dict.ContainsKey("Value"))
                {
                    var toTry = "false";
                    if (dict.ContainsKey("IsComplete"))
                        toTry = dict["IsComplete"].ToString();
                    else if (dict.ContainsKey("IsChecked"))
                        toTry = dict["IsChecked"].ToString();
                    else if (dict.ContainsKey("Complete"))
                        toTry = dict["Complete"].ToString();
                    else if (dict.ContainsKey("Checked"))
                        toTry = dict["Checked"].ToString();

                    bool.TryParse(toTry, out bool isChecked);

                    var value = dict.ContainsKey("Value") ? dict["Value"].ToString().Trim() : dict.ToString();
                    rows.Add(new ListItem(value.ToPSObject(shallow: true), isChecked));
                }
                else
                {
                    List<ListItem> nestedRows = new();

                    foreach (var entry in dict)
                    {
                        var v = entry.Value;
                        if (v.IsIterable())
                        {
                            var converted = Convert(listType, new List<PSObject>(new PSObject[] { v }), ref state, depth + 1);
                            nestedRows.Add(new ListItem(entry.Key.ToPSObject(shallow: true)));
                            nestedRows.Add(new ListItem(converted.ToPSObject(shallow: true)));
                        }
                        else
                        {
                            nestedRows.Add(new ListItem($"{entry.Key}: {entry.Value}"));
                        }
                    }

                    rows.Add(new ListItem(new ConversionResult(listType, nestedRows, ref state, depth, 1).ToPSObject(shallow: true)));
                }

            }
            else if (pso.IsIterable())
            {
                var it = (List<PSObject>)pso.BaseObject;
                if (it.Count == 2 && it.ElementAt(0).IsString() && it.ElementAt(1).IsBool())
                {
                    rows.Add(new ListItem(it.ElementAt(0), (bool)it.ElementAt(1).BaseObject));
                }
                else
                {
                    var converted = Convert(listType, it, ref state, depth + 1);
                    rows.Add(new ListItem(converted.ToPSObject(shallow: true)));
                }

            }
            else
            {
                rows.Add(new ListItem(pso));
            }
        }

        return new ConversionResult(listType, rows, ref state, depth, onum);
    }
}