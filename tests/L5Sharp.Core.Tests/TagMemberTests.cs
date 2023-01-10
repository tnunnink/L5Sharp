using System;
using System.Linq;
using FluentAssertions;
using L5Sharp.Creators;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TagMemberTests
    {
        [Test]
        public void ComplexMember_ShouldHaveExpectedValues()
        {
            var tag = Tag.Create<TIMER>("Test");

            var member = tag.Member(m => m.DN);

            member.Name.Should().Be("DN");
            member.Description.Should().BeEmpty();
            member.TagName.Should().Be("Test.DN");
            member.DataType.Should().BeOfType<BOOL>();
            member.Dimensions.Should().Be(Dimensions.Empty);
            member.Radix.Should().Be(Radix.Decimal);
            member.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            member.Root.Should().BeSameAs(tag);
            member.Parent.Should().NotBeNull();
            member.Value.Should().Be(false);
            member.MemberType.Should().Be(MemberType.ValueMember);
        }
        
        [Test]
        public void HasMember_Null_ShouldThrowArgumentNullException()
        {
            var tag = Tag.Create<MyNestedType>("Test");
            var timer = tag.Member(m => m.Tmr);

            FluentActions.Invoking(() => timer.Contains(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void HasMember_ValidMember_ShouldBeTrue()
        {
            var tag = Tag.Create<MyNestedType>("Test");
            var timer = tag.Member(m => m.Tmr);

            var result = timer.Contains("PRE");

            result.Should().BeTrue();
        }

        [Test]
        public void HasMember_InvalidMember_ShouldBeFalse()
        {
            var tag = Tag.Create<MyNestedType>("Test");
            var timer = tag.Member(m => m.Tmr);

            var result = timer.Contains("PRESET");

            result.Should().BeFalse();
        }

        [Test]
        public void Member_NonExisting_ShouldThrowInvalidMemberPathException()
        {
            var tag = Tag.Create<MyNestedType>("Test");
            var timer = tag.Member(m => m.Tmr);

            FluentActions.Invoking(() => timer.Member("Invalid")).Should().Throw<InvalidMemberPathException>();
        }
        
        [Test]
        public void Member_Null_ShouldThrowArgumentNullException()
        {
            var tag = Tag.Create<MyNestedType>("Test");
            var timer = tag.Member(m => m.Tmr);

            FluentActions.Invoking(() =>  timer.Member(null!)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Member_ValidMember_ShouldNotBeNull()
        {
            var tag = Tag.Create<MyNestedType>("Test");
            var timer = tag.Member(m => m.Tmr);

            var member = timer.Member("PRE");
            
            member.Should().NotBeNull();
        }

        [Test]
        public void Member_MemberExists_ShouldNotBeNull()
        {
            var tag = Tag.Create<MyNestedType>("Test");
            var timer = tag.Member(m => m.Tmr);

            var member = timer.Member(m => m.PRE);

            member.Should().NotBeNull();
        }

        [Test]
        public void Member_MemberExists_ShouldHaveExpectedPropertyValues()
        {
            var tag = Tag.Create<MyNestedType>("Test");
            var timer = tag.Member(m => m.Tmr);

            var member = timer.Member(m => m.PRE);

            member.Name.Should().Be("PRE");
            member.TagName.Should().Be("Test.Tmr.PRE");
            member.DataType.Should().BeOfType<DINT>();
            member.Dimensions.Should().Be(Dimensions.Empty);
            member.Radix.Should().Be(Radix.Decimal);
            member.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            member.Value.Should().Be(0);
        }

        [Test]
        public void Members_WhenCalled_ShouldHaveExpectedCount()
        {
            var tag = Tag.Create<MyNestedType>("Test");
            var timer = tag.Member(m => m.Tmr);

            var members = timer.Members();

            members.Should().HaveCount(5);
        }
        
        [Test]
        public void Members_WhenCalled_ShouldHaveExpectedMemberTagNames()
        {
            var tag = Tag.Create<MyNestedType>("Test");
            var timer = tag.Member(m => m.Tmr);

            var members = timer.Members();

            members.Should().Contain(m => m.TagName == "Test.Tmr.DN");
        }

        [Test]
        public void TagNames_WhenCalled_ShouldHaveExpectedCount()
        {
            var tag = Tag.Create<MyNestedType>("Test");
            var timer = tag.Member(m => m.Tmr);

            var members = timer.TagNames();

            members.Should().HaveCount(5);
        }
        
        [Test]
        public void TagNames_WhenCalled_ShouldHaveExpectedNames()
        {
            var tag = Tag.Create<MyNestedType>("Test");
            var timer = tag.Member(m => m.Tmr);

            var members = timer.TagNames().ToList();

            members.Should().Contain("Test.Tmr.DN");
            members.Should().Contain("Test.Tmr.TT");
            members.Should().Contain("Test.Tmr.EN");
            members.Should().Contain("Test.Tmr.PRE");
            members.Should().Contain("Test.Tmr.ACC");
        }

        [Test]
        public void SingleIndex_ValidIndex_ShouldNotBeNull()
        {
            var tag = Tag.Create<STRING>("Test");
            var data = tag.Member("DATA");

            var element = data[10];

            element.Should().NotBeNull();
        }
        
        [Test]
        public void SingleIndex_InvalidIndex_ShouldThrowArgumentException()
        {
            var tag = Tag.Create<STRING>("Test");
            var data = tag.Member("DATA");

            FluentActions.Invoking(() => data[-1]).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void SetValue_Null_ShouldThrowArgumentNullException()
        {
            var tag = Tag.Create<TIMER>("Test");
            var member = tag.Member("PRE");
            
            FluentActions.Invoking(() => member.SetValue(null!)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void SetValue_InvalidType_ShouldThrowArgumentException()
        {
            var tag = Tag.Create<TIMER>("Test");
            var member = tag.Member("PRE");
            
            FluentActions.Invoking(() => member.SetValue(new BOOL(true))).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void SetValue_NonAtomic_ShouldThrowInvalidOperationException()
        {
            var tag = Tag.Create<MyNestedType>("Test");
            var member = tag.Member("Tmr");
            
            FluentActions.Invoking(() => member.SetValue(new BOOL(true))).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void SetValue_ValidValue_ValueShouldBeExpected()
        {
            var tag = Tag.Create<TIMER>("Test");
            var member = tag.Member("PRE");
            
            member.SetValue(new DINT(500));

            member.Value.Should().Be(500);
        }
        
        [Test]
        public void SetData_Null_ShouldThrowArgumentNullException()
        {
            var tag = Tag.Create<MyNestedType>("Test");
            var member = tag.Member(m => m.Tmr);

            FluentActions.Invoking(() => member.SetData(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SetData_ValidDataType_ShouldUpdateMemberValues()
        {
            var tag = Tag.Create<MyNestedType>("Test");
            var member = tag.Member(m => m.Tmr);
            
            member.SetData(new TIMER(5000));

            member.Member(m => m.PRE).Value.Should().Be(5000);
        }
    }
}