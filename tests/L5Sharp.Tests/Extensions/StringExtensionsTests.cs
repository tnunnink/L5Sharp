using AutoFixture;
using FluentAssertions;
using L5Sharp.Extensions;

namespace L5Sharp.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public void IsEmpty_Empty_ShouldBeTrue()
        {
            string.Empty.IsEmpty().Should().BeTrue();
        }
        
        [Test]
        public void IsEmpty_NotEmpty_ShouldBeFalse()
        {
            var fixture = new Fixture();
            fixture.Create<string>().IsEmpty().Should().BeFalse();
        }
    }
}