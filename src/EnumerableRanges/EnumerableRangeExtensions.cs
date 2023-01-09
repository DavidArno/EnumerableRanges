namespace EnumerableRanges;

public static class EnumerableRangeExtensions
{
    public static IEnumerator<int> GetEnumerator(this Range range) => new RangeEnumerator(range);

    public static IEnumerable<T> Select<T>(this Range range, Func<int, T> selector)
    {
        foreach (var item in range)
        {
            yield return selector(item);
        }
    }

    public static IEnumerable<int> Where(this Range range, Func<int, bool> predicate)
    {
        foreach (var item in range)
        {
            if (predicate(item)) yield return item;
        }
    }
}