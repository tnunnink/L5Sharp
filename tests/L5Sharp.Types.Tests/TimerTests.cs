using System;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class TimerTests
    {
        [Test]
        public void Constructor_WhenCalled_ShouldNotBeNull()
        {
            var type = new Timer();
            type.Should().NotBeNull();
        }
        
        [Test]
        public void Constructor_ValidDint_ShouldHaveExpectedPREValue()
        {
            var type = new Timer(new Dint(5000));
            
            type.PRE.DataType.Value.Should().Be(5000);
        }
        
        [Test]
        public void Class_GetValue_ShouldBeExpected()
        {
            var type = new Timer();
            
            type.Class.Should().Be(DataTypeClass.Predefined);
        }

        [Test]
        public void Instantiate_WhenCalled_ShouldBeEqualToDefault()
        {
            var type = new Timer();

            var instance = type.Instantiate();

            instance.Should().NotBeNull();
            instance.Should().BeEquivalentTo(new Timer());
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

        [Test]
        public void CastingMembers_ToMutableCollection_ShouldNotBeProhibited()
        {
            var type = new Timer();
            
            FluentActions.Invoking(() => (MemberCollection)type.Members).Should().Throw<InvalidCastException>();
        }
    }
}