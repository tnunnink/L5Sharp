using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class ElementTests
    {
        [Test]
        public void New_ValidArguments_ShouldNotBeNull()
        {
            var element = new Element<Dint>("[0]", new Dint());

            element.Should().NotBeNull();
        }
        
        [Test]
        public void New_Overloads_shouldBeExpected()
        {
            var element = new Element<Dint>("[0]", new Dint(), Radix.Binary, ExternalAccess.None, "This is a test");

            element.Name.Should().Be("[0]");
            element.DataType.Should().BeOfType<Dint>();
            element.Dimension.Should().Be(Dimensions.Empty);
            element.Radix.Should().Be(Radix.Binary);
            element.ExternalAccess.Should().Be(ExternalAccess.None);
            element.Description.Should().Be("This is a test");
        }
        
        [Test]
        public void New_ValidArguments_ShouldHaveExpectedDefaults()
        {
            var element = new Element<Dint>("[0]", new Dint());

            element.Name.Should().Be("[0]");
            element.DataType.Should().BeOfType<Dint>();
            element.Dimension.Should().Be(Dimensions.Empty);
            element.Radix.Should().Be(Radix.Decimal);
            element.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
        }
        
        [Test]
        public void TypedEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Element<Dint>("[0]", new Dint());
            var second = new Element<Dint>("[0]", new Dint());

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreSame_ShouldBeTrue()
        {
            var first = new Element<Dint>("[0]", new Dint());
            var second = first;

            var result = first.Equals(second);

            result.Should().BeTrue();
        }


        [Test]
        public void TypedEquals_Null_ShouldBeFalse()
        {
            var first = new Element<Dint>("[0]", new Dint());

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Element<Dint>("[0]", new Dint());
            var second = new Element<Dint>("[0]", new Dint());

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new Element<Dint>("[0]", new Dint());
            var second = first;

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }


        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new Element<Dint>("[0]", new Dint());

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Element<Dint>("[0]", new Dint());
            var second = new Element<Dint>("[0]", new Dint());

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new Element<Dint>("[0]", new Dint());
            var second = new Element<Dint>("[0]", new Dint());

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var first = new Element<Dint>("[0]", new Dint());

            var hash = first.GetHashCode();

            hash.Should().NotBe(0);
        }
    }
}