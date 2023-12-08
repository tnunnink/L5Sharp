using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Enums;

namespace L5Sharp.Tests.Components;

[TestFixture]
public class ProgramTests
{
    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var program = new Program();

        program.Should().NotBeNull();
    }

    [Test]
    public void New_Default_ShouldHaveExpectedValues()
    {
        var program = new Program();

        program.Name.Should().Be(string.Empty);
        program.Description.Should().BeNull();
        program.Use.Should().BeNull();
        program.Type.Should().Be(ProgramType.Normal);
        program.TestEdits.Should().BeFalse();
        program.Disabled.Should().BeFalse();
        program.MainRoutineName.Should().BeNull();
        program.FaultRoutineName.Should().BeNull();
        program.UseAsFolder.Should().BeFalse();
        program.Tags.Should().BeEmpty();
        program.Routines.Should().BeEmpty();
    }

    [Test]
    public void New_Initialized_ShouldHaveExpectedValues()
    {
        var program = new Program
        {
            Name = "Test",
            Description = "This is a test",
            Use = Use.Target,
            Type = ProgramType.EquipmentPhase,
            TestEdits = true,
            Disabled = true,
            MainRoutineName = "Main",
            FaultRoutineName = "Fault",
            UseAsFolder = true,
            Tags = new LogixContainer<Tag> { new() { Name = "Test", Value = true } },
            Routines = new LogixContainer<Routine>()
        };

        program.Name.Should().Be("Test");
        program.Description.Should().Be("This is a test");
        program.Use.Should().Be(Use.Target);
        program.Type.Should().Be(ProgramType.EquipmentPhase);
        program.TestEdits.Should().BeTrue();
        program.Disabled.Should().BeTrue();
        program.MainRoutineName.Should().Be("Main");
        program.FaultRoutineName.Should().Be("Fault");
        program.UseAsFolder.Should().BeTrue();
    }

    [Test]
    public void AddTag_ValidTag_ShouldHaveExpectedCount()
    {
        var program = new Program();

        program.Tags.Add(new Tag { Name = "Test", Value = 1234 });

        program.Tags.Should().HaveCount(1);
    }
    
    [Test]
    public Task AddTag_ValidTag_ShouldBeVerified()
    {
        var program = new Program();

        program.Tags.Add(new Tag { Name = "Test", Value = 1234 });

        return Verify(program.Serialize().ToString());
    }

    [Test]
    public void AddRoutine_ValidRoutine_ShouldHaveExpectedCount()
    {
        var program = new Program();

        program.Routines.Add(new Routine { Name = "Test", Description = "This is a test", Type = RoutineType.RLL });

        program.Routines.Should().HaveCount(1);
    }
    
    [Test]
    public Task AddRoutine_ValidRoutine_ShouldBeVerified()
    {
        var program = new Program();

        program.Routines.Add(new Routine { Name = "Test", Description = "This is a test", Type = RoutineType.RLL });

        return Verify(program.Serialize().ToString());
    }

    [Test]
    public Task Serialize_Initialized_ShouldBeVerified()
    {
        var program = new Program
        {
            Name = "Test",
            Description = "This is a test",
            Use = Use.Target,
            Type = ProgramType.EquipmentPhase,
            TestEdits = true,
            Disabled = true,
            MainRoutineName = "Main",
            FaultRoutineName = "Fault",
            UseAsFolder = true,
            Tags = new LogixContainer<Tag> { new() { Name = "Test", Value = true } },
            Routines = new LogixContainer<Routine>()
        };

        var xml = program.Serialize().ToString();

        return Verify(xml);
    }
}