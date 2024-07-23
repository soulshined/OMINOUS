using System;
using System.Collections.Generic;

namespace Ominous.Extensions;

internal static class IEnumerableExtensions
{
    internal static string Join<T>(this IEnumerable<T> t) => Join(t, Environment.NewLine);

    internal static string Join<T>(this IEnumerable<T> t, string delimiter) => string.Join(delimiter, t);
}