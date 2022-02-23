using FluentAssertions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Enums.Tests
{
    [TestFixture]
    public class TagDataFormatTests
    {
        [Test]
        public void New_Decorated_ShouldBeExpected()
        {
            var sut = TagDataFormat.Decorated;

            sut.Should().NotBeNull();
            sut.Value.Should().Be("Decorated");
        }
        
        [Test]
        public void New_String_ShouldBeExpected()
        {
            var sut = TagDataFormat.String;

            sut.Should().NotBeNull();
            sut.Value.Should().Be("String");
        }
        
        [Test]
        public void New_Alarm_ShouldBeExpected()
        {
            var sut = TagDataFormat.Alarm;

            sut.Should().NotBeNull();
            sut.Value.Should().Be("Alarm");
        }
        
        [Test]
        public void New_L5K_ShouldBeExpected()
        {
            var sut = TagDataFormat.L5K;

            sut.Should().NotBeNull();
            sut.Value.Should().Be("L5K");
        }

        [Test]
        public void FromDataType_String_ShouldBeString()
        {
            var format = TagDataFormat.FromDataType(new String());

            format.Should().Be(TagDataFormat.String);
        }
        
        [Test]
        public void FromDataType_AlarmDigital_ShouldBeAlarm()
        {
            var format = TagDataFormat.FromDataType(new AlarmDigital());

            format.Should().Be(TagDataFormat.Alarm);
        }
        
        [Test]
        public void FromDataType_AlarmAnalog_ShouldBeAlarm()
        {
            var format = TagDataFormat.FromDataType(new AlarmAnalog());

            format.Should().Be(TagDataFormat.Alarm);
        }
        
        [Test]
        public void FromDataType_Complex_ShouldBeDecorated()
        {
            var format = TagDataFormat.FromDataType(new Timer());

            format.Should().Be(TagDataFormat.Decorated);
        }
    }
}