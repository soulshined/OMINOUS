using System;
using System.Text;
using Ominous.Constants;
using Ominous.Extensions;
using Ominous.Model;

namespace Ominous.Commands;

public partial class ConvertToImageCmdlet
{

    internal sealed class ConversionResult : AbstractConversionResult
    {
        private string Link { get; }
        private string Alt { get; }
        private string Title { get; }
        private string Src { get; }
        private string Caption { get; }
        internal ConversionResult(string src, string alt, string title, string caption, ref State state, string link = null) : base(0, ref state)
        {
            Src = src.Trim();
            Title = title?.Trim();
            Alt = alt?.Trim();
            Link = link?.Trim();
            Caption = caption?.Trim();
            if (string.IsNullOrWhiteSpace(Link))
                Link = null;
            if (string.IsNullOrWhiteSpace(Alt))
                Alt = null;
            if (string.IsNullOrWhiteSpace(Title))
                Title = null;
            if (string.IsNullOrWhiteSpace(Caption))
                Caption = null;
        }

        public override string ToMarkdown(FlavorType flavor)
        {
            StringBuilder sb = new();
            var alt = ExecAfterBlocks(Alt ?? Title ?? Src);

            var imageMkdn = string.Format("![{0}]({1}{2})", alt, Src, null == Title ? "" : $" \"{Title}\"");

            if (null != Link)
                sb.AppendLine(string.Format("[{0}]({1})", imageMkdn, Link));
            else sb.AppendLine(imageMkdn);

            if (null != Caption)
                sb.AppendLine(string.Format("*{0}*", Caption));

            return sb.ToString().TrimNewLines();
        }

        public override string ToHtml(FlavorType flavor) =>
            throw new NotImplementedException();
    }

}