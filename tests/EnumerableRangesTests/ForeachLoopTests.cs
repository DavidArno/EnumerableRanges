using EnumerableRanges;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace EnumerableRangesTests;

[TestFixture]
public static class ForeachLoopTests
{
    [Test]
    public static void AscendingExclusiveRange_EnumeratesCorrectValues()
    {
        var result = "";
        foreach (var i in 1..5)
        {
            result += i;
        }

        AreEqual("1234", result);
    }

    [Test]
    public static void DescendingExclusiveRange_EnumeratesCorrectValues()
    {
        var result = "";
        foreach (var i in 5..1)
        {
            result += i;
        }

        AreEqual("5432", result);
    }

    [Test]
    public static void AscendingInclusiveRange_EnumeratesCorrectValues()
    {
        var result = "";
        foreach (var i in 1..^5)
        {
            result += i;
        }

        AreEqual("12345", result);
    }

    [Test]
    public static void DescendingInclusiveRange_EnumeratesCorrectValues()
    {
        var result = "";
        foreach (var i in 5..^1)
        {
            result += i;
        }

        AreEqual("54321", result);
    }

    [Test]
    public static void RangeWithImplicitStart_EnumeratesFromZero()
    {
        var result = "";
        foreach (var i in ..5)
        {
            result += i;
        }

        AreEqual("01234", result);
    }

    [TestCase]
    public static void RangeWithImplicitEnd_EnumeratesDescendingToZeroInclusive()
    {
        var result = "";
        foreach (var i in 5..)
        {
            result += i;
        }

        AreEqual("543210", result);
    }

    [TestCase]
    public static void RangeWithImplicitStartAndEnd_EnumeratesZeroOnly()
    {
        var result = "";
        foreach (var i in ..)
        {
            result += i;
        }

        AreEqual("0", result);
    }

    [TestCase]
    public static void RangeWithInclusiveStart_IgnoresInclusivityAndEnumeratesAsNormal()
    {
        var result = "";
        foreach (var i in ^1..5)
        {
            result += i;
        }

        AreEqual("1234", result);
    }
}
