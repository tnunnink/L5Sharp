namespace L5Sharp.Core.Rungs;

/// <summary>
/// Provides a concrete implementation for building Ladder Logic (RLL) <see cref="Routine"/> objects.
/// The <see cref="RllBuilder"/> class supports a fluent interface to define program associations and add rungs
/// with optional comments before constructing the full routine.
/// </summary>
public class RllBuilder(string routineName) : IRllBuilder
{
    /// <summary>
    /// Represents an instance of a <see cref="Routine"/> object being constructed within the <see cref="RllBuilder"/>.
    /// This field holds the current state of the routine being configured, allowing the addition of rungs,
    /// comments, and program associations prior to finalizing the build process.
    /// </summary>
    private readonly Routine _routine = new(routineName, RoutineType.RLL);

    /// <inheritdoc />
    public IRllBuilder InProgram(string programName)
    {
        var program = new Program(programName, ProgramType.Normal);
        program.Routines.Add(_routine);
        return this;
    }

    /// <inheritdoc />
    public IRllBuilder WithRung(string text, string? comment = null)
    {
        _routine.Rungs.Add(new Rung(text) { Comment = comment });
        return this;
    }

    /// <inheritdoc />
    public Routine Build()
    {
        return _routine;
    }
}