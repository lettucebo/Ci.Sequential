using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ci.Sequential.Core.Test
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void Id1ShouldLessThanId2()
        {
            var id1 = GuidSequential.NewGuid();
            var id2 = GuidSequential.NewGuid();

            Console.WriteLine(id1);
            Console.WriteLine(id2);

            var result = id1.CompareTo(id2);

            result.Should().BeLessThan(0);
        }
    }
}
