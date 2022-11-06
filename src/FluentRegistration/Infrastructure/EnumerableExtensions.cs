using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentRegistration.Infrastructure;

internal static class EnumerableExtensions
{
    public static bool None<TItem>(this IEnumerable<TItem> self)
    {
        return !self.Any();
    }

    public static bool None<TSource>(this IEnumerable<TSource> self, Func<TSource, bool> predicate)
    {
        return !self.Any(predicate);
    }
}
