using System.Collections.Generic;
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
        public void GetMemberNames_WhenCalledHasMembers_ShouldNotBeEmpty()
        {
            var tag = Tag.Create("Test", (IDataType)new Counter());

            var members = tag.GetMemberNames();

            members.Should().NotBeEmpty();
        }

        [Test]
        public void GetDeepMembersNames_WhenCalledHasMembers_ShouldNotBeEmpty()
        {
            var tag = Tag.Create("Test", (IDataType)new Counter());

            var members = tag.GetDeepMembersNames();

            members.Should().NotBeEmpty();
        }

        [Test]
        public void GetDeepMembersNames_NestedStructure_ShouldContainDotProperties()
        {
            var tag = Tag.Create("Test", (IDataType)new Message());

            var members = tag.GetDeepMembersNames();

            members.Should().Contain(s => s.Contains('.'));
        }

        [Test]
        public void StringIndex_ValidNameHasMember_ShouldNotBeNull()
        {
            var tag = Tag.Create<Timer>("Test");

            var member = tag["PRE"];

            member.Should().NotBeNull();
        }

        [Test]
        public void StringIndex_NonExistingMember_ShouldBeNull()
        {
            var tag = Tag.Create<Timer>("Test");

            var member = tag["Member"];

            member.Should().BeNull();
        }

        [Test]
        public void StringIndex_SameMember_DataShouldReferenceSameInstance()
        {
            var tag = Tag.Create<Timer>("Test");

            var first = tag["ACC"];
            var second = tag["ACC"];

            first.GetData().Should().BeSameAs(second.GetData());
        }

        [Test]
        public void ExpressionIndex_ValidNameHasMember_ShouldNotBeNull()
        {
            var tag = Tag.Create<Timer>("Test");

            var member = tag[t => t.PRE];

            member.Should().NotBeNull();
        }

        [Test]
        public void ExpressionIndex_SameMember_DataShouldReferenceSameInstance()
        {
            var tag = Tag.Create<Timer>("Test");

            var first = tag[t => t.PRE];
            var second = tag[t => t.PRE];

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
        public void SetMember_Radix_ShouldUpdateRadix()
        {
            var tag = Tag.Create<Timer>("Test");

            tag.SetMember(t => t.ACC, Radix.Binary);

            tag.GetMember(t => t.ACC).Radix.Should().Be(Radix.Binary);
        }

        /*[Test]
        public void SetMember_Description_ShouldBeExpectedDescription()
        {
            var tag = Tag.Create<Counter>("Test");

            tag.SetMember(c => c.CD, "This member is the counter down bit");

            tag.GetMember(c => c.CD).Description.Should().Be("This member is the counter down bit");
        }*/

        [Test]
        public void DotDownTesting()
        {
            var tag = Tag.Create<MyNestedType>("Test");

            var simple = tag.GetMember(t => t.SimpleBool);
            simple.Should().NotBeNull();

            var stringDataElement = tag
                .GetMember(t => t.String)
                .GetMember(t => t.DATA[4]);
            
            stringDataElement.Should().NotBeNull();

            var timerMember = tag.GetMember(t => t.Timer).GetMember(t => t.PRE);

            timerMember.Should().NotBeNull();
            
            var indexing = tag["Timer"]["PRE"];
            
            indexing.Should().NotBeNull();
        }
    }

    public class MyNestedType : DataType
    {
        public MyNestedType() :
            base(nameof(MyNestedType), "This is a test data type")
        {
            Members.Add(SimpleBool);
            Members.Add(String);
            Members.Add(Timer);
        }

        public IMember<Bool> SimpleBool = Member.Create<Bool>(nameof(SimpleBool));
        public IMember<String> String = Member.Create<String>(nameof(String));
        public IMember<Timer> Timer = Member.Create<Timer>(nameof(Timer));
    }
}