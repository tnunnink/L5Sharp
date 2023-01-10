using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types.Predefined;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class AlarmAnalogTest
    {
        [Test]
        public void Constructor_WhenCalled_ShouldNotBeNull()
        {
            var type = new ALARM_ANALOG();

            type.Should().NotBeNull();
        }
        
        [Test]
        public void Class_GetValue_ShouldBeExpected()
        {
            var type = new ALARM_ANALOG();
            
            type.Class.Should().Be(DataTypeClass.Predefined);
        }

        [Test]
        public void Instantiate_WhenCalled_ShouldBeEqualToDefault()
        {
            var type = new ALARM_ANALOG();

            var instance = type.Instantiate();

            instance.Should().NotBeNull();
            instance.Should().BeEquivalentTo(new ALARM_ANALOG());
        }
    }
}