using L5Sharp.Rockwell;

namespace L5Sharp;

/// <summary>
/// A service for providing <see cref="CatalogEntry"/> data for a specified catalog number.
/// </summary>
public interface ILogixCatalogService
{
    /// <summary>
    /// Gets an <see cref="CatalogEntry"/> for the specified catalog number.
    /// </summary>
    /// <param name="catalogNumber">The string catalog number for which to retrieve the entry.</param>
    /// <returns>A <see cref="CatalogEntry"/> instance containing information related to the specified module.</returns>
    CatalogEntry Lookup(string catalogNumber);
}