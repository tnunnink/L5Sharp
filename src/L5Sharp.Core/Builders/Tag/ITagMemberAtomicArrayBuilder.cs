namespace L5Sharp.Core;

/// <summary>
/// Defines a builder interface for configuring tag member arrays, allowing for the assignment
/// of values, structures, and descriptions within the array elements.
/// </summary>
public interface ITagMemberAtomicArrayBuilder<in TAtomic> where TAtomic : AtomicData
{
    /// <summary>
    /// Assigns a value to the array element at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the array element to which the value will be assigned.</param>
    /// <param name="value">The value to be assigned to the specified array element.</param>
    /// <returns>An instance of <see cref="ITagMemberAtomicArrayBuilder{TAtomic}"/> for further configuration.</returns>
    ITagMemberAtomicArrayBuilder<TAtomic> WithElement(int index, TAtomic value);

    /// <summary>
    /// Assigns a description to the tag member array.
    /// </summary>
    /// <param name="description">The description to be applied to the tag member array.</param>
    /// <returns>An instance of <see cref="ITagMemberAtomicArrayBuilder{TAtomic}"/> for further configuration.</returns>
    ITagMemberAtomicArrayBuilder<TAtomic> WithDescription(string description);
}