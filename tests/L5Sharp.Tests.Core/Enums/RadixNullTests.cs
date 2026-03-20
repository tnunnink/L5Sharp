using FluentAssertions;

namespace L5Sharp.Tests.Core.Enums
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
            FluentActions.Invoking(() => Radix.Null.Format(12))
                .Should().Throw<NotSupportedException>();
        }

        [Test]
        public void Parse_WhenCalled_ReturnsNull()
        {
            FluentActions.Invoking(() => Radix.Null.Parse<int>("Doesn't matter"))
                .Should().Throw<NotSupportedException>();
        }
    }
}