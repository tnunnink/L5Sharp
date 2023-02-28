using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Tests.Types.Custom;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;
using L5Sharp.Utilities;
using NUnit.Framework;

namespace L5Sharp.Tests.Components
{
    [TestFixture]
    public class TagTests
    {
        [Test]
        public void TagTesting()
        {
            var tag = new Tag();

            tag.Name.Should().BeEmpty();
            tag.Data.Should().Be(LogixType.Null);
            tag.DataType.Should().Be("NULL");
            tag.Dimensions.Should().Be(Dimensions.Empty);
            tag.Radix.Should().Be(Radix.Null);
            tag.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            tag.Description.Should().BeEmpty();
            tag.Comments.Should().BeEmpty();
            tag.Units.Should().BeEmpty();
            tag.Constant.Should().BeFalse();
            tag.Usage.Should().Be(TagUsage.Normal);
            tag.TagType.Should().Be(TagType.Base);
            tag.AliasFor.Should<TagName>().Be(TagName.Empty);
            tag.TagName.Should<TagName>().Be(TagName.Empty);
            tag.MemberType.Should().Be(MemberType.Unknown);
        }

        [Test]
        public void New_ValueTypeTag_ShouldHaveExpectedProperties()
        {
            var tag = new Tag
            {
                Name = "Test",
                Description = "This is a test",
                Data = new BOOL(),
                ExternalAccess = ExternalAccess.ReadOnly,
                TagType = TagType.Alias,
                Usage = TagUsage.Local,
                AliasFor = new TagName("SomeOtherTag"),
                Constant = true,
                Comments = new Dictionary<TagName, string>{{"SomeOperand", "A test commend"}},
                Units = new Dictionary<TagName, string>{{"SomeOperand", "A test unit"}}
            };

            tag.Name.Should().Be("Test");
            tag.Data.Should().BeOfType<BOOL>();
            tag.DataType.Should().Be("BOOL");
            tag.Dimensions.Should().Be(Dimensions.Empty);
            tag.Radix.Should().Be(Radix.Decimal);
            tag.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
            tag.Description.Should().Be("This is a test");
            tag.Comments.Should().HaveCount(1);
            tag.Units.Should().HaveCount(1);
            tag.Constant.Should().BeTrue();
            tag.Usage.Should().Be(TagUsage.Local);
            tag.TagType.Should().Be(TagType.Alias);
            tag.AliasFor.Should<TagName>().Be("SomeOtherTag");
            tag.TagName.Should<TagName>().Be("Test");
            tag.MemberType.Should().Be(MemberType.ValueMember);
        }

        [Test]
        public void Member_ValidMember_ShouldBeExpectedTagMember()
        {
            var tag = new Tag
            {
                Name = "Test",
                Data = new TIMER()
            };

            var member = tag.Member("DN");

            member.Should().NotBeNull();
            member?.TagName.Should<TagName>().Be("Test.DN");
            member?.Data.Should().BeOfType<BOOL>();
            member?.DataType.Should().Be("BOOL");
            member?.Dimensions.Should().Be(Dimensions.Empty);
            member?.Radix.Should().Be(Radix.Decimal);
            member?.Comment.Should().BeEmpty();
            member?.Unit.Should().BeEmpty();
        }

        [Test]
        public void Member_NestedType_ShouldBeExpected()
        {
            var tag = new Tag
            {
                Name = "Test",
                Data = new MyNestedType()
            };

            var m1 = tag.Member("Simple.M1");

            m1.Should().NotBeNull();
            m1?.TagName.Should<TagName>().Be("Test.Simple.M1");
        }
        
        [Test]
        public void Member_ChainedCalls_ShouldBeExpected()
        {
            var tag = new Tag
            {
                Name = "Test",
                Data = new MyNestedType()
            };

            var m1 = tag.Member("Simple")?.Member("M1");

            m1.Should().NotBeNull();
            m1?.TagName.Should<TagName>().Be("Test.Simple.M1");
        }

        [Test]
        public void Member_NonExisting_ShouldReturnNull()
        {
            var tag = new Tag()
            {
                Name = "Test",
                Data = new TIMER()
            };

            var member = tag.Member("Fake");

            member.Should().BeNull();
        }

        [Test]
        public void Members_SimpleStructure_ShouldHaveExpectedCount()
        {
            var tag = new Tag
            {
                Name = "Test",
                Data = new TIMER()
            };

            var members = tag.Members();

            members.Should().HaveCount(5);
        }
        
        [Test]
        public void Members_SimpleStructure_ShouldHaveContainExpectedTagNames()
        {
            var tag = new Tag
            {
                Name = "Test",
                Data = new TIMER()
            };

            var members = tag.Members().ToList();

            members.Should().Contain(t => t.TagName == "Test.DN");
            members.Should().Contain(t => t.TagName == "Test.EN");
            members.Should().Contain(t => t.TagName == "Test.TT");
            members.Should().Contain(t => t.TagName == "Test.ACC");
            members.Should().Contain(t => t.TagName == "Test.PRE");
        }

        [Test]
        public void Members_NestedStructure_ShouldHaveExpectedCount()
        {
            var tag = new Tag
            {
                Name = "Test",
                Data = new MyNestedType()
            };

            var members = tag.Members().ToList();

            members.Should().HaveCount(35);
        }
        
        [Test]
        public void Members_NestedStructure_AllDataTypesShouldNotBeNull()
        {
            var tag = new Tag
            {
                Name = "Test",
                Data = new MyNestedType()
            };

            var data = tag.Members().ToList().Select(m => m.Data).ToList();

            data.Should().AllBeAssignableTo<ILogixType>();
        }

        [Test]
        public void Members_NestedStructure_ShouldHaveExpectedTagNames()
        {
            var tag = new Tag
            {
                Name = "Test",
                Data = new MyNestedType()
            };

            var members = tag.Members().ToList();

            members.Should().Contain(t => t.TagName == "Test.Simple.M1");
            members.Should().Contain(t => t.TagName == "Test.Simple.M2");
            members.Should().Contain(t => t.TagName == "Test.Simple.M3");
            members.Should().Contain(t => t.TagName == "Test.Simple.M4");
            members.Should().Contain(t => t.TagName == "Test.Simple.M5");
            members.Should().Contain(t => t.TagName == "Test.Simple.M6");
            members.Should().Contain(t => t.TagName == "Test.Tmr.DN");
            members.Should().Contain(t => t.TagName == "Test.Tmr.EN");
            members.Should().Contain(t => t.TagName == "Test.Tmr.TT");
            members.Should().Contain(t => t.TagName == "Test.Tmr.ACC");
            members.Should().Contain(t => t.TagName == "Test.Tmr.PRE");
            members.Should().Contain(t => t.TagName == "Test.Str.LEN");
            members.Should().Contain(t => t.TagName == "Test.Str.DATA");
            members.Should().Contain(t => t.TagName == "Test.Str.DATA[0]");
        }

        [Test]
        public void Members_TagNameContainsTmr_ShouldReturnExpectedCount()
        {
            var tag = new Tag
            {
                Name = "Test",
                Data = new MyNestedType()
            };

            var members = tag.Members(t => t.Contains("Tmr"));

            members.Should().HaveCount(6);
        }

        [Test]
        public void Members_TagNameWithMoreThanThreeMembers_ShouldReturnExpectedCount()
        {
            var tag = new Tag
            {
                Name = "Test",
                Data = new MyNestedType()
            };

            var members = tag.Members(t => t.Count() > 3);

            members.Should().HaveCount(1);
        }
        
        [Test]
        public void Members_TagNameEqualToMemberWithMemberNameComparer_ShouldReturnExpectedCount()
        {
            var tag = new Tag
            {
                Name = "Test",
                Data = new MyNestedType()
            };

            var members = tag.Members(t => TagName.Equals(t, "M1", TagNameComparer.MemberName));

            members.Should().HaveCount(1);
        }
        
        [Test]
        public void Members_TagMemberWithBOOLDataTypeName_ShouldReturnExpectedCount()
        {
            var tag = new Tag
            {
                Name = "Test",
                Data = new MyNestedType()
            };

            var members = tag.Members(t => t.DataType == "BOOL").ToList();

            members.Should().HaveCount(50);
        }

        [Test]
        public void MembersOf_NestedStructure_ShouldHaveExpectedCount()
        {
            var tag = new Tag
            {
                Name = "Test",
                Data = new MyNestedType()
            };

            var members = tag.MembersOf("Tmr").ToList();

            members.Should().HaveCount(5);
        }
        
        [Test]
        public void MembersOf_NonExistingMember_ShouldBeEmpty()
        {
            var tag = new Tag
            {
                Name = "Test",
                Data = new MyNestedType()
            };

            var members = tag.MembersOf("Fake").ToList();

            members.Should().BeEmpty();
        }


    }
}