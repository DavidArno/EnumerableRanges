using System.Diagnostics.Metrics;
using System.Linq;

namespace EnumerableRanges;

public static class EnumerableRangeExtensions
{
    public static IEnumerator<int> GetEnumerator(this Range range) => new RangeEnumerator(range);

    public static IEnumerable<int> AsEnumeration(this Range range)
    {
        foreach (var item in range)
        {
            yield return item;
        }
    }

    public static IEnumerable<T> Select<T>(this Range range, Func<int, T> selector)
    {
        foreach (var item in range)
        {
            yield return selector(item);
        }
    }

    public static IEnumerable<T> Select<T>(this Range range, Func<int, int, T> selector)
    {
        var i = 0;
        foreach (var item in range)
        {
            yield return selector(item, i++);
        }
    }

    public static IEnumerable<int> Where(this Range range, Func<int, bool> predicate)
    {
        foreach (var item in range)
        {
            if (predicate(item)) yield return item;
        }
    }

    public static IEnumerable<int> Where(this Range range, Func<int, int, bool> predicate)
    {
        var i = 0;
        foreach (var item in range)
        {
            if (predicate(item, i++)) yield return item;
        }
    }

    public static int Count(this Range range) 
        => range.Start.Value is var s && range.End.Value is var e && range.End.IsFromEnd is var addOne && s <= e
            ? e - s + (addOne ? 1 : 0) 
            : s - e + (addOne ? 1 : 0);

    public static int Count(this Range range, Func<int, bool> predicate)
    {
        var count = 0;
        foreach (var item in range)
        {
            if (predicate(item)) count++;
        }

        return count;
    }

    public static long LongCount(this Range range) => (long)range.Count();

    public static long LongCount(this Range range, Func<int, bool> predicate) => (long)range.Count(predicate);

    public static IEnumerable<int> Concat(this Range first, IEnumerable<int> second)
    {
        return second == null ? throw new ArgumentNullException(nameof(second)) : ConcatWorker(first, second);

        static IEnumerable<int> ConcatWorker(Range first, IEnumerable<int> second)
        {
            foreach (var item in first)
            {
                yield return item;
            }

            foreach (var item in second)
            {
                yield return item;
            }
        }
    }

    public static IEnumerable<int> Concat(this IEnumerable<int> first, Range second)
    {
        return first == null ? throw new ArgumentNullException(nameof(first)) : ConcatWorker(first, second);

        static IEnumerable<int> ConcatWorker(IEnumerable<int> first, Range second)
        {
            foreach (var item in first)
            {
                yield return item;
            }

            foreach (var item in second)
            {
                yield return item;
            }
        }
    }

    public static IEnumerable<int> Concat(this Range first, Range second)
    {
        foreach (var item in first)
        {
            yield return item;
        }

        foreach (var item in second)
        {
            yield return item;
        }
    }

    public static IEnumerable<TResult> Join<TInner, TKey, TResult>(
        this Range outer,
        IEnumerable<TInner> inner,
        Func<int, TKey> outerKeySelector,
        Func<TInner, TKey> innerKeySelector,
        Func<int, TInner, TResult> resultSelector)
    {
        return outer.AsEnumeration().Join(
            inner,
            outerKeySelector,
            innerKeySelector,
            resultSelector);
    }

    public static IEnumerable<TResult> Join<TOuter, TKey, TResult>(
        this IEnumerable<TOuter> outer,
        Range inner,
        Func<TOuter, TKey> outerKeySelector,
        Func<int, TKey> innerKeySelector,
        Func<TOuter, int, TResult> resultSelector)
    {
        return outer.Join(
            inner.AsEnumeration(),
            outerKeySelector,
            innerKeySelector,
            resultSelector);
    }

    public static IEnumerable<TResult> Join<TKey, TResult>(
        this Range outer,
        Range inner,
        Func<int, TKey> outerKeySelector,
        Func<int, TKey> innerKeySelector,
        Func<int, int, TResult> resultSelector)
    {
        return outer.AsEnumeration().Join(
            inner.AsEnumeration(),
            outerKeySelector,
            innerKeySelector,
            resultSelector);
    }

    /*
    public static IEnumerable<TResult> GroupJoin<TKey, TResult>(
        this Range outer,
        Range inner,
        Func<int, TKey> outerKeySelector,
        Func<int, TKey> innerKeySelector,
        Func<int, Range, TResult> resultSelector)
    {
        return outer.AsEnumeration().GroupJoin(
            inner.AsEnumeration(),
            outerKeySelector,
            innerKeySelector,
            resultSelector);
    }
*/
}