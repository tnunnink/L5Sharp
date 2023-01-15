using System;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Core;
using NUnit.Framework;

namespace L5Sharp.Tests.Core
{
    [TestFixture]
    public class ComponentNameTests
    {
        [Test]
        public void New_ValidName_ShouldNotBeNull()
        {
            var name = new ComponentName("Test");

            name.Should().NotBeNull();
        }
        
        [Test]
        public void New_ValidName_ShouldBeExpected()
        {
            var name = new ComponentName("Test");

            name.Should().Be("Test");
        }

        [Test]
        public void New_InvalidName_ShouldThrowComponentNameInvalidException()
        {
            var fixture = new Fixture();
            FluentActions.Invoking(() => new ComponentName(fixture.Create<string>())).Should()
                .Throw<ArgumentException>();
        }

        [Test]
        public void New_EmptyString_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new ComponentName(string.Empty)).Should()
                .Throw<ArgumentException>();   
        }
        
        [Test]
        public void New_Null_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new ComponentName(null)).Should()
                .Throw<ArgumentException>();   
        }

        [Test]
        public void Set_Empty_ShouldThrowArgumentException()
        {
            var name = new ComponentName("Test");

            FluentActions.Invoking(() => name = string.Empty).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Set_ValidName_ShouldBeExpectedValue()
        {
            var name = new ComponentName("Test");

            name = "New";

            name.ToString().Should().Be("New");
        }
        
        [Test]
        public void Set_InvalidName_ShouldThrowComponentNameInvalidException()
        {
            var fixture = new Fixture();
            var name = new ComponentName("Test");

            FluentActions.Invoking(() => name = fixture.Create<string>()).Should()
                .Throw<ArgumentException>();
        }

        [Test]
        public void Set_StartsWithNumber_ShouldThrowComponentNameInvalidException()
        {
            var name = new ComponentName("Test");

            FluentActions.Invoking(() => name = "1Test").Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void Set_StartsInvalidCharacter_ShouldThrowComponentNameInvalidException()
        {
            var name = new ComponentName("Test");

            FluentActions.Invoking(() => name = "$Test").Should().Throw<ArgumentException>();
        }

        [Test]
        public void CompareTo_Equal_ShouldBeZero()
        {
            var first = new ComponentName("Test");
            var second = new ComponentName("Test");

            var result = first.CompareTo(second);

            result.Should().Be(0);
        }
        
        [Test]
        public void CompareTo_Different_ShouldNotBeZero()
        {
            var first = new ComponentName("Test1");
            var second = new ComponentName("Test2");

            var result = first.CompareTo(second);

            result.Should().NotBe(0);
        }
        
        [Test]
        public void CompareTo_Same_ShouldBeZero()
        {
            var first = new ComponentName("Test1");
            var second = first;

            var result = first.CompareTo(second);

            result.Should().Be(0);
        }
        
        [Test]
        public void CompareTo_Null_ShouldBeOne()
        {
            var first = new ComponentName("Test");

            var result = first.CompareTo(null!);

            result.Should().Be(1);
        }

        [Test]
        public void ImplicitOperator_String_ShouldBeOfTypeString()
        {
            var name = new ComponentName("Test");

            string value = name;

            value.Should().BeOfType<string>();
            value.Should().Be("Test");
        }
        
        
        [Test]
        public void ImplicitOperator_ComponentName_ShouldBeOfTypeComponentName()
        {
            ComponentName name = "Test";

            name.Should().BeOfType<ComponentName>();
            name.ToString().Should().Be("Test");
        }

        [Test]
        public void Copy_WhenCalled_ReturnsDifferentInstanceWithSameValue()
        {
            var name = new ComponentName("Test");

            var copy = name.Copy();

            copy.Should().NotBeSameAs(name);
            copy.Should().BeEquivalentTo(name);
        }
        
        [Test]
        public void TypedEquals_AreEqual_ShouldBeTrue()
        {
            var first = new ComponentName("Test");
            var second = new ComponentName("Test");

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreSame_ShouldBeTrue()
        {
            var first = new ComponentName("Test");
            var second = first;

            var result = first.Equals(second);

            result.Should().BeTrue();
        }


        [Test]
        public void TypedEquals_Null_ShouldBeFalse()
        {
            var first = new ComponentName("Test");

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new ComponentName("Test");
            var second = new ComponentName("Test");

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new ComponentName("Test");
            var second = first;

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }


        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new ComponentName("Test");

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new ComponentName("Test");
            var second = new ComponentName("Test");

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new ComponentName("Test");
            var second = new ComponentName("Test");

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var first = new ComponentName("Test");

            var hash = first.GetHashCode();

            hash.Should().NotBe(0);
        }
    }
}