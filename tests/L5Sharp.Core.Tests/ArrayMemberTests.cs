﻿using System.Collections;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class ArrayMemberTests
    {
        [Test]
        public void Create_ValidName_ShouldNotBeNull()
        {
            var member = Member.Create<Dint>("Test", new Dimensions(10));

            member.Should().NotBeNull();
        }

        [Test]
        public void Create_ValidDimensions_ShouldBeOfTypeIArrayMember()
        {
            var member = Member.Create<Dint>("Test", new Dimensions(10));

            member.Should().BeOfType<ArrayMember<Dint>>();
        }

        [Test]
        public void Index_Get_ShouldReturnValidElement()
        {
            var member = Member.Create<Dint>("Test", new Dimensions(10));

            var element = member[5];

            element.Should().NotBeNull();
            element.Name.Should().Be("[5]");
            element.DataType.Should().BeOfType<Dint>();
            element.Radix.Should().Be(Radix.Decimal);
            element.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            element.Description.Should().BeNull();
        }

        [Test]
        public void New_Overloaded_ShouldHaveExpectedValues()
        {
            var member = Member.Create<Dint>("Test", new Dimensions(10), Radix.Binary, ExternalAccess.None,
                "This is a test");

            member.Should().NotBeNull();
            member.Name.ToString().Should().Be("Test");
            member.DataType.Should().BeOfType<Dint>();
            member.Radix.Should().Be(Radix.Binary);
            member.ExternalAccess.Should().Be(ExternalAccess.None);
            member.Description.Should().Be("This is a test");
        }

        [Test]
        public void IterateCollection_ShouldAllNotBeNull()
        {
            var member = Member.Create<Dint>("Test", new Dimensions(10));

            foreach (var element in member)
            {
                element.Should().NotBeNull();
            }
        }

        [Test]
        public void Copy_WhenCalled_ShouldNotBeSameButEqual()
        {
            var member = Member.Create<Dint>("Test", new Dimensions(10), Radix.Binary, ExternalAccess.ReadOnly,
                "This is a test");

            var copy = member.Copy();

            copy.Should().NotBeSameAs(member);
            copy.Name.Should().NotBeSameAs(member.Name);
            copy.DataType.Should().NotBeSameAs(member.DataType);
            copy.Dimension.Should().NotBeSameAs(member.Dimension);
            copy.Description.Should().NotBeSameAs(member.Description);
        }

        [Test]
        public void TypedEquals_AreEqual_ShouldBeTrue()
        {
            var first = (ArrayMember<Dint>)Member.Create<Dint>("Test", new Dimensions(10));
            var second = (ArrayMember<Dint>)Member.Create<Dint>("Test", new Dimensions(10));

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreSame_ShouldBeTrue()
        {
            var first = (ArrayMember<Dint>)Member.Create<Dint>("Test", new Dimensions(10));
            var second = first;

            var result = first.Equals(second);

            result.Should().BeTrue();
        }


        [Test]
        public void TypedEquals_Null_ShouldBeFalse()
        {
            var first = (ArrayMember<Dint>)Member.Create<Dint>("Test", new Dimensions(10));

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = Member.Create<Dint>("Test", new Dimensions(10));
            var second = Member.Create<Dint>("Test", new Dimensions(10));

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = Member.Create<Dint>("Test", new Dimensions(10));
            var second = first;

            var result = first.Equals(second);

            result.Should().BeTrue();
        }


        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = Member.Create<Dint>("Test", new Dimensions(10));

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = (ArrayMember<Dint>)Member.Create<Dint>("Test", new Dimensions(10));
            var second = (ArrayMember<Dint>)Member.Create<Dint>("Test", new Dimensions(10));

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = (ArrayMember<Dint>)Member.Create<Dint>("Test", new Dimensions(10));
            var second = (ArrayMember<Dint>)Member.Create<Dint>("Test", new Dimensions(10));

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var first = Member.Create<Dint>("Test", new Dimensions(10));

            var hash = first.GetHashCode();

            hash.Should().NotBe(0);
        }

        [Test]
        public void GetEnumerator_Object_ShouldNotBeNull()
        {
            var first = (IEnumerable)Member.Create<Dint>("Test", new Dimensions(10));

            var enumerator = first.GetEnumerator();

            enumerator.Should().NotBeNull();
        }
    }
}