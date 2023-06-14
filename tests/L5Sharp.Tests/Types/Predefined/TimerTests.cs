using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Tests.Types.Predefined
{
    [TestFixture]
    public class TimerTests
    {
        [Test]
        public void New_WhenCalled_ShouldNotBeNull()
        {
            var type = new TIMER();
            
            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_Default_ShouldHaveExpectedValues()
        {
            var type = new TIMER();
            
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Members.Should().HaveCount(5);
            type.DN.Should().Be(0);
            type.EN.Should().Be(0);
            type.TT.Should().Be(0);
            type.ACC.Should().Be(0);
            type.PRE.Should().Be(0);
        }

        [Test]
        public void New_Overload_ShouldHaveExpectedValues()
        {
            var timer = new TIMER
            {
                DN = false,
                PRE = 6000,
                ACC = 3403,
                TT = true,
                EN = true
            };

            timer.PRE.Should().Be(6000);
            timer.ACC.Should().Be(3403);
            timer.DN.Should().Be(false);
            timer.TT.Should().Be(true);
            timer.EN.Should().Be(true);
        }
        
        [Test]
        public Task Serialize_Overload_ShouldBeVerified()
        {
            var timer = new TIMER
            {
                DN = false,
                PRE = 6000,
                ACC = 3403,
                TT = true,
                EN = true
            };

            var xml = timer.Serialize().ToString();

            return Verify(xml);
        }
    }
}