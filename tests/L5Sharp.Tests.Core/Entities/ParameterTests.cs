using FluentAssertions;

namespace L5Sharp.Tests.Core.Entities;

[TestFixture]
public class ParameterTests
{
    [Test]
    public void New_NullTagName_ShouldThrowException()
    {
        FluentActions.Invoking(() => new Parameter(null!, new DINT())).Should().Throw<ArgumentException>();
    }

    [Test]
    public void New_DefaultInstance_ShouldHaveExpectedValues()
    {
        var parameter = new Parameter();

        parameter.Name.Should().BeEmpty();
        parameter.Description.Should().BeNull();
        parameter.TagType.Should().Be(TagType.Base);
        parameter.DataType.Should().Be("DINT");
        parameter.Usage.Should().Be(TagUsage.Input);
        parameter.Dimensions.Should().BeNull();
        parameter.Radix.Should().Be(Radix.Decimal);
        parameter.ExternalAccess.Should().Be(Access.ReadWrite);
        parameter.Default.Should().BeNull();
        parameter.Required.Should().BeFalse();
        parameter.Visible.Should().BeFalse();
        parameter.Constant.Should().BeNull();
        parameter.AliasFor.Should().BeNull();
    }

    [Test]
    public void New_Override_ShouldBeExpected()
    {
        var parameter = new Parameter
        {
            Name = "Test",
            Description = "This is a test",
            DataType = "MyType",
            Usage = TagUsage.InOut,
            Required = true,
            Visible = true,
            Constant = true,
            AliasFor = "TestTag"
        };

        parameter.Name.Should().Be("Test");
        parameter.Description.Should().Be("This is a test");
        parameter.DataType.Should().Be("MyType");
        parameter.Usage.Should().Be(TagUsage.InOut);
        parameter.Required.Should().BeTrue();
        parameter.Visible.Should().BeTrue();
        parameter.Constant.Should().BeTrue();
    }

    [Test]
    public Task Serialize_NameAndValue_ShouldBeVerified()
    {
        var parameter = new Parameter("Test", new DINT(123));

        var xml = parameter.Serialize().ToString();

        return VerifyXml(xml);
    }
    
    [Test]
    public Task Serialize_OverrideParameters_ShouldBeVerified()
    {
        var parameter = new Parameter
        {
            Name = "Test",
            Description = "This is a test",
            DataType = "MyType",
            Usage = TagUsage.InOut,
            Radix = Radix.Null,
            Required = true,
            Visible = true,
            Constant = true,
            AliasFor = "TestTag"
        };

        var xml = parameter.Serialize().ToString();

        return VerifyXml(xml);
    }

    [Test]
    public void ToMember_InputAtomicParameter_ShouldBeExpected()
    {
        var parameter = new Parameter("Test", new DINT(123));

        var member = parameter.ToMember();

        member.Name.Should().Be("Test");
        member.Value.Should().Be(new INT(123));
    }

    [Test]
    public void ToTag_ValidName_ShouldBeExpected()
    {
        var parameter = new Parameter
        {
            Name = "Test",
            DataType = "MyDataType"
        };

        var tag = parameter.ToTag("MyParameterTag");

        tag.Should().NotBeNull();
        tag.Name.Should().Be("MyParameterTag");
        tag.DataType.Should().Be("MyDataType");
        tag.Value.Should().BeOfType<StructureData>();
    }
    [Test]
    public void Clone_WhenCalled_ShouldReturnExpectedType()
    {
        var parameter = new Parameter
        {
            Name = "Test",
            Description = "This is a test",
            DataType = "MyType",
            Usage = TagUsage.InOut,
            Required = true,
            Visible = true,
            Constant = true,
            AliasFor = "TestTag"
        };

        var clone = parameter.Clone();

        clone.Should().BeOfType<Parameter>();
        clone.Should().NotBeSameAs(parameter);
        clone.Name.Should().Be(parameter.Name);
        clone.Description.Should().Be(parameter.Description);
        clone.DataType.Should().Be(parameter.DataType);
        clone.Usage.Should().Be(parameter.Usage);
        clone.Required.Should().Be(parameter.Required);
        clone.Visible.Should().Be(parameter.Visible);
        clone.Constant.Should().Be(parameter.Constant);
        clone.AliasFor.Should().Be(parameter.AliasFor);
    }
}