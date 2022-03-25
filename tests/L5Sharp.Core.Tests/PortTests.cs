using System;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class PortTests
    {
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var port = new Port(1, "ICP");

            port.Should().NotBeNull();
        }

        [Test]
        public void New_Default_ShouldHaveExpected()
        {
            var port = new Port(1, "ICP");

            port.Id.Should().Be(1);
            port.Type.Should().Be("ICP");
            port.Upstream.Should().BeFalse();
            port.DownstreamOnly.Should().BeFalse();
            port.Address.Should().BeEmpty();
            port.BusSize.Should().Be(0);
        }

        [Test]
        public void New_Overrides_ShouldHaveExpectedProperties()
        {
            var port = new Port(2, "ICP", "0", true, 17, true);

            port.Id.Should().Be(2);
            port.Type.Should().Be("ICP");
            port.Upstream.Should().BeTrue();
            port.DownstreamOnly.Should().BeTrue();
            port.Address.Should().Be("0");
            port.BusSize.Should().Be(17);
        }

        [Test]
        public void Configure_UpstreamOnDownstreamPort_ShouldThrowInvalidOperationException()
        {
            var port = new Port(1, "ICP", downstreamOnly: true);

            FluentActions.Invoking(() => port.Configure("1", true, 10)).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Configure_Valid_ShouldBeExpected()
        {
            var port = new Port(1, "ICP");

            var result = port.Configure("1", true, 10);

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Type.Should().Be("ICP");
            result.Address.Should().Be("1");
            result.Upstream.Should().BeTrue();
            result.DownstreamOnly.Should().BeFalse();
            result.BusSize.Should().Be(10);
        }

        [Test]
        public void TypedEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Port(1, "ICP");
            var second = new Port(1, "ICP");

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreSame_ShouldBeTrue()
        {
            var first = new Port(1, "ICP");
            
            var result = first.Equals(first);

            result.Should().BeTrue();
        }


        [Test]
        public void TypedEquals_Null_ShouldBeFalse()
        {
            var first = new Port(1, "ICP");
            
            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Port(1, "ICP");
            var second = new Port(1, "ICP");

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new Port(1, "ICP");

            var result = first.Equals((object)first);

            result.Should().BeTrue();
        }


        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new Port(1, "ICP");

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Port(1, "ICP");
            var second = new Port(1, "ICP");

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new Port(1, "ICP");
            var second = new Port(1, "ICP");

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeId()
        {
            var first = new Port(1, "ICP");

            var hash = first.GetHashCode();

            hash.Should().Be(1);
        }
    }
}