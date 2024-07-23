using Ominous.Extensions;

namespace Ominous.Utils;

public sealed class MarkdownUtils
{
    public static readonly char[] ESCAPABLE_CHRS = new char[] {
        '`',
        '*',
        '_',
        '{', '}',
        '[', ']',
        '<', '>',
        '(', ')',
        '#',
        '+',
        '-',
        '.',
        '!',
        '|'
    };

    public static void Escape(ref string s) => Escape(ref s, ESCAPABLE_CHRS);

    public static void Escape(ref string s, params char[] chars)
    {
        foreach (char ch in chars)
        {
            s = s.Replace($"{ch}", $"\\{ch}");
        }
    }

    public static void HtmlEncode(ref string s, params char[] chars)
    {
        s = s.Replace("\r", "");

        if (chars.Length == 0)
        {
            chars = new char[] { '\n', '\t', ' ', '|', '-', '<', '>', '[', ']' };
        }

        foreach (char ch in chars)
        {
            switch (ch)
            {
                case '\n':
                    s = s.Replace("\n", "<br>");
                    break;
                case '\t':
                    s = s.Replace("\t", "&nbsp;".Repeat(3));
                    break;
                case ' ':
                    s = s.Replace(" ", "&nbsp;");
                    break;
                case '|':
                    s = s.Replace("|", "&verbar;");
                    break;
                case '-':
                    s = s.Replace("-", "&hyphen;");
                    break;
                case '<':
                    s = s.Replace("<", "&lt;");
                    break;
                case '>':
                    s = s.Replace("<", "&gt;");
                    break;
                case '[':
                    s = s.Replace("[", "&lbrack;");
                    break;
                case ']':
                    s = s.Replace("]", "&rbrack;");
                    break;
                default:
                    break;
            }
        }
    }
}