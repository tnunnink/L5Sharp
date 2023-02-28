using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Core;
using NUnit.Framework;

namespace L5Sharp.Tests.Core
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
            FluentActions.Invoking(() => new TagName(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void New_Empty_ShouldNotBeNullAndIsEmptyTrue()
        {
            var tagName = new TagName(string.Empty);

            tagName.Should<string>().NotBeNull();
            tagName.IsEmpty.Should().BeTrue();
        }

        [Test]
        public void New_Invalid_ShouldNotBeNullAndIsValidFalse()
        {
            var fixture = new Fixture();
            var tagName = new TagName(fixture.Create<string>());

            tagName.Should<string>().NotBeNull();
            tagName.IsValid.Should().BeFalse();
        }

        [Test]
        public void New_SimpleTagName_ShouldNotBeNull()
        {
            var tagName = new TagName("Test");

            tagName.Should<string>().NotBeNull();
        }
        
        [Test]
        public void New_SimpleTagName_ShouldBeExpected()
        {
            var tagName = new TagName("Test");

            tagName.Tag.Should().Be("Test");
            tagName.Operand.Should().BeEmpty();
            tagName.Path.Should().BeEmpty();
            tagName.Depth.Should().Be(0);
            tagName.Should<string>().HaveCount(1);
            tagName.Should<string>().HaveCount(1);
            tagName.IsEmpty.Should().BeFalse();
            tagName.IsValid.Should().BeTrue();
        }

        [Test]
        public void New_Valid_ShouldNotBeNull()
        {
            var tagName = new TagName(TestTagName);

            tagName.Should<string>().NotBeNull();
        }

        [Test]
        public void New_Valid_ShouldHaveExpectedProperties()
        {
            var tagName = new TagName(TestTagName);

            tagName.Tag.Should().Be(Base);
            tagName.Operand.Should().Be(Operand);
            tagName.Path.Should().Be(Path);
            tagName.Depth.Should().Be(3);
            tagName.Should<string>().HaveCount(4);
            tagName.IsEmpty.Should().BeFalse();
            tagName.IsValid.Should().BeTrue();
        }

        [Test]
        public void New_ModuleTagName_ShouldNotBeNull()
        {
            var tagName = new TagName("RackIO:1:I.Slot[2].Data.4");

            tagName.Should<string>().NotBeNull();
        }
        
        [Test]
        public void New_ModuleTagName_ShouldHaveExpectedRoot()
        {
            var tagName = new TagName("RackIO:1:I.Slot[2].Data.4");

            tagName.Tag.Should().Be("RackIO:1:I");
            tagName.Operand.Should().Be(".Slot[2].Data.4");
            tagName.Path.Should().Be("Slot[2].Data.4");
            tagName.Depth.Should().Be(4);
            tagName.Should<string>().HaveCount(5);
            tagName.IsEmpty.Should().BeFalse();
            tagName.IsValid.Should().BeTrue();
        }

        [Test]
        public void Empty_WhenCalled_ShouldBeEmptyValue()
        {
            var tagName = TagName.Empty;

            tagName.Should<string>().BeEquivalentTo(new TagName(""));
        }
        
        [Test]
        public void Empty_WhenCalled_IsEmptyShouldBeTrue()
        {
            var tagName = TagName.Empty;

            tagName.IsEmpty.Should().BeTrue();
        }
        
        [Test]
        public void Empty_WhenCalled_IsValidShouldBeFalse()
        {
            var tagName = TagName.Empty;

            tagName.IsValid.Should().BeFalse();
        }

        [Test]
        public void Members_WhenCalled_ShouldContainExpectedValues()
        {
            var tagName = new TagName(TestTagName);

            var members = tagName;

            members.Should<string>().BeEquivalentTo(Members);
        }

        [Test]
        public void Combine_TwoMemberNames_ShouldBeExpected()
        {
            var tagName = TagName.Combine(Base, Path);

            tagName.Should<TagName>().Be(TestTagName);
        }
        
        [Test]
        public void Combine_ManyNames_ShouldBeExpected()
        {
            var tagName = TagName.Combine("MyTag", "SomeArray", "[1]", "ArrayElement", "SomeMember", ".1");

            tagName.Should<TagName>().Be("MyTag.SomeArray[1].ArrayElement.SomeMember.1");
        }

        [Test]
        public void Combine_CollectionOfValidMembers_ShouldBeExpectedValue()
        {
            var tagName = TagName.Combine(Members);

            tagName.Should<TagName>().Be(TestTagName);
        }

        [Test]
        public void Combine_CollectionOfInvalidMembers_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var members = fixture.CreateMany<string>();

            FluentActions.Invoking(() => TagName.Combine(members)).Should().Throw<FormatException>();
        }

        [Test]
        public void Contains_NullName_ShouldThrowArgumentNullException()
        {
            var tagName = new TagName(TestTagName);

            FluentActions.Invoking(() => tagName.Contains(null!)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Contains_NameNotInTagName_ShouldBeFalse()
        {
            var tagName = new TagName(TestTagName);

            var result = tagName.Contains("SomeName");

            result.Should().BeFalse();
        }
        
        [Test]
        public void Contains_NameContainsTagName_ShouldBeTrue()
        {
            var tagName = new TagName(TestTagName);

            var result = tagName.Contains(Base);

            result.Should().BeTrue();
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
