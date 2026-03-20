using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace L5Sharp.Generators;

/// <summary>
/// Provides extension methods for working with Roslyn symbols and related functionality.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Retrieves the argument values of a specified attribute as strings from a given symbol.
    /// </summary>
    /// <param name="symbol">The symbol to search for the attribute.</param>
    /// <param name="attributeName">The fully qualified name of the attribute whose arguments are to be retrieved.</param>
    /// <returns>A collection of string values representing the arguments of the specified attribute.</returns>
    /// <remarks>This includes all arguments for all instances of the attribute for a given symbol</remarks>
    public static IEnumerable<string> GetAttributeArguments(this INamedTypeSymbol symbol, string attributeName)
    {
        return symbol.GetAttributes()
            .Where(a => a.AttributeClass?.ToDisplayString() == attributeName)
            .SelectMany(a => a.ConstructorArguments.Select(x => x.Value?.ToString()))
            .Where(s => s is not null);
    }
}