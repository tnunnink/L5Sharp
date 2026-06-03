using System.Xml.Linq;
using FluentAssertions;

namespace L5Sharp.Tests.Core;

[TestFixture]
public class LogixContentTests
{
    [Test]
    public void New_NullElement_ShouldThrowArgumentNullException()
    {
        FluentActions.Invoking(() => new LogixContent(null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void New_WrongElementName_ShouldThrowInvalidOperationException()
    {
        var element = new XElement("WrongName");
        FluentActions.Invoking(() => new LogixContent(element)).Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void New_ValidElement_ShouldHaveExpectedProperties()
    {
        var date = new DateTime(2026, 6, 3, 5, 36, 0);
        var element = new XElement("RSLogix5000Content",
            new XAttribute("SchemaRevision", "1.23"),
            new XAttribute("SoftwareRevision", "32.01"),
            new XAttribute("TargetName", "TestController"),
            new XAttribute("TargetType", "Controller"),
            new XAttribute("TargetCount", "1"),
            new XAttribute("ContainsContext", "true"),
            new XAttribute("Owner", "TestOwner"),
            new XAttribute("ExportDate", date.ToString("ddd MMM d HH:mm:ss yyyy"))
        );

        var content = new LogixContent(element);

        content.SchemaRevision.Should().Be(new Revision(1, 23));
        content.SoftwareRevision.Should().Be(new Revision(32, 1));
        content.TargetName.Should().Be("TestController");
        content.TargetType.Should().Be("Controller");
        content.TargetCount.Should().Be(1);
        content.ContainsContext.Should().BeTrue();
        content.Owner.Should().Be("TestOwner");
        content.ExportDate.Should().Be(date);
    }

    [Test]
    public void Empty_ShouldHaveDefaultValues()
    {
        var content = LogixContent.Empty();

        content.SchemaRevision.Should().Be(new Revision());
        content.SoftwareRevision.Should().Be(new Revision());
        content.TargetName.Should().BeEmpty();
        content.TargetType.Should().Be("Controller");
        content.ContainsContext.Should().BeFalse();
        content.Owner.Should().Be(Environment.UserName);
        content.ExportDate.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(5));
    }

    [Test]
    public void Create_ValidArguments_ShouldHaveExpectedProperties()
    {
        var content = LogixContent.Create("MyController", "1756-L83E", new Revision(32, 11));

        content.TargetName.Should().Be("MyController");
        content.TargetType.Should().Be("Controller");
        content.SoftwareRevision.Should().Be(new Revision(32, 11));
        content.ContainsContext.Should().BeFalse();
    }

    [Test]
    public void Create_NullName_ShouldThrowArgumentNullException()
    {
        FluentActions.Invoking(() => LogixContent.Create(null!, "1756-L83E")).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Create_NullProcessor_ShouldThrowArgumentNullException()
    {
        FluentActions.Invoking(() => LogixContent.Create("Test", null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Create_WithComponent_ShouldHaveExpectedProperties()
    {
        var tag = new Tag { Name = "TestTag", Value = 100 };
        var content = LogixContent.Create(tag);

        content.TargetName.Should().Be("TestTag");
        content.TargetType.Should().Be("Tag");
        content.ContainsContext.Should().BeTrue();
    }

    [Test]
    public void Create_WithNullComponent_ShouldThrowArgumentNullException()
    {
        FluentActions.Invoking(() => LogixContent.Create<Tag>(null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void ToString_ShouldReturnExpectedFormat()
    {
        var element = new XElement("RSLogix5000Content",
            new XAttribute("TargetName", "TestName"),
            new XAttribute("TargetType", "TestType"),
            new XAttribute("TargetCount", "5")
        );
        var content = new LogixContent(element);

        content.ToString().Should().Be("TestType/TestName/5");
    }

    [Test]
    public void NormalizeContent_TargetTypeModule_ShouldMoveModulesToController()
    {
        var element = new XElement("RSLogix5000Content",
            new XAttribute("TargetType", "Module"),
            new XElement("Module", new XAttribute("Name", "Mod1")),
            new XElement("Module", new XAttribute("Name", "Mod2"))
        );

        var content = new LogixContent(element);

        // Accessing the underlying controller via Element (though it's internal/protected, 
        // we can check if they were moved by looking at the resulting XML structure if needed, 
        // but LogixContent doesn't expose the controller directly. 
        // However, we can check the element nodes)

        var controller = content.Serialize().Element("Controller");
        controller.Should().NotBeNull();
        controller.Element("Modules")!.Elements("Module").Should().HaveCount(2);
    }

    [Test]
    public void ExportOptions_ShouldReturnExpectedValues()
    {
        var element = new XElement("RSLogix5000Content",
            new XAttribute("ExportOptions", "Option1 Option2 Option3")
        );
        var content = new LogixContent(element);

        content.ExportOptions.Should().BeEquivalentTo("Option1", "Option2", "Option3");
    }
}