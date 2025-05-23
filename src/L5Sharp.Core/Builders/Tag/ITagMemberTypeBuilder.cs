using System;

namespace L5Sharp.Core;

/// <summary>
/// Represents an interface for building and configuring tag members and their types
/// within a structured tag hierarchy.
/// </summary>
/// <typeparam name="TBuilder">The type of the parent builder interface, enabling fluent configuration.</typeparam>
public interface ITagMemberTypeBuilder<out TBuilder>
{
    /// <summary>
    /// Configures the tag member as an atomic member of the specified type.
    /// </summary>
    /// <typeparam name="TAtomic">The type of the atomic data to be associated with the tag member. Must inherit from <see cref="AtomicData"/> and have a parameterless constructor.</typeparam>
    /// <returns>An instance of <see cref="ITagMemberAtomicBuilder{TAtomic, TReturn}"/>, which allows further configuration of the atomic tag member.</returns>
    ITagMemberAtomicBuilder<TAtomic, TBuilder> AsAtomic<TAtomic>() where TAtomic : AtomicData, new();

    /// <summary>
    /// Configures the tag member as a structure of the specified data type and allows further customization via the provided action.
    /// </summary>
    /// <param name="dataType">The name of the data type to associate with the structure.</param>
    /// <param name="action">An action that provides further configuration for the structure using an <see cref="ITagMemberStructureBuilder"/>.</param>
    /// <returns>An instance of the parent builder type, allowing additional configuration of the tag member.</returns>
    TBuilder AsStructure(string dataType, Action<ITagMemberStructureBuilder> action);

    /// <summary>
    /// Configures the tag member as an array of the specified atomic type and dimensions.
    /// </summary>
    /// <typeparam name="TAtomic">The type of the atomic data to be associated with the array. Must inherit from <see cref="AtomicData"/> and have a parameterless constructor.</typeparam>
    /// <param name="dimensions">The dimensions of the array to configure.</param>
    /// <param name="action">An action for further configuration of the array structure.</param>
    /// <returns>An instance of the builder, allowing for additional configuration of the tag member.</returns>
    TBuilder AsArray<TAtomic>(Dimensions dimensions, Action<ITagMemberAtomicArrayBuilder<TAtomic>> action)
        where TAtomic : AtomicData, new();

    /// <summary>
    /// Configures the tag member as an array of the specified data type and dimensions, allowing further configuration through the provided action.
    /// </summary>
    /// <param name="dataType">The string representation of the data type for the array elements.</param>
    /// <param name="dimensions">The dimensions of the array, defining its size and structure.</param>
    /// <param name="action">An action for further configuration of the array structure.</param>
    /// <returns>An instance of the builder, allowing further configuration of the tag member.</returns>
    TBuilder AsArray(string dataType, Dimensions dimensions, Action<ITagMemberStructureArrayBuilder> action);
}