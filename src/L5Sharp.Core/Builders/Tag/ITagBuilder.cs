namespace L5Sharp.Core;

/// <summary>
/// Represents a builder interface for creating and configuring instances of <see cref="Tag"/>.
/// Provides methods to finalize the configuration and construct the resulting tag.
/// </summary>
public interface ITagBuilder
{
    /// <summary>
    /// Finalizes the configuration of the builder and creates an instance of the <see cref="Tag"/> class.
    /// </summary>
    /// <returns>An instance of the configured <see cref="Tag"/>.</returns>
    Tag Build();
}