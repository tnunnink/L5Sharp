using System.Linq.Dynamic.Core;
using System.Text.RegularExpressions;

namespace L5Sharp.CLI.Internal;

public static class GenericExtensions
{
    /// <summary>
    /// Determines if the specified file path corresponds to a valid Logix file
    /// of type ACD or L5X.
    /// </summary>
    /// <param name="path">The file path to check for a Logix file type.</param>
    /// <returns>True if the file is of type ACD or L5X, otherwise false.</returns>
    public static bool IsLogixFile(this string path)
    {
        return path.IsLogixFile(FileType.ACD) || path.IsLogixFile(FileType.L5X);
    }

    /// <summary>
    /// Determines if the specified file path corresponds to a project file with an extension matching the given project type.
    /// </summary>
    /// <param name="path">The file path to check for a valid project file extension.</param>
    /// <param name="type">The type of project to verify against the file extension.</param>
    /// <returns>True if the file extension matches the specified project type, otherwise false.</returns>
    public static bool IsLogixFile(this string path, FileType type)
    {
        if (!Path.Exists(path)) return false;
        if (!File.Exists(path)) return false;
        return string.Equals(Path.GetExtension(path), $".{type}", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Retrieves a user-friendly display name for the specified type.
    /// </summary>
    /// <param name="type">The type for which to get the display name.</param>
    /// <returns>A string representing the user-friendly display name of the type.</returns>
    public static string GetTypeDisplayName(this Type type)
    {
        switch (type.IsGenericType)
        {
            case true when type.GetGenericTypeDefinition() == typeof(Nullable<>):
            {
                var underlyingType = Nullable.GetUnderlyingType(type);
                return $"{GetTypeDisplayName(underlyingType!)}?";
            }
            case true when type.GetGenericTypeDefinition() == typeof(IEnumerable<>):
            {
                var enumerableType = type.GenericTypeArguments.First();
                return $"{GetTypeDisplayName(enumerableType)}[]";
            }
            default:
                return type.Name switch
                {
                    "String" => "string",
                    "Int32" => "int",
                    "Boolean" => "bool",
                    "DateTime" => "DateTime",
                    "Single" => "float",
                    "Double" => "double",
                    "Decimal" => "decimal",
                    _ => type.Name
                };
        }
    }

    /// <summary>
    /// Filters a collection of elements based on a provided selector function and filter pattern.
    /// The filter pattern supports wildcard characters such as '*' (matches any sequence of characters)
    /// and '?' (matches any single character).
    /// </summary>
    /// <param name="collection">The collection of elements to filter.</param>
    /// <param name="selector">
    /// A function to extract the string representation of each element in the collection
    /// to apply the filter comparison.
    /// </param>
    /// <param name="pattern">
    /// The pattern used to match the string representation of the elements.
    /// Wildcards '*' and '?' are supported. If null or empty, the method returns the original collection.
    /// </param>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <returns>A filtered collection of elements that match the provided pattern.</returns>
    internal static IEnumerable<T> FilterByPattern<T>(this IEnumerable<T> collection,
        Func<T, string?> selector, string? pattern)
    {
        if (string.IsNullOrEmpty(pattern)) return collection;

        var escaped = Regex.Escape(pattern);

        var regexPattern = escaped
            .Replace(@"\*", ".*") // * matches any characters
            .Replace(@"\?", "."); // ? matches a single character

        var regex = new Regex($"^{regexPattern}$", RegexOptions.IgnoreCase);

        return collection.Where(x => regex.IsMatch(selector(x) ?? string.Empty));
    }

    /// <summary>
    /// Filters the given collection based on the specified flag and selector function.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to filter.</param>
    /// <param name="selector">A function to test each element for a condition.</param>
    /// <param name="flag">A boolean value that determines whether to apply the selector filter.</param>
    /// <returns>A collection containing elements that satisfy the filtering condition.</returns>
    internal static IEnumerable<T> FilterBySwitch<T>(this IEnumerable<T> collection, Func<T, bool> selector, bool flag)
    {
        if (!flag) return collection;
        return collection.Where(x => selector(x) == flag);
    }

    /// <summary>
    /// Filters a collection based on a boolean flag condition.
    /// </summary>
    /// <param name="collection">The collection of items to filter.</param>
    /// <param name="selector">A function to extract the boolean property of each item.</param>
    /// <param name="option">The boolean flag to filter on, or null to include all items.</param>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <returns>A filtered collection of items based on the flag condition.</returns>
    internal static IEnumerable<T> FilterByFlag<T>(this IEnumerable<T> collection,
        Func<T, bool> selector,
        bool? option)
    {
        return collection.Where(x => option is null || selector(x) == option);
    }

    /// <summary>
    /// Filters a collection of items based on a specified dynamic LINQ expression.
    /// </summary>
    /// <param name="collection">The source collection to filter.</param>
    /// <param name="expression">The dynamic LINQ expression used to filter the collection.</param>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <returns>A filtered collection of items matching the specified expression.</returns>
    internal static IEnumerable<T> FilterByExpression<T>(this IEnumerable<T> collection, string? expression)
    {
        if (string.IsNullOrEmpty(expression))
            return collection;

        return collection.AsQueryable().Where(expression);
    }
}