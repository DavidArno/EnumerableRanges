using EnumerableRanges;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace EnumerableRangesTests;

[TestFixture]
public static class NonQueryMethodsTests
{
    [Test]
    public static void Ranges_CanBeCounted()
    {
        Multiple(() => {
            AreEqual(5, (..5).Count());
            AreEqual(6, (..^5).Count());
            AreEqual(5, (5..0).Count());
            AreEqual(6, (5..).Count());
            AreEqual(1, (..).Count());
            AreEqual(0, (..0).Count());
        });
    }

    [Test]
    public static void Ranges_CanBeCountedAsLongs()
    {
        Multiple(() => {
            AreEqual(5L, (..5).LongCount());
            AreEqual(6L, (..^5).LongCount());
            AreEqual(5L, (5..0).LongCount());
            AreEqual(6L, (5..).LongCount());
            AreEqual(1L, (..).LongCount());
            AreEqual(0L, (..0).LongCount());
        });
    }

    [Test]
    public static void Ranges_CanBeCountedWithPredicate()
    {
        AreEqual(5, (10..20).Count(x => x % 2 == 0));
    }

    [Test]
    public static void Ranges_CanBeCountedAsLongWithPredicate()
    {
        AreEqual(6L, (10..^21).LongCount(x => x % 2 == 0));
    }

    [Test]
    public static void Range_CanBeConcatenatedWithAnotherRange()
    {
        var result = string.Join(" ", (3..8).Concat(8..11));
        Assert.AreEqual("3 4 5 6 7 8 9 10", result);
    }

    [Test]
    public static void Range_CanBeConcatenatedWithEnumerable()
    {
        var numbers = new[] { 9, 10, 11, 12 };
        var result = string.Join(" ", (3..8).Concat(numbers));
        Assert.AreEqual("3 4 5 6 7 9 10 11 12", result);
    }

    [Test]
    public static void Enumerable_CanBeConcatenatedWithRange()
    {
        var numbers = new[] { 9, 10, 11, 12 };
        var result = string.Join(" ", numbers.Concat(13..^15));
        Assert.AreEqual("9 10 11 12 13 14 15", result);
    }
}
