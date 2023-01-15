using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;
using NUnit.Framework;

namespace L5Sharp.Tests.Types
{
    [TestFixture]
    public class TimerTests
    {
        [Test]
        public void Constructor_WhenCalled_ShouldNotBeNull()
        {
            var type = new TIMER();
            type.Should().NotBeNull();
        }

        [Test]
        public void Constructor_ValidDint_ShouldHaveExpectedPREValue()
        {
            var type = new TIMER(new DINT(5000));

            type.PRE.Should().Be(5000);
        }

        [Test]
        public void Class_GetValue_ShouldBeExpected()
        {
            var type = new TIMER();

            type.Class.Should().Be(DataTypeClass.Predefined);
        }

        [Test]
        public void Members_ShouldNotBeEmpty()
        {
            var type = new TIMER();

            type.Members().Should().NotBeEmpty();
            type.Members().Should().HaveCount(5);
        }

        [Test]
        public void InstanceMembers_Get_ShouldNotBeNull()
        {
            var type = new TIMER();

            type.DN.Should().NotBeNull();
            type.EN.Should().NotBeNull();
            type.TT.Should().NotBeNull();
            type.ACC.Should().NotBeNull();
            type.PRE.Should().NotBeNull();
        }

        [Test]
        public void SetMember_ValidValue_ShouldBeExpectedValue()
        {
            var timer = new TIMER();

            timer.PRE = 6000;

            timer.PRE.Should().Be(new DINT(6000));
        }

        [Test]
        public void Member_Get_ShouldContinueToReturnSameReference()
        {
            var type = new TIMER();

            type.DN.Should().BeSameAs(type.DN);
            type.TT.Should().BeSameAs(type.TT);
            type.EN.Should().BeSameAs(type.EN);
            type.PRE.Should().BeSameAs(type.PRE);
            type.ACC.Should().BeSameAs(type.ACC);
        }
    }
}