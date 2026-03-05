using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Tests.Core.Data.Custom;

// ReSharper disable UseObjectOrCollectionInitializer

namespace L5Sharp.Tests.Core.Components;

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
        tag.Description.Should().BeNull();
        tag.DataType.Should().Be("NULL");
        tag.Dimensions.Should().Be(Dimensions.Empty);
        tag.Radix.Should().Be(Radix.Null);
        tag.ExternalAccess.Should().Be(Access.ReadWrite);
        tag.Value.Should().Be(LogixType.Null);
        tag.Constant.Should().BeFalse();
        tag.TagType.Should().Be(TagType.Base);
        tag.Usage.Should().BeNull();
        tag.AliasFor.Should().BeNull();
        tag.Alias.Should().BeNull();
        tag.Unit.Should().BeNull();
        tag.Base.Should().BeSameAs(tag);
        tag.Parent.Should().BeNull();
        tag.TagName.Should().Be(TagName.Empty);
        tag.Comments.Should().BeNull();
        tag.Units.Should().BeNull();
    }

    [Test]
    public void New_Overloaded_ShouldHaveExpectedProperties()
    {
        var tag = new Tag
        {
            Name = "Test",
            Description = "This is a test",
            Value = new BOOL(true),
            ExternalAccess = Access.ReadOnly,
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
        tag.ExternalAccess.Should().Be(Access.ReadOnly);
        tag.Description.Should().Be("This is a test");
        tag.Constant.Should().BeTrue();
        tag.Usage.Should().Be(TagUsage.Local);
        tag.TagType.Should().Be(TagType.Alias);
        tag.AliasFor.Should().Be("SomeOtherTag");
        tag.TagName.Should().Be("Test");
        tag.Unit.Should().BeNull();
        tag.Base.Should().BeSameAs(tag);
        tag.Parent.Should().BeNull();
    }

    [Test]
    public void New_NameAndValueOverload_ShouldHaveExpectedValues()
    {
        var tag = new Tag("Test", 123);

        tag.Name.Should().Be("Test");
        tag.Value.Should().BeOfType<DINT>();
        tag.Value.Should().Be(123);
    }

    [Test]
    public void New_Atomic_ShouldHaveExpectedValue()
    {
        var tag = new Tag { Name = "Test", Value = true };

        tag.Value.Should().BeOfType<BOOL>();
        tag.Value.Should().Be(true);
    }

    [Test]
    public void New_Structure_ShouldHaveExpectedValue()
    {
        var tag = new Tag { Name = "Test", Value = new TIMER() };

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

        tag.Dimensions.Should().Be(new Dimensions(5));
        tag.Value.As<ArrayData>()[0].Should().Be(0);
        tag.Value.As<ArrayData>()[1].Should().Be(1);
        tag.Value.As<ArrayData>()[2].Should().Be(2);
        tag.Value.As<ArrayData>()[3].Should().Be(3);
        tag.Value.As<ArrayData>()[4].Should().Be(4);
    }

    [Test]
    public void Root_FromDescendantMember_ShouldNotBeNullAndSameAsTag()
    {
        var tag = new Tag { Name = "Test", Value = new TIMER() };

        var root = tag["DN"].Base;

        root.Should().NotBeNull();
        root.Should().BeSameAs(tag);
    }

    [Test]
    public void Root_FromNestedDescendantMember_ShouldNotBeNullAndSameAsTag()
    {
        var tag = new Tag { Name = "Test", Value = new MyNestedData() };

        var root = tag["Simple.M1"].Base;

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
        var tag = new Tag { Name = "Test", Value = new MyNestedData() };

        var parent = tag["Simple.M1"].Parent;

        parent.Should().NotBeNull();
        parent?.Value.Should().BeOfType<MySimpleData>();
        parent?.TagName.Should().Be("Test.Simple");
    }

    [Test]
    public void New_NamedComplexType_ShouldHaveExpectedDataTypeAndMembers()
    {
        var tag = new Tag { Name = "Test", Value = new StructureData("MyCustomType") };

        tag.AddMember("Member01", new DINT(100));
        tag.AddMember("Member02", new TIMER { PRE = 3000 });
        tag.AddMember("Member03", new StructureData("SubType"));

        tag.DataType.Should().Be("MyCustomType");
        tag.Members().Where(m => m.TagName.Depth == 1).Should().HaveCount(3);
    }

    [Test]
    public Task Class_SetValidValue_ShouldBeVerified()
    {
        var tag = new Tag { Name = "Test", Class = ComponentClass.Safety, Value = 100 };

        var xml = tag.Serialize().ToString();

        return VerifyXml(xml);
    }

    [Test]
    public void ToString_WhenCalled_ShouldReturnTagName()
    {
        var tag = new Tag { Name = "Test", Value = true };

        tag.ToString().Should().Be("Test");
    }

    /*
    [Test]
    public void With_RootTagValidValue_ShouldUpdateValue()
    {
        var tag = new Tag { Name = "Test", Value = new DINT() };

        var result = tag.With(new REAL(2.3f));

        result.Should().NotBeNull();
        result.Name.Should().Be("Test");
        result.DataType.Should().Be("REAL");
        result.Value.Should().BeOfType<REAL>();
        result.Value.Should().Be(2.3f);
    }

    [Test]
    public void With_NestedComplexType_ShouldHaveUpdatedValue()
    {
        var tag = new Tag { Name = "Test", Value = new MyNestedData() };

        var member = tag["Simple.M4"];

        var result = member.With(new REAL(2.3f));

        result.Should().NotBeNull();
        result.Name.Should().Be("Test");
        result.TagName.Should().Be("Test.Simple.M4");
        result.DataType.Should().Be("REAL");
        result.Value.Should().BeOfType<REAL>();
        result.Value.Should().Be(2.3f);
    }*/

    [Test]
    public Task Duplicate_ValidConfig_ShouldBeVerified()
    {
        var content = TestContent.Test;
        var tag = content.Get<Tag>(Known.Tag);

        var duplicate = tag.Duplicate(t =>
        {
            t.Name = "NewSimpleTag";
            t.Description = "This is an updated tag";
            t["DintMember"].Value = 56203;
            t["RealMember"].Value = 345.6f;
        });

        return Verify(duplicate.Serialize().ToString());
    }

    [Test]
    public void Replace_NameProperty_ShouldBeVerified()
    {
        var tag = Tag.New<BOOL>("MyBoolTag");

        tag.Replace("My", "Test", t => t.Name);

        tag.Name.Should().StartWith("Test");
    }

    [Test]
    public void Replace_DescriptionProperty_ShouldBeExpected()
    {
        var tag = Tag.New<TIMER>("SomeTimer");
        tag.Description = "This is a test tag";

        tag.Replace("test", "timer", t => t.Description);

        tag.Description.Should().Contain("timer");
    }

    #endregion

    #region DeserializeTests

    [Test]
    public void New_NullElement_ShouldThrowArgumentNullException()
    {
        FluentActions.Invoking(() => new Tag(null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void New_ElementWithNoData_ShouldHaveExpectedValues()
    {
        const string xml =
            @"<Tag Name=""Test"" TagType=""Base"" DataType=""DINT"" Radix=""Decimal"" Constant=""false"" ExternalAccess=""Read/Write"" />";
        var element = XElement.Parse(xml);

        var tag = new Tag(element);

        tag.Should().NotBeNull();
        tag.Name.Should().Be("Test");
        tag.Description.Should().BeNull();
        tag.DataType.Should().Be("NULL");
        tag.Dimensions.Should().Be(Dimensions.Empty);
        tag.Radix.Should().Be(Radix.Null);
        tag.ExternalAccess.Should().Be(Access.ReadWrite);
        tag.TagType.Should().Be(TagType.Base);
        tag.Constant.Should().BeFalse();
        tag.Value.Should().Be(LogixType.Null);
    }

    [Test]
    public void New_TestSimpleBool_ShouldHaveExpectedValues()
    {
        var element = XElement.Parse(Sample.TagElement.TestSimpleBool());

        var tag = new Tag(element);

        tag.Should().NotBeNull();
        tag.Name.Should().Be("SimpleBool");
        tag.DataType.Should().Be(nameof(BOOL));
        tag.Value.Should().BeOfType<BOOL>();
        tag.Value.Should().Be(0);
    }

    [Test]
    public void New_TestSimpleSint_ShouldHaveExpectedValues()
    {
        var element = XElement.Parse(Sample.TagElement.TestSimpleSint());

        var tag = new Tag(element);

        tag.Should().NotBeNull();
        tag.Name.Should().Be("SimpleSint");
        tag.DataType.Should().Be(nameof(SINT));
        tag.Radix.Should().Be(Radix.Hex);
        tag.Value.Should().BeOfType<SINT>();
        tag.Value.Should().Be(12);
    }

    [Test]
    public void New_TestSimpleInt_ShouldHaveExpectedValues()
    {
        var element = XElement.Parse(Sample.TagElement.TestSimpleInt());

        var tag = new Tag(element);

        tag.Should().NotBeNull();
        tag.Name.Should().Be("SimpleInt");
        tag.DataType.Should().Be(nameof(INT));
        tag.Value.Should().BeOfType<INT>();
        tag.Value.Should().Be(4321);
    }

    [Test]
    public void New_TestSimpleDint_ShouldHaveExpectedValues()
    {
        var element = XElement.Parse(Sample.TagElement.TestSimpleDint());

        var tag = new Tag(element);

        tag.Should().NotBeNull();
        tag.Name.Should().Be("SimpleDint");
        tag.DataType.Should().Be(nameof(DINT));
        tag.Value.Should().BeOfType<DINT>();
        tag.Value.Should().Be(123392);
    }

    [Test]
    public void New_TestSimpleLint_ShouldHaveExpectedValues()
    {
        var element = XElement.Parse(Sample.TagElement.TestSimpleLint());

        var tag = new Tag(element);

        tag.Should().NotBeNull();
        tag.Name.Should().Be("SimpleLint");
        tag.DataType.Should().Be(nameof(LINT));
        tag.Value.Should().BeOfType<LINT>();
        tag.Value.Should().Be(123);
    }

    [Test]
    public void New_TestSimpleReal_ShouldHaveExpectedValues()
    {
        var element = XElement.Parse(Sample.TagElement.TestSimpleReal());

        var tag = new Tag(element);

        tag.Should().NotBeNull();
        tag.Name.Should().Be("SimpleReal");
        tag.DataType.Should().Be(nameof(REAL));
        tag.Value.Should().BeOfType<REAL>();
        tag.Value.Should().Be(1.23f);
    }

    [Test]
    public void New_TestStringType_ShouldHaveExpectedValues()
    {
        var element = XElement.Parse(Sample.TagElement.TestStringTag());

        var tag = new Tag(element);

        tag.Should().NotBeNull();
        tag.Name.Should().Be("TestStringTag");
        tag.DataType.Should().Be("MyStringType");
        tag.Value.Should().BeOfType<StringData>();
        tag.Value.Should().Be("This is a $$ tests");
    }

    [Test]
    public void New_TestTimerType_ShouldHaveExpectedValues()
    {
        var element = XElement.Parse(Sample.TagElement.TestTimerTag());

        var tag = new Tag(element);

        tag.Should().NotBeNull();
        tag.Name.Should().Be("TestTimer");
        tag.DataType.Should().Be("TIMER");
        tag["PRE"].Value.Should().Be(1000);
        tag["PRE"].Description.Should().Be("Test Timer PRE");
    }

    [Test]
    public void New_TestSimpleType_ShouldHaveExpectedValues()
    {
        var element = XElement.Parse(Sample.TagElement.TestSimpleTag());

        var tag = new Tag(element);

        tag.Should().NotBeNull();
        tag.Name.Should().Be("TestSimpleTag");
        tag.DataType.Should().Be("SimpleType");
        tag.Constant.Should().BeFalse();
        tag.ExternalAccess.Should().Be(Access.ReadOnly);
        tag.Value.Should().BeOfType<StructureData>();
        tag.Member("BoolMember").Should().NotBeNull();
        tag.Member("SintMember").Should().NotBeNull();
        tag.Member("IntMember").Should().NotBeNull();
        tag.Member("DintMember").Should().NotBeNull();
        tag.Member("LintMember").Should().NotBeNull();
        tag.Member("RealMember").Should().NotBeNull();
    }

    [Test]
    public void New_TestComplexType_ShouldHaveExpectedValue()
    {
        var element = XElement.Parse(Sample.TagElement.TestComplexTag());

        var tag = new Tag(element);

        tag.Should().NotBeNull();
        tag.Name.Should().Be("TestComplexTag");
        tag.DataType.Should().Be("ComplexType");
        tag.Dimensions.Should().Be(Dimensions.Empty);
        tag.Radix.Should().Be(Radix.Null);
        tag.ExternalAccess.Should().Be(Access.None);
        tag.Constant.Should().BeFalse();
    }

    #endregion

    #region ValueTesting

    [Test]
    public void GetValue_Default_ShouldNotBeNullType()
    {
        var tag = new Tag { Name = "Test" };

        var value = tag.Value;

        value.Should().NotBeNull();
        value.Should().BeOfType<NullData>();
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

        tag.Value.Should().Be(43);
        tag.Value.Should().BeOfType<DINT>();
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
        tag.Value = new StructureData("Test", new List<LogixMember>
        {
            new("PRE", 5000),
            new("ACC", 1234),
            new("DN", 1),
            new("TT", 1),
            new("EN", 1),
        });

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

        tag.Value.As<ArrayData>()[0].Should().Be(4);
        tag.Value.As<ArrayData>()[1].Should().Be(3);
        tag.Value.As<ArrayData>()[2].Should().Be(2);
        tag.Value.As<ArrayData>()[3].Should().Be(1);
    }

    [Test]
    public void SetValue_StructureArrayType_ShouldHaveExpectedValues()
    {
        var tag = new Tag { Name = "Test", Value = new ArrayData<TIMER>(4) };

        tag.Value = new ArrayData<TIMER>([
            new TIMER { PRE = 100 },
            new TIMER { PRE = 200 },
            new TIMER { PRE = 300 }
        ]);

        tag.Value.As<ArrayData<TIMER>>().Should().HaveCount(4);
        tag.Value.As<ArrayData<TIMER>>()[0].PRE.Should().Be(100);
        tag.Value.As<ArrayData<TIMER>>()[1].PRE.Should().Be(200);
        tag.Value.As<ArrayData<TIMER>>()[2].PRE.Should().Be(300);
    }

    [Test]
    public void SetValue_InvalidType_ShouldThrowArgumentException()
    {
        var tag = new Tag { Name = "Test", Value = new TIMER() };

        FluentActions.Invoking(() => tag.Value = new REAL(43)).Should().Throw<ArgumentException>();
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
    public Task SetValue_StaticMemberOfNestedType_ShouldBeVerifiedXml()
    {
        var tag = new Tag { Name = "Test", Value = new MyNestedData() };

        tag.Value.As<MyNestedData>().Simple.M4 = 5000;

        var xml = tag.Serialize().ToString();
        return Verify(xml);
    }

    [Test]
    public void SetValue_AsDictionary_ShouldBeExpectedValues()
    {
        var tag = new Tag { Name = "Test", Value = new TIMER() };

        tag.Value = new Dictionary<string, LogixData>
        {
            { "PRE", 5000 },
            { "ACC", 1234 },
            { "DN", true }
        };

        tag.Value.As<TIMER>().PRE.Should().Be(5000);
        tag.Value.As<TIMER>().ACC.Should().Be(1234);
        tag.Value.As<TIMER>().DN.Should().Be(true);
    }

    [Test]
    public Task SetValue_AnalogAlarm_ShouldBeVerified()
    {
        var tag = new Tag { Name = "Test" };

        tag.Value = new ALARM_ANALOG { In = 1.23f, HHEnabled = true, MinDurationPRE = 3000 };

        var xml = tag.Serialize().ToString();
        return Verify(xml);
    }

    #endregion

    #region MembersTesting

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
        var tag = new Tag { Name = "Test", Value = new MyNestedData() };

        var member = tag.Member("Simple.M1");

        member.Should().NotBeNull();
        member?.TagName.Should().Be("Test.Simple.M1");
    }

    [Test]
    public void Member_ChainedCalls_ShouldBeExpected()
    {
        var tag = new Tag { Name = "Test", Value = new MyNestedData() };

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
    public void Members_StringType_ShouldHaveSingleRootMember()
    {
        var tag = new Tag { Name = "Test", Value = "This is a test value" };

        var members = tag.Members().ToList();

        members.Should().HaveCount(1);
    }

    [Test]
    public void Members_NestedStructure_ShouldHaveExpectedCount()
    {
        var tag = new Tag { Name = "Test", Value = new MyNestedData() };

        var members = tag.Members().ToList();

        members.Should().NotBeEmpty();
    }

    [Test]
    public void Members_NestedStructure_AllDataTypesShouldNotBeNull()
    {
        var tag = new Tag { Name = "Test", Value = new MyNestedData() };

        var result = tag.Members().ToList().Select(m => m.Value).ToList();

        result.Should().AllBeAssignableTo<LogixData>();
    }

    [Test]
    public void Members_NestedStructure_ShouldHaveExpectedTagNames()
    {
        var tag = new Tag
        {
            Name = "Test",
            Value = new MyNestedData()
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
        members.Should().Contain(t => t.TagName == "Test.Str");
    }

    [Test]
    public void Members_TagNameContainsTmr_ShouldReturnExpectedCount()
    {
        var tag = new Tag
        {
            Name = "Test",
            Value = new MyNestedData()
        };

        var members = tag.Members(t => t.Contains("Tmr"));

        members.Should().HaveCountGreaterThan(0);
    }

    [Test]
    public void Members_TagNameWithMoreThanTwoMembers_ShouldReturnExpectedCount()
    {
        var tag = new Tag
        {
            Name = "Test",
            Value = new MyNestedData()
        };

        var members = tag.Members((TagName t) => t.Members().Count() > 2);

        members.Should().NotBeEmpty();
    }

    [Test]
    public void Members_TagNameEqualToMemberWithMemberNameComparer_ShouldReturnExpectedCount()
    {
        var tag = new Tag
        {
            Name = "Test",
            Value = new MyNestedData()
        };

        var members = tag.Members(t => t.Element == "M1");

        members.Should().HaveCount(1);
    }

    [Test]
    public void Members_TagMemberWithBOOLDataTypeName_ShouldReturnExpectedCount()
    {
        var tag = new Tag
        {
            Name = "Test",
            Value = new MyNestedData()
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
            Value = new MyNestedData()
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
            Value = new MyNestedData()
        };

        var members = tag.MembersOf("Fake").ToList();

        members.Should().BeEmpty();
    }

    [Test]
    public void TagNames_NestedType_ShouldHaveExpectedCount()
    {
        var tag = new Tag("Test", new MyNestedData());

        var names = tag.TagNames().ToList();

        names.Should().NotBeEmpty();
    }

    #endregion

    #region DescriptionTests

    [Test]
    public Task SetDescription_OnRoot_ShouldUpdateRootDescriptionElement()
    {
        var tag = new Tag { Name = "Test", Value = 100, Description = "This is a description test" };

        var xml = tag.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public Task SetDescription_OnMemberTag_ShouldCreateAndUpdateCommentsElement()
    {
        var tag = new Tag { Name = "Test", Value = new TIMER(), Description = "This is a description test" };

        tag["PRE"].Description = "This is a member comment";

        var xml = tag.Serialize().ToString();
        return Verify(xml);
    }

    [Test]
    public void GetDescription_OnMemberTag_ShouldHaveInheritedParentDescription()
    {
        var tag = new Tag { Name = "Test", Value = new TIMER(), Description = "TIMER TAG" };

        var comment = tag["PRE"].Description;

        comment.Should().Be("TIMER TAG");
    }

    [Test]
    public void GetDescription_FromUserDefinedWithPassThroughEnabled_ShouldHavePassThroughDescription()
    {
        const string expectedBase = "Base";
        var expectedMember = string.Concat(expectedBase, " ", "User defined complex type", " ", "Test Bool");
        var content = TestContent.Test;

        var tag = content.Get<Tag>("TestComplexTag");

        tag.Description.Should().Be(expectedBase);
        tag["SimpleMember.BoolMember"].Description.Should().Be(expectedMember);
    }

    #endregion

    #region CommentsTests

    [Test]
    public void Comments_ValidCollection_ShouldHaveExpectedCount()
    {
        // ReSharper disable once UseObjectOrCollectionInitializer
        var tag = new Tag("Test", 100);

        tag.Comments =
        [
            new Comment(".1", "This is the comment"),
            new Comment(".2", "this is another comment")
        ];

        tag.Comments.Should().HaveCount(2);
    }

    [Test]
    public Task Comments_ValidCollection_ShouldBeVerified()
    {
        var tag = new Tag("Test", 100, "This is a test tag");

        tag.Comments =
        [
            new Comment(".1", "This is the comment"),
            new Comment(".2", "this is another comment")
        ];

        var xml = tag.Serialize().ToString();

        return Verify(xml);
    }

    #endregion

    #region BuilderTests

    [Test]
    public void New_AtomicTypeAndName_ShouldBeExpected()
    {
        var tag = Tag.New<DINT>("TestTag");

        tag.Name.Should().Be("TestTag");
        tag.Value.Should().NotBeNull();
        tag.Value.Should().BeOfType<DINT>();
        tag.Value.Should().Be(0);
    }
    
    [Test]
    public void New_ComplexTypeAndName_ShouldBeExpected()
    {
        var tag = Tag.New<TIMER>("MyTimer");

        tag.Name.Should().Be("MyTimer");
        tag.Value.Should().NotBeNull();
        tag.Value.Should().NotBe(LogixType.Null);
    }

    [Test]
    public void New_ArrayOfAtomicData_ShouldBeExpected()
    {
        var tag = Tag.New<DINT>("ArrayTag", 10);

        tag.Name.Should().Be("ArrayTag");
        tag.Dimensions.Should().Be(10);
        tag.Value.Should().NotBeNull();
        tag.Value.Should().BeOfType<ArrayData>();
    }

    [Test]
    public void Build_SimpleAtomicTypeWithValue_ShouldHaveExpectedValues()
    {
        var tag = Tag.Named("SomeAtomic")
            .WithValue(123)
            .Build();

        tag.Name.Should().Be("SomeAtomic");
        tag.DataType.Should().Be("DINT");
        tag.Value.Should().Be(123);
        tag.Radix.Should().Be(Radix.Decimal);
        tag.ExternalAccess.Should().Be(Access.ReadWrite);
        tag.TagType.Should().Be(TagType.Base);
    }

    [Test]
    public void Build_SimpleNoneAccess_ShouldHaveExpectedAccess()
    {
        var tag = Tag.Named("SomeAtomic")
            .WithAccess(Access.None)
            .Build();

        tag.ExternalAccess.Should().Be(Access.None);
    }

    [Test]
    public void Build_SimpleReadOnlyAccess_ShouldHaveExpectedAccess()
    {
        var tag = Tag.Named("SomeAtomic")
            .ReadOnly()
            .Build();

        tag.ExternalAccess.Should().Be(Access.ReadOnly);
    }

    [Test]
    public void Build_SimpleReadWriteAccess_ShouldHaveExpectedAccess()
    {
        var tag = Tag.Named("SomeAtomic")
            .ReadWrite()
            .Build();

        tag.ExternalAccess.Should().Be(Access.ReadWrite);
    }

    [Test]
    public void Build_SimpleWithUsage_ShouldHaveExpectedAccess()
    {
        var tag = Tag.Named("SomeAtomic")
            .WithUsage(TagUsage.Public)
            .Build();

        tag.Usage.Should().Be(TagUsage.Public);
    }

    [Test]
    public void Build_SimpleWithNormalUsage_ShouldHaveExpectedAccess()
    {
        var tag = Tag.Named("SomeAtomic")
            .Normal()
            .Build();

        tag.Usage.Should().Be(TagUsage.Normal);
    }

    [Test]
    public void Build_SimpleWithDescription_ShouldHaveExpectedDescription()
    {
        var tag = Tag.Named("SomeAtomic")
            .WithDescription("This is a test of the fluent tag builder")
            .Build();

        tag.Description.Should().Be("This is a test of the fluent tag builder");
    }

    [Test]
    public void Build_SimpleConstant_ShouldHaveExpectedConstant()
    {
        var tag = Tag.Named("SomeAtomic")
            .Constant()
            .Build();

        tag.Constant.Should().BeTrue();
    }

    [Test]
    public void Build_SimpleConsumer_ShouldHaveExpectedInfo()
    {
        var tag = Tag.Named("ConsumerTag")
            .Consumes(cb => cb
                .Provider("RemoteProviderName")
                .RemoteTag("RemoteTagName.Member.Value")
                .RPI(100)
                .Unicast()
            )
            .Build();

        tag.Name.Should().Be("ConsumerTag");
        tag.TagType.Should().Be(TagType.Consumed);
        tag.ConsumeInfo.Should().NotBeNull();
        tag.ConsumeInfo?.Producer.Should().Be("RemoteProviderName");
        tag.ConsumeInfo?.RemoteTag.Should().Be("RemoteTagName.Member.Value");
        tag.ConsumeInfo?.RPI.Should().Be(100);
        tag.ConsumeInfo?.Unicast.Should().BeTrue();
    }

    [Test]
    public void Build_SimpleProducer_ShouldHaveExpectedInfo()
    {
        var tag = Tag.Named("ProducerTag")
            .Produces(b => b
                .WithMaxCount(5)
                .SendEventTrigger()
                .Unicast()
            )
            .Build();

        tag.Name.Should().Be("ProducerTag");
        tag.TagType.Should().Be(TagType.Produced);
        tag.ProduceInfo.Should().NotBeNull();
        tag.ProduceInfo?.ProduceCount.Should().Be(5);
        tag.ProduceInfo?.ProgrammaticallySendEventTrigger.Should().BeTrue();
        tag.ProduceInfo?.UnicastPermitted.Should().BeTrue();
    }

    [Test]
    public Task Build_SimpleAliasTag_ShouldBeVerified()
    {
        var tag = Tag.Named("MyTagName")
            .AliasFor("SomeOtherTag")
            .Build();

        return VerifyXml(tag.Serialize().ToString());
    }

    [Test]
    public Task Build_PredefinedTypeDefaultValue_ShouldBeVerified()
    {
        var tag = Tag.Named("MyTagName")
            .WithValue<TIMER>()
            .WithDescription("Builder example of creating a complex predefined data type with default data.")
            .Build();

        return VerifyXml(tag.Serialize().ToString());
    }

    [Test]
    public Task Build_PredefinedTypeConfiguredValue_ShouldBeVerified()
    {
        var tag = Tag.Named("MyTagName")
            .WithValue<TIMER>(t =>
            {
                t.PRE = 10000;
                t.ACC = 1234;
                t.DN = true;
                t.EN = true;
            })
            .WithDescription("Builder example of creating a complex predefined data type with configured data.")
            .Build();

        return VerifyXml(tag.Serialize().ToString());
    }

    [Test]
    public Task Build_UserDefinedTypeDefaultValue_ShouldBeVerified()
    {
        var tag = Tag.Named("MyTagName")
            .WithValue<MyNestedData>()
            .WithDescription("Builder example of creating a complex user defined data type with defualt data.")
            .Build();

        return VerifyXml(tag.Serialize().ToString());
    }

    [Test]
    public Task Build_UserDefinedTypeConfiguredValue_ShouldBeVerified()
    {
        var tag = Tag.Named("MyTagName")
            .WithValue<MyNestedData>(d =>
            {
                d.Simple.M2 = 123;
                d.Tmr.PRE = 12345;
                d.Flags[5] = true;
                d.Counters[1].PRE = 4;
                d.Names[3] = "This is a complex test";
            })
            .WithDescription("Builder example of creating a complex user defined data type and setting member value.")
            .Build();

        return VerifyXml(tag.Serialize().ToString());
    }

    #endregion

    #region ProduceConsumeTests

    [Test]
    public Task ProduceInfo_SetNewValue_ShouldBeVerified()
    {
        var tag = new Tag { Name = "Test", Value = 123, TagType = TagType.Produced };

        tag.ProduceInfo = new ProduceInfo
        {
            ProduceCount = 2,
            ProgrammaticallySendEventTrigger = true,
            UnicastPermitted = true,
            MinimumRPI = 0.33,
            MaximumRPI = 12345.11,
            DefaultRPI = 100
        };

        var xml = tag.Serialize().ToString();

        return VerifyXml(xml);
    }

    [Test]
    public Task ConsumeInfo_SetNewValue_ShouldBeVerified()
    {
        var tag = new Tag { Name = "Test", Value = 123, TagType = TagType.Consumed };

        tag.ConsumeInfo = new ConsumeInfo
        {
            Producer = "SomeController",
            RemoteTag = "SomeTag.Name.Value.1",
            RemoteInstance = 0,
            Unicast = true,
            RPI = 100.12
        };

        var xml = tag.Serialize().ToString();

        return VerifyXml(xml);
    }

    #endregion

    #region ReferencesTests

    [Test]
    public void References_ForAllTagsExampleFile_ShouldNotTakeForever()
    {
        var content = TestContent.Example;

        var result = content.Query<Tag>().SelectMany(t => t.Members()).SelectMany(t => t.References()).ToList();

        result.Should().NotBeEmpty();
    }

    [Test]
    public void TagCount()
    {
        var content = TestContent.Example;

        var tags = content.Query<Tag>().ToList();

        Console.WriteLine(tags.Count);
    }

    #endregion
}