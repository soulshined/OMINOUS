using System;
using System.Text;

namespace Ominous.Extensions;

internal static class StringExtensions
{
    internal static string Repeat(this string val, IConvertible quantity)
    {
        var limit = Convert.ToUInt64(quantity);
        var sb = new StringBuilder(val);
        for (ulong i = 1; i <= limit; i++)
            sb.Append(val);

        return sb.ToString();
    }

    internal static string Repeat(this char c, IConvertible quantity)
    {
        var repeatCount = Convert.ToInt32(quantity);
        if (repeatCount <= 0) return new StringBuilder().Append(c).ToString();

        return new StringBuilder().Append(c, repeatCount + 1).ToString();
    }

    internal static string TrimNewLines(this string s) => s.Trim('\r', '\n');
}