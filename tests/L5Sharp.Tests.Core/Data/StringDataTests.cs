using System.Xml.Linq;
using FluentAssertions;

namespace L5Sharp.Tests.Core.Data;

[TestFixture]
public class StringDataTests
{
    [Test]
    public void New_NullElement_ShouldThrowException()
    {
        FluentActions.Invoking(() => new StringData(null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void New_NullName_ShouldThrowException()
    {
        FluentActions.Invoking(() => new StringData((string)null!)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void New_NullNameWithValue_ShouldThrowException()
    {
        FluentActions.Invoking(() => new StringData(null!, "This is a test")).Should().Throw<ArgumentException>();
    }

    [Test]
    public void New_NameAndEmptyValue_ShouldBeEmpty()
    {
        var data = new StringData("MyString", string.Empty);

        data.Name.Should().Be("MyString");
        data.Members.Should().BeEmpty();
        data.Should().BeEmpty();
    }

    [Test]
    public void New_ValueOverload_ShouldBeExpected()
    {
        var data = new StringData("MyString", "This is the value");

        data.Name.Should().Be("MyString");
        data.Should().BeEquivalentTo("This is the value");
    }

    [Test]
    public void ToString_Empty_ShouldBeExpected()
    {
        var data = new StringData("MyString", string.Empty);

        var value = data.ToString();

        value.Should().BeEmpty();
    }

    [Test]
    public void ToString_ValueNoSpecialCharacters_ShouldBeExpected()
    {
        var data = new StringData("MyString", "This is a test");

        var value = data.ToString();

        value.Should().Be("This is a test");
    }

    [Test]
    public void ToString_ValueSpecialCharacters_ShouldBeExpected()
    {
        var data = new StringData("MyString", "This is a $'special character$' test");

        var value = data.ToString();

        value.Should().Be("This is a $'special character$' test");
    }

    [Test]
    public void Length_EmptyValue_ShouldBeExpected()
    {
        var data = new StringData("MyString", string.Empty);

        data.Length.Should().Be(0);
    }

    [Test]
    public void Length_EmptyValue_ShouldHaveExpectedCount()
    {
        var data = new StringData("MyString", "this is a test");

        data.Length.Should().Be(14);
    }

    [Test]
    public void New_ElementDataEmptyString_ShouldHaveExpectedValue()
    {
        const string xml = "<Data Format=\"String\" Length=\"0\"><![CDATA[]]></Data>";
        var element = XElement.Parse(xml);

        var data = new StringData(element);

        data.Should().BeEmpty();
    }

    [Test]
    public void New_ElementDataWithValue_ShouldHaveExpectedValue()
    {
        const string xml = "<Data Format=\"String\" Length=\"24\"><![CDATA[This is the string value]]></Data>";
        var element = XElement.Parse(xml);

        var data = new StringData(element);

        data.Should().BeEquivalentTo("This is the string value");
    }

    [Test]
    public void New_DataValueMemberElement_ShouldHaveExpectedValue()
    {
        const string xml =
            """
            <DataValueMember Name="DATA" DataType="STRING" Radix="ASCII">
                <![CDATA['This is a test']]>
            </DataValueMember>
            """;
        var element = XElement.Parse(xml);

        var data = new StringData(element);

        data.Name.Should().Be("STRING");
        data.Should().BeEquivalentTo("This is a test");
    }

    [Test]
    public void New_ElementStringStructure_ShouldHaveExpectedValue()
    {
        const string xml =
            """
            <Structure DataType="STRING">
                <DataValueMember Name="LEN" DataType="DINT" Radix="Decimal" Value="14"/>
                <DataValueMember Name="DATA" DataType="STRING" Radix="ASCII">
                    <![CDATA[This is a test]]>
                </DataValueMember>
            </Structure>
            """;
        var element = XElement.Parse(xml);

        var data = new StringData(element);

        data.Name.Should().Be("STRING");
        data.Should().BeEquivalentTo("This is a test");
    }

    [Test]
    public void New_ElementStringStructureMember_ShouldHaveExpectedValue()
    {
        const string xml =
            """
            <StructureMember Name="StringMember" DataType="STRING">
                <DataValueMember Name="LEN" DataType="DINT" Radix="Decimal" Value="14"/>
                <DataValueMember Name="DATA" DataType="STRING" Radix="ASCII">
                    <![CDATA[This is a test]]>
                </DataValueMember>
            </StructureMember>
            """;
        var element = XElement.Parse(xml);

        var type = new StringData(element);

        type.Name.Should().Be("STRING");
        type.Should().BeEquivalentTo("This is a test");
    }


    [Test]
    public void ToArray_NonEmpty_ShouldBeExpected()
    {
        var data = new StringData("MyString", "This is a test");

        var characters = data.ToArray();

        characters[0].Should().Be('T');
        characters[1].Should().Be('h');
        characters[2].Should().Be('i');
        characters[3].Should().Be('s');
        characters[4].Should().Be(' ');
        characters[5].Should().Be('i');
        characters[6].Should().Be('s');
        characters[7].Should().Be(' ');
        characters[8].Should().Be('a');
        characters[9].Should().Be(' ');
        characters[10].Should().Be('t');
        characters[11].Should().Be('e');
        characters[12].Should().Be('s');
        characters[13].Should().Be('t');
    }

    [Test]
    public Task Serialize_NameAndEmptyValue_ShouldBeVerified()
    {
        var data = new StringData("MyString", string.Empty);

        var xml = data.Serialize().ToString();

        return VerifyXml(xml);
    }

    [Test]
    public Task Serialize_WithValue_ShouldBeVerified()
    {
        var data = new StringData("MyString", "This is the string value");

        var xml = data.Serialize().ToString();

        return VerifyXml(xml);
    }

    [Test]
    public Task SerializeStructure_EmptyValue_ShouldBeVerified()
    {
        var data = new StringData("MyString", string.Empty);

        var xml = data.ToStructureElement().ToString();

        return VerifyXml(xml);
    }

    [Test]
    public Task SerializeStructure_WithValue_ShouldBeVerified()
    {
        var data = new StringData("MyString", "This is the string value");

        var xml = data.ToStructureElement().ToString();

        return VerifyXml(xml);
    }

    [Test]
    public void GetHashCode_WhenCalled_ShouldBeExpected()
    {
        var expected = "This is a test".GetHashCode();
        var type = new StringData("MyString", "This is a test");

        var code = type.GetHashCode();

        code.Should().Be(expected);
    }

    [Test]
    public void Equals_EqualStringType_ShouldBeTrue()
    {
        var a = new StringData("MyString", "Test");
        var b = new StringData("MyString", "Test");

        var result = a.Equals(b);

        result.Should().BeTrue();
    }

    [Test]
    public void Equals_NotEqualStringType_ShouldBeFalse()
    {
        var a = new StringData("MyString", "Test1");
        var b = new StringData("MyString", "Test2");

        var result = a.Equals(b);

        result.Should().BeFalse();
    }

    [Test]
    public void Equals_EqualString_ShouldBeTrue()
    {
        var a = new StringData("MyString", "Test");
        const string b = "Test";

        // ReSharper disable once SuspiciousTypeConversion.Global
        var result = a.Equals(b);

        result.Should().BeTrue();
    }

    [Test]
    public void Equals_NotEqualString_ShouldBeFalse()
    {
        var a = new StringData("MyString", "Test1");
        const string b = "Test2";

        // ReSharper disable once SuspiciousTypeConversion.Global
        var result = a.Equals(b);

        result.Should().BeFalse();
    }

    [Test]
    public void Equals_null_ShouldBeFalse()
    {
        var a = new StringData("MyString", "Test1");

        var result = a.Equals(null!);

        result.Should().BeFalse();
    }
}