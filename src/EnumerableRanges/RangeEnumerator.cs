using System.Collections;

namespace EnumerableRanges;

public struct RangeEnumerator : IEnumerator<int>
{
    private readonly int _end;
    private readonly bool _incrementing;

    public RangeEnumerator(Range range)
    {
        var start = range.Start.Value;
        var end = range.End.Value;
        _incrementing = start <= end;
        Current = _incrementing ? start - 1 : start + 1;
        _end = (range.End.IsFromEnd, _incrementing, end) switch
        {
            (false, _, var e) => e,
            (true, true, var e) => e + 1,
            (true, false, var e) => e - 1
        };
    }

    public int Current { get; private set; }

    object IEnumerator.Current => Current;

    public bool MoveNext() => _incrementing ? ++Current < _end : --Current > _end;

    public void Dispose() { }

    public void Reset() => throw new NotImplementedException();
}