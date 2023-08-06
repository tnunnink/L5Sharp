using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Core.Tests.Types;

[TestFixture]
public class StringTypeTests
{
    [Test]
    public void New_NullName_ShouldThrowException()
    {
        FluentActions.Invoking(() => new StringType(null!, "")).Should().Throw<ArgumentException>();
    }

    [Test]
    public void New_NullValue_ShouldHaveEmptyString()
    {
        var type = new StringType("Test", null!);

        type.Should().BeEmpty(string.Empty);
    }

    [Test]
    public void New_EmptyString_ShouldNotBeNull()
    {
        var type = new StringType("Test", "");

        type.Should().NotBeNull();
    }

    [Test]
    public void New_Default_ShouldHaveExpectedValues()
    {
        var type = new StringType();

        type.Name.Should().Be(nameof(StringType));
        type.Family.Should().Be(DataTypeFamily.String);
        type.Class.Should().Be(DataTypeClass.Unknown);
        type.Members.Should().HaveCount(2);
        type.LEN.Should().Be(0);
        type.DATA.As<ArrayType<SINT>>();
    }
    
    [Test]
    public void New_ElementDataEmptyString_ShouldHaveExpectedValue()
    {
        const string xml = "<Data Format=\"String\" Length=\"0\"><![CDATA[]]></Data>";
        var element = XElement.Parse(xml);

        var type = new StringType(element);

        type.Should().BeEmpty();
    }

    [Test]
    public void New_ElementDataWithValue_ShouldHaveExpectedValue()
    {
        const string xml = "<Data Format=\"String\" Length=\"24\"><![CDATA[This is the string value]]></Data>";
        var element = XElement.Parse(xml);

        var type = new StringType(element);

        type.Name.Should().Be(nameof(StringType));
        type.LEN.Should().Be(24);
        type.Should().BeEquivalentTo("This is the string value");
    }
    
    [Test]
    public void New_ElementStringStructure_ShouldHaveExpectedValue()
    {
        const string xml = @"<Structure DataType=""STRING"">
                                <DataValueMember Name=""LEN"" DataType=""DINT"" Radix=""Decimal"" Value=""14""/>
                                <DataValueMember Name=""DATA"" DataType=""STRING"" Radix=""ASCII"">
                                <![CDATA[This is a test]]>
                                </DataValueMember>
                            </Structure>";
        var element = XElement.Parse(xml);

        var type = new StringType(element);
        
        type.Name.Should().Be("STRING");
        type.LEN.Should().Be(14);
        type.Should().BeEquivalentTo("This is a test");
    }
    
    [Test]
    public void New_ElementStringStructureMember_ShouldHaveExpectedValue()
    {
        const string xml = @"<StructureMember Name=""StringMember"" DataType=""STRING"">
                                <DataValueMember Name=""LEN"" DataType=""DINT"" Radix=""Decimal"" Value=""14""/>
                                <DataValueMember Name=""DATA"" DataType=""STRING"" Radix=""ASCII"">
                                <![CDATA[This is a test]]>
                                </DataValueMember>
                            </StructureMember>";
        var element = XElement.Parse(xml);

        var type = new StringType(element);
        
        type.Name.Should().Be("STRING");
        type.LEN.Should().Be(14);
        type.Should().BeEquivalentTo("This is a test");
    }

    [Test]
    public void LEN_GetValue_ShouldBeExpected()
    {
        var type = new StringType("this is a test");

        var len = type.LEN;

        len.Should().Be(14);
    }

    [Test]
    public void LEN_SetValue_ShouldStillBeSameAsStringLength()
    {
        var type = new StringType("this is a test");

        type.LEN = 20;

        type.LEN.Should().Be(14);
    }

    [Test]
    public void DATA_GetValue_ShouldBeExpected()
    {
        var type = new StringType("This is a test");

        var data = type.DATA;

        data[0].Should().Be('T');
        data[1].Should().Be('h');
        data[2].Should().Be('i');
        data[3].Should().Be('s');
        data[4].Should().Be(' ');
        data[5].Should().Be('i');
        data[6].Should().Be('s');
        data[7].Should().Be(' ');
        data[8].Should().Be('a');
        data[9].Should().Be(' ');
        data[10].Should().Be('t');
        data[11].Should().Be('e');
        data[12].Should().Be('s');
        data[13].Should().Be('t');
    }

    [Test]
    public void DATA_SetValue_ShouldBeExpected()
    {
        var type = new StringType("This is a test");

        type.DATA = new ArrayType<SINT>(new SINT[] { 1, 2, 3, 4 });

        type.DATA[0].Should().Be(1);
        type.DATA[1].Should().Be(2);
        type.DATA[2].Should().Be(3);
        type.DATA[3].Should().Be(4);
    }

    [Test]
    public void DATA_SetToSintArrayOrAsciiCharacters_ShouldBeExpected()
    {
        var type = new StringType("This is a test");

        type.DATA = new SINT[] { "'T'", "'h'", "'a'", "'t'" };

        type.Should().BeEquivalentTo("That is a test");
    }

    [Test]
    public Task Serialize_Default_ShouldBeVerified()
    {
        var type = new StringType();

        var xml = type.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public Task Serialize_WithValues_ShouldBeVerified()
    {
        var type = new StringType("MyStringType", "This is the string value");

        var xml = type.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public Task SerializeStructure_Default_ShouldBeVerified()
    {
        var type = new StringType();

        var xml = type.SerializeStructure().ToString();

        return Verify(xml);
    }
    
    [Test]
    public Task SerializeStructure_WithValues_ShouldBeVerified()
    {
        var type = new StringType("MyStringType", "This is the string value");

        var xml = type.SerializeStructure().ToString();

        return Verify(xml);
    }

    [Test]
    public void GetHashCode_WhenCalled_ShouldBeExpected()
    {
        var expected = "This is a test".GetHashCode();
        var type = new StringType("This is a test");

        var code = type.GetHashCode();

        code.Should().Be(expected);
    }

    [Test]
    public void Operator_String_ShouldBeExpected()
    {
        StringType type = "This is a test";

        type.Name.Should().Be(nameof(StringType));
        type.Should().BeEquivalentTo("This is a test");
    }

    [Test]
    public void Operator_StringType_ShouldBeExpected()
    {
        var type = new StringType("This is a test");

        string value = type;

        value.Should().BeEquivalentTo("This is a test");
    }

    [Test]
    public void Equals_EqualStringType_ShouldBeTrue()
    {
        StringType a = "Test";
        StringType b = "Test";

        var result = a.Equals(b);

        result.Should().BeTrue();
    }
    
    [Test]
    public void Equals_NotEqualStringType_ShouldBeFalse()
    {
        StringType a = "Test1";
        StringType b = "Test2";

        var result = a.Equals(b);

        result.Should().BeFalse();
    }
    
    [Test]
    public void Equals_EqualString_ShouldBeTrue()
    {
        StringType a = "Test";
        const string b = "Test";

        // ReSharper disable once SuspiciousTypeConversion.Global
        var result = a.Equals(b);

        result.Should().BeTrue();
    }
    
    [Test]
    public void Equals_NotEqualString_ShouldBeFalse()
    {
        StringType a = "Test1";
        const string b = "Test2";

        // ReSharper disable once SuspiciousTypeConversion.Global
        var result = a.Equals(b);

        result.Should().BeFalse();
    }

    [Test]
    public void Equals_null_ShouldBeFalse()
    {
        StringType a = "Test";
        
        var result = a.Equals(null!);

        result.Should().BeFalse();
    }
}