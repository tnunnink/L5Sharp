using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Tests.Types.Custom;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Tests.Components
{
    [TestFixture]
    public class TagTests
    {
        #region BasicTests

        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var tag = new Tag();

            tag.Should().NotBeNull();
        }

        [Test]
        public void New_Default_ShouldHaveDefaultValues()
        {
            var tag = new Tag();

            tag.Name.Should().BeEmpty();
            tag.Value.Should().Be(LogixData.Null);
            tag.DataType.Should().Be("NULL");
            tag.Dimensions.Should().Be(Dimensions.Empty);
            tag.Radix.Should().Be(Radix.Null);
            tag.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            tag.Description.Should().BeNull();
            tag.Constant.Should().BeFalse();
            tag.TagType.Should().Be(TagType.Base);
            tag.TagName.Should().Be(TagName.Empty);
            tag.Usage.Should().BeNull();
            tag.AliasFor.Should().BeNull();
            tag.Comment.Should().BeNull();
            tag.Unit.Should().BeNull();
            tag.Root.Should().BeSameAs(tag);
            tag.Parent.Should().BeNull();
        }

        [Test]
        public void New_Overloaded_ShouldHaveExpectedProperties()
        {
            var tag = new Tag
            {
                Name = "Test",
                Description = "This is a test",
                Value = new BOOL(true),
                ExternalAccess = ExternalAccess.ReadOnly,
                TagType = TagType.Alias,
                Usage = TagUsage.Local,
                AliasFor = new TagName("SomeOtherTag"),
                Constant = true
            };

            tag.Name.Should().Be("Test");
            tag.Value.Should().BeOfType<BOOL>();
            tag.Value.Should().Be(true);
            tag.DataType.Should().Be("BOOL");
            tag.Dimensions.Should().Be(Dimensions.Empty);
            tag.Radix.Should().Be(Radix.Decimal);
            tag.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
            tag.Description.Should().Be("This is a test");
            tag.Constant.Should().BeTrue();
            tag.Usage.Should().Be(TagUsage.Local);
            tag.TagType.Should().Be(TagType.Alias);
            tag.AliasFor.Should().Be("SomeOtherTag");
            tag.TagName.Should().Be("Test");
            tag.Comment.Should().Be("This is a test");
            tag.Unit.Should().BeNull();
            tag.Root.Should().BeSameAs(tag);
            tag.Parent.Should().BeNull();
        }

        [Test]
        public void New_Atomic_ShouldHaveExpectedValue()
        {
            var tag = new Tag { Name = "Test", Value = new BOOL() };

            tag.Value.Should().BeOfType<BOOL>();
            tag.Value.Should().Be(false);
        }

        [Test]
        public void New_Structure_ShouldHaveExpectedValue()
        {
            var tag = new Tag { Name = "Test", Value = new TIMER() };

            tag.Value.Should().BeOfType<TIMER>();
            tag.Value.As<TIMER>().PRE.Should().Be(0);
            tag.Value.As<TIMER>().ACC.Should().Be(0);
            tag.Value.As<TIMER>().DN.Should().Be(0);
            tag.Value.As<TIMER>().TT.Should().Be(0);
            tag.Value.As<TIMER>().EN.Should().Be(0);
        }

        [Test]
        public void New_Array_ShouldHaveExpectedValue()
        {
            var tag = new Tag { Name = "Test", Value = new DINT[] { 0, 1, 2, 3, 4 } };

            tag.Value.Should().BeOfType<ArrayType>();
            tag.Dimensions.Should().Be(new Dimensions(5));
            tag.Value.As<ArrayType>()[0].Should().Be(0);
            tag.Value.As<ArrayType>()[1].Should().Be(1);
            tag.Value.As<ArrayType>()[2].Should().Be(2);
            tag.Value.As<ArrayType>()[3].Should().Be(3);
            tag.Value.As<ArrayType>()[4].Should().Be(4);
        }

        [Test]
        public void Root_FromDescendantMember_ShouldNotBeNullAndSameAsTag()
        {
            var tag = new Tag { Name = "Test", Value = new TIMER() };

            var root = tag["DN"].Root;

            root.Should().NotBeNull();
            root.Should().BeSameAs(tag);
        }

        [Test]
        public void Root_FromNestedDescendantMember_ShouldNotBeNullAndSameAsTag()
        {
            var tag = new Tag { Name = "Test", Value = new MyNestedType() };

            var root = tag["Simple.M1"].Root;

            root.Should().NotBeNull();
            root.Should().BeSameAs(tag);
        }

        [Test]
        public void Parent_FromDescendantMember_ShouldNotBeNullAndSameAsTag()
        {
            var tag = new Tag { Name = "Test", Value = new TIMER() };

            var parent = tag["DN"].Parent;

            parent.Should().NotBeNull();
            parent.Should().BeSameAs(tag);
        }

        [Test]
        public void Parent_FromNestedDescendantMember_ShouldBeExpected()
        {
            var tag = new Tag { Name = "Test", Value = new MyNestedType() };

            var parent = tag["Simple.M1"].Parent;

            parent.Should().NotBeNull();
            parent?.Value.Should().BeOfType<MySimpleType>();
            parent?.TagName.Should().Be("Test.Simple");
        }

        [Test]
        public void Names_WhenCalled_ContainsExpectedNames()
        {
            var tag = new Tag { Name = "Test", Value = new TIMER() };

            var names = tag.Names().ToList();

            names.Should().Contain("Test");
            names.Should().Contain("Test.PRE");
            names.Should().Contain("Test.PRE.0");
            names.Should().Contain("Test.PRE.31");
            names.Should().Contain("Test.ACC");
            names.Should().Contain("Test.ACC.0");
            names.Should().Contain("Test.ACC.31");
            names.Should().Contain("Test.DN");
            names.Should().Contain("Test.TT");
            names.Should().Contain("Test.EN");
        }

        #endregion

        #region ValueTesting

        [Test]
        public void GetValue_Default_ShouldNotBeNullType()
        {
            var tag = new Tag { Name = "Test" };

            var value = tag.Value;

            value.Should().NotBeNull();
            value.Should().BeOfType<NullType>();
        }

        [Test]
        public void GetValue_Atomic_ShouldNotBeExpected()
        {
            var tag = new Tag { Name = "Test", Value = new DINT() };

            var value = tag.Value;

            value.Should().NotBeNull();
            value.Should().BeOfType<DINT>();
            value.Should().Be(0);
        }

        [Test]
        public void SetValue_AtomicSameType_ShouldHaveExpectedValeAndType()
        {
            var tag = new Tag { Name = "Test", Value = new DINT() };

            tag.Value = 43;

            tag.Value.Should().BeOfType<DINT>();
            tag.Value.As<DINT>().Should().Be(43);
        }

        [Test]
        public void SetValue_SetAtomicDifferentType_ShouldHaveExpectedValeAndType()
        {
            var tag = new Tag { Name = "Test", Value = new DINT() };

            tag.Value = new INT(43);

            tag.Value.Should().BeOfType<DINT>();
            tag.Value.As<DINT>().Should().Be(43);
        }

        [Test]
        public void SetValue_SetAtomic_ShouldRaiseDataChanged()
        {
            var tag = new Tag { Name = "Test", Value = new DINT() };
            using var monitor = tag.Value.Monitor();

            tag.Value = 43;

            monitor.Should().Raise("DataChanged");
        }

        [Test]
        public void SetValue_StructureType_ShouldHaveExpectedValues()
        {
            var tag = new Tag { Name = "Test", Value = new TIMER() };

            tag.Value = new TIMER
            {
                PRE = 5000,
                ACC = 1234,
                DN = 1,
                TT = 1,
                EN = 1,
            };

            tag.Value.Should().BeOfType<TIMER>();
            tag.Value.As<TIMER>().PRE.Should().Be(5000);
            tag.Value.As<TIMER>().ACC.Should().Be(1234);
            tag.Value.As<TIMER>().DN.Should().Be(1);
            tag.Value.As<TIMER>().TT.Should().Be(1);
            tag.Value.As<TIMER>().EN.Should().Be(1);
        }

        [Test]
        public void SetValue_StructureTypeAsComplexType_ShouldHaveExpectedValues()
        {
            var tag = new Tag { Name = "Test", Value = new TIMER() };

            //Name does not matter just the members
            tag.Value = new ComplexType("Test", new List<Member>
            {
                new("PRE", 5000),
                new("ACC", 1234),
                new("DN", 1),
                new("TT", 1),
                new("EN", 1),
            });

            tag.Value.Should().BeOfType<TIMER>();
            tag.Value.As<TIMER>().PRE.Should().Be(5000);
            tag.Value.As<TIMER>().ACC.Should().Be(1234);
            tag.Value.As<TIMER>().DN.Should().Be(1);
            tag.Value.As<TIMER>().TT.Should().Be(1);
            tag.Value.As<TIMER>().EN.Should().Be(1);
        }

        [Test]
        public void SetValue_AtomicArrayType_ShouldHaveExpectedValues()
        {
            var tag = new Tag { Name = "Test", Value = new DINT[] { 1, 2, 3, 4 } };

            tag.Value = new DINT[] { 4, 3, 2, 1 };

            tag.Value.Should().BeOfType<ArrayType>();
            tag.Value.As<ArrayType>()[0].Should().Be(4);
            tag.Value.As<ArrayType>()[1].Should().Be(3);
            tag.Value.As<ArrayType>()[2].Should().Be(2);
            tag.Value.As<ArrayType>()[3].Should().Be(1);
        }

        [Test]
        public void SetValue_StructureArrayType_ShouldHaveExpectedValues()
        {
            var tag = new Tag { Name = "Test", Value = new TIMER[] { new(), new(), new(), new() } };

            //array length does not matter. indices will be join on what is available.
            tag.Value = new TIMER[] { new() { PRE = 100 }, new() { PRE = 200 }, new() { PRE = 300 } };

            tag.Value.Should().BeOfType<ArrayType>();
            tag.Value.As<ArrayType>()[0].As<TIMER>().PRE.Should().Be(100);
            tag.Value.As<ArrayType>()[1].As<TIMER>().PRE.Should().Be(200);
            tag.Value.As<ArrayType>()[2].As<TIMER>().PRE.Should().Be(300);
            tag.Value.As<ArrayType>()[3].As<TIMER>().PRE.Should().Be(0);
        }

        [Test]
        public void SetValue_InvalidType_ShouldThrowArgumentException()
        {
            var tag = new Tag { Name = "Test", Value = new TIMER() };

            FluentActions.Invoking(() => tag.Value = new REAL(43)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void SetValue_StaticMemberOfStructureType_ShouldRaiseDataChanged()
        {
            var tag = new Tag { Name = "Test", Value = new TIMER() };
            using var monitor = tag.Value.Monitor();

            tag.Value.As<TIMER>().PRE = 5000;

            monitor.Should().Raise("DataChanged");
        }

        [Test]
        public Task SetValue_StaticMemberOfStructureType_ShouldBeVerifiedXml()
        {
            var tag = new Tag { Name = "Test", Value = new TIMER() };

            tag.Value.As<TIMER>().PRE = 5000;

            var xml = tag.Serialize().ToString();
            return Verify(xml);
        }

        [Test]
        public void SetValue_StaticMemberOfNestedType_ShouldRaiseDataChanged()
        {
            var tag = new Tag { Name = "Test", Value = new MyNestedType() };
            using var monitor = tag.Value.Monitor();

            tag.Value.As<MyNestedType>().Simple.M4 = 5000;

            monitor.Should().Raise("DataChanged");
        }

        [Test]
        public Task SetValue_StaticMemberOfNestedType_ShouldBeVerifiedXml()
        {
            var tag = new Tag { Name = "Test", Value = new MyNestedType() };

            tag.Value.As<MyNestedType>().Simple.M4 = 5000;

            var xml = tag.Serialize().ToString();
            return Verify(xml);
        }

        [Test]
        public void Set_AsDictionary()
        {
            var tag = new Tag { Name = "Test", Value = new TIMER() };

            tag.Value = new Dictionary<string, LogixType>
            {
                { "PRE", 5000 },
                { "ACC", 1234 },
                { "DN", true },
            };
        }

        #endregion

        #region MembersTesting

        [Test]
        public void Member_ArrayIndexValid_ShouldBeExpected()
        {
            var tag = new Tag { Name = "Test", Value = new DINT[] { 1, 2, 3, 4 } };

            var member = tag[2];

            member.Should().NotBeNull();
            member.TagName.Should().Be("Test[2]");
            member.Value.Should().BeOfType<DINT>();
            member.Value.Should().Be(3);
            member.DataType.Should().Be("DINT");
            member.Dimensions.Should().Be(Dimensions.Empty);
            member.Radix.Should().Be(Radix.Decimal);
            member.Description.Should().BeNull();
        }

        [Test]
        public void Member_ArrayIndexInvalid_ShouldThrowArgumentException()
        {
            var tag = new Tag { Name = "Test", Value = new DINT[] { 1, 2, 3, 4 } };

            FluentActions.Invoking(() => tag[4]).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Member_NameIndexValid_ShouldBeExpected()
        {
            var tag = new Tag { Name = "Test", Value = new TIMER() };

            var member = tag["DN"];

            member.Should().NotBeNull();
            member.TagName.Should().Be("Test.DN");
            member.Value.Should().BeOfType<BOOL>();
            member.DataType.Should().Be("BOOL");
            member.Dimensions.Should().Be(Dimensions.Empty);
            member.Radix.Should().Be(Radix.Decimal);
            member.Description.Should().BeNull();
        }

        [Test]
        public void Member_NameIndexInvalid_ShouldThrowArgumentException()
        {
            var tag = new Tag { Name = "Test", Value = new TIMER() };

            FluentActions.Invoking(() => tag["Fake"]).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Member_ValidMember_ShouldBeExpectedTagMember()
        {
            var tag = new Tag { Name = "Test", Value = new TIMER() };

            var member = tag.Member("DN");

            member?.Should().NotBeNull();
            member?.TagName.Should().Be("Test.DN");
            member?.Value.Should().BeOfType<BOOL>();
            member?.DataType.Should().Be("BOOL");
            member?.Dimensions.Should().Be(Dimensions.Empty);
            member?.Radix.Should().Be(Radix.Decimal);
            member?.Description.Should().BeNull();
        }

        [Test]
        public void Member_NestedType_ShouldBeExpected()
        {
            var tag = new Tag { Name = "Test", Value = new MyNestedType() };

            var member = tag.Member("Simple.M1");

            member.Should().NotBeNull();
            member?.TagName.Should().Be("Test.Simple.M1");
        }

        [Test]
        public void Member_ChainedCalls_ShouldBeExpected()
        {
            var tag = new Tag { Name = "Test", Value = new MyNestedType() };

            var nested = tag.Member("Simple")?.Member("M1");

            nested.Should().NotBeNull();
            nested?.TagName.Should().Be("Test.Simple.M1");
        }

        [Test]
        public void Member_NonExisting_ShouldReturnNull()
        {
            var tag = new Tag { Name = "Test", Value = new TIMER() };

            var member = tag.Member("Fake");

            member.Should().BeNull();
        }

        [Test]
        public void Members_SimpleStructure_ShouldHaveExpectedCount()
        {
            var tag = new Tag { Name = "Test", Value = new TIMER() };

            var members = tag.Members().ToList();

            members.Should().HaveCountGreaterThan(0);
        }

        [Test]
        public void Members_SimpleStructure_ShouldHaveContainExpectedTagNames()
        {
            var tag = new Tag { Name = "Test", Value = new TIMER() };

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
            var tag = new Tag { Name = "Test", Value = new MyNestedType() };

            var members = tag.Members().ToList();

            members.Should().NotBeEmpty();
        }

        [Test]
        public void Members_NestedStructure_AllDataTypesShouldNotBeNull()
        {
            var tag = new Tag { Name = "Test", Value = new MyNestedType() };

            var result = tag.Members().ToList().Select(m => m.Value).ToList();

            result.Should().AllBeAssignableTo<LogixType>();
        }

        [Test]
        public void Members_NestedStructure_ShouldHaveExpectedTagNames()
        {
            var tag = new Tag
            {
                Name = "Test",
                Value = new MyNestedType()
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
                Value = new MyNestedType()
            };

            var members = tag.Members(t => t.Contains("Tmr"));

            members.Should().HaveCountGreaterThan(0);
        }

        [Test]
        public void Members_TagNameWithMoreThanThreeMembers_ShouldReturnExpectedCount()
        {
            var tag = new Tag
            {
                Name = "Test",
                Value = new MyNestedType()
            };

            var members = tag.Members(t => t.Members.Count() > 4);

            members.Should().NotBeEmpty();
        }

        [Test]
        public void Members_TagNameEqualToMemberWithMemberNameComparer_ShouldReturnExpectedCount()
        {
            var tag = new Tag
            {
                Name = "Test",
                Value = new MyNestedType()
            };

            var members = tag.Members(t => TagName.Equals(t, "M1", TagNameComparer.Member));

            members.Should().HaveCount(1);
        }

        [Test]
        public void Members_TagMemberWithBOOLDataTypeName_ShouldReturnExpectedCount()
        {
            var tag = new Tag
            {
                Name = "Test",
                Value = new MyNestedType()
            };

            var members = tag.Members(t => t.DataType == "BOOL").ToList();

            members.Should().NotBeEmpty();
        }

        [Test]
        public void MembersOf_NestedStructure_ShouldHaveExpectedCount()
        {
            var tag = new Tag
            {
                Name = "Test",
                Value = new MyNestedType()
            };

            var members = tag.MembersOf("Tmr").ToList();

            members.Should().HaveCountGreaterThan(0);
        }

        [Test]
        public void MembersOf_NonExistingMember_ShouldBeEmpty()
        {
            var tag = new Tag
            {
                Name = "Test",
                Value = new MyNestedType()
            };

            var members = tag.MembersOf("Fake").ToList();

            members.Should().BeEmpty();
        }

        #endregion


        [Test]
        public void Add_StructureType_ShouldThrowInvalidOperationException()
        {
            var tag = new Tag { Name = "Test", Value = new TIMER() };

            FluentActions.Invoking(() => tag.Add(new Member("Test", 123))).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Add_ComplexType_ShouldUpdateStructure()
        {
            var tag = new Tag { Name = "Test", Value = new MySimpleType() };

            tag.Add(new Member("Test", 123));

            tag["Test"].Should().NotBeNull();
            tag["Test"].Value.Should().BeOfType<DINT>();
            tag["Test"].Value.Should().Be(123);
        }

        [Test]
        public Task Add_ComplexType_ShouldBeVerified()
        {
            var tag = new Tag { Name = "Test", Value = new MySimpleType() };

            tag.Add(new Member("Test", 123));

            var xml = tag.Serialize().ToString();
            return Verify(xml);
        }
    }
}