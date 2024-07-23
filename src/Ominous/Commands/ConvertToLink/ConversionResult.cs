using System;
using System.Text;
using Ominous.Constants;
using Ominous.Extensions;
using Ominous.Model;

namespace Ominous.Commands;

public partial class ConvertToLinkCmdlet
{

    internal sealed class ConversionResult : AbstractConversionResult
    {
        private string Title { get; }
        private string Value { get; }
        private bool IsNewTab { get; }

        internal ConversionResult(string value, ref State state, string title = null, bool isNewTab = false, uint depth = 0) : base(depth, ref state)
        {
            Title = string.IsNullOrWhiteSpace(title) ? null : ExecAfterBlocks(title.Trim());
            Value = ExecAfterBlocks(value.Trim());
            IsNewTab = isNewTab;
        }

        public override string ToHtml(FlavorType flavor) => throw new NotImplementedException();

        public override string ToMarkdown(FlavorType flavor)
        {
            StringBuilder sb = new();

            if (null == Title && !IsNewTab)
            {
                sb.AppendLine(ExecAfterBlocks(Value).TrimNewLines());
            }
            else
            {
                string identifier = ExecAfterBlocks(Title ?? Value);
                string url = Value;
                if (IsNewTab)
                {
                    sb.AppendLine($"<a href=\"{url}\" target=\"_blank\">{identifier}</a>");
                }
                else
                {
                    sb.AppendLine($"[{identifier}]({url})");
                }
            }

            return sb.ToString().TrimNewLines();
        }
    }

}