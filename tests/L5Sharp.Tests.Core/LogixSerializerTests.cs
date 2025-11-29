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
}