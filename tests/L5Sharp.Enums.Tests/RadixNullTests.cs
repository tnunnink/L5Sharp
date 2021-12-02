using FluentAssertions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Enums.Tests
{
    [TestFixture]
    public class RadixNullTests
    {
        [Test]
        public void Null_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Null;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Null);
        }

        [Test]
        public void Format_WhenCalled_ReturnsNull()
        {
            var formatted = Radix.Null.Format(new Dint());

            formatted.Should().BeNull();
        }
        
        [Test]
        public void Parse_WhenCalled_ReturnsNull()
        {
            var parsed = Radix.Null.Parse("Doesn't matter");

            parsed.Should().BeNull();
        }
    }
}