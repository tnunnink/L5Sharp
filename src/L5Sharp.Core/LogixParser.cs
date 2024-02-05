using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;

namespace L5Sharp.Core;

/// <summary>
/// Static class containing mappings for converting string values (XML typed value) to the strongly type object.
/// </summary>
public static class LogixParser
{
    private static readonly Lazy<Dictionary<Type, Parsers>> Parsers = new(() =>
        GetParsers().ToDictionary(t => t.Key, v => v.Value), LazyThreadSafetyMode.ExecutionAndPublication);

    /// <summary>
    /// Determines if the provided type is one that the <see cref="LogixParser"/> extensions can parse, meaning it is
    /// either a type that implements a custom type converter from string (primitive .NET types) or one that implements
    /// <see cref="ILogixParsable{T}"/> which are cached within this static factory class.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>
    /// <c>true</c> if the type has a type converter that can convert from string or if the type implements
    /// (or derives from a class implementing) <see cref="ILogixParsable{T}"/>; Otherwise, <c>false</c>.
    /// </returns>
    public static bool IsParsable(this Type type)
    {
        return Parsers.Value.ContainsKey(type) || TypeDescriptor.GetConverter(type).CanConvertFrom(typeof(string));
    }

    /// <summary>
    /// Parses the provided string input to the specified type using the predefined L5X parser functions.
    /// </summary>
    /// <param name="input">The string input to parse.</param>
    /// <typeparam name="T">The type of property to return.</typeparam>
    /// <returns>The resulting parsed value.</returns>
    /// <exception cref="InvalidOperationException">When a parser was not found to the specified type.</exception>
    /// <exception cref="ArgumentException">If the resulting parsed type does not match the specified generic type parameter.</exception>
    public static T Parse<T>(this string input)
    {
        var parser = GetParser(typeof(T));
        var value = parser(input);
        if (value is not T typed)
            throw new ArgumentException($"The input '{input}' could not be parsed to type '{typeof(T)}");
        return typed;
    }

    /// <summary>
    /// Parses the provided string input to the specified type using the predefined L5X parser functions.
    /// </summary>
    /// <param name="input">The string input to parse.</param>
    /// <param name="type">The type to parse the input to.</param>
    /// <returns>The resulting parsed object value.</returns>
    /// <exception cref="InvalidOperationException">When a parser was not found to the specified type.</exception>
    public static object Parse(this string input, Type type)
    {
        var parser = GetParser(type);
        var value = parser(input);
        return value;
    }

    /// <summary>
    /// Tries to parse the input string into the specified data type.
    /// </summary>
    /// <typeparam name="T">The data type to parse the input string into.</typeparam>
    /// <param name="input">The input string to be parsed.</param>
    /// <returns>
    /// The parsed value if the input string can be successfully parsed into the specified data type;
    /// otherwise, the default value of the specified data type.
    /// </returns>
    public static T? TryParse<T>(this string input)
    {
        var parser = GetTryParser(typeof(T));
        var value = parser(input);
        return value is T typed ? typed : default;
    }

    /// <summary>
    /// Tries to parse the input string as the specified type.
    /// </summary>
    /// <param name="input">The string to parse.</param>
    /// <param name="type">The type to parse the string as.</param>
    /// <returns>
    /// The parsed value if the input string could be parsed as the specified type; otherwise, null.
    /// </returns>
    public static object? TryParse(this string input, Type type)
    {
        var parser = GetTryParser(type);
        return parser(input);
    }

    /// <summary>
    /// Retrieves the parser function for the specified type.
    /// </summary>
    /// <param name="type">The type for which the parser is requested.</param>
    /// <returns>The parser function that can parse a string into an object of the specified type.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no parse function has been defined for the specified type.</exception>
    /// <remarks>
    /// Simply looks to the local parser cache and returns one if found for the specified type.
    /// Otherwise will use the <see cref="TypeDescriptor"/> of the current type and return the ConvertFrom function
    /// if is capable of converting from a string type.
    /// </remarks>
    private static Func<string, object> GetParser(Type type)
    {
        if (Parsers.Value.TryGetValue(type, out var parsers))
            return parsers.Parse;

        //The fallback is just seeing if the type has a defined converter which primitive types do.
        var converter = TypeDescriptor.GetConverter(type);
        if (converter.CanConvertFrom(typeof(string)))
            return s => converter.ConvertFrom(s)!;

        throw new InvalidOperationException($"No parse function has been defined for type '{type}'");
    }

    private static Func<string, object?> GetTryParser(Type type)
    {
        if (Parsers.Value.TryGetValue(type, out var parsers))
            return parsers.TryParse;

        //The fallback is just seeing if the type has a defined converter which primitive types do.
        var converter = TypeDescriptor.GetConverter(type);
        if (converter.CanConvertFrom(typeof(string)))
            return s => converter.IsValid(s) ? converter.ConvertFrom(s) : null;

        return _ => null;
    }

    private static IEnumerable<KeyValuePair<Type, Parsers>> GetParsers()
    {
        var types = typeof(LogixParser).Assembly.GetTypes().Where(IsLogixParsable);

        foreach (var type in types)
        {
            var parse = BuildParseFunction(type);
            var tryParse = BuildTryParseFunction(type);
            var parsers = new Parsers(parse, tryParse);
            yield return new KeyValuePair<Type, Parsers>(type, parsers);
        }
    }

    private static Func<string, object> BuildParseFunction(Type type)
    {
        var method = type.GetMethod("Parse",
            BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy, [typeof(string)]);

        if (method is null)
            throw new InvalidOperationException($"No Parse method defined for type {type.Name}.");

        var parameter = Expression.Parameter(typeof(string), "s");
        var call = Expression.Call(method, parameter);
        var converted = Expression.Convert(call, typeof(object));
        var lambda = Expression.Lambda<Func<string, object>>(converted, parameter);
        return lambda.Compile();
    }

    private static Func<string, object?> BuildTryParseFunction(Type type)
    {
        var method = type.GetMethod("TryParse",
            BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy, [typeof(string)]);

        if (method is null)
            throw new InvalidOperationException($"No TryParse method defined for type {type.Name}.");

        var parameter = Expression.Parameter(typeof(string), "s");
        var call = Expression.Call(method, parameter);
        var converted = Expression.Convert(call, typeof(object));
        var lambda = Expression.Lambda<Func<string, object?>>(converted, parameter);
        return lambda.Compile();
    }

    private static bool IsLogixParsable(Type type)
    {
        return type.GetInterfaces().Any(i =>
                   i.IsGenericType &&
                   i.GetGenericTypeDefinition() == typeof(ILogixParsable<>) &&
                   i.GetGenericArguments().All(a => !a.IsGenericTypeParameter));
    }
}

internal record Parsers(Func<string, object> Parse, Func<string, object?> TryParse);