namespace L5Sharp.Core;

/// <summary>
/// Represents a builder for configuring and constructing atomic tags in a Logix environment.
/// </summary>
/// <typeparam name="TAtomic">
/// The type of atomic data for the tag to be built. Must inherit from <see cref="AtomicData"/> and have a parameterless constructor.
/// </typeparam>
public interface ITagBaseAtomicBuilder<in TAtomic> : ITagBaseBuilder<ITagBaseAtomicBuilder<TAtomic>>
    where TAtomic : AtomicData, new()
{
    /// <summary>
    /// Sets the value of the atomic tag being constructed.
    /// </summary>
    /// <param name="value">The value to be assigned to the tag. This must be of type <typeparamref name="TAtomic"/>.</param>
    /// <returns>An instance of <see cref="ITagBaseAtomicBuilder{TAtomic}"/> for further configuration.</returns>
    ITagBaseAtomicBuilder<TAtomic> WithValue(TAtomic value);
}