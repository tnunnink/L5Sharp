namespace L5Sharp.Core;

/// <summary>
/// Represents a builder interface for creating and configuring atomic tag arrays in a Logix-based project.
/// </summary>
public interface ITagBaseAtomicArrayBuilder<in TAtomic> : ITagBaseBuilder<ITagBaseAtomicArrayBuilder<TAtomic>>
    where TAtomic : AtomicData
{
    /// <summary>
    /// Sets a specified value at a given index within the tag array.
    /// </summary>
    /// <param name="index">The zero-based index of the tag array where the value will be set.</param>
    /// <param name="value">The <see cref="LogixData"/> value to assign at the specified index.</param>
    /// <returns>An instance of <see cref="ITagBaseAtomicArrayBuilder{TAtomic}"/> for further configuration.</returns>
    ITagBaseAtomicArrayBuilder<TAtomic> WithElement(int index, TAtomic value);
}