using System;
using FluentAssertions;
using L5Sharp.Core;
using NUnit.Framework;

namespace L5Sharp.Tests
{
    [TestFixture]
    public class BasicComponentTests
    {
        [Test]
        public void Create_DataTypeMember_ShouldNotBeNull()
        {
            var member = new DataTypeMember("Test", Logix.DataType.Dint);

            member.Should().NotBeNull();
        }
    }
}