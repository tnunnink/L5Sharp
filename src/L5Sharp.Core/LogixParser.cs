using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;

namespace L5Sharp.Core;

/// <summary>
/// Static class containing extensions for parsing a given string value into a specified type, either primitive or a type
/// defined in this library that implements the <see cref="ILogixParsable{T}"/>. This is the means through which we will
/// "deserialize" or map string values to strongly typed values.
/// </summary>
public static class LogixParser
{
    /// <summary>
    /// The internal cache of all parse functions for types found in this library.
    /// </summary>
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
    /// Retrieves the <c>Parse</c> function for the specified type.
    /// </summary>
    /// <param name="type">The type for which the parser is requested.</param>
    /// <returns>The parser function that can parse a string into an object of the specified type.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no parse function has been defined for the specified type.</exception>
    /// <remarks>
    /// Simply looks to the local parser cache and returns one if found for the specified type.
    /// Otherwise, will use the <see cref="TypeDescriptor"/> of the current type and return the ConvertFrom function
    /// if is capable of converting from a string type.
    /// </remarks>
    private static Func<string, object> GetParser(Type type)
    {
        if (Parsers.Value.TryGetValue(type, out var parser))
            return parser.Parse;

        //Intercept any .NET bool type because we want to handle 1/0 case as Logix sometimes uses those values instead of true/false.
        if (type == typeof(bool) || Nullable.GetUnderlyingType(type) == typeof(bool))
            return s => ParseBool(s);

        //The fallback is just seeing if the type has a defined converter which primitive types do.
        var converter = TypeDescriptor.GetConverter(type);
        if (converter.CanConvertFrom(typeof(string)))
            return s => converter.ConvertFrom(s)!;

        throw new InvalidOperationException($"No parse function has been defined for type '{type}'");
    }

    /// <summary>
    /// Retrieves the <c>TryParse</c> function for the specified type.
    /// </summary>
    /// <param name="type">The type for which the parser is requested.</param>
    /// <returns>The parser function that can attempt to parse a string into an object of the specified type and return default if not..</returns>
    /// <remarks>
    /// Simply looks to the local parser cache and returns one if found for the specified type.
    /// Otherwise, will use the <see cref="TypeDescriptor"/> of the current type and return the ConvertFrom function
    /// if is capable of converting from a string type. If nothing is found then returns a func that always returns <c>null</c>.
    /// </remarks>
    private static Func<string, object?> GetTryParser(Type type)
    {
        if (Parsers.Value.TryGetValue(type, out var parsers))
            return parsers.TryParse;
        
        //Intercept any .NET bool type because we want to handle 1/0 case as Logix sometimes uses those values instead of true/false.
        if (type == typeof(bool) || Nullable.GetUnderlyingType(type) == typeof(bool))
            return s => TryParseBool(s);

        //The fallback is just seeing if the type has a defined converter which primitive types do.
        var converter = TypeDescriptor.GetConverter(type);
        if (converter.CanConvertFrom(typeof(string)))
            return s => converter.IsValid(s) ? converter.ConvertFrom(s) : null;

        return _ => null;
    }

    /// <summary>
    /// Scans this assembly and returns a collection of types and corresponding <see cref="Parsers"/> which represent
    /// the functions for parsing string to the concrete typed object.
    /// </summary>
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

    /// <summary>
    /// Given the type implementing <see cref="ILogixParsable{T}"/>, builds a function that calls the <c>Parse</c>
    /// method of the type given an object to parse. Building a function using expressions will be faster than
    /// using reflection to invoke the method.
    /// </summary>
    private static Func<string, object> BuildParseFunction(Type type)
    {
        const BindingFlags flags = BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy;
        var method = type.GetMethods(flags).FirstOrDefault(type.IsParseFunction);

        if (method is null)
            throw new InvalidOperationException($"No Parse method defined for type {type.Name}.");

        var parameter = Expression.Parameter(typeof(string), "s");
        var call = Expression.Call(method, parameter);
        var converted = Expression.Convert(call, typeof(object));
        var lambda = Expression.Lambda<Func<string, object>>(converted, parameter);
        return lambda.Compile();
    }

    /// <summary>
    /// Given the type implementing <see cref="ILogixParsable{T}"/>, builds a function that calls the <c>TypeParse</c>
    /// method of the type given an object to parse. Building a function using expressions will be faster than
    /// using reflection to invoke the method.
    /// </summary>
    private static Func<string, object?> BuildTryParseFunction(Type type)
    {
        const BindingFlags flags = BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy;
        var method = type.GetMethods(flags).FirstOrDefault(type.IsTryParseFunction);

        if (method is null)
            throw new InvalidOperationException($"No TryParse method defined for type {type.Name}.");

        var parameter = Expression.Parameter(typeof(string), "s");
        var call = Expression.Call(method, parameter);
        var converted = Expression.Convert(call, typeof(object));
        var lambda = Expression.Lambda<Func<string, object?>>(converted, parameter);
        return lambda.Compile();
    }

    /// <summary>
    /// Determines if the provided type is a type implementing the <see cref="ILogixParsable{T}"/> interface and
    /// has a generic type argument that is not a generic type parameter. This should essentially
    /// return all concrete types that are parsable (have Parse/TryParse methods) in this library.
    /// </summary>
    private static bool IsLogixParsable(Type type)
    {
        return type.GetInterfaces().Any(i =>
            i.IsGenericType
            && i.GetGenericTypeDefinition() == typeof(ILogixParsable<>)
            && i.GetGenericArguments().All(a => !a.IsGenericParameter)
        );
    }

    /// <summary>
    /// Determines if the provided method info represents the <c>Parse</c> function defined by the
    /// interface <see cref="ILogixParsable{T}"/>.
    /// </summary>
    private static bool IsParseFunction(this Type type, MethodInfo info)
    {
        var parameters = info.GetParameters();

        return info.Name.Equals("Parse")
               && info.ReturnType.IsAssignableFrom(type)
               && parameters.Length == 1
               && parameters[0].ParameterType == typeof(string);
    }

    /// <summary>
    /// Determines if the provided method info represents the <c>TryParse</c> function defined by the
    /// interface <see cref="ILogixParsable{T}"/>.
    /// </summary>
    private static bool IsTryParseFunction(this Type type, MethodInfo info)
    {
        var parameters = info.GetParameters();

        return info.Name.Equals("TryParse")
               && info.ReturnType.IsAssignableFrom(type)
               && parameters.Length == 1
               && parameters[0].ParameterType == typeof(string);
    }

    /// <summary>
    /// Parses a given string input to a boolean value. This is a custom parser for boolean as Logix sometimes uses
    /// 1/0 instead of true/false. All booleans will be intercepted and use this custom parser instead of the default
    /// type descriptor function.
    /// </summary>
    private static bool ParseBool(string input)
    {
        if (string.IsNullOrEmpty(input))
            throw new ArgumentException("Can not parse null or emprty string to boolean");

        input = input.ToLower();

        return input switch
        {
            "1" => true,
            "0" => false,
            "true" => true,
            "false" => false,
            _ => throw new ArgumentOutOfRangeException(
                $"The input '{input}' could not be parsed to type '{typeof(bool)}'")
        };
    }

    /// <summary>
    /// Tries to parse a given string input to a boolean value. This is a custom parser for boolean as Logix sometimes uses
    /// 1/0 instead of true/false. All booleans will be intercepted and use this custom parser instead of the default
    /// type descriptor function.
    /// </summary>
    private static bool? TryParseBool(string input)
    {
        if (string.IsNullOrEmpty(input))
            return null;

        input = input.ToLower();

        return input switch
        {
            "1" => true,
            "0" => false,
            "true" => true,
            "false" => false,
            _ => null
        };
    }
}

/// <summary>
/// And internal record containing the Parse and TypeParse function for a given Type.
/// </summary>
internal record Parsers(Func<string, object> Parse, Func<string, object?> TryParse)
{
    public Func<string, object> Parse { get; } = Parse;
    public Func<string, object?> TryParse { get; } = TryParse;
}