using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Configurations.Tests
{
    [TestFixture]
    public class DataTypeMemberConfigurationTests
    {
        [Test]
        public void HasDescription_WhenCalled_ShouldBeExpected()
        {
            var config = new DataTypeMemberConfiguration("Test");

            config.HasDescription("This is a test");

            var result = config.Compile();
            result.Description.Should().Be("This is a test");
        }
        
        [Test]
        public void OfType_WhenCalled_ShouldBeExpected()
        {
            var config = new DataTypeMemberConfiguration("Test");

            config.OfType(new Dint());

            var result = config.Compile();
            result.DataType.Should().Be(new Dint());
        }
        
        [Test]
        public void WithDimension_WhenCalled_ShouldBeExpected()
        {
            var config = new DataTypeMemberConfiguration("Test");

            config.WithDimension(new Dimensions(10));

            var result = config.Compile();
            result.Dimensions.Should().Be(new Dimensions(10));
        }

    }
}