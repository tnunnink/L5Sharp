using System.Xml.Linq;
using FluentAssertions;

namespace L5Sharp.Tests.Core.Elements;

[TestFixture]
public class ScheduledProgramTests
{
    [Test]
    public void New_ValidName_ShouldCreateInstance()
    {
        var scheduledProgram = new ScheduledProgram("MyProgram");

        scheduledProgram.Should().NotBeNull();
        scheduledProgram.Name.Should().Be("MyProgram");
    }

    [Test]
    public void New_FromXElement_ShouldCreateInstance()
    {
        var element = XElement.Parse("<ScheduledProgram Name=\"TestProgram\" />");

        var scheduledProgram = new ScheduledProgram(element);

        scheduledProgram.Should().NotBeNull();
        scheduledProgram.Name.Should().Be("TestProgram");
    }

    [Test]
    public void Name_WhenAccessed_ShouldReturnCorrectValue()
    {
        var scheduledProgram = new ScheduledProgram("ProgramName");

        var name = scheduledProgram.Name;

        name.Should().Be("ProgramName");
    }

    [Test]
    public void New_NullName_ShouldThrowArgumentException()
    {
        FluentActions.Invoking(() => new ScheduledProgram((string)null!))
            .Should().Throw<ArgumentException>()
            .WithMessage("Name cannot be null or empty.*")
            .WithParameterName("name");
    }

    [Test]
    public void New_EmptyName_ShouldThrowArgumentException()
    {
        FluentActions.Invoking(() => new ScheduledProgram(string.Empty))
            .Should().Throw<ArgumentException>()
            .WithMessage("Name cannot be null or empty.*")
            .WithParameterName("name");
    }

    [Test]
    public void Equals_SameInstance_ShouldReturnTrue()
    {
        var scheduledProgram = new ScheduledProgram("MyProgram");

        var result = scheduledProgram.Equals(scheduledProgram);

        result.Should().BeTrue();
    }

    [Test]
    public void Equals_SameName_ShouldReturnTrue()
    {
        var first = new ScheduledProgram("MyProgram");
        var second = new ScheduledProgram("myprogram");

        var result = first.Equals(second);

        result.Should().BeTrue();
    }

    [Test]
    public void Equals_DifferentName_ShouldReturnFalse()
    {
        var first = new ScheduledProgram("Program1");
        var second = new ScheduledProgram("Program2");

        var result = first.Equals(second);

        result.Should().BeFalse();
    }

    [Test]
    public void Equals_StringWithSameName_ShouldReturnTrue()
    {
        var scheduledProgram = new ScheduledProgram("MyProgram");

        var result = scheduledProgram.Equals("myprogram");

        result.Should().BeTrue();
    }

    [Test]
    public void Equals_StringWithDifferentName_ShouldReturnFalse()
    {
        var scheduledProgram = new ScheduledProgram("MyProgram");

        var result = scheduledProgram.Equals("OtherProgram");

        result.Should().BeFalse();
    }

    [Test]
    public void Equals_Null_ShouldReturnFalse()
    {
        var scheduledProgram = new ScheduledProgram("MyProgram");

        var result = scheduledProgram.Equals(null);

        result.Should().BeFalse();
    }

    [Test]
    public void GetHashCode_SameName_ShouldReturnSameValue()
    {
        var first = new ScheduledProgram("MyProgram");
        var second = new ScheduledProgram("myprogram");

        var hash1 = first.GetHashCode();
        var hash2 = second.GetHashCode();

        hash1.Should().Be(hash2);
    }

    [Test]
    public void EqualityOperator_EqualInstances_ShouldReturnTrue()
    {
        var first = new ScheduledProgram("MyProgram");
        var second = new ScheduledProgram("myprogram");

        var result = first == second;

        result.Should().BeTrue();
    }

    [Test]
    public void EqualityOperator_DifferentInstances_ShouldReturnFalse()
    {
        var first = new ScheduledProgram("Program1");
        var second = new ScheduledProgram("Program2");

        var result = first == second;

        result.Should().BeFalse();
    }

    [Test]
    public void InequalityOperator_EqualInstances_ShouldReturnFalse()
    {
        var first = new ScheduledProgram("MyProgram");
        var second = new ScheduledProgram("myprogram");

        var result = first != second;

        result.Should().BeFalse();
    }

    [Test]
    public void InequalityOperator_DifferentInstances_ShouldReturnTrue()
    {
        var first = new ScheduledProgram("Program1");
        var second = new ScheduledProgram("Program2");

        var result = first != second;

        result.Should().BeTrue();
    }

    [Test]
    public void ImplicitConversion_FromString_ShouldCreateScheduledProgram()
    {
        ScheduledProgram scheduledProgram = "MyProgram";

        scheduledProgram.Should().NotBeNull();
        scheduledProgram.Name.Should().Be("MyProgram");
    }
}