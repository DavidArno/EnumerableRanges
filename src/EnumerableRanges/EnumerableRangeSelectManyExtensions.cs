using System;
using System.Diagnostics.Metrics;
using System.Linq;

namespace EnumerableRanges;

public static class EnumerableRangeSelectManyExtensions
{
    public static IEnumerable<TResult> SelectMany<TResult>(
        this Range range,
        Func<int, Range> selector,
        Func<int, int, TResult> resultSelector)
    {
        if (selector == null) throw new ArgumentNullException(nameof(selector));
        if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

        return SelectManyWorker(range, (x, _) => selector(x), resultSelector);
    }

    public static IEnumerable<TResult> SelectMany<TResult>(
        this Range range,
        Func<int, int, Range> selector,
        Func<int, int, TResult> resultSelector)
    {
        if (selector == null) throw new ArgumentNullException(nameof(selector));
        if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

        return SelectManyWorker(range, selector, resultSelector);
    }

    public static IEnumerable<TResult> SelectMany<T, TResult>(
        this Range range, 
        Func<int, IEnumerable<T>> selector,
        Func<int, T, TResult> resultSelector)
    {
        if (selector == null) throw new ArgumentNullException(nameof(selector));
        if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

        return SelectManyWorker(range, selector, resultSelector);
    }

    public static IEnumerable<TResult> SelectMany<T, TResult>(
        this IEnumerable<T> enumeration, 
        Func<T, Range> selector,
        Func<T, int, TResult> resultSelector)
    {
        if (enumeration == null) throw new ArgumentNullException(nameof(enumeration));
        if (selector == null) throw new ArgumentNullException(nameof(selector));
        if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

        return SelectManyWorker(enumeration, selector, resultSelector);
    }
    private static IEnumerable<TResult> SelectManyWorker<TResult>(
        this Range range, 
        Func<int, int,  Range> selector,
        Func<int, int, TResult> resultSelector)
    {
        var index = 0;
        foreach (var first in range)
        {
            foreach (var second in selector(first, index++))
            {
                yield return resultSelector(first, second);
            }
        }
    }

    private static IEnumerable<TResult> SelectManyWorker<T, TResult>(
        this Range range, 
        Func<int, IEnumerable<T>> selector,
        Func<int, T, TResult> resultSelector)
    {
        foreach (var first in range)
        {
            foreach (var second in selector(first))
            {
                yield return resultSelector(first, second);
            }
        }
    }

    private static IEnumerable<TResult> SelectManyWorker<T, TResult>(
        this IEnumerable<T> enumeration, 
        Func<T, Range> selector,
        Func<T, int, TResult> resultSelector)
    {
        foreach (var first in enumeration)
        {
            foreach (var second in selector(first))
            {
                yield return resultSelector(first, second);
            }
        }
    }
}