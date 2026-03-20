namespace L5Sharp.Core;

/// <summary>
/// Provides a fluent interface for building Ladder Logic (RLL) <see cref="Routine"/> objects.
/// This specialized builder allows adding rungs with logic and comments before creating the routine.
/// </summary>
public interface IRllBuilder : IRoutineBuilder<IRllBuilder>
{
    /// <summary>
    /// Adds a rung with the specified ladder logic content to the routine being built.
    /// </summary>
    /// <param name="text">The ladder logic text content of the rung (e.g., "XIC(Tag1)OTE(Tag2);").</param>
    /// <param name="comment">An optional comment describing the rung's purpose or logic.</param>
    /// <returns>The builder instance for method chaining.</returns>
    IRllBuilder WithRung(string text, string? comment = null);
}