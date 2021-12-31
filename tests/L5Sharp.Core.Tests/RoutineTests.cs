using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class RoutineTests
    {
        [Test]
        public void Create_NonGeneric_ShouldNotBeNull()
        {
            var routine = Routine.Create("Test");

            routine.Should().NotBeNull();
        }
        
        [Test]
        public void Create_Default_ShouldNotBeNull()
        {
            var routine = Routine.Create<LadderLogic>("Test");

            routine.Should().NotBeNull();
        }

        [Test]
        public void Create_Overloaded_ShouldHaveExpectedValues()
        {
            var routine = Routine.Create<LadderLogic>("Test", "This is a test routine");

            routine.Name.Should().Be("Test");
            routine.Description.Should().Be("This is a test routine");
            routine.Type.Should().Be(RoutineType.Rll);
            routine.Content.Should().NotBeNull();
        }
        
        [Test]
        public void TypedEquals_AreEqual_ShouldBeTrue()
        {
            var first = (Routine<LadderLogic>)Routine.Create<LadderLogic>("Test");
            var second = (Routine<LadderLogic>)Routine.Create<LadderLogic>("Test");

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreSame_ShouldBeTrue()
        {
            var first = (Routine<LadderLogic>)Routine.Create<LadderLogic>("Test");

            var result = first.Equals(first);

            result.Should().BeTrue();
        }


        [Test]
        public void TypedEquals_Null_ShouldBeFalse()
        {
            var first = (Routine<LadderLogic>)Routine.Create<LadderLogic>("Test");

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = (Routine<LadderLogic>)Routine.Create<LadderLogic>("Test");
            var second = (Routine<LadderLogic>)Routine.Create<LadderLogic>("Test");

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = (Routine<LadderLogic>)Routine.Create<LadderLogic>("Test");

            var result = first.Equals((object)first);

            result.Should().BeTrue();
        }


        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = (Routine<LadderLogic>)Routine.Create<LadderLogic>("Test");

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = (Routine<LadderLogic>)Routine.Create<LadderLogic>("Test");
            var second = (Routine<LadderLogic>)Routine.Create<LadderLogic>("Test");

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = (Routine<LadderLogic>)Routine.Create<LadderLogic>("Test");
            var second = (Routine<LadderLogic>)Routine.Create<LadderLogic>("Test");

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var first = (Routine<LadderLogic>)Routine.Create<LadderLogic>("Test");

            var hash = first.GetHashCode();

            hash.Should().NotBe(0);
        }
    }
}