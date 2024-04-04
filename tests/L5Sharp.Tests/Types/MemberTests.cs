using System.Xml.Linq;
using FluentAssertions;

// ReSharper disable UseObjectOrCollectionInitializer

namespace L5Sharp.Tests.Types;

[TestFixture]
public class MemberTests
{
    [Test]
    public void New_ValidArguments_ShouldNotBeNull()
    {
        var member = new Member("Test", new BOOL());

        member.Should().NotBeNull();
    }

    [Test]
    public void New_ValidArguments_ShouldHaveExpectedValues()
    {
        var member = new Member("Test", new BOOL());

        member.Name.Should().Be("Test");
        member.Value.Should().BeOfType<BOOL>();
    }

    [Test]
    public void New_NullName_ShouldThrowException()
    {
        FluentActions.Invoking(() => new Member(null!, new BOOL())).Should().Throw<ArgumentException>();
    }

    [Test]
    public void New_NullType_ShouldThrowException()
    {
        FluentActions.Invoking(() => new Member("Test", null!)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void New_ValueMemberElement_ShouldHaveExpectedProperties()
    {
        const string xml = "<DataValueMember Name=\"Test\" DataType=\"DINT\" Radix=\"Decimal\" Value=\"123\" />";
        var element = XElement.Parse(xml);
        var type = element.Deserialize<LogixType>();

        var member = new Member("Test", type);

        member.Name.Should().Be("Test");
        member.Value.Should().BeOfType<DINT>();
        member.Value.Should().Be(123);
    }

    [Test]
    public void New_DataValueElement_ShouldHaveExpectedProperties()
    {
        const string xml = "<DataValue DataType=\"DINT\" Radix=\"Decimal\" Value=\"123\" />";
        var element = XElement.Parse(xml);
        var type = element.Deserialize<LogixType>();

        var member = new Member("Test", type);

        member.Name.Should().Be("Test");
        member.Value.Should().BeOfType<DINT>();
        member.Value.Should().Be(123);
    }

    [Test]
    public void New_StructureElement_ShouldHaveExpectedProperties()
    {
        const string xml = @"<Structure DataType=""TIMER"">
            <DataValueMember Name=""PRE"" DataType=""DINT"" Radix=""Decimal"" Value=""1000"" />
            <DataValueMember Name=""ACC"" DataType=""DINT"" Radix=""Decimal"" Value=""2000"" />
            <DataValueMember Name=""EN"" DataType=""BOOL"" Value=""1"" />
            <DataValueMember Name=""TT"" DataType=""BOOL"" Value=""1"" />
            <DataValueMember Name=""DN"" DataType=""BOOL"" Value=""1"" />
            </Structure>";
        var element = XElement.Parse(xml);
        var type = element.Deserialize<LogixType>();

        var member = new Member("Test", type);

        member.Name.Should().Be("Test");
        member.Value.Should().BeOfType<TIMER>();
        member.Value.Member("PRE")!.Value.Should().Be(1000);
        member.Value.Member("ACC")!.Value.Should().Be(2000);
        member.Value.Member("EN")!.Value.Should().Be(1);
        member.Value.Member("TT")!.Value.Should().Be(1);
        member.Value.Member("DN")!.Value.Should().Be(1);
    }

    [Test]
    public void New_StructureMemberElement_ShouldHaveExpectedProperties()
    {
        const string xml = @"<StructureMember Name=""Test"" DataType=""TIMER"">
            <DataValueMember Name=""PRE"" DataType=""DINT"" Radix=""Decimal"" Value=""1000"" />
            <DataValueMember Name=""ACC"" DataType=""DINT"" Radix=""Decimal"" Value=""2000"" />
            <DataValueMember Name=""EN"" DataType=""BOOL"" Value=""1"" />
            <DataValueMember Name=""TT"" DataType=""BOOL"" Value=""1"" />
            <DataValueMember Name=""DN"" DataType=""BOOL"" Value=""1"" />
            </StructureMember>";
        var element = XElement.Parse(xml);
        var type = element.Deserialize<LogixType>();

        var member = new Member("Test", type);

        member.Name.Should().Be("Test");
        member.Value.Should().BeOfType<TIMER>();
        member.Value.Member("PRE")!.Value.Should().Be(1000);
        member.Value.Member("ACC")!.Value.Should().Be(2000);
        member.Value.Member("EN")!.Value.Should().Be(1);
        member.Value.Member("TT")!.Value.Should().Be(1);
        member.Value.Member("DN")!.Value.Should().Be(1);
    }

    [Test]
    public void New_UserTypeMemberElement_ShouldHaveExpectedProperties()
    {
        const string xml = @"<StructureMember Name=""Test"" DataType=""CustomType"">
            <DataValueMember Name=""PRE"" DataType=""DINT"" Radix=""Decimal"" Value=""1000"" />
            <DataValueMember Name=""ACC"" DataType=""DINT"" Radix=""Decimal"" Value=""2000"" />
            <DataValueMember Name=""EN"" DataType=""BOOL"" Value=""1"" />
            <DataValueMember Name=""TT"" DataType=""BOOL"" Value=""1"" />
            <DataValueMember Name=""DN"" DataType=""BOOL"" Value=""1"" />
            </StructureMember>";
        var element = XElement.Parse(xml);
        var type = element.Deserialize<LogixType>();

        var member = new Member("Test", type);

        member.Name.Should().Be("Test");
        member.Value.Should().BeOfType<ComplexType>();
        member.Value.Name.Should().Be("CustomType");
        member.Value.Member("PRE")!.Value.Should().Be(1000);
        member.Value.Member("ACC")!.Value.Should().Be(2000);
        member.Value.Member("EN")!.Value.Should().Be(1);
        member.Value.Member("TT")!.Value.Should().Be(1);
        member.Value.Member("DN")!.Value.Should().Be(1);
    }

    [Test]
    public void New_StringMemberElement_ShouldHaveExpectedProperties()
    {
        const string xml = @"<StructureMember Name=""Test"" DataType=""STRING"">
            <DataValueMember Name=""LEN"" DataType=""DINT"" Radix=""Decimal"" Value=""27"" />
            <DataValueMember Name=""DATA"" DataType=""STRING"" Radix=""ASCII""><![CDATA[This is a string type value]]></DataValueMember>
            </StructureMember>";
        var element = XElement.Parse(xml);
        var type = element.Deserialize<LogixType>();

        var member = new Member("Test", type);

        member.Name.Should().Be("Test");
        member.Value.Should().BeOfType<STRING>();
        member.Value.Member("LEN")!.Value.Should().Be(27);
        member.Value.ToString().Should().Be("This is a string type value");
    }

    [Test]
    public void New_ArrayOfAtomicMemberElement_ShouldHaveExpectedProperties()
    {
        const string xml = @"<ArrayMember Name=""Test"" DataType=""REAL"" Dimensions=""4"" Radix=""Float"">
            <Element Index=""[0]"" Value=""1.1"" />
            <Element Index=""[1]"" Value=""2.2"" />
            <Element Index=""[2]"" Value=""3.3"" />
            <Element Index=""[3]"" Value=""4.4"" />
            </ArrayMember>";
        var element = XElement.Parse(xml);
        var type = element.Deserialize<LogixType>();

        var member = new Member("Test", type);

        member.Name.Should().Be("Test");
        member.Value.As<ArrayType>()[0].Should().Be(1.1f);
        member.Value.As<ArrayType>()[1].Should().Be(2.2f);
        member.Value.As<ArrayType>()[2].Should().Be(3.3f);
        member.Value.As<ArrayType>()[3].Should().Be(4.4f);
    }

    [Test]
    public void New_ArrayOfStructureMemberElement_ShouldHaveExpectedProperties()
    {
        const string xml = @"<ArrayMember Name=""Test"" DataType=""TIMER"" Dimensions=""3"">
            <Element Index=""[0]"">
            <Structure DataType=""TIMER"">
            <DataValueMember Name=""PRE"" DataType=""DINT"" Radix=""Decimal"" Value=""0"" />
            <DataValueMember Name=""ACC"" DataType=""DINT"" Radix=""Decimal"" Value=""0"" />
            <DataValueMember Name=""EN"" DataType=""BOOL"" Value=""0"" />
            <DataValueMember Name=""TT"" DataType=""BOOL"" Value=""0"" />
            <DataValueMember Name=""DN"" DataType=""BOOL"" Value=""0"" />
            </Structure>
            </Element>
            <Element Index=""[1]"">
            <Structure DataType=""TIMER"">
            <DataValueMember Name=""PRE"" DataType=""DINT"" Radix=""Decimal"" Value=""0"" />
            <DataValueMember Name=""ACC"" DataType=""DINT"" Radix=""Decimal"" Value=""0"" />
            <DataValueMember Name=""EN"" DataType=""BOOL"" Value=""0"" />
            <DataValueMember Name=""TT"" DataType=""BOOL"" Value=""0"" />
            <DataValueMember Name=""DN"" DataType=""BOOL"" Value=""0"" />
            </Structure>
            </Element>
            <Element Index=""[2]"">
            <Structure DataType=""TIMER"">
            <DataValueMember Name=""PRE"" DataType=""DINT"" Radix=""Decimal"" Value=""0"" />
            <DataValueMember Name=""ACC"" DataType=""DINT"" Radix=""Decimal"" Value=""0"" />
            <DataValueMember Name=""EN"" DataType=""BOOL"" Value=""0"" />
            <DataValueMember Name=""TT"" DataType=""BOOL"" Value=""0"" />
            <DataValueMember Name=""DN"" DataType=""BOOL"" Value=""0"" />
            </Structure>
            </Element>
            </ArrayMember>";
        var element = XElement.Parse(xml);
        var type = element.Deserialize<LogixType>();

        var member = new Member("Test", type);

        member.Name.Should().Be("Test");
        member.Value.As<ArrayType>()[0].Should().BeOfType<TIMER>();
        member.Value.As<ArrayType>()[1].Should().BeOfType<TIMER>();
        member.Value.As<ArrayType>()[2].Should().BeOfType<TIMER>();
    }

    [Test]
    public void New_ArrayOfStringMemberElement_ShouldHaveExpectedProperties()
    {
        const string xml = @"<ArrayMember Name=""Test"" DataType=""STRING"" Dimensions=""3"">
            <Element Index=""[0]"">
            <Structure DataType=""STRING"">
            <DataValueMember Name=""LEN"" DataType=""DINT"" Radix=""Decimal"" Value=""5"" />
            <DataValueMember Name=""DATA"" DataType=""STRING"" Radix=""ASCII""><![CDATA[Test1]]></DataValueMember>
            </Structure>
            </Element>
            <Element Index=""[1]"">
            <Structure DataType=""STRING"">
            <DataValueMember Name=""LEN"" DataType=""DINT"" Radix=""Decimal"" Value=""5"" />
            <DataValueMember Name=""DATA"" DataType=""STRING"" Radix=""ASCII""><![CDATA[Test2]]></DataValueMember>
            </Structure>
            </Element>
            <Element Index=""[2]"">
            <Structure DataType=""STRING"">
            <DataValueMember Name=""LEN"" DataType=""DINT"" Radix=""Decimal"" Value=""5"" />
            <DataValueMember Name=""DATA"" DataType=""STRING"" Radix=""ASCII""><![CDATA[Test3]]></DataValueMember>
            </Structure>
            </Element>
            </ArrayMember>";
        var element = XElement.Parse(xml);
        var type = element.Deserialize<LogixType>();

        var member = new Member("Test", type);

        member.Name.Should().Be("Test");
        member.Value.As<ArrayType>()[0].Should().BeOfType<STRING>();
        member.Value.As<ArrayType>()[1].Should().BeOfType<STRING>();
        member.Value.As<ArrayType>()[2].Should().BeOfType<STRING>();
    }

    [Test]
    public void GetDataType_AtomicType_ShouldHaveExpectedValues()
    {
        var member = new Member("Test", 123);

        var dataType = member.Value;

        dataType.Should().NotBeNull();
        dataType.Should().BeOfType<DINT>();
        dataType.Should().Be(123);
    }

    [Test]
    public void GetDataType_StructureType_ShouldHaveExpectedValues()
    {
        var member = new Member("Test", new TIMER { PRE = 123 });

        var dataType = member.Value;

        dataType.Should().NotBeNull();
        dataType.Should().BeOfType<TIMER>();
        dataType.As<TIMER>().PRE.Should().Be(123);
    }

    [Test]
    public void GetDataType_ArrayType_ShouldHaveExpectedValues()
    {
        var member = new Member("Test", new DINT[] { 1, 2, 3, 4 });

        var dataType = member.Value;

        dataType.Should().NotBeNull();
        dataType.As<ArrayType>()[0].Should().Be(1);
        dataType.As<ArrayType>()[1].Should().Be(2);
        dataType.As<ArrayType>()[2].Should().Be(3);
        dataType.As<ArrayType>()[3].Should().Be(4);
    }

    [Test]
    public void GetDataType_StringType_ShouldHaveExpectedValues()
    {
        var member = new Member("Test", "This is a test");

        var dataType = member.Value;

        dataType.Should().NotBeNull();
        dataType.Should().BeOfType<STRING>();
        dataType.Should().Be("This is a test");
    }

    [Test]
    public void SetValue_AtomicToStructure_ShouldThrowInvalidCastException()
    {
        var member = new Member("Test", 123);

        FluentActions.Invoking(() => member.Value = new TIMER()).Should().Throw<InvalidCastException>();
    }

    [Test]
    public void SetValue_StructureToAtomic_ShouldThrowInvalidCastException()
    {
        var member = new Member("Test", new TIMER());

        FluentActions.Invoking(() => member.Value = 123).Should().Throw<InvalidCastException>();
    }

    [Test]
    public void SetValue_ToNull_ShouldThrowException()
    {
        var member = new Member("Test", 123);

        FluentActions.Invoking(() => member.Value = null!).Should().Throw<ArgumentException>();
    }

    [Test]
    public void SetValue_ToNullType_ShouldThrowException()
    {
        var member = new Member("Test", 123);

        FluentActions.Invoking(() => member.Value = LogixType.Null).Should().Throw<ArgumentException>();
    }

    [Test]
    public void SetValue_BOOL_ShouldBeExpectedValue()
    {
        var member = new Member("Test", false);

        member.Value = true;

        member.Value.Should().BeOfType<BOOL>();
        member.Value.As<BOOL>().Radix.Should().Be(Radix.Decimal);
        member.Value.Should().Be(true);
    }

    [Test]
    public void SetValue_SINT_ShouldBeExpectedValue()
    {
        var member = new Member("Test", new SINT(12, Radix.Binary));

        member.Value = 21;

        member.Value.Should().BeOfType<SINT>();
        member.Value.As<SINT>().Radix.Should().Be(Radix.Binary);
        member.Value.Should().Be(21);
    }

    [Test]
    public void SetValue_INT_ShouldBeExpectedValue()
    {
        var member = new Member("Test", new INT(1234, Radix.Binary));

        member.Value = 4321;

        member.Value.Should().BeOfType<INT>();
        member.Value.As<INT>().Radix.Should().Be(Radix.Binary);
        member.Value.Should().Be(4321);
    }

    [Test]
    public void SetValue_DINT_ShouldBeExpectedValue()
    {
        var member = new Member("Test", 123);

        member.Value = 321;

        member.Value.Should().BeOfType<DINT>();
        member.Value.As<DINT>().Radix.Should().Be(Radix.Decimal);
        member.Value.Should().Be(321);
    }

    [Test]
    public void SetValue_LINT_ShouldBeExpectedValue()
    {
        var member = new Member("Test", new LINT(long.MaxValue, Radix.Hex));

        member.Value = new DINT(1000, Radix.Ascii);

        member.Value.Should().BeOfType<LINT>();
        member.Value.As<LINT>().Radix.Should().Be(Radix.Hex);
        member.Value.Should().Be(1000);
    }

    [Test]
    public void SetValue_USINT_ShouldBeExpectedValue()
    {
        var member = new Member("Test", new USINT(12, Radix.Binary));

        member.Value = 21;

        member.Value.Should().BeOfType<USINT>();
        member.Value.As<USINT>().Radix.Should().Be(Radix.Binary);
        member.Value.Should().Be(21);
    }

    [Test]
    public void SetValue_UINT_ShouldBeExpectedValue()
    {
        var member = new Member("Test", new UINT(1234, Radix.Binary));

        member.Value = 4321;

        member.Value.Should().BeOfType<UINT>();
        member.Value.As<UINT>().Radix.Should().Be(Radix.Binary);
        member.Value.Should().Be(4321);
    }

    [Test]
    public void SetValue_UDINT_ShouldBeExpectedValue()
    {
        var member = new Member("Test", new UDINT(123));

        member.Value = 321;

        member.Value.Should().BeOfType<UDINT>();
        member.Value.As<UDINT>().Radix.Should().Be(Radix.Decimal);
        member.Value.Should().Be(321);
    }

    [Test]
    public void SetValue_ULINT_ShouldBeExpectedValue()
    {
        var member = new Member("Test", new ULINT(long.MaxValue, Radix.Hex));

        member.Value = new DINT(1000, Radix.Ascii);

        member.Value.Should().BeOfType<ULINT>();
        member.Value.As<ULINT>().Radix.Should().Be(Radix.Hex);
        member.Value.Should().Be(1000);
    }

    [Test]
    public void SetValue_REAL_ShouldBeExpectedValue()
    {
        var member = new Member("Test", new REAL(float.MaxValue, Radix.Exponential));

        member.Value = 123.123f;

        member.Value.Should().BeOfType<REAL>();
        member.Value.As<REAL>().Radix.Should().Be(Radix.Exponential);
        member.Value.Should().Be(123.123f);
    }

    [Test]
    public void SetValue_LREAL_ShouldBeExpectedValue()
    {
        var member = new Member("Test", new LREAL(123.123));

        member.Value = 123.123;

        member.Value.Should().BeOfType<LREAL>();
        member.Value.As<LREAL>().Radix.Should().Be(Radix.Float);
        member.Value.Should().Be(123.123);
    }

    [Test]
    public void SetValue_STRING_ShouldBeExpected()
    {
        var member = new Member("Test", "this is a test string");

        member.Value = "This is an updated test string";

        member.Value.Should().BeOfType<STRING>();
        member.Value.ToString().Should().Be("This is an updated test string");
    }

    [Test]
    public void SetValue_StructureAsConcreteType_ShouldBeExpectedValue()
    {
        var member = new Member("Test", new TIMER());

        member.Value = new TIMER { PRE = 5000, EN = true };

        member.Value.As<TIMER>().PRE.Should().Be(5000);
        member.Value.As<TIMER>().EN.Should().Be(true);
    }

    [Test]
    public void SetValue_StructureAsGenericStructureType_ShouldBeExpectedValue()
    {
        var member = new Member("Test", new TIMER());

        member.Value = new ComplexType("TIMER", new List<Member> { new("PRE", 5000), new("EN", true) });

        member.Value.As<TIMER>().PRE.Should().Be(5000);
        member.Value.As<TIMER>().EN.Should().Be(true);
    }

    [Test]
    public void SetValue_StructureAsDictionary_ShouldBeExpectedValue()
    {
        var member = new Member("Test", new TIMER());

        member.Value = new Dictionary<string, LogixType>
        {
            { "PRE", 5000 },
            { "EN", true }
        };

        member.Value.As<TIMER>().PRE.Should().Be(5000);
        member.Value.As<TIMER>().EN.Should().Be(true);
    }

    [Test]
    public void SetValue_StructureWithMembersNotInType_ShouldOnlySetMembersThatAreInType()
    {
        var member = new Member("Test", new TIMER());

        member.Value = new Dictionary<string, LogixType>
        {
            { "Test", 123 },
            { "PRE", 5000 },
            { "Array", new DINT[] { 1, 2, 3, 4 } },
            { "EN", true }
        };

        member.Value.As<TIMER>().PRE.Should().Be(5000);
        member.Value.As<TIMER>().ACC.Should().Be(0);
        member.Value.As<TIMER>().EN.Should().Be(true);
        member.Value.As<TIMER>().TT.Should().Be(false);
        member.Value.As<TIMER>().DN.Should().Be(false);
    }

    [Test]
    public Task Serialize_AtomicDataType_ShouldBeVerified()
    {
        var member = new Member("Test", 123);

        var xml = member.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public Task Serialize_StructureDataType_ShouldBeVerified()
    {
        var member = new Member("Test", new TIMER { PRE = 1000, ACC = 2000, EN = 1, DN = 1, TT = 1 });

        var xml = member.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public Task Serialize_StringDataType_ShouldBeVerified()
    {
        var member = new Member("Test", "This is a string type value");

        var xml = member.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public Task Serialize_ArrayOfAtomicDataType_ShouldBeVerified()
    {
        var member = new Member("Test", new REAL[] { 1.1f, 2.2f, 3.3f, 4.4f });

        var xml = member.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public Task Serialize_ArrayOfStructureDataType_ShouldBeVerified()
    {
        var member = new Member("Test", new TIMER[] { new(), new(), new() });

        var xml = member.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public Task Serialize_ArrayOfStringDataType_ShouldBeVerified()
    {
        var member = new Member("Test", new STRING[] { "Test1", "Test2", "Test3" });

        var xml = member.Serialize().ToString();

        return Verify(xml);
    }

    #region InternalsTests

    [Test]
    public void New_ElementOverload_ShouldHaveExpectedValues()
    {
        const string xml = "<DataValueMember Name=\"Test\" DataType=\"DINT\" Radix=\"Decimal\" Value=\"123\" />";
        var element = XElement.Parse(xml);

        var member = new Member(element);

        member.Name.Should().Be("Test");
        member.Value.Should().BeOfType<DINT>();
        member.Value.Should().Be(123);
    }

    #endregion
}