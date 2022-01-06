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
        [Test]
        public void New_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new TagName(null!)).Should().Throw<ArgumentNullException>();
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
            var tagName = new TagName("MyTag.Member01.Indication_Active_Tag");

            tagName.Should().NotBeNull();
        }

        [Test]
        public void Members_WhenCalled_ShouldHaveExpectedCount()
        {
            var tagName = new TagName("MyTag.Member01.Indication_Active_Tag");

            tagName.Members.Should().HaveCount(3);
        }

        [Test]
        public void Base_WhenCalled_ShouldBeExpected()
        {
            var tagName = new TagName("MyTag.Member01.Indication_Active_Tag");

            var result = tagName.BaseName;

            result.Should().Be("MyTag");
        }

        [Test]
        public void Combine_TwoMemberNames_ShouldBeExpected()
        {
            var baseName = "MyTag";
            var memberName = "Member_01";

            var tagName = TagName.Combine(baseName, memberName);

            tagName.Should().Be($"{baseName}.{memberName}");
        }

        [Test]
        public void Combine_CollectionOfValidMembers_ShouldBeExpectedValue()
        {
            var memberNames = new List<string>
            {
                "MyTag",
                "Some_Member",
                "[3]",
                "Indication"
            };

            var tagName = TagName.Combine(memberNames);

            tagName.Should().Be("MyTag.Some_Member[3].Indication");
        }
    }
}