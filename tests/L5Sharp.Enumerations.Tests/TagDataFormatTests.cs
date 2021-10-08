using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Enumerations.Tests
{
    [TestFixture]
    public class TagDataFormatTests
    {
        [Test]
        public void New_Decorated_ShouldNotBeNullAndHaveExpectedValue()
        {
            var format = TagDataFormat.Decorated;

            format.Should().NotBeNull();
            format.Name.Should().Be("Decorated");
        }
        
        [Test]
        public void New_L5K_ShouldNotBeNullAndHaveExpectedValue()
        {
            var format = TagDataFormat.L5K;

            format.Should().NotBeNull();
            format.Name.Should().Be("L5K");
        }
        
        
        [Test]
        public void New_String_ShouldNotBeNullAndHaveExpectedValue()
        {
            var format = TagDataFormat.String;

            format.Should().NotBeNull();
            format.Name.Should().Be("String");
        }
        
        
        [Test]
        public void New_Alarm_ShouldNotBeNullAndHaveExpectedValue()
        {
            var format = TagDataFormat.Alarm;

            format.Should().NotBeNull();
            format.Name.Should().Be("Alarm");
        }
    }
}