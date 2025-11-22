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
    public void IsRegistered_DataElement_ShouldBeTrue()
    {
        var result = LogixSerializer.IsRegistered(typeof(TIMER));

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
    public void IsRegistered_DataElementByName_ShouldBeTrue()
    {
        var result = LogixSerializer.IsRegistered("COUNTER");

        result.Should().BeTrue();
    }

    [Test]
    public void IsRegistered_CustomTypeByName_ShouldBeTrue()
    {
        var result = LogixSerializer.IsRegistered("ChildElement");

        result.Should().BeTrue();
    }
}