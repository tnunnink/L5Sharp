using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Elements;
using L5Sharp.Enums;

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
        routine.Description.Should().BeEmpty();
        routine.Type.Should().Be(RoutineType.Typeless);
        routine.Container.Should().BeEmpty();
        routine.Scope.Should().Be(Scope.Null);
    }

    [Test]
    public void New_RungCollection_ShouldHaveExpectedCount()
    {
        var rungs = new List<Rung>
        {
            new() { Text = "XIC(SomeTag)OTE(SomeOtherTag);" },
            new() { Text = "XIC(SomeTag)OTE(SomeOtherTag);" },
            new() { Text = "XIC(SomeTag)OTE(SomeOtherTag);" }
        };

        var routine = new Routine { Content = rungs.Cast<ILogixCode>().ToList() };

        routine.Content.Count.Should().Be(3);
    }
}