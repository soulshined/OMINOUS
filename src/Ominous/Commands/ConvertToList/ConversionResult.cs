using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Ominous.Constants;
using Ominous.Extensions;
using Ominous.Model;
using Ominous.Model.List;

namespace Ominous.Commands;

public partial class ConvertToListCmdlet
{

    internal sealed class ConversionResult : AbstractConversionResult
    {
        private List<ListItem> Items { get; } = new();
        private int StartNumber { get; set; } = 1;
        private ListType ListType { get; } = ListType.Unordered;
        private static readonly string NUMBERED_START_REGEX_PATTERN = @"^\d+\.";
        private static readonly Regex NUMBERED_START_REGEX = new(NUMBERED_START_REGEX_PATTERN);

        internal ConversionResult(ListType listType, List<ListItem> items, ref State state, uint depth = 0, int orderedNumber = 1) : base(depth, ref state)
        {
            StartNumber = orderedNumber;
            ListType = listType;
            Items.AddRange(items);
        }

        public override string ToMarkdown(FlavorType flavor)
        {
            string indent = Depth == 0 ? "" : "  ".Repeat(Depth - 1);
            StringBuilder sb = new();

            foreach (ListItem i in Items)
            {
                if (i.PSObject.IsNull() || i.PSObject.IsString())
                {
                    var s = i.PSObject.BaseObject?.ToString() ?? "";
                    string prefix = "- ";

                    switch (ListType)
                    {
                        case ListType.Ordered:
                            prefix = $"{++StartNumber}. ";
                            break;
                        case ListType.Task:
                            prefix = string.Format("- [{0}] ", i.IsChecked ? "x" : " ");
                            break;
                        case ListType.Unordered:
                            if (NUMBERED_START_REGEX.IsMatch(s))
                            {
                                var split = s.Split(new char[1] { '.' }, 2);
                                s = split[0] + "\\." + split[1];
                            }
                            break;
                    }

                    sb.AppendLine($"{indent}{prefix}{ExecAfterBlocks(s).TrimNewLines()}");
                }
                else if (i.PSObject.BaseObject is ConversionResult result)
                {
                    sb.AppendLine(ExecAfterBlocks(result.ToMarkdown(flavor)).TrimNewLines());
                }
                else
                {
                    throw new NotImplementedException("ToMarkdown(): " + i.GetType().ToString());
                }
            }

            return EOL + sb.ToString().TrimNewLines() + EOL;
        }

        public override string ToHtml(FlavorType flavor)
        {
            string tagName = ListType == ListType.Ordered ? "ol" : "ul";
            StringBuilder sb = new($"<{tagName}>");

            foreach (object i in Items)
            {
                var asString = i is ConversionResult result ? result.ToHtml(flavor).TrimEnd() : i;
                if (asString is string s)
                {
                    sb.Append("<li>");

                    if (ListType == ListType.Task)
                        sb.Append("<input type=\"checkbox\">");

                    sb.Append(ExecAfterBlocks(s)).Append("</li>");
                }
                else if (asString is ListItem li)
                {
                    sb.Append("<li>");

                    if (ListType == ListType.Task)
                    {
                        sb.Append("<input type=\"checkbox\"").Append(li.IsChecked ? " checked" : "").Append(">");

                    }

                    if (li.PSObject.BaseObject.GetType() == typeof(ConversionResult))
                    {
                        sb.Append(((ConversionResult)li.PSObject.BaseObject).ToHtml(flavor).TrimEnd());
                    }
                    else
                    {
                        sb.Append(ExecAfterBlocks(li.PSObject.BaseObject?.ToString() ?? ""));
                    }

                    sb.Append("</li>");
                }
                else throw new NotImplementedException("ToHtml(): " + i.GetType().ToString());
            }

            return sb.Append($"</{tagName}>").ToString();
        }
    }

}