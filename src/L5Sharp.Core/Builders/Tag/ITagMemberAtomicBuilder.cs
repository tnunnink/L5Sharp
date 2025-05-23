namespace L5Sharp.Core;

/// <summary>
/// Defines a builder interface for configuring atomic tag members in a structured and fluent manner.
/// </summary>
/// <typeparam name="TAtomic">The type of the atomic tag data being built.</typeparam>
/// <typeparam name="TBuilder">The type of the builder implementing this interface, enabling fluent configuration.</typeparam>
public interface ITagMemberAtomicBuilder<in TAtomic, out TBuilder> where TAtomic : AtomicData
{
    /// <summary>
    /// Assigns a value to the current tag member being built.
    /// </summary>
    /// <param name="value">The value to set for the tag member.</param>
    /// <returns>The current tag member atomic builder instance.</returns>
    TBuilder WithValue(TAtomic value);

    /// <summary>
    /// Sets the description for the current tag member being built.
    /// </summary>
    /// <param name="description">The description to assign to the tag member.</param>
    /// <returns>The current tag member atomic builder instance.</returns>
    ITagMemberAtomicBuilder<TAtomic, TBuilder> WithDescription(string description);
}