using System.Xml.Linq;
using FluentAssertions;

namespace L5Sharp.Tests.Core;

[TestFixture]
public class LogixSerializerTests
{
    [Test]
    public void IsRegistered_CoreElement_ShouldBeTrue()
    {
        var result = LogixSerializer.IsRegistered(typeof(Tag));

        result.Should().BeTrue();
    }

    [Test]
    public void IsRegistered_CustomTYpe_ShouldBeTrue()
    {
        var result = LogixSerializer.IsRegistered(typeof(TestElement));

        result.Should().BeTrue();
    }

    [Test]
    public void IsRegistered_CoreElementByName_ShouldBeTrue()
    {
        var result = LogixSerializer.IsRegistered("Module");

        result.Should().BeTrue();
    }

    [Test]
    public void IsRegistered_AlarmAnalogParameters_ShouldBeTrue()
    {
        var result = LogixSerializer.IsRegistered("AlarmAnalogParameters");

        result.Should().BeTrue();
    }

    [Test]
    public void IsRegistered_CustomTypeByName_ShouldBeTrue()
    {
        var result = LogixSerializer.IsRegistered("ChildElement");

        result.Should().BeTrue();
    }

    [Test]
    public Task Serialize_WhenCalled_ShouldBeVerified()
    {
        var test = new DataType("Testing");

        var xml = LogixSerializer.Serialize(test);

        return VerifyXml(xml);
    }

    [Test]
    public void Deserialize_AtomicValue_ShouldBeExpected()
    {
        const string xml = "<DataValue DataType=\"DINT\" Radix=\"Decimal\" Value=\"123\" />";

        var result = XElement.Parse(xml).Deserialize<DINT>();

        result.Should().BeOfType<DINT>();
        result.Should().Be(123);
    }

    [Test]
    public void Register_NullDeserializer_ShouldThrowArgumentNullException()
    {
        FluentActions.Invoking(() => LogixSerializer.Register<Tag>(null!, "Tag"))
            .Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Register_NoNames_ShouldThrowArgumentException()
    {
        FluentActions.Invoking(() => LogixSerializer.Register<Tag>(e => new Tag(e)))
            .Should().Throw<ArgumentException>();
    }

    [Test]
    public void Register_ValidRegistration_ShouldBeRegistered()
    {
        LogixSerializer.Register<TestElement>(e => new TestElement(e), "CustomName");

        LogixSerializer.IsRegistered(typeof(TestElement)).Should().BeTrue();
        LogixSerializer.IsRegistered("CustomName").Should().BeTrue();
    }

    [Test]
    public void NamesFor_RegisteredType_ShouldReturnExpectedNames()
    {
        var names = LogixSerializer.NamesFor(typeof(Tag));

        names.Should().Contain("Tag");
    }

    [Test]
    public void NamesFor_Interface_ShouldReturnImplementingTypesNames()
    {
        // Tag implements ILogixElement (indirectly through LogixElement)
        var names = LogixSerializer.NamesFor(typeof(ILogixElement));

        names.Should().Contain("Tag");
        names.Should().Contain("Module");
    }

    [Test]
    public void NamesFor_NonRegisteredType_ShouldBeEmpty()
    {
        var names = LogixSerializer.NamesFor(typeof(string));

        names.Should().BeEmpty();
    }

    [Test]
    public void Deserialize_NullElement_ShouldThrowArgumentNullException()
    {
        XElement element = null!;

        FluentActions.Invoking(() => _ = element.Deserialize()).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Deserialize_UnknownElement_ShouldThrowNotSupportedException()
    {
        var element = new XElement("UnknownElement");

        FluentActions.Invoking(() => _ = element.Deserialize()).Should().Throw<NotSupportedException>();
    }

    [Test]
    public void Deserialize_IncompatibleType_ShouldThrowInvalidCastException()
    {
        var element = new XElement("Tag", new XAttribute("Name", "TestTag"));

        FluentActions.Invoking(() => _ = element.Deserialize<Module>()).Should().Throw<InvalidCastException>();
    }

    [Test]
    public void Deserialize_DataValueMember_ShouldBeExpected()
    {
        const string xml = "<DataValueMember DataType=\"SINT\" Radix=\"Decimal\" Value=\"10\" />";

        var result = XElement.Parse(xml).Deserialize<SINT>();

        result.Should().BeOfType<SINT>();
        result.Should().Be(10);
    }

    [Test]
    public void Deserialize_Structure_ShouldBeExpected()
    {
        const string xml =
            "<Structure DataType=\"TIMER\"><DataValueMember Name=\"PRE\" DataType=\"DINT\" Value=\"1000\"/></Structure>";

        var result = XElement.Parse(xml).Deserialize<StructureData>();

        result.Should().NotBeNull();
        result.Name.Should().Be("TIMER");
    }

    [Test]
    public void Deserialize_StringData_ShouldBeExpected()
    {
        const string xml = "<Data Format=\"String\"><![CDATA[Test String]]></Data>";

        var result = XElement.Parse(xml).Deserialize();

        result.Should().BeOfType<StringData>();
        result.ToString().Should().Be("Test String");
    }

    [Test]
    public void Deserialize_ArrayElement_ShouldBeExpected()
    {
        const string xml = "<Element Index=\"[0]\" Value=\"1.2\" DataType=\"REAL\" />";

        var result = XElement.Parse(xml).Deserialize();

        result.Should().BeOfType<REAL>();
        ((REAL)result).Should().Be(1.2f);
    }
}