using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Predefined;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class AlarmDigitalTests
    {
        [Test]
        public void Constructor_WhenCalled_ShouldNotBeNull()
        {
            var type = new AlarmDigital();

            type.Should().NotBeNull();
        }
        
        [Test]
        public void Class_GetValue_ShouldBeExpected()
        {
            var type = new AlarmDigital();
            
            type.Class.Should().Be(DataTypeClass.Predefined);
        }

        [Test]
        public void Instantiate_WhenCalled_ShouldBeEqualToDefault()
        {
            var type = new AlarmDigital();

            var instance = type.Instantiate();

            instance.Should().NotBeNull();
            instance.Should().BeEquivalentTo(new AlarmDigital());
        }
    }
}