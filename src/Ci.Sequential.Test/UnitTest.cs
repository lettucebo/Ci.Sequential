using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Guid = System.Guid;

namespace Ci.Sequential.Test
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void IdShouldNotBeEmpty()
        {
            var id = GuidSequential.NewGuid();

            id.Should().NotBeEmpty();
        }

        [TestMethod]
        public void Id1ShouldLessThanId2()
        {
            var id1 = GuidSequential.NewGuid();
            var id2 = GuidSequential.NewGuid();

            var list = new List<SqlGuid>() { id2, id1 };
            list.Sort();
            var result = list.First();

            result.Value.Should().Be(id1);
        }
    }
}
