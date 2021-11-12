using System;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class MemberTests
    {
        [Test]
        public void Create_ValidNameAndType_ShouldNotBeNull()
        {
            var type = new DataType("Test");
            var member = Member.Create("Test", type);

            member.Should().NotBeNull();
        }
        
        [Test]
        public void Create_TypedValidNameAndType_ShouldNotBeNull()
        {
            var member = Member.Create("Test", new Bool());

            member.Should().NotBeNull();
        }

        [Test]
        public void Create_TypedValidName_ShouldNotBeNull()
        {
            var member = Member.Create<Bool>("Test");

            member.Should().NotBeNull();
        }

        [Test]
        public void New_ArrayType_ShouldHaveExpectedLength()
        {
            var fixture = new Fixture();
            var length = fixture.Create<ushort>();
            var array = Member.Create<Dint>("Test", new Dimensions(length));

            array.Elements.Length.Should().Be(length);
            array.Elements.Should().BeOfType<IMember<Dint>[]>();
        }

        [Test]
        public void New_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Member.Create<Dint>(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void New_NullType_ShouldHaveNullType()
        {
            var member = Member.Create("Name", null);
            member.DataType.Should().BeNull();
        }

        [Test]
        public void New_OverrideProperties_ShouldHaveExpectedOverloads()
        {
            var member = Member.Create("Member", (IDataType)new Real(), new Dimensions(35), Radix.Exponential,
                ExternalAccess.ReadOnly, "Test");

            member.Should().NotBeNull();
            member.Name.Should().Be("Member");
            member.DataType.Should().Be(new Real(Radix.Exponential));
            member.Dimensions.Length.Should().Be(35);
            member.Radix.Should().Be(Radix.Exponential);
            member.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
            member.Description.Should().Be("Test");
            member.Elements.Length.Should().Be(35);
            member.Elements.Should().AllBeOfType<Member<IDataType>>();
        }

        [Test]
        public void Name_GetValue_ShouldBeExpected()
        {
            var member = Member.Create<Real>("Member");

            var name = member.Name;

            name.Should().Be("Member");
        }

        [Test]
        public void DataType_GetValue_ShouldBeExpected()
        {
            var member = Member.Create<Real>("Member");

            var dataType = member.DataType;

            dataType.Should().Be(new Real());
        }

        [Test]
        public void Dimension_GetValue_ShouldBeExpected()
        {
            var member = Member.Create<Real>("Member");

            var dimension = member.Dimensions;

            dimension.Length.Should().Be(0);
        }

        [Test]
        public void Radix_GetValue_ShouldBeExpected()
        {
            var member = Member.Create<Real>("Member");

            var radix = member.Radix;

            radix.Should().Be(Radix.Float);
        }

        [Test]
        public void ExternalAccess_GetValue_ShouldBeExpected()
        {
            var member = Member.Create<Real>("Member");

            var access = member.ExternalAccess;

            access.Should().Be(ExternalAccess.ReadWrite);
        }

        [Test]
        public void Description_GetValue_ShouldBeExpected()
        {
            var member = Member.Create<Real>("Member");

            var description = member.Description;

            description.Should().BeNull();
        }

        [Test]
        public void SetRadix_Null_ShouldThrowArgumentNullException()
        {
            var member = Member.Create<Real>("Test");

            FluentActions.Invoking(() => member.SetRadix(null)).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void SetRadix_InvalidRadix_ShouldThrowRadixNotSupportException()
        {
            var member = Member.Create<Real>("Test");

            FluentActions.Invoking(() => member.SetRadix(Radix.Decimal)).Should()
                .Throw<RadixNotSupportedException>();
        }

        [Test]
        public void SetRadix_ValidRadix_ShouldBeExpectedValue()
        {
            var member = Member.Create<Real>("Test");

            member.SetRadix(Radix.Exponential);

            member.Radix.Should().Be(Radix.Exponential);
        }
        
        [Test]
        public void SetDescription_Null_ShouldRevertToDefaultDescription()
        {
            var member = Member.Create<Real>("Test", description: "this is a test");

            member.SetDescription(null);

            member.Description.Should().Be("this is a test");
        }
        
        [Test]
        public void SetDescription_EmptyString_ShouldRevertToDefaultDescription()
        {
            var member = Member.Create<Real>("Test", description: "this is a test");

            member.SetDescription(string.Empty);

            member.Description.Should().Be("this is a test");
        }
        
        [Test]
        public void SetDescription_String_ShouldBeEmpty()
        {
            var member = Member.Create<Real>("Test");

            member.SetDescription("This is a test");

            member.Description.Should().Be("This is a test");
        }

        [Test]
        public void Elements_GetValue_ShouldBeEmptyArray()
        {
            var member = Member.Create<Real>("Member");

            var elements = member.Elements;

            elements.Should().BeEmpty();
        }

        [Test]
        public void Copy_WhenCalled_ShouldReturnDifferentInstances()
        {
            var member = Member.Create<Bool>("Test").As<Member<Bool>>();

            var copy = member.Copy();

            copy.Should().NotBeSameAs(member);
            copy.DataType.Should().NotBeSameAs(member.DataType);
            copy.Dimensions.Should().NotBeSameAs(member.Dimensions);
        }

        [Test]
        public void Copy_WhenCalled_PropertiesShouldBeEqual()
        {
            var member = Member.Create<Bool>("Test").As<Member<Bool>>();

            var copy = member.Copy();

            copy.Should().BeEquivalentTo(member);
        }

        [Test]
        public void TypedEquals_AreEqual_ShouldBeTrue()
        {
            var first = (Member<Bool>)Member.Create("Test", new Bool());
            var second = (Member<Bool>)Member.Create("Test", new Bool());

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreSame_ShouldBeTrue()
        {
            var first = (Member<Bool>)Member.Create("Test", new Bool());
            var second = first;

            var result = first.Equals(second);

            result.Should().BeTrue();
        }


        [Test]
        public void TypedEquals_Null_ShouldBeFalse()
        {
            var first = (Member<Bool>)Member.Create("Test", new Bool());

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = Member.Create("Test", new Bool());
            var second = Member.Create("Test", new Bool());

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = Member.Create("Test", new Bool());
            var second = first;

            var result = first.Equals(second);

            result.Should().BeTrue();
        }


        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = Member.Create("Test", new Bool());

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = (Member<Bool>)Member.Create("Test", new Bool());
            var second = (Member<Bool>)Member.Create("Test", new Bool());

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = (Member<Bool>)Member.Create("Test", new Bool());
            var second = (Member<Bool>)Member.Create("Test", new Bool());

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var first = Member.Create("Test", new Bool());

            var hash = first.GetHashCode();

            hash.Should().NotBe(0);
        }
        
        
        [Test]
        public void DimensionsAttribute_Get_ShouldBeDimension()
        {
            var member = Member.Create("Test", new Bool(), new Dimensions(4));

            var property = member.GetType().GetProperty("Dimensions");
            var attribute = (XmlAttributeAttribute) property?.GetCustomAttributes(typeof(XmlAttributeAttribute), true).First();

            attribute.Should().NotBeNull();
            attribute.AttributeName.Should().Be("Dimension");
        }
    }
}