using System;

namespace L5Sharp.Core;

/// <summary>
/// Represents a builder for configuring an array of tag elements within a Logix structure,
/// supporting configuration of individual structures at specific indices.
/// </summary>
public interface ITagBaseStructureArrayBuilder : ITagBaseBuilder<ITagBaseStructureArrayBuilder>
{
    /// <summary>
    /// Configures a structure at a specified index within the tag array, using the provided builder action.
    /// </summary>
    /// <param name="index">The zero-based index of the tag array where the structure will be configured.</param>
    /// <param name="action">An action that provides a builder for defining the structure's members and properties.</param>
    /// <returns>An instance of <see cref="ITagBaseStructureArrayBuilder"/> for further configuration.</returns>
    ITagBaseStructureArrayBuilder WithElement(int index, Action<ITagMemberStructureBuilder> action);
}