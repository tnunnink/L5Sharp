using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TagComplexTypeTests
    {
        [Test]
        public void GetMemberList_WhenCalledHasMembers_ShouldNotBeEmpty()
        {
            var tag = Tag.Create("Test", (IDataType)new Counter());

            var members = tag.GetMemberList();

            members.Should().NotBeEmpty();
        }

        [Test]
        public void GetDeepMemberList_WhenCalledHasMembers_ShouldNotBeEmpty()
        {
            var tag = Tag.Create("Test", (IDataType)new Counter());

            var members = tag.GetDeepMembersList();

            members.Should().NotBeEmpty();
        }

        [Test]
        public void GetDeepMemberList_NestedStructure_ShouldContainDotProperties()
        {
            var tag = Tag.Create("Test", (IDataType)new Message());

            var members = tag.GetDeepMembersList();

            members.Should().Contain(s => s.Contains('.'));
        }
        
        [Test]
        public void GetMember_NameOverloadValidNameHasMember_ShouldNotBeNull()
        {
            var tag = Tag.Create<Timer>("Test");

            var member = tag["PRE"];

            member.Should().NotBeNull();
        }

        [Test]
        public void GetMember_NameOverloadNonExistingMember_ShouldBeNull()
        {
            var tag = Tag.Create<Timer>("Test");

            var member = tag["Member"];

            member.Should().BeNull();
        }

        [Test]
        public void GetMembers_NameOverloadSameMember_DataShouldReferenceSameInstance()
        {
            var tag = Tag.Create<Timer>("Test");

            var first = tag["ACC"];
            var second = tag["ACC"];

            first.GetData().Should().BeSameAs(second.GetData());
        }

        [Test]
        public void GetMember_FuncOverloadValidNameHasMember_ShouldNotBeNull()
        {
            var tag = Tag.Create<Timer>("Test");

            var member = tag.GetMember(t => t.PRE);

            member.Should().NotBeNull();
        }

        [Test]
        public void GetMembers_FuncOverloadSameMember_DataShouldReferenceSameInstance()
        {
            var tag = Tag.Create<Timer>("Test");

            var first = tag.GetMember(d => d.ACC);
            var second = tag.GetMember(d => d.ACC);

            first.GetData().Should().BeSameAs(second.GetData());
        }


        [Test]
        public void SetMember_ValidNameHasMember_ShouldNotBeNull()
        {
            var tag = Tag.Create<Timer>("Test");

            tag.SetMember(t => t.ACC, new Dint(5001));

            tag.GetMember(t => t.ACC).GetData().As<Dint>().Should().Be(5001);
        }

        [Test]
        public void GetMember_GenericTagType_ShouldNotBeNull()
        {
            var tag = Tag.Create("Test", (IDataType)new Timer());

            var value = tag["PRE"].GetData().As<Dint>();

            value.Should().NotBeNull();
        }

        [Test]
        public void GetMember_Null_ShouldBeNull()
        {
            var tag = Tag.Create("Test", (IDataType)new Timer());

            var member = tag[(string)null];

            member.Should().BeNull();
        }

        [Test]
        public void GetMember_InvalidMemberHasMember_MemberShouldBeNull()
        {
            var tag = Tag.Create("Test", (IDataType)new Timer());

            var member = tag["Invalid"];

            member.Should().BeNull();
        }
        
        [Test]
        public void SetData_Extension_ShouldThrowNotConfigurableException()
        {
            var target = Tag.Create("TargetTag", new Timer(3000));
            var source = Tag.Create("SourceTag", new Timer(5000));

            target.SetData(source);
        }

        [Test]
        public void SetMember_Radix_ShouldUpdateRadix()
        {
            var tag = Tag.Create<Timer>("Test");

            tag.SetMember(t => t.ACC, Radix.Binary);

            tag.GetMember(t => t.ACC).Radix.Should().Be(Radix.Binary);
        }

        [Test]
        public void SetMember_Description_ShouldBeExpectedDescription()
        {
            var tag = Tag.Create<Counter>("Test");
            
            tag.SetMember(c => c.CD, "This member is the counter down bit");

            tag.GetMember(c => c.CD).Description.Should().Be("This member is the counter down bit");
        }
    }
}