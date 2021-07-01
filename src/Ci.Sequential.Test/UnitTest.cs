using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
