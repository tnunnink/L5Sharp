using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class AlarmAnalogTest
    {
        [Test]
        public void Constructor_WhenCalled_ShouldNotBeNull()
        {
            var type = new AlarmAnalog();

            type.Should().NotBeNull();
        }
        
        [Test]
        public void Class_GetValue_ShouldBeExpected()
        {
            var type = new AlarmAnalog();
            
            type.Class.Should().Be(DataTypeClass.Predefined);
        }

        [Test]
        public void Instantiate_WhenCalled_ShouldBeEqualToDefault()
        {
            var type = new AlarmAnalog();

            var instance = type.Instantiate();

            instance.Should().NotBeNull();
            instance.Should().BeEquivalentTo(new AlarmAnalog());
        }
    }
}