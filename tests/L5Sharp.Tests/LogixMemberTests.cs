using System.Xml.Linq;
using FluentAssertions;

// ReSharper disable UseObjectOrCollectionInitializer

namespace L5Sharp.Tests;

[TestFixture]
public class LogixMemberTests
{
    [Test]
    public void New_ValidArguments_ShouldNotBeNull()
    {
        var member = new LogixMember("Test", new BOOL());

        member.Should().NotBeNull();
    }

    [Test]
    public void New_ValidArguments_ShouldHaveExpectedValues()
    {
        var member = new LogixMember("Test", new BOOL());

        member.Name.Should().Be("Test");
        member.DataType.Should().BeOfType<BOOL>();
    }

    [Test]
    public void New_NullName_ShouldHaveEmptyName()
    {
        var member = new LogixMember(null!, new BOOL());

        member.Should().NotBeNull();
        member.Name.Should().BeEmpty();
    }

    [Test]
    public void New_NullType_ShouldHaveDataTypeOfTypeNullType()
    {
        var member = new LogixMember("Test", null!);

        member.Should().NotBeNull();
        member.DataType.Should().NotBeNull();
        member.DataType.Should().BeOfType<NullType>();
    }

    [Test]
    public void New_NullElement_ShouldThrowArgumentNullException()
    {
        FluentActions.Invoking(() => new LogixMember(null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void New_ValueMemberElement_ShouldHaveExpectedProperties()
    {
        const string xml = "<DataValueMember Name=\"Test\" DataType=\"DINT\" Radix=\"Decimal\" Value=\"123\" />";
        var element = XElement.Parse(xml);

        var member = new LogixMember(element);

        member.Name.Should().Be("Test");
        member.DataType.Should().BeOfType<DINT>();
        member.DataType.Should().Be(123);
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

        var member = new LogixMember(element);

        member.Name.Should().Be("Test");
        member.DataType.Should().BeOfType<TIMER>();
        member.DataType.Member("PRE")!.DataType.Should().Be(1000);
        member.DataType.Member("ACC")!.DataType.Should().Be(2000);
        member.DataType.Member("EN")!.DataType.Should().Be(1);
        member.DataType.Member("TT")!.DataType.Should().Be(1);
        member.DataType.Member("DN")!.DataType.Should().Be(1);
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

        var member = new LogixMember(element);

        member.Name.Should().Be("Test");
        member.DataType.Should().BeOfType<ComplexType>();
        member.DataType.Name.Should().Be("CustomType");
        member.DataType.Member("PRE")!.DataType.Should().Be(1000);
        member.DataType.Member("ACC")!.DataType.Should().Be(2000);
        member.DataType.Member("EN")!.DataType.Should().Be(1);
        member.DataType.Member("TT")!.DataType.Should().Be(1);
        member.DataType.Member("DN")!.DataType.Should().Be(1);
    }

    [Test]
    public void New_StringMemberElement_ShouldHaveExpectedProperties()
    {
        const string xml = @"<StructureMember Name=""Test"" DataType=""STRING"">
            <DataValueMember Name=""LEN"" DataType=""DINT"" Radix=""Decimal"" Value=""27"" />
            <DataValueMember Name=""DATA"" DataType=""STRING"" Radix=""ASCII""><![CDATA[This is a string type value]]></DataValueMember>
            </StructureMember>";
        var element = XElement.Parse(xml);

        var member = new LogixMember(element);

        member.Name.Should().Be("Test");
        member.DataType.Should().BeOfType<STRING>();
        member.DataType.Member("LEN")!.DataType.Should().Be(27);
        member.DataType.ToString().Should().Be("This is a string type value");
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

        var member = new LogixMember(element);

        member.Name.Should().Be("Test");
        member.DataType.Should().BeOfType<ArrayType<REAL>>();
        member.DataType.As<ArrayType>()[0].Should().Be(1.1f);
        member.DataType.As<ArrayType>()[1].Should().Be(2.2f);
        member.DataType.As<ArrayType>()[2].Should().Be(3.3f);
        member.DataType.As<ArrayType>()[3].Should().Be(4.4f);
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

        var member = new LogixMember(element);

        member.Name.Should().Be("Test");
        member.DataType.Should().BeOfType<ArrayType<TIMER>>();
        member.DataType.As<ArrayType>()[0].Should().BeOfType<TIMER>();
        member.DataType.As<ArrayType>()[1].Should().BeOfType<TIMER>();
        member.DataType.As<ArrayType>()[2].Should().BeOfType<TIMER>();
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

        var member = new LogixMember(element);

        member.Name.Should().Be("Test");
        member.DataType.Should().BeOfType<ArrayType<STRING>>();
        member.DataType.As<ArrayType>()[0].Should().BeOfType<STRING>();
        member.DataType.As<ArrayType>()[1].Should().BeOfType<STRING>();
        member.DataType.As<ArrayType>()[2].Should().BeOfType<STRING>();
    }

    [Test]
    public void GetDataType_AtomicType_ShouldHaveExpectedValues()
    {
        var member = new LogixMember("Test", 123);

        var dataType = member.DataType;

        dataType.Should().NotBeNull();
        dataType.Should().BeOfType<DINT>();
        dataType.Should().Be(123);
    }

    [Test]
    public void GetDataType_StructureType_ShouldHaveExpectedValues()
    {
        var member = new LogixMember("Test", new TIMER { PRE = 123 });

        var dataType = member.DataType;

        dataType.Should().NotBeNull();
        dataType.Should().BeOfType<TIMER>();
        dataType.As<TIMER>().PRE.Should().Be(123);
    }

    [Test]
    public void GetDataType_ArrayType_ShouldHaveExpectedValues()
    {
        var member = new LogixMember("Test", new DINT[] { 1, 2, 3, 4 });

        var dataType = member.DataType;

        dataType.Should().NotBeNull();
        dataType.Should().BeOfType<ArrayType<DINT>>();
        dataType.As<ArrayType>()[0].Should().Be(1);
        dataType.As<ArrayType>()[1].Should().Be(2);
        dataType.As<ArrayType>()[2].Should().Be(3);
        dataType.As<ArrayType>()[3].Should().Be(4);
    }

    [Test]
    public void GetDataType_StringType_ShouldHaveExpectedValues()
    {
        var member = new LogixMember("Test", "This is a test");

        var dataType = member.DataType;

        dataType.Should().NotBeNull();
        dataType.Should().BeOfType<STRING>();
        dataType.Should().Be("This is a test");
    }
    
    [Test]
    public void SetDataType_AtomicToStructure_ShouldThrowInvalidCastException()
    {
        var member = new LogixMember("Test", 123);

        FluentActions.Invoking(() => member.DataType = new TIMER()).Should().Throw<InvalidCastException>();
    }

    [Test]
    public void SetDataType_StructureToAtomic_ShouldThrowArgumentException()
    {
        var member = new LogixMember("Test", new TIMER());

        FluentActions.Invoking(() => member.DataType = 123).Should().Throw<ArgumentException>();
    }
    
    [Test]
    public void SetDataType_FromValueToNull_ShouldBeOfTypeNullType()
    {
        var member = new LogixMember("Test", 123);

        member.DataType = null!;

        member.DataType.Should().BeOfType<NullType>();
    }

    [Test]
    public void SetDataType_FromNullToValue_ShouldHaveExpectedValues()
    {
        var member = new LogixMember("Test", LogixData.Null);

        member.DataType = 123;

        member.DataType.Should().BeOfType<DINT>();
        member.DataType.Should().Be(123);
    }

    [Test]
    public void SetDataType_BOOL_ShouldBeExpectedValue()
    {
        var member = new LogixMember("Test", false);

        member.DataType = true;

        member.DataType.Should().BeOfType<BOOL>();
        member.DataType.As<BOOL>().Radix.Should().Be(Radix.Decimal);
        member.DataType.Should().Be(true);
    }

    [Test]
    public void SetDataType_SINT_ShouldBeExpectedValue()
    {
        var member = new LogixMember("Test", new SINT(12, Radix.Binary));

        member.DataType = 21;

        member.DataType.Should().BeOfType<SINT>();
        member.DataType.As<SINT>().Radix.Should().Be(Radix.Binary);
        member.DataType.Should().Be(21);
    }
    
    [Test]
    public void SetDataType_INT_ShouldBeExpectedValue()
    {
        var member = new LogixMember("Test", new INT(1234, Radix.Binary));

        member.DataType = 4321;

        member.DataType.Should().BeOfType<INT>();
        member.DataType.As<INT>().Radix.Should().Be(Radix.Binary);
        member.DataType.Should().Be(4321);
    }
    
    [Test]
    public void SetDataType_DINT_ShouldBeExpectedValue()
    {
        var member = new LogixMember("Test", 123);

        member.DataType = 321;

        member.DataType.Should().BeOfType<DINT>();
        member.DataType.As<DINT>().Radix.Should().Be(Radix.Decimal);
        member.DataType.Should().Be(321);
    }
    
    [Test]
    public void SetDataType_LINT_ShouldBeExpectedValue()
    {
        var member = new LogixMember("Test", new LINT(long.MaxValue, Radix.Hex));

        member.DataType = new DINT(1000, Radix.Ascii);

        member.DataType.Should().BeOfType<LINT>();
        member.DataType.As<LINT>().Radix.Should().Be(Radix.Hex);
        member.DataType.Should().Be(1000);
    }
    
    [Test]
    public void SetDataType_USINT_ShouldBeExpectedValue()
    {
        var member = new LogixMember("Test", new USINT(12, Radix.Binary));

        member.DataType = 21;

        member.DataType.Should().BeOfType<USINT>();
        member.DataType.As<USINT>().Radix.Should().Be(Radix.Binary);
        member.DataType.Should().Be(21);
    }
    
    [Test]
    public void SetDataType_UINT_ShouldBeExpectedValue()
    {
        var member = new LogixMember("Test", new UINT(1234, Radix.Binary));

        member.DataType = 4321;

        member.DataType.Should().BeOfType<UINT>();
        member.DataType.As<UINT>().Radix.Should().Be(Radix.Binary);
        member.DataType.Should().Be(4321);
    }
    
    [Test]
    public void SetDataType_UDINT_ShouldBeExpectedValue()
    {
        var member = new LogixMember("Test", new UDINT(123));

        member.DataType = 321;

        member.DataType.Should().BeOfType<UDINT>();
        member.DataType.As<UDINT>().Radix.Should().Be(Radix.Decimal);
        member.DataType.Should().Be(321);
    }
    
    [Test]
    public void SetDataType_ULINT_ShouldBeExpectedValue()
    {
        var member = new LogixMember("Test", new ULINT(long.MaxValue, Radix.Hex));

        member.DataType = new DINT(1000, Radix.Ascii);

        member.DataType.Should().BeOfType<ULINT>();
        member.DataType.As<ULINT>().Radix.Should().Be(Radix.Hex);
        member.DataType.Should().Be(1000);
    }
    
    [Test]
    public void SetDataType_REAL_ShouldBeExpectedValue()
    {
        var member = new LogixMember("Test", new REAL(float.MaxValue, Radix.Exponential));

        member.DataType = 123.123f;

        member.DataType.Should().BeOfType<REAL>();
        member.DataType.As<REAL>().Radix.Should().Be(Radix.Exponential);
        member.DataType.Should().Be(123.123f);
    }
    
    [Test]
    public void SetDataType_LREAL_ShouldBeExpectedValue()
    {
        var member = new LogixMember("Test", new LREAL(double.MaxValue));

        member.DataType = 123.123;

        member.DataType.Should().BeOfType<LREAL>();
        member.DataType.As<LREAL>().Radix.Should().Be(Radix.Float);
        member.DataType.Should().Be(123.123);
    }

    [Test]
    public void SetDataType_StructureAsConcreteType_ShouldBeExpectedValue()
    {
        var member = new LogixMember("Test", new TIMER());

        member.DataType = new TIMER { PRE = 5000, EN = true };

        member.DataType.As<TIMER>().PRE.Should().Be(5000);
        member.DataType.As<TIMER>().EN.Should().Be(true);
    }

    [Test]
    public void SetDataType_StructureAsGenericStructureType_ShouldBeExpectedValue()
    {
        var member = new LogixMember("Test", new TIMER());

        member.DataType = new ComplexType("TIMER", new List<LogixMember> { new("PRE", 5000), new("EN", true) });

        member.DataType.As<TIMER>().PRE.Should().Be(5000);
        member.DataType.As<TIMER>().EN.Should().Be(true);
    }

    [Test]
    public void SetDataType_StructureAsDictionary_ShouldBeExpectedValue()
    {
        var member = new LogixMember("Test", new TIMER());

        member.DataType = new Dictionary<string, LogixType>
        {
            { "PRE", 5000 },
            { "EN", true }
        };

        member.DataType.As<TIMER>().PRE.Should().Be(5000);
        member.DataType.As<TIMER>().EN.Should().Be(true);
    }

    [Test]
    public void SetDataType_StructureWithMembersNotInType_ShouldOnlySetMembersThatAreInType()
    {
        var member = new LogixMember("Test", new TIMER());

        member.DataType = new Dictionary<string, LogixType>
        {
            { "Test", 123 },
            { "PRE", 5000 },
            { "Array", new DINT[] { 1, 2, 3, 4 } },
            { "EN", true }
        };

        member.DataType.As<TIMER>().PRE.Should().Be(5000);
        member.DataType.As<TIMER>().ACC.Should().Be(0);
        member.DataType.As<TIMER>().EN.Should().Be(true);
        member.DataType.As<TIMER>().TT.Should().Be(false);
        member.DataType.As<TIMER>().DN.Should().Be(false);
    }

    [Test]
    public void SetDataType_ImmediateAtomic_ShouldRaiseDataChangedEvent()
    {
        var member = new LogixMember("Test", 123);
        var monitor = member.Monitor();

        member.DataType = 321;

        monitor.Should().Raise("DataChanged");
    }

    [Test]
    public void SetDataType_ImmediateStructure_ShouldRaiseDataChangedEvent()
    {
        var member = new LogixMember("Test", new TIMER());
        var monitor = member.Monitor();

        member.DataType = new TIMER { PRE = 5000 };

        monitor.Should().Raise("DataChanged");
    }

    [Test]
    public void SetDataType_ImmediateArray_ShouldRaiseDataChangedEvent()
    {
        var member = new LogixMember("Test", new DINT[] { 1, 2, 3, 4 });
        var monitor = member.Monitor();

        member.DataType = new DINT[] { 4, 3, 2, 1 };

        monitor.Should().Raise("DataChanged");
    }

    [Test]
    public void SetDataType_NestedAtomicMember_ShouldRaiseDataChangedEventAndHaveCorrectValue()
    {
        var member = new LogixMember("Test", 0);
        var monitor = member.Monitor();

        member.DataType.Member("0")!.DataType = true;

        monitor.Should().Raise("DataChanged");
        member.DataType.Should().Be(1);
    }

    [Test]
    public void SetDataType_NestedStructureMember_ShouldRaiseDataChangedEvent()
    {
        var member = new LogixMember("Test", new TIMER());
        var monitor = member.Monitor();

        member.DataType.Member("PRE")!.DataType = 10000;

        monitor.Should().Raise("DataChanged");
    }

    [Test]
    public void SetDataType_NestedStructureMemberStatic_ShouldRaiseDataChangedEvent()
    {
        var member = new LogixMember("Test", new TIMER());
        var monitor = member.Monitor();

        member.DataType.As<TIMER>().PRE = 10000;

        monitor.Should().Raise("DataChanged");
    }

    [Test]
    public Task Serialize_AtomicDataType_ShouldBeVerified()
    {
        var member = new LogixMember("Test", 123);

        var xml = member.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public Task Serialize_StructureDataType_ShouldBeVerified()
    {
        var member = new LogixMember("Test", new TIMER { PRE = 1000, ACC = 2000, EN = 1, DN = 1, TT = 1 });

        var xml = member.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public Task Serialize_StringDataType_ShouldBeVerified()
    {
        var member = new LogixMember("Test", "This is a string type value");

        var xml = member.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public Task Serialize_ArrayOfAtomicDataType_ShouldBeVerified()
    {
        var member = new LogixMember("Test", new REAL[] { 1.1f, 2.2f, 3.3f, 4.4f });

        var xml = member.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public Task Serialize_ArrayOfStructureDataType_ShouldBeVerified()
    {
        var member = new LogixMember("Test", new TIMER[] { new(), new(), new() });

        var xml = member.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public Task Serialize_ArrayOfStringDataType_ShouldBeVerified()
    {
        var member = new LogixMember("Test", new STRING[] { "Test1", "Test2", "Test3" });

        var xml = member.Serialize().ToString();

        return Verify(xml);
    }
}