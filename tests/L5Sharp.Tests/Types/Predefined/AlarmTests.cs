using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Tests.Types.Predefined
{
    [TestFixture]
    public class AlarmTests
    {
        [Test]
        public void Constructor_WhenCalled_ShouldNotBeNull()
        {
            var type = new ALARM();

            type.Should().NotBeNull();
        }
        
        [Test]
        public void Class_GetValue_ShouldBeExpected()
        {
            var type = new ALARM();
            
            type.Class.Should().Be(DataTypeClass.Predefined);
        }

        [Test]
        public void UpdateMemberValue_ShouldBeExpected()
        {
            var alarm = new ALARM();

            alarm.EnableIn = false;

            alarm.EnableIn = true;

            alarm.EnableIn.Should().Be(true);
        }
    }
}