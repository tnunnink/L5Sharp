using FluentAssertions;

namespace L5Sharp.Tests.Components;

[TestFixture]
public class RoutineTests
{
    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var routine = new Routine();

        routine.Should().NotBeNull();
    }

    [Test]
    public void New_Default_ShouldHaveDefaultValues()
    {
        var routine = new Routine();

        routine.Name.Should().BeEmpty();
        routine.Description.Should().BeNull();
        routine.Type.Should().Be(RoutineType.RLL);
        routine.Content<Rung>().Should().NotBeNull();
    }

    [Test]
    public void New_StructuredText_ShouldHaveExpectedValues()
    {
        var routine = new Routine(RoutineType.ST);

        routine.Name.Should().BeEmpty();
        routine.Description.Should().BeNull();
        routine.Type.Should().Be(RoutineType.ST);
        routine.Content<Line>().Should().NotBeNull();
        routine.Content<Line>().Should().BeEmpty();
    }
    
    [Test]
    public void New_FunctionBlockDiagram_ShouldHaveExpectedValues()
    {
        var routine = new Routine(RoutineType.FBD);

        routine.Name.Should().BeEmpty();
        routine.Description.Should().BeNull();
        routine.Type.Should().Be(RoutineType.FBD);
        routine.Content<Sheet>().Should().NotBeNull();
        routine.Content<Sheet>().Should().BeEmpty();
    }

    [Test]
    public void New_RungCollection_ShouldHaveExpectedCount()
    {
        var routine = new Routine();
        
        var rungs = new List<Rung>
        {
            new("XIC(SomeTag)OTE(SomeOtherTag);"),
            new("XIC(SomeTag)OTE(SomeOtherTag);"),
            new("XIC(SomeTag)OTE(SomeOtherTag);")
        };
        
        routine.Content<Rung>().AddRange(rungs);

        routine.Content<Rung>().Count().Should().Be(3);
    }
}