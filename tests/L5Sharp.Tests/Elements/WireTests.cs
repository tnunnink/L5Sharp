using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Elements;
using L5Sharp.Samples;
using L5Sharp.Utilities;

namespace L5Sharp.Tests.Elements;

[TestFixture]
public class WireTests
{
    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var wire = new Wire();

        wire.Should().NotBeNull();
    }

    [Test]
    public void New_Default_ShouldHaveExpectedValues()
    {
        var wire = new Wire();

        wire.FromID.Should().Be(0);
        wire.ToID.Should().Be(0);
        wire.FromParam.Should().BeNull();
        wire.ToParam.Should().BeNull();
    }

    [Test]
    public void New_ValidElement_ShouldNotBeNull()
    {
        var wire = new Wire(new XElement(L5XName.Wire));

        wire.Should().NotBeNull();
    }

    [Test]
    public void New_NullElement_ShouldThrowArgumentNullException()
    {
        FluentActions.Invoking(() => new Wire(null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void New_Overloaded_ShouldHaveExpectedValues()
    {
        var wire = new Wire
        {
            FromID = 1,
            ToID = 2,
            FromParam = "In",
            ToParam = "Out"
        };

        wire.FromID.Should().Be(1);
        wire.ToID.Should().Be(2);
        wire.FromParam.Should().Be("In");
        wire.ToParam.Should().Be("Out");
    }

    [Test]
    public void Endpoints_FirstWire_ShouldNotBeEmpty()
    {
        var content = Logix.Load(Known.Test);
        var sheet = content.Query<Sheet>().First();
        var wire = sheet.Connectors().First();

        var endpoint = wire.Endpoint(0);

        endpoint.Should().NotBeNull();
    }
    
    [Test]
    public void Endpoints_WireToConnector_ShouldNotBeEmpty()
    {
        var content = Logix.Load(Known.Test);
        var sheet = content.Query<Sheet>().First();
        var wire = sheet.Connectors().First(w => w.ToID == 6);

        var endpoint = wire.Endpoint(9);

        endpoint.Should().NotBeNull();
    }
}