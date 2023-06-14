using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Tests.Types;

[TestFixture]
public class StructureTypeTests
{
    [Test]
    public void New_ValidArguments_ShouldNotBeNull()
    {
        var type = new StructureType("Test", new List<Member>());

        type.Should().NotBeNull();
    }

    [Test]
    public void New_NullName_ShouldThrowArgumentException()
    {
        FluentActions.Invoking(() => new StructureType(null!, new List<Member>())).Should().Throw<ArgumentException>();
    }

    [Test]
    public void New_NullMembers_ShouldThrowArgumentException()
    {
        FluentActions.Invoking(() => new StructureType("Test", null!)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void New_EmptyMembers_ShouldHaveExpectedValues()
    {
        var type = new StructureType("Test", new List<Member>());

        type.Should().NotBeNull();
        type.Name.Should().Be("Test");
        type.Family.Should().Be(DataTypeFamily.None);
        type.Class.Should().Be(DataTypeClass.Unknown);
        type.Members.Should().BeEmpty();
    }

    [Test]
    public void New_WithMembers_ShouldHaveExpectedValues()
    {
        var type = new StructureType("Test", new List<Member>
        {
            new("Member1", true),
            new("Member2", (byte)255),
            new("Member3", 1000),
            new("Member4", 4.5f),
            new("Member5", new TIMER())
        });


        type.Should().NotBeNull();
        type.Name.Should().Be("Test");
        type.Family.Should().Be(DataTypeFamily.None);
        type.Class.Should().Be(DataTypeClass.Unknown);
        type.Members.Should().HaveCount(5);
    }

    [Test]
    public void ToString_WhenCalled_ShouldBeName()
    {
        var type = new StructureType("Test", new List<Member>());

        var name = type.ToString();

        name.Should().Be(type.Name);
    }

    [Test]
    public void Clone_WhenCalled_ShouldNotBeSameAsButEqual()
    {
        var type = new StructureType("Test", new List<Member>
        {
            new("Member1", true),
            new("Member2", (byte)255),
            new("Member3", 1000),
            new("Member4", 4.5f),
            new("Member5", new TIMER())
        });
        
        var clone = (StructureType)type.Clone();

        clone.Should().BeOfType<StructureType>();
        clone.Should().NotBeSameAs(type);
        clone.Name.Should().Be(type.Name);
        clone.Members.Should().HaveCount(type.Members.Count());
    }
}