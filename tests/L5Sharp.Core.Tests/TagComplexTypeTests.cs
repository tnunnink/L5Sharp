using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TagComplexTypeTests
    {
        [Test]
        public void GetMemberNames_HasMembers_ShouldHaveExpectedCount()
        {
            var tag = Tag.Create("Test", new Timer());

            var members = tag.GetMemberNames();

            members.Should().HaveCount(5);
        }

        [Test]
        public void GetDeepMembersNames_HasMembers_ShouldNotBeEmpty()
        {
            var tag = Tag.Create("Test", new Timer());

            var members = tag.GetDeepMembersNames();

            members.Should().HaveCount(5);
        }

        [Test]
        public void GetDeepMembersNames_NestedStructure_ShouldContainDot()
        {
            var tag = Tag.Create("Test", new Message());

            var members = tag.GetDeepMembersNames();

            members.Should().Contain(s => s.Contains('.'));
        }

        [Test]
        public void NameIndex_Null_ShouldBeNull()
        {
            var tag = Tag.Create<Timer>("Test");

            var member = tag[null!];

            member.Should().BeNull();
        }
        
        [Test]
        public void NameIndex_EmptyString_ShouldBeNull()
        {
            var tag = Tag.Create<Timer>("Test");

            var member = tag[string.Empty];

            member.Should().BeNull();
        }

        [Test]
        public void NameIndex_NonExistingMember_ShouldBeNull()
        {
            var tag = Tag.Create<Timer>("Test");

            var member = tag["Invalid"];

            member.Should().BeNull();
        }
        
        [Test]
        public void StringIndex_ValidNameHasMember_ShouldNotBeNull()
        {
            var tag = Tag.Create<Timer>("Test");

            var member = tag["PRE"];

            member.Should().NotBeNull();
        }

        [Test]
        public void NameIndex_ValidNested_ShouldBeExpected()
        {
            var tag = Tag.Create<MyNestedType>("Test");
            
            var member = tag["Tmr.PRE"];

            member.Should().NotBeNull();
            member?.Name.Should().Be("PRE");
            member?.DataType.Should().Be(nameof(Dint).ToUpper());
        }
        
        [Test]
        public void NameIndex_ChainedCalls_ShouldBeExpected()
        {
            var tag = Tag.Create<MyNestedType>("Test");
            
            var member = tag["Tmr"]?["PRE"];

            member.Should().NotBeNull();
            member?.Name.Should().Be("PRE");
            member?.DataType.Should().Be(nameof(Dint).ToUpper());
        }

        [Test]
        public void GetMember_ValidMember_ShouldNotBeNull()
        {
            var tag = Tag.Create<Timer>("Test");

            var member = tag.GetMember(t => t.PRE);

            member.Should().NotBeNull();
        }
        
        [Test]
        public void GetMember_ChainedCalls_ShouldNotBeNull()
        {
            var tag = Tag.Create<MyNestedType>("Test");

            var member = tag.GetMember(t => t.Str).GetMember(t => t.DATA.DataType[4]);

            member.Should().NotBeNull();
            member.Name.Should().Be("[0]");
            member.DataType.Should().Be(nameof(Sint).ToUpper());
        }
    }
}