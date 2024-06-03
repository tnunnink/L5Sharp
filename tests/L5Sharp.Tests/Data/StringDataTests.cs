using System.Xml.Linq;
using FluentAssertions;

namespace L5Sharp.Tests.Data;

[TestFixture]
public class StringDataTests
{
    [Test]
    public void New_NameAndValueOverload_ShouldHaveExpectedValues()
    {
        var type = new StringData("MyStringType", "This is the test value");

        type.Name.Should().Be("MyStringType");
        type.ToString().Should().Be("This is the test value");
        type.LEN.Should().Be(22);
        type.DATA.Should().NotBeEmpty();
        type.Members.Should().HaveCount(2);
    }
    
    [Test]
    public void New_NullName_NameShouldBeEmpty()
    {
        var type = new StringData(null!, "This is the test value");

        type.Name.Should().BeEmpty();
    }
    
    [Test]
    public void New_EmptyName_NameShouldBeEmpty()
    {
        var type = new StringData(string.Empty, "This is the test value");

        type.Name.Should().BeEmpty();
    }

    [Test]
    public void New_NullValue_ShouldHaveEmptyStringValue()
    {
        var type = new StringData("Test", null!);

        type.ToString().Should().BeEmpty();
    }

    [Test]
    public void New_EmptyValue_ShouldBeEmpty()
    {
        var type = new StringData("Test", "");

        type.ToString().Should().BeEmpty();
    }

    [Test]
    public void New_ValueOverloadEmpty_ShouldBeExpected()
    {
        var type = new StringData(string.Empty);
        
        type.ToString().Should().BeEmpty();
    }

    [Test]
    public void New_ValueOverloadNonEmpty_ShouldBeExpected()
    {
        var type = new StringData("This is the value");
        
        type.ToString().Should().Be("This is the value");
    }
    
    [Test]
    public void New_ValueOverload_ShouldHaveExpectedLength()
    {
        var type = new StringData("This is the value");
        
        type.LEN.Should().Be(17);
        type.DATA.Dimensions.Length.Should().Be(17);
    }

    [Test]
    public void New_ElementDataEmptyString_ShouldHaveExpectedValue()
    {
        const string xml = "<Data Format=\"String\" Length=\"0\"><![CDATA[]]></Data>";
        var element = XElement.Parse(xml);

        var type = new StringData(element);

        type.Should().BeEmpty();
    }

    [Test]
    public void New_ElementDataWithValue_ShouldHaveExpectedValue()
    {
        const string xml = "<Data Format=\"String\" Length=\"24\"><![CDATA[This is the string value]]></Data>";
        var element = XElement.Parse(xml);

        var type = new StringData(element);

        type.Name.Should().BeEmpty();
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

        var type = new StringData(element);

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

        var type = new StringData(element);

        type.Name.Should().Be("STRING");
        type.LEN.Should().Be(14);
        type.Should().BeEquivalentTo("This is a test");
    }

    [Test]
    public void ToString_Empty_ShouldBeExpected()
    {
        var type = new StringData(string.Empty);

        var value = type.ToString();

        value.Should().Be("");
    }
    
    [Test]
    public void ToString_ValueNoSpecialCharacters_ShouldBeExpected()
    {
        var type = new StringData("This is a test");

        var value = type.ToString();

        value.Should().Be("This is a test");
    }
    
    [Test]
    public void ToString_ValueSpecialCharacters_ShouldBeExpected()
    {
        var type = new StringData("This is a $'special character$' test");

        var value = type.ToString();

        value.Should().Be("This is a $'special character$' test");
    }

    [Test]
    public void Members_GetValue_ShouldBeExpected()
    {
        var type = new StringData("TestType", "This is a test value");

        var member = type.Members.ToList();

        var len = member[0];
        len.Name.Should().Be("LEN");
        len.Value.Should().Be(20);

        var data = member[1];
        data.Name.Should().Be("DATA");
        data.Value.Should().BeOfType<ArrayData<SINT>>();

        var members = data.Value.Members.ToList();
        members.Should().NotBeEmpty();
    }
    
    [Test]
    public void LEN_GetValueEmpty_ShouldBeExpected()
    {
        var type = new StringData("TestType", string.Empty);

        var len = type.LEN;

        len.Should().Be(0);
    }

    [Test]
    public void LEN_GetValueNonEmpty_ShouldBeExpected()
    {
        var type = new StringData("TestType", "this is a test");

        var len = type.LEN;

        len.Should().Be(14);
    }
    
    [Test]
    public void DATA_GetValueEmpty_ShouldBeExpected()
    {
        var type = new StringData("TestType", string.Empty);

        var data = type.DATA;

        data.Should().BeEmpty();
    }

    [Test]
    public void DATA_GetValueNonEmpty_ShouldBeExpected()
    {
        var type = new StringData("TestType", "This is a test");

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
    public Task Serialize_Default_ShouldBeVerified()
    {
        var type = new StringData("MyStringType", string.Empty);

        var xml = type.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public Task Serialize_WithValues_ShouldBeVerified()
    {
        var type = new StringData("MyStringType", "This is the string value");

        var xml = type.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public Task SerializeStructure_Default_ShouldBeVerified()
    {
        var type = new StringData("MyStringType", string.Empty);

        var xml = type.SerializeStructure().ToString();

        return Verify(xml);
    }

    [Test]
    public Task SerializeStructure_WithValues_ShouldBeVerified()
    {
        var type = new StringData("MyStringType", "This is the string value");

        var xml = type.SerializeStructure().ToString();

        return Verify(xml);
    }

    [Test]
    public void GetHashCode_WhenCalled_ShouldBeExpected()
    {
        var expected = "This is a test".GetHashCode();
        var type = new StringData("MyStringType", "This is a test");

        var code = type.GetHashCode();

        code.Should().Be(expected);
    }

    [Test]
    public void Operator_StringType_ShouldBeExpected()
    {
        var type = new StringData("MyStringType", "This is a test");

        string value = type;

        value.Should().Be("This is a test");
    }

    [Test]
    public void Equals_EqualStringType_ShouldBeTrue()
    {
        var a = new StringData("MyStringType", "Test");
        var b = new StringData("MyStringType", "Test");

        var result = a.Equals(b);

        result.Should().BeTrue();
    }

    [Test]
    public void Equals_NotEqualStringType_ShouldBeFalse()
    {
        var a = new StringData("MyStringType", "Test1");
        var b = new StringData("MyStringType", "Test2");

        var result = a.Equals(b);

        result.Should().BeFalse();
    }

    [Test]
    public void Equals_EqualString_ShouldBeTrue()
    {
        var a = new StringData("MyStringType", "Test");
        const string b = "Test";

        // ReSharper disable once SuspiciousTypeConversion.Global
        var result = a.Equals(b);

        result.Should().BeTrue();
    }

    [Test]
    public void Equals_NotEqualString_ShouldBeFalse()
    {
        var a = new StringData("MyStringType", "Test1");
        const string b = "Test2";

        // ReSharper disable once SuspiciousTypeConversion.Global
        var result = a.Equals(b);

        result.Should().BeFalse();
    }

    [Test]
    public void Equals_null_ShouldBeFalse()
    {
        var a = new StringData("MyStringType", "Test1");

        var result = a.Equals(null!);

        result.Should().BeFalse();
    }
}