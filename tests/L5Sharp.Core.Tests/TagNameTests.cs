using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TagNameTests
    {
        private const string TestTagName = "MyTag_01.SomeMember[1].Some_Element_Property";
        private const string Base = "MyTag_01";
        private const string Operand = ".SomeMember[1].Some_Element_Property";
        private const string Path = "SomeMember[1].Some_Element_Property";

        private static readonly IEnumerable<string> Members = new List<string>
        {
            "MyTag_01",
            "SomeMember",
            "[1]",
            "Some_Element_Property"
        };


        [Test]
        public void New_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new TagName(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_Empty_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new TagName(string.Empty)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_Invalid_ShouldThrowFormatException()
        {
            var fixture = new Fixture();
            FluentActions.Invoking(() => new TagName(fixture.Create<string>())).Should().Throw<FormatException>();
        }

        [Test]
        public void New_Valid_ShouldNotBeNull()
        {
            var tagName = new TagName(TestTagName);

            tagName.Should().NotBeNull();
        }

        [Test]
        public void New_Valid_ShouldHaveExpectedProperties()
        {
            var tagName = new TagName(TestTagName);

            tagName.Base.Should().Be(Base);
            tagName.Operand.Should().Be(Operand);
            tagName.Path.Should().Be(Path);
            tagName.Members.Should().HaveCount(4);
        }

        [Test]
        public void Members_WhenCalled_ShouldContainExpectedValues()
        {
            var tagName = new TagName(TestTagName);

            var members = tagName.Members;

            members.Should().BeEquivalentTo(Members);
        }

        [Test]
        public void Combine_TwoMemberNames_ShouldBeExpected()
        {
            var tagName = TagName.Combine(Base, Path);

            tagName.Should().Be(TestTagName);
        }

        [Test]
        public void Combine_CollectionOfValidMembers_ShouldBeExpectedValue()
        {
            var tagName = TagName.Combine(Members);

            tagName.Should().Be(TestTagName);
        }

        [Test]
        public void Combine_CollectionOfInvalidMembers_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var members = fixture.CreateMany<string>();

            FluentActions.Invoking(() => TagName.Combine(members)).Should().Throw<FormatException>();
        }

        [Test]
        public void ImplicitOperator_Byte_ShouldBeExpected()
        {
            TagName tagName = TestTagName;

            tagName.Equals(TestTagName).Should().BeTrue();
        }

        [Test]
        public void ImplicitOperator_ScanRate_ShouldBeExpected()
        {
            string tagName = new TagName(TestTagName);

            tagName.Equals(TestTagName).Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreEqual_ShouldBeTrue()
        {
            var first = new TagName(TestTagName);
            var second = new TagName(TestTagName);

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreNotEqual_ShouldBeFalse()
        {
            var first = new TagName(TestTagName);
            var second = new TagName(Path);

            var result = first.Equals(second);

            result.Should().BeFalse();
        }

        [Test]
        public void TypedEquals_Null_ShouldBeFalse()
        {
            var first = new TagName(TestTagName);

            var result = first.Equals(null!);

            result.Should().BeFalse();
        }

        [Test]
        public void TypedEquals_AreSame_ShouldBeTrue()
        {
            var first = new TagName(TestTagName);

            var result = first.Equals(first);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new TagName(TestTagName);
            var second = new TagName(TestTagName);

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new TagName(TestTagName);

            var result = first.Equals((object)first);

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new TagName(TestTagName);
            var second = new TagName(TestTagName);

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new TagName(TestTagName);
            var second = new TagName(TestTagName);

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var first = new TagName(TestTagName);

            var hash = first.GetHashCode();

            hash.Should().NotBe(0);
        }

        [Test]
        public void ToString_WhenCalled_ShouldReturnExpectedValue()
        {
            var first = new TagName(TestTagName);

            var value = first.ToString();

            value.Should().Be(TestTagName);
        }
        
        [Test]
        public void CompareTo_Equal_ShouldBeZero()
        {
            var first = new TagName(TestTagName);
            var second = new TagName(TestTagName);

            var result = first.CompareTo(second);

            result.Should().Be(0);
        }
        
        [Test]
        public void CompareTo_Different_ShouldNotBeZero()
        {
            var first = new TagName(TestTagName);
            var second = new TagName(Path);

            var result = first.CompareTo(second);

            result.Should().NotBe(0);
        }
        
        [Test]
        public void CompareTo_Same_ShouldBeZero()
        {
            var first = new TagName(TestTagName);

            var result = first.CompareTo(first);

            result.Should().Be(0);
        }
        
        [Test]
        public void CompareTo_Null_ShouldBeOne()
        {
            var first = new TagName(TestTagName);

            var result = first.CompareTo(null!);

            result.Should().Be(1);
        }
    }
}