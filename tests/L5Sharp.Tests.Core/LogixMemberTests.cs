using System.Xml.Linq;
using FluentAssertions;

// ReSharper disable UseObjectOrCollectionInitializer

namespace L5Sharp.Tests.Core;

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
        member.Value.Should().BeOfType<BOOL>();
    }

    [Test]
    public void New_NullName_ShouldThrowException()
    {
        FluentActions.Invoking(() => new LogixMember(null!, new BOOL())).Should().Throw<ArgumentException>();
    }

    [Test]
    public void New_NullType_ShouldThrowException()
    {
        FluentActions.Invoking(() => new LogixMember("Test", null!)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void New_ValueMemberElement_ShouldHaveExpectedProperties()
    {
        const string xml = "<DataValueMember Name=\"Test\" DataType=\"DINT\" Radix=\"Decimal\" Value=\"123\" />";
        var element = XElement.Parse(xml);

        var member = new LogixMember(element);

        member.Name.Should().Be("Test");
        member.Value.Should().BeOfType<DINT>();
        member.Value.Should().Be(123);
    }

    [Test]
    public void New_DataValueElement_ShouldHaveExpectedProperties()
    {
        const string xml = "<DataValue DataType=\"DINT\" Radix=\"Decimal\" Value=\"123\" />";
        var element = XElement.Parse(xml).Deserialize<LogixData>();

        var member = new LogixMember("Test", element);

        member.Name.Should().Be("Test");
        member.Value.Should().BeOfType<DINT>();
        member.Value.Should().Be(123);
    }

    [Test]
    public void New_StructureElement_ShouldHaveExpectedProperties()
    {
        const string xml =
            """
            <Structure DataType="TIMER">
                <DataValueMember Name="PRE" DataType="DINT" Radix="Decimal" Value="1000" />
                <DataValueMember Name="ACC" DataType="DINT" Radix="Decimal" Value="2000" />
                <DataValueMember Name="EN" DataType="BOOL" Value="1" />
                <DataValueMember Name="TT" DataType="BOOL" Value="1" />
                <DataValueMember Name="DN" DataType="BOOL" Value="1" />
            </Structure>
            """;
        var element = XElement.Parse(xml);
        var type = element.Deserialize<LogixData>();

        var member = new LogixMember("Test", type);

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
        const string xml =
            """
            <StructureMember Name="Test" DataType="TIMER">
                <DataValueMember Name="PRE" DataType="DINT" Radix="Decimal" Value="1000" />
                <DataValueMember Name="ACC" DataType="DINT" Radix="Decimal" Value="2000" />
                <DataValueMember Name="EN" DataType="BOOL" Value="1" />
                <DataValueMember Name="TT" DataType="BOOL" Value="1" />
                <DataValueMember Name="DN" DataType="BOOL" Value="1" />
            </StructureMember>
            """;
        var element = XElement.Parse(xml);

        var member = new LogixMember(element);

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

        var member = new LogixMember(element);

        member.Name.Should().Be("Test");
        member.Value.Should().BeOfType<StructureData>();
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
        const string xml =
            """
            <StructureMember Name="Test" DataType="STRING">
                <DataValueMember Name="LEN" DataType="DINT" Radix="Decimal" Value="27" />
                <DataValueMember Name="DATA" DataType="STRING" Radix="ASCII"><![CDATA[This is a string type value]]></DataValueMember>
            </StructureMember>
            """;
        var element = XElement.Parse(xml);

        var member = new LogixMember(element);

        member.Name.Should().Be("Test");
        member.Value.As<STRING>().Should().HaveCount(27);
        member.Value.As<STRING>().Should().BeEquivalentTo("This is a string type value");
    }

    [Test]
    public void New_ArrayOfAtomicMemberElement_ShouldHaveExpectedProperties()
    {
        const string xml =
            """
            <Array DataType="REAL" Dimensions="4" Radix="Float">
                <Element Index="[0]" Value="1.1" />
                <Element Index="[1]" Value="2.2" />
                <Element Index="[2]" Value="3.3" />
                <Element Index="[3]" Value="4.4" />
            </Array>
            """;

        var element = XElement.Parse(xml);
        var type = element.Deserialize<LogixData>();

        var member = new LogixMember("Test", type);

        member.Name.Should().Be("Test");
        member.Value.As<ArrayData>()[0].Should().Be(1.1f);
        member.Value.As<ArrayData>()[1].Should().Be(2.2f);
        member.Value.As<ArrayData>()[2].Should().Be(3.3f);
        member.Value.As<ArrayData>()[3].Should().Be(4.4f);
    }

    [Test]
    public void New_ArrayOfStructureMemberElement_ShouldHaveExpectedProperties()
    {
        const string xml =
            """
            <Array DataType="TIMER" Dimensions="3">
                <Element Index="[0]">
                    <Structure DataType="TIMER">
                        <DataValueMember Name="PRE" DataType="DINT" Radix="Decimal" Value="0" />
                        <DataValueMember Name="ACC" DataType="DINT" Radix="Decimal" Value="0" />
                        <DataValueMember Name="EN" DataType="BOOL" Value="0" />
                        <DataValueMember Name="TT" DataType="BOOL" Value="0" />
                        <DataValueMember Name="DN" DataType="BOOL" Value="0" />
                    </Structure>
                </Element>
                <Element Index="[1]">
                    <Structure DataType="TIMER">
                        <DataValueMember Name="PRE" DataType="DINT" Radix="Decimal" Value="0" />
                        <DataValueMember Name="ACC" DataType="DINT" Radix="Decimal" Value="0" />
                        <DataValueMember Name="EN" DataType="BOOL" Value="0" />
                        <DataValueMember Name="TT" DataType="BOOL" Value="0" />
                        <DataValueMember Name="DN" DataType="BOOL" Value="0" />
                    </Structure>
                </Element>
                <Element Index="[2]">
                    <Structure DataType="TIMER">
                        <DataValueMember Name="PRE" DataType="DINT" Radix="Decimal" Value="0" />
                        <DataValueMember Name="ACC" DataType="DINT" Radix="Decimal" Value="0" />
                        <DataValueMember Name="EN" DataType="BOOL" Value="0" />
                        <DataValueMember Name="TT" DataType="BOOL" Value="0" />
                        <DataValueMember Name="DN" DataType="BOOL" Value="0" />
                    </Structure>
                </Element>
            </Array>
            """;
        
        var element = XElement.Parse(xml);
        var type = element.Deserialize<LogixData>();

        var member = new LogixMember("Test", type);

        member.Name.Should().Be("Test");
        member.Value.As<ArrayData>()[0].Should().BeOfType<TIMER>();
        member.Value.As<ArrayData>()[1].Should().BeOfType<TIMER>();
        member.Value.As<ArrayData>()[2].Should().BeOfType<TIMER>();
    }

    [Test]
    public void New_ArrayOfStringMemberElement_ShouldHaveExpectedProperties()
    {
        const string xml =
            """
            <Array DataType="STRING" Dimensions="3">
                <Element Index="[0]">
                    <Structure DataType="STRING">
                        <DataValueMember Name="LEN" DataType="DINT" Radix="Decimal" Value="5" />
                        <DataValueMember Name="DATA" DataType="STRING" Radix="ASCII"><![CDATA[Test1]]></DataValueMember>
                    </Structure>
                </Element>
                <Element Index="[1]">
                    <Structure DataType="STRING">
                        <DataValueMember Name="LEN" DataType="DINT" Radix="Decimal" Value="5" />
                        <DataValueMember Name="DATA" DataType="STRING" Radix="ASCII"><![CDATA[Test2]]></DataValueMember>
                    </Structure>
                </Element>
                <Element Index="[2]">
                    <Structure DataType="STRING">
                        <DataValueMember Name="LEN" DataType="DINT" Radix="Decimal" Value="5" />
                        <DataValueMember Name="DATA" DataType="STRING" Radix="ASCII"><![CDATA[Test3]]></DataValueMember>
                    </Structure>
                </Element>
            </Array>
            """;
        var element = XElement.Parse(xml);
        var type = element.Deserialize<LogixData>();

        var member = new LogixMember("Test", type);

        member.Name.Should().Be("Test");
        member.Value.As<ArrayData>()[0].As<StringData>().Should().BeEquivalentTo("Test1");
        member.Value.As<ArrayData>()[1].As<StringData>().Should().BeEquivalentTo("Test2");
        member.Value.As<ArrayData>()[2].As<StringData>().Should().BeEquivalentTo("Test3");
    }

    [Test]
    public void GetDataType_AtomicType_ShouldHaveExpectedValues()
    {
        var member = new LogixMember("Test", 123);

        var dataType = member.Value;

        dataType.Should().NotBeNull();
        dataType.Should().BeOfType<DINT>();
        dataType.Should().Be(123);
    }

    [Test]
    public void GetDataType_StructureType_ShouldHaveExpectedValues()
    {
        var member = new LogixMember("Test", new TIMER { PRE = 123 });

        var dataType = member.Value;

        dataType.Should().NotBeNull();
        dataType.Should().BeOfType<TIMER>();
        dataType.As<TIMER>().PRE.Should().Be(123);
    }

    [Test]
    public void GetDataType_ArrayType_ShouldHaveExpectedValues()
    {
        var member = new LogixMember("Test", new DINT[] { 1, 2, 3, 4 });

        var dataType = member.Value;

        dataType.Should().NotBeNull();
        dataType.As<ArrayData>()[0].Should().Be(1);
        dataType.As<ArrayData>()[1].Should().Be(2);
        dataType.As<ArrayData>()[2].Should().Be(3);
        dataType.As<ArrayData>()[3].Should().Be(4);
    }

    [Test]
    public void GetDataType_StringType_ShouldHaveExpectedValues()
    {
        var member = new LogixMember("Test", "This is a test");

        var data = member.Value;

        data.Should().Be("This is a test");
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
        var member = new LogixMember("Test", ArrayData.New<REAL>([1.1f, 2.2f, 3.3f, 4.4f]));

        var xml = member.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public Task Serialize_ArrayOfStructureDataType_ShouldBeVerified()
    {
        var member = new LogixMember("Test", ArrayData.New<TIMER>([new TIMER(), new TIMER(), new TIMER()]));

        var xml = member.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public Task Serialize_ArrayOfStringDataType_ShouldBeVerified()
    {
        var member = new LogixMember("Test", new STRING[] { "Test1", "Test2", "Test3" }.ToArrayData());

        var xml = member.Serialize().ToString();

        return Verify(xml);
    }

    #region InternalsTests

    [Test]
    public void New_ElementOverload_ShouldHaveExpectedValues()
    {
        const string xml = "<DataValueMember Name=\"Test\" DataType=\"DINT\" Radix=\"Decimal\" Value=\"123\" />";
        var element = XElement.Parse(xml);

        var member = new LogixMember(element);

        member.Name.Should().Be("Test");
        member.Value.Should().BeOfType<DINT>();
        member.Value.Should().Be(123);
    }

    #endregion
}