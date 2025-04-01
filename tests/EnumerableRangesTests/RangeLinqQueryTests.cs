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

    [Test]
    public static void Ranges_CanHandleSelectManyWithAnotherRange()
    {
        var result = string.Join(
            " ",
            from x in 10..^11
            from y in 1..^2
            select x * 10 + y);

        AreEqual("101 102 111 112", result);
    }

    [Test]
    public static void Ranges_CanHandleSelectManyWithAnEnumeration()
    {
        const string letters = "abc";
        var result = string.Join(
            " ",
            from x in 1..^2
            from y in letters
            select $"{x}{y}");

        AreEqual("1a 1b 1c 2a 2b 2c", result);
    }

    [Test]
    public static void Enumerations_CanHandleSelectManyWithRange()
    {
        const string letters = "def";
        var result = string.Join(
            " ",
            from x in letters
            from y in 0..^1
            select $"{y}{x}");

        AreEqual("0d 1d 0e 1e 0f 1f", result);
    }

    [Test]
    public static void Ranges_CanBeJoinedWithEnumerations()
    {
        const string numbers = "0214";
        var result = string.Join(
            " ",
            from x in 0..^3
            join y in numbers on (char)(x + 48) equals y
            select $"{y}{x}");

        AreEqual("00 11 22", result);
    }

    [Test]
    public static void Enumerations_CanBeJoinedWithRanges()
    {
        const string numbers = "0214";
        var result = string.Join(
            " ",
            from x in numbers
            join y in 0..^3 on x equals (char)(y + 48)
            select $"{y}{x}");

        AreEqual("00 22 11", result);
    }

    [Test]
    public static void Ranges_CanBeJoinedWithRanges()
    {
        var result = string.Join(
            " ",
            from x in 5..0
            join y in 0..^3 on x equals y
            select $"{x}{y}");

        AreEqual("33 22 11", result);
    }

    //[Test]
    //public static void Ranges_()
    //{
    //    var result = string.Join(
    //        " ",
    //        from x in 5..0
    //        join y in 0..^3 on x equals y into r
    //        select $"{x}{y}");

    //    AreEqual("33 22 11", result);
    //}
}
