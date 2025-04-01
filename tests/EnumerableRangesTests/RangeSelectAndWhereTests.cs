using EnumerableRanges;
using NUnit.Framework;

namespace EnumerableRangesTests;

[TestFixture]
public static class RangeSelectAndWhereTests
{
    [Test]
    public static void Range_CanAcceptASelectAndEnumeratesCorrectly()
    {
        var result = string.Join("", (3..8).Select(x => x));
        Assert.AreEqual("34567", result);
    }

    [Test]
    public static void Range_CanAcceptASelectWithIndexAndEnumeratesCorrectly()
    {
        var result = string.Join(" ", (3..8).Select((x, i) => x * i));
        Assert.AreEqual("0 4 10 18 28", result);
    }

    [Test]
    public static void Range_CanAcceptWhereAndEnumeratesCorrectly()
    {
        var result = string.Join("", (3..8).Where(x => x % 2 == 0));
        Assert.AreEqual("46", result);
    }

    [Test]
    public static void Range_CanAcceptWhereWithIndexAndEnumeratesCorrectly()
    {
        var result = string.Join("", (4..^0).Where((x, i) => x == i));
        Assert.AreEqual("2", result);
    }
}
