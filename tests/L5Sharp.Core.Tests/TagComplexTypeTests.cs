using System;
using System.Linq;
using FluentAssertions;
using L5Sharp.Atomics;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Factories;
using L5Sharp.Predefined;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TagComplexTypeTests
    {
        [Test]
        public void Create_Timer_ShouldHaveExpectedValues()
        {
            var tag = Tag.Create<Timer>("Test");

            tag.Name.Should().Be("Test");
            tag.Description.Should().BeEmpty();
            tag.TagName.Should().Be("Test");
            tag.DataType.Should().BeOfType<Timer>();
            tag.Dimensions.Should().Be(Dimensions.Empty);
            tag.Radix.Should().Be(Radix.Null);
            tag.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            tag.TagType.Should().Be(TagType.Base);
            tag.Usage.Should().Be(TagUsage.Null);
            tag.Constant.Should().BeFalse();
            tag.IsValueMember.Should().BeFalse();
            tag.IsStructureMember.Should().BeTrue();
            tag.IsArrayMember.Should().BeFalse();
            tag.Parent.Should().BeNull();
            tag.Root.Should().BeSameAs(tag);
        }
        
        [Test]
        public void GetTagNames_Timer_ShouldHaveExpectedCount()
        {
            var tag = Tag.Create("Test", new Timer());

            var tagNames = tag.TagNames();

            tagNames.Should().HaveCount(5);
        }

        [Test]
        public void GetTagNames_Timer_ShouldHaveExpectedNames()
        {
            var tag = Tag.Create("Test", new Timer());

            var tagNames = tag.TagNames().ToList();

            tagNames.Should().Contain("Test.PRE");
            tagNames.Should().Contain("Test.ACC");
            tagNames.Should().Contain("Test.EN");
            tagNames.Should().Contain("Test.TT");
            tagNames.Should().Contain("Test.DN");
        }

        [Test]
        public void HasMember_Null_ShouldBeFalse()
        {
            var tag = Tag.Create<Timer>("Test");

            FluentActions.Invoking(() => tag.HasMember(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void HasMember_ValidExistingTagName_ShouldBeTrue()
        {
            var tag = Tag.Create<Timer>("Test");

            var result = tag.HasMember("Test.PRE");

            result.Should().BeTrue();
        }

        [Test]
        public void NameIndex_Null_ShouldThrowArgumentNullException()
        {
            var tag = Tag.Create<Timer>("Test");

            FluentActions.Invoking(() => tag.Member(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void NameIndex_EmptyString_ShouldThrowArgumentException()
        {
            var tag = Tag.Create<Timer>("Test");

            FluentActions.Invoking(() => tag.Member(string.Empty)).Should().Throw<ArgumentException>()
                .WithMessage("TagName can not be null or empty.");
        }

        [Test]
        public void NameIndex_NonExistingMember_ShouldThrowArgumentException()
        {
            var tag = Tag.Create<Timer>("Test");

            FluentActions.Invoking(() => tag.Member("Invalid")).Should().Throw<InvalidMemberPathException>()
                .WithMessage("The tag name 'Invalid' is not a valid member path for type 'TIMER'.");
        }

        [Test]
        public void NameIndex_ValidRelativeHasMember_ShouldNotBeNull()
        {
            var tag = Tag.Create<Timer>("Test");

            var member = tag.Member("PRE");

            member.Should().NotBeNull();
        }
        
        [Test]
        public void NameIndex_ValidAbsoluteHasMember_ShouldNotBeNull()
        {
            var tag = Tag.Create<Timer>("Test");

            var member = tag.Member("Test.PRE");

            member.Should().NotBeNull();
        }

        [Test]
        public void NameIndex_ValidNested_ShouldBeExpected()
        {
            var tag = Tag.Create<MyNestedType>("Test");

            var member = tag.Member("Tmr.PRE");

            member.Should().NotBeNull();
            member.Name.Should().Be("PRE");
            member.DataType.Should().BeOfType<Dint>();
        }

        [Test]
        public void NameIndex_ChainedCalls_ShouldBeExpected()
        {
            var tag = Tag.Create<MyNestedType>("Test");

            var member = tag.Member("Tmr").Member("PRE");

            member.Should().NotBeNull();
            member.Name.Should().Be("PRE");
            member.DataType.Should().BeOfType<Dint>();
        }

        [Test]
        public void GetMember_ValidMember_ShouldNotBeNull()
        {
            var tag = Tag.Create<Timer>("Test");

            var member = tag.Member(t => t.PRE);

            member.Should().NotBeNull();
        }

        [Test]
        public void GetMember_ChainedCalls_ShouldNotBeNull()
        {
            var tag = Tag.Create<MyNestedType>("Test");

            var member = tag
                .Member(t => t.Str)
                .Member(t => t.DATA)
                .Member(t => t[0]);

            member.Should().NotBeNull();
            member.Name.Should().Be("[0]");
            member.DataType.Should().BeOfType<Sint>();
        }

        [Test]
        public void GetMembers_WhenCalled_ShouldNotBeEmpty()
        {
            var tag = Tag.Create<Timer>("Test");

            var members = tag.Members();

            members.Should().HaveCount(5);
        }
    }
}