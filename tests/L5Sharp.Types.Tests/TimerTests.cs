using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class TimerTests
    {
        [Test]
        public void New_WhenCalled_ShouldNotBeNull()
        {
            var type = new Timer();
            type.Should().NotBeNull();
        }

        [Test]
        public void Members_ShouldNotBeEmpty()
        {
            var type = new Timer();

            type.Members.Should().NotBeEmpty();
            type.Members.Should().HaveCount(5);
        }

        [Test]
        public void InstanceMembers_Get_ShouldNotBeNull()
        {
            var type = new Timer();
            
            type.DN.Should().NotBeNull();
            type.EN.Should().NotBeNull();
            type.TT.Should().NotBeNull();
            type.ACC.Should().NotBeNull();
            type.PRE.Should().NotBeNull();
        }
        
        [Test]
        public void Member_Get_ShouldContinueToReturnSameReference()
        {
            var type = new Timer();
            
            type.DN.Should().BeSameAs(type.DN);
            type.TT.Should().BeSameAs(type.TT);
            type.EN.Should().BeSameAs(type.EN);
            type.PRE.Should().BeSameAs(type.PRE);
            type.ACC.Should().BeSameAs(type.ACC);
        }
    }
}