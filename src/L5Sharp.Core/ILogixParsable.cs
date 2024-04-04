namespace L5Sharp.Core;

/// <summary>
/// Represents an interface that defines methods to parse string values into an object of type T.
/// </summary>
/// <typeparam name="T">The type of object to parse the string value into.</typeparam>
public interface ILogixParsable<out T> where T : ILogixParsable<T>
{
    //This is only available in newer versions of .NET and we are supporting .NET standard 2.0. Classes still have to 
    //implement this interface where applied to satisfy the newer version, but will technically be an empty marker
    //interface for older versions.
#if NET7_0_OR_GREATER
    /// <summary>
    /// Parses the provided string into an instance of this type.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>A new instance of this type representing the parsed value.</returns>
    static abstract T Parse(string value);

    /// <summary>
    /// Tries to parse the provided string into an instance of this type.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>A new instance of this type representing the parsed value if successful; Otherwise, <c>null</c>.</returns>
    static abstract T? TryParse(string? value);
#endif
}