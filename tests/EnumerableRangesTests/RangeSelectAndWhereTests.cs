using EnumerableRanges;
using NUnit.Framework;
using System.Security.Cryptography.X509Certificates;

namespace EnumerableRangesTests;

[TestFixture]
internal class RangeSelectAndWhereTests
{
    [Test]
    public static void Range_CanAcceptASelectAndEnumeratesCorrectly()
    {
        var result = string.Join("", (3..8).Select(x => x));
        Assert.AreEqual("34567", result);
    }

    [Test]
    public static void Range_CanAcceptAWhereAndEnumeratesCorrectly()
    {
        var result = string.Join("", (3..8).Where(x => x % 2 == 0));
        Assert.AreEqual("46", result);
    }
}
