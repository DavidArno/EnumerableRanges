using EnumerableRanges;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace EnumerableRangesTests;

[TestFixture]
internal class RangeLinqQueryTests
{
    [Test]
    public static void Range_CanHandleSimpleSelect()
    {
        var result = string.Join(" ", from x in 10..15 select x);
        AreEqual("10 11 12 13 14", result);
    }

    [Test]
    public static void Range_CanHandleSimpleWhere()
    {
        var result = string.Join(" ", from x in 10..^15 where x > 11 select x);
        AreEqual("12 13 14 15", result);
    }

    [Test]
    public static void Range_CanHandleWhereAndTransformingSelect()
    {
        var result = string.Join(" ", from x in 10..^15 where x > 11 select x * 10);
        AreEqual("120 130 140 150", result);
    }

    [Test]
    public static void Range_CanHandleOrderBy()
    {
        var result = string.Join(" ", from x in 10..^15 where x > 10 orderby x descending select x);
        AreEqual("15 14 13 12 11", result);
    }
}
