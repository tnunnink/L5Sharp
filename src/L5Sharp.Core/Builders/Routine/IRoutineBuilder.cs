namespace L5Sharp.Core;

/// <summary>
/// Provides a fluent interface for building <see cref="Routine"/> objects in memory.
/// This interface supports method chaining to configure routine properties before creation.
/// </summary>
/// <typeparam name="TBuilder">The concrete builder type that implements this interface, enabling fluent chaining.</typeparam>
public interface IRoutineBuilder<out TBuilder> where TBuilder : IRoutineBuilder<TBuilder>
{
    /// <summary>
    /// Specifies the parent program that will contain the routine being built.
    /// </summary>
    /// <param name="programName">The name of the parent program that will contain this routine.</param>
    /// <returns>The builder instance for method chaining.</returns>
    TBuilder InProgram(string programName);

    /// <summary>
    /// Creates and returns the configured <see cref="Routine"/> instance based on the builder's current state.
    /// </summary>
    /// <returns>A new <see cref="Routine"/> object with the configured properties.</returns>
    Routine Build();
}