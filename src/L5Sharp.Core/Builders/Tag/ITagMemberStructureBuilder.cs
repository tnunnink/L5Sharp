namespace L5Sharp.Core;

/// <summary>
/// Represents a builder interface for defining and configuring structure members of a tag.
/// </summary>
public interface ITagMemberStructureBuilder
{
    /// <summary>
    /// Adds a new member to the tag structure with the specified name.
    /// </summary>
    /// <param name="name">The name of the tag member to be added.</param>
    /// <returns>
    /// An instance of <see cref="ITagMemberTypeBuilder{TBuilder}"/> for further configuration of
    /// the added tag member's type and structure.
    /// </returns>
    ITagMemberTypeBuilder<ITagMemberStructureBuilder> AddMember(string name);

    /// <summary>
    /// Assigns a specified value to a tag member with the given name within the structure.
    /// </summary>
    /// <param name="name">The name of the tag member to which the value should be assigned.</param>
    /// <param name="value">The <see cref="LogixData"/> value to be assigned to the specified tag member.</param>
    /// <returns>
    /// An updated instance of <see cref="ITagMemberStructureBuilder"/> for further configuration of the tag structure.
    /// </returns>
    ITagMemberStructureBuilder WithValue(TagName name, LogixData value);

    /// <summary>
    /// Assigns a description to the current tag member within the structure.
    /// </summary>
    /// <param name="description">The description string to be assigned to the tag member.</param>
    /// <returns>
    /// An instance of <see cref="ITagMemberStructureBuilder"/> for further configuration of the tag structure.
    /// </returns>
    ITagMemberStructureBuilder WithDescription(string description);
}