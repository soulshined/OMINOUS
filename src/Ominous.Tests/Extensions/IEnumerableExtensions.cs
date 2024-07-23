namespace Ominous.Tests.Extensions;

internal static class IEnumerableExtensions
{
    internal record MatchResult<T>
    {
        internal int Index = -1;
        internal object? Value;

        internal MatchResult(object value) : this(value, -1) { }

        internal MatchResult(object? value, int index = -1)
        {
            Index = index;
            Value = value;
        }
    }

    internal static MatchResult<T>? Find<T>(this IEnumerable<T> haystack, Predicate<T> predicate)
    {
        for (int i = 0; i < haystack.Count(); i++)
        {
            var e = haystack.ElementAt(i);
            if (predicate.Invoke(e)) return new MatchResult<T>(e, i);
        }

        return null;
    }

    internal static string Join<T>(this IEnumerable<T> t)
    {
        return Join(t, Environment.NewLine);
    }

    internal static string Join<T>(this IEnumerable<T> t, string delimiter)
    {
        return string.Join(delimiter, t);
    }
}