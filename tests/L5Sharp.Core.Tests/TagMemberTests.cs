using System;
using System.Collections.Generic;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TagMemberTests
    {
        [Test]
        public void ComplexGetMember_ShouldHaveExpectedValues()
        {
            var tag = Tag.Create<Timer>("Test");

            var member = tag.GetMember(m => m.DN);

            member.Name.Should().Be("DN");
            member.Description.Should().BeEmpty();
            member.TagName.Should().Be("Test.DN");
            member.DataType.Should().BeOfType<Bool>();
            member.Dimensions.Should().Be(Dimensions.Empty);
            member.Radix.Should().Be(Radix.Decimal);
            member.ExternalAccess.Should().Be(ExternalAccess.None);
            //member.Parent.Should().BeSameAs(tag);
            member.Root.Should().BeSameAs(tag);
            member.Value.Should().Be(false);
        }

        [Test]
        public void NameIndexer_NonExisting_ShouldThrowKeyNotFoundException()
        {
            var tag = Tag.Create<MyNestedType>("Test");
            var timer = tag.GetMember(m => m.Tmr);

            FluentActions.Invoking(() => timer["Invalid"]).Should().Throw<KeyNotFoundException>();
        }
        
        [Test]
        public void NameIndexer_Null_ShouldThrowArgumentNullException()
        {
            var tag = Tag.Create<MyNestedType>("Test");
            var timer = tag.GetMember(m => m.Tmr);

            FluentActions.Invoking(() =>  timer[null!]).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void NameIndexer_ValidMember_ShouldNotBeNull()
        {
            var tag = Tag.Create<MyNestedType>("Test");
            var timer = tag.GetMember(m => m.Tmr);

            var member = timer["PRE"];
            
            member.Should().NotBeNull();
        }

        [Test]
        public void GetMember_MemberExists_ShouldNotBeNull()
        {
            var tag = Tag.Create<MyNestedType>("Test");

            var timer = tag.GetMember(m => m.Tmr);

            var member = timer.GetMember(m => m.PRE);

            member.Should().NotBeNull();
        }

        [Test]
        public void GetMember_MemberExists_ShouldHaveExpectedPropertyValues()
        {
            var tag = Tag.Create<MyNestedType>("Test");

            var timer = tag.GetMember(m => m.Tmr);

            var member = timer.GetMember(m => m.PRE);

            member.Name.Should().Be("PRE");
            member.TagName.Should().Be("Test.Tmr.PRE");
            member.DataType.Should().BeOfType<Dint>();
            member.Dimensions.Should().Be(Dimensions.Empty);
            member.Radix.Should().Be(Radix.Decimal);
            member.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            member.Parent.Should().BeSameAs(timer);
            member.Root.Should().BeSameAs(tag);
            member.Value.Should().Be(0);
        }
        
        [Test]
        public void SetValue_Null_ShouldThrowArgumentNullException()
        {
            var tag = Tag.Create<Timer>("Test");
            var member = tag["PRE"];
            
            FluentActions.Invoking(() => member?.SetValue(null!)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void SetValue_InvalidType_ShouldThrowArgumentException()
        {
            var tag = Tag.Create<Timer>("Test");
            var member = tag["PRE"];
            
            FluentActions.Invoking(() => member?.SetValue(new Bool(true))).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void SetValue_NonAtomic_ShouldThrowInvalidOperationException()
        {
            var tag = Tag.Create<MyNestedType>("Test");
            var member = tag["Tmr"];
            
            FluentActions.Invoking(() => member?.SetValue(new Bool(true))).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void SetValue_ValidValue_ValueShouldBeExpected()
        {
            var tag = Tag.Create<Timer>("Test");
            var member = tag["PRE"];
            
            member.SetValue(new Dint(500));

            member.Value.Should().Be(500);
        }
    }
}