namespace L5Sharp.Core;

/// <summary>
/// Represents a builder for configuring a tag as an alias in a Logix-based control system.
/// Provides the ability to associate descriptive information with the alias tag during its configuration.
/// </summary>
public interface ITagAliasBuilder : ITagBuilder
{
    /// <summary>
    /// Sets the description for the tag being built.
    /// </summary>
    /// <param name="description">The description text to associate with the tag.</param>
    /// <returns>An updated instance of the <see cref="ITagBuilder"/> to allow for method chaining.</returns>
    ITagBuilder WithDescription(string description);
}