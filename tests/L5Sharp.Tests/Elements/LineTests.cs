using System.Xml.Linq;
using FluentAssertions;

namespace L5Sharp.Tests.Elements;

[TestFixture]
public class LineTests
{
    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var line = new Line();

        line.Should().NotBeNull();
    }
    
    [Test]
    public void New_Default_ShouldNotBeAttached()
    {
        var sheet = new Line();

        sheet.IsAttached.Should().BeFalse();
        sheet.L5X.Should().BeNull();
    }

    [Test]
    public void New_ValidElement_ShouldNotBeNull()
    {
        var element = new XElement(L5XName.Line, new XAttribute(L5XName.Number, 1), new XCData("Test"));

        var line = new Line(element);

        line.Should().NotBeNull();
    }
    
    [Test]
    public void New_ValidElement_ShouldHaveExpectedValues()
    {
        var element = new XElement(L5XName.Line, new XAttribute(L5XName.Number, 1), new XCData("Test"));

        var line = new Line(element);
        
        line.Number.Should().Be(1);
        line.Container.Should().BeEmpty();
        line.Routine.Should().BeNull();
        line.ToString().Should().Be("Test");
    }

    [Test]
    public void New_ValidText_ShouldBeExpected()
    {
        var line = new Line("Test");
        
        line.Number.Should().Be(0);
        line.Container.Should().BeEmpty();
        line.Routine.Should().BeNull();
        line.ToString().Should().Be("Test");
    }
}