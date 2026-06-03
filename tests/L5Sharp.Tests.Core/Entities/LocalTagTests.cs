using FluentAssertions;

namespace L5Sharp.Tests.Core.Entities;

[TestFixture]
public class LocalTagTests
{
    [Test]
    public void New_Default_ShouldHaveExpectedDefaults()
    {
        var tag = new LocalTag();

        tag.Name.Should().BeEmpty();
        tag.DataType.Should().Be("DINT");
        tag.Usage.Should().Be(TagUsage.Local);
        tag.Radix.Should().Be(Radix.Decimal);
        tag.ExternalAccess.Should().Be(Access.None);
    }

    [Test]
    public void New_WithNameAndValue_ShouldHaveExpectedValues()
    {
        var tag = new LocalTag("TestTag", new BOOL(true), "Test Description");

        tag.Name.Should().Be("TestTag");
        tag.DataType.Should().Be("BOOL");
        tag.Default.Should().Be(new BOOL(true));
        tag.Description.Should().Be("Test Description");
        tag.Usage.Should().Be(TagUsage.Local);
    }

    [Test]
    public void New_OverriddenProperties_ShouldHaveExpectedValues()
    {
        var tag = new LocalTag
        {
            Name = "Overridden",
            DataType = "INT",
            Radix = Radix.Binary,
            ExternalAccess = Access.ReadOnly,
            Description = "New Description"
        };

        tag.Name.Should().Be("Overridden");
        tag.DataType.Should().Be("INT");
        tag.Radix.Should().Be(Radix.Binary);
        tag.ExternalAccess.Should().Be(Access.ReadOnly);
        tag.Description.Should().Be("New Description");
        tag.Usage.Should().Be(TagUsage.Local);
    }

    [Test]
    public Task Serialize_Default_ShouldBeVerified()
    {
        var tag = new LocalTag();

        return VerifyXml(tag.Serialize().ToString());
    }

    [Test]
    public Task Serialize_WithNameAndValue_ShouldBeVerified()
    {
        var tag = new LocalTag("TestTag", new BOOL(true), "Test Description");

        return VerifyXml(tag.Serialize().ToString());
    }
}
