namespace L5Sharp.Core;

/// <summary>
/// Represents a builder interface for constructing tag base structures in a Logix environment.
/// </summary>
public interface ITagBaseStructureBuilder : ITagBaseBuilder<ITagBaseStructureBuilder>
{
    /// <summary>
    /// Adds a member to the tag structure with the specified name.
    /// </summary>
    /// <param name="name">The name of the member to add.</param>
    /// <returns>A new <see cref="ITagMemberTypeBuilder{TReturn}"/> interface to define the type of the added member.</returns>
    ITagMemberTypeBuilder<ITagBaseStructureBuilder> AddMember(string name);

    /// <summary>
    /// Sets a specific value to a tag member with the given name.
    /// </summary>
    /// <param name="name">The name of the tag member to which the value will be assigned.</param>
    /// <param name="value">The value to be assigned to the specified tag member.</param>
    /// <returns>The current instance of the <see cref="ITagBaseStructureBuilder"/> for method chaining.</returns>
    ITagBaseStructureBuilder WithValue(TagName name, LogixData value);
}