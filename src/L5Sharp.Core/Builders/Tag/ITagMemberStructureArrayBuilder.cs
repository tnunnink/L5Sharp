using System;

namespace L5Sharp.Core;

/// <summary>
/// Defines an interface for building and configuring a structure-based tag array member within a control system.
/// </summary>
public interface ITagMemberStructureArrayBuilder
{
    /// <summary>
    /// Configures a structure at a specified index within the tag array, using the provided builder action.
    /// </summary>
    /// <param name="index">The zero-based index of the tag array where the structure will be configured.</param>
    /// <param name="action">An action that provides a builder for defining the structure's members and properties.</param>
    /// <returns>An instance of <see cref="ITagMemberStructureArrayBuilder"/> for further configuration.</returns>
    ITagMemberStructureArrayBuilder WithElement(int index, Action<ITagMemberStructureBuilder> action);

    /// <summary>
    /// Assigns a description to the tag structure array, providing additional details or context.
    /// </summary>
    /// <param name="description">The description to be applied to the tag structure array.</param>
    /// <returns>An instance of <see cref="ITagMemberStructureArrayBuilder"/> for continued configuration.</returns>
    ITagMemberStructureArrayBuilder WithDescription(string description);
}