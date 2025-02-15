using FluentAssertions;

namespace L5Sharp.Tests.Core.Common
{
    [TestFixture]
    public class TaskPriorityTests
    {
        [Test]
        public void New_ValidArgument_ShouldNotBeNull()
        {
            var priority = new TaskPriority(10);

            priority.Should().NotBeNull();
        }
        
        [Test]
        public void New_ValidArgument_ShouldHaveValue()
        {
            var priority = new TaskPriority(10);

            priority.Equals(10).Should().BeTrue();
        }
        
        [Test]
        public void New_InvalidArgument_ShouldThrowException()
        {
            FluentActions.Invoking(() => new TaskPriority(16)).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void ImplicitOperator_Byte_ShouldBeExpected()
        {
            TaskPriority priority = 10;

            priority.Equals(10).Should().BeTrue();
        }

        [Test]
        public void ImplicitOperator_Type_ShouldBeExpected()
        {
            byte priority = new TaskPriority(10);

            priority.Equals(10).Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreEqual_ShouldBeTrue()
        {
            var first = new TaskPriority(10);
            var second = new TaskPriority(10);

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreNotEqual_ShouldBeFalse()
        {
            var first = new TaskPriority(10);
            var second = new TaskPriority(9);

            var result = first.Equals(second);

            result.Should().BeFalse();
        }

        [Test]
        public void TypedEquals_AreSame_ShouldBeTrue()
        {
            var first = new TaskPriority(10);
            var second = first;

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new TaskPriority(10);
            var second = new TaskPriority(10);

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new TaskPriority(10);
            var second = first;

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new TaskPriority(10);
            var second = new TaskPriority(10);

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new TaskPriority(10);
            var second = new TaskPriority(10);

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var first = new TaskPriority(10);

            var hash = first.GetHashCode();

            hash.Should().NotBe(0);
        }

        [Test]
        public void ToString_WhenCalled_ShouldReturnExpectedValue()
        {
            var rate = new TaskPriority(10);

            var value = rate.ToString();

            value.Should().Be("10");
        }
    }
}