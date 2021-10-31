using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Configurations.Tests
{
    [TestFixture]
    public class DataTypeMemberConfigurationTests
    {
        [Test]
        public void WithDescription_WhenCalled_ShouldBeExpected()
        {
            var config = new DataTypeMemberConfiguration("Test");

            config.HasDescription("This is a test");

            var result = config.Compile();
            result.Should().Be("This is a test");
        }
    }
}