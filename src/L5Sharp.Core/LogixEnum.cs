using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace L5Sharp.Core;

/// <summary>
/// A base class for all logix enumeration types.
/// </summary>
/// <remarks>
/// This abstraction was added to allow caller to obtains a <see cref="LogixEnum"/> without having express the type statically
/// or using generics. The makes it easier to get a collection of enumeration options for a given type. It also makes reflection
/// code and patter matching easier since we don't have to worry about generic type parameters.
/// </remarks>
public abstract class LogixEnum
{
    /// <summary>
    /// a global enum cache for all enumeration types defined in the assembly. 
    /// </summary>
    private static readonly Lazy<Dictionary<Type, LogixEnum[]>> Enums = new(AllOptions,
        LazyThreadSafetyMode.ExecutionAndPublication);

    /// <summary>
    /// Creates an enumeration with the specified name.
    /// </summary>
    /// <param name="name">The common name of the enumeration option.</param>
    /// <exception cref="ArgumentNullException"><paramref name="name"/> is null.</exception>
    protected LogixEnum(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    /// <summary>
    /// The display name of the enumeration type.
    /// </summary>
    /// <value>A <see cref="string"/> common enumeration field name.</value>
    public string Name { get; }

    /// <summary>
    /// Retrieves all names for all <see cref="LogixEnum"/> defined in the library.
    /// </summary>
    /// <returns>A collection <see cref="string"/> names for all enums.</returns>
    public static IEnumerable<string> Names()
    {
        return Enums.Value.SelectMany(v => v.Value).Select(x => x.Name);
    }

    /// <summary>
    /// Retrieves all names for a <see cref="LogixEnum"/> of the specified enum type.
    /// </summary>
    /// <param name="type">The type for which to retrieve the enumeration names.</param>
    /// <returns>A collection <see cref="string"/> name for the type. If the type is not a valid type,
    /// then an empty collection.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="type"/> is null.</exception>
    /// <exception cref="ArgumentException"><paramref name="type"/> is not a valid <see cref="LogixEnum"/>.</exception>
    public static IEnumerable<string> Names(Type type)
    {
        if (type is null)
            throw new ArgumentNullException(nameof(type));

        return Enums.Value.TryGetValue(type, out var options)
            ? options.Select(e => e.Name)
            : throw new ArgumentException($"Type '{type.Name}' is not a {nameof(LogixEnum)}.");
    }

    /// <summary>
    /// Retrieves all names for a <see cref="LogixEnum"/> of the specified enum type.
    /// </summary>
    /// <typeparam name="TEnum">The type for which to retrieve the enumeration names.</typeparam>
    /// <returns>A collection <see cref="string"/> name for the type. If the type is not a valid type,
    /// then an empty collection.</returns>
    public static IEnumerable<string> Names<TEnum>() where TEnum : LogixEnum
    {
        return Enums.Value[typeof(TEnum)].Select(e => e.Name);
    }

    /// <summary>
    /// Retrieves all <see cref="LogixEnum"/> options for the provided enumeration type.
    /// </summary>
    /// <param name="type">The type for which to retrieve the enumeration names.</param>
    /// <returns>A collection <see cref="LogixEnum"/> values for the type. If the type is not a valid type,
    /// then an empty collection.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="type"/> is null.</exception>
    /// <exception cref="ArgumentException"><paramref name="type"/> is not a valid <see cref="LogixEnum"/>.</exception>
    public static IEnumerable<LogixEnum> Options(Type type)
    {
        if (type is null)
            throw new ArgumentNullException(nameof(type));

        return Enums.Value.TryGetValue(type, out var options)
            ? options
            : throw new ArgumentException($"Type '{type.Name}' is not a {nameof(LogixEnum)}.");
    }

    /// <summary>
    /// Retrieves all <see cref="LogixEnum"/> options for the provided enumeration type.
    /// </summary>
    /// <typeparam name="TEnum">The type for which to retrieve the enumeration names.</typeparam>
    /// <returns>A collection <see cref="LogixEnum"/> values for the type. If the type is not a valid type,
    /// then an empty collection.</returns>
    public static IEnumerable<TEnum> Options<TEnum>() where TEnum : LogixEnum
    {
        return Enums.Value[typeof(TEnum)].Cast<TEnum>();
    }

    /// <summary>
    /// Finds all types deriving from this base class and retrieves each statically defined <see cref="LogixEnum"/> instance
    /// for the types, and returns a dictionary. This is the primary initialization factory for the lazy global enum cache.
    /// </summary>
    /// <returns>A <see cref="Dictionary{TKey,TValue}"/> containing types and collections enumeration objects
    /// associated with the type.</returns>
    private static Dictionary<Type, LogixEnum[]> AllOptions()
    {
        var baseType = typeof(LogixEnum);

        return baseType.Assembly.GetTypes()
            .Where(t => baseType.IsAssignableFrom(t))
            .ToDictionary(t => t, t => GetOptions(t).ToArray());
    }

    /// <summary>
    /// Retrieves all statically declared <see cref="LogixEnum"/> fields for the provided type using reflection.
    /// </summary>
    /// <param name="type">The <see cref="LogixEnum"/> type for which to find the enum options.</param>
    /// <returns>A collection of <see cref="LogixEnum"/> options defined as static fields on the type.</returns>
    /// <exception cref="InvalidOperationException">The provided <paramref name="type"/> is not assignable from
    /// a <see cref="LogixEnum"/> type.</exception>
    private static IEnumerable<LogixEnum> GetOptions(Type type)
    {
        if (!typeof(LogixEnum).IsAssignableFrom(type))
            throw new InvalidOperationException($"Can not retrieve LogixEnum options for type '{type}'");

        return type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(f => type.IsAssignableFrom(f.FieldType))
            .Select(f => (LogixEnum)f.GetValue(null)!)
            .OrderBy(e => e.Name);
    }
}

/// <summary>
/// A base class for all logix enumeration types.
/// </summary>
/// <remarks>
/// This code was taken from https://github.com/ardalis/SmartEnum and modified to suit needs of this library.
/// Wanted to remove and external dependencies and not rely on other packages.
/// This class provided some base functionality for working with a logix enum type.
/// This includes methods for retrieving all enums of a specified type, and parsing enums from a name or value.
/// </remarks>
/// <typeparam name="TEnum">The type that is inheriting from this class.</typeparam>
/// <typeparam name="TValue">The type of the inner value.</typeparam>
public abstract class LogixEnum<TEnum, TValue> : LogixEnum,
    IEquatable<LogixEnum<TEnum, TValue>>,
    IComparable<LogixEnum<TEnum, TValue>>,
    ILogixParsable<TEnum>
    where TEnum : LogixEnum<TEnum, TValue>
    where TValue : IEquatable<TValue>, IComparable<TValue>
{
    private static readonly Lazy<Dictionary<string, TEnum>> NameLookup = new(() =>
        Options<TEnum>().ToDictionary(item => item.Name, StringComparer.OrdinalIgnoreCase));

    private static readonly Lazy<Dictionary<string, TEnum>> ValueLookup = new(() =>
        Options<TEnum>().ToDictionary(item => item.ToString()));

    /// <summary>
    /// Creates an enumeration with the specified name and value.
    /// </summary>
    /// <param name="name">The common name of the enumeration option.</param>
    /// <param name="value">The corresponding value of the enumeration option.</param>
    /// <exception cref="ArgumentNullException"><c>name</c> or <c>value</c> are null.</exception>
    protected LogixEnum(string name, TValue value) : base(name)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// The value of the enumeration type.
    /// </summary>
    /// <value>A value of the specified enumeration type.</value>
    public TValue Value { get; }

    /// <summary>
    /// Returns all enumeration options for the specified enumeration type.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{T}"/> containing all enumeration values of the specified type.</returns>
    public static IEnumerable<TEnum> All() => Options<TEnum>().ToList().AsReadOnly();

    /// <summary>
    /// Parses the specified string representation of an enumeration name or value into its corresponding
    /// enumeration type.
    /// </summary>
    /// <param name="value">The string representation of the enum. This can be the name or value.</param>
    /// <returns>The enum value corresponding to the specified string representation.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="value"/> is null.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if no enum value with the specified string representation is found.</exception>
    /// <remarks>
    /// This method will first check for enums by name. If none exist, then it will check the value lookup dictionary
    /// of enumeration values converted to string. This combines factories for name and value
    /// into a single method to avoid having to worry about the right one to use.
    /// In this library we typically represent the XML value as the <see cref="Value"/> property which is also a string,
    /// but we also in some places will relay on name, and we want to support both.
    /// </remarks>
    public static TEnum Parse(string value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        if (NameLookup.Value.TryGetValue(value, out var named))
            return named;

        if (ValueLookup.Value.TryGetValue(value, out var literal))
            return literal;

        throw new KeyNotFoundException($"No {typeof(TEnum).Name} with Value {value} found.");
    }

    /// <summary>
    /// Tries to parse the specified string representation of an enumeration name or value into its corresponding
    /// enumeration type.
    /// </summary>
    /// <param name="value">The string to parse. This can be the name or value.</param>
    /// <returns>The enum value corresponding to the specified string representation if found; Otherwise, <c>null</c>.</returns>
    /// <remarks>
    /// This method will first check for enums by name. If none exist, then it will check the value lookup dictionary
    /// of enumeration values converted to string. This combines factories for name and value
    /// into a single method to avoid having to worry about the right one to use.
    /// In this library we typically represent the XML value as the <see cref="Value"/> property which is also a string,
    /// but we also in some places will relay on name, and we want to support both.
    /// </remarks>
    public static TEnum? TryParse(string? value)
    {
        if (value is null) return default;

        if (NameLookup.Value.TryGetValue(value, out var named))
            return named;

        // ReSharper disable once ConvertIfStatementToReturnStatement
        // ReSharper disable once CanSimplifyDictionaryTryGetValueWithGetValueOrDefault
        if (ValueLookup.Value.TryGetValue(value, out var literal))
            return literal;

        return default;
    }

    /// <inheritdoc />
    public sealed override string ToString() => Value.ToString()!;

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is not LogixEnum<TEnum, TValue> logixEnum) return false;
        return GetType() == obj.GetType() && Equals(Value, logixEnum.Value);
    }

    /// <inheritdoc />
    public override int GetHashCode() => Value.GetHashCode();

    /// <summary>
    /// Returns a value indicating whether this instance is equal to a specified <see cref="LogixEnum{TEnum, TValue}"/> value.
    /// </summary>
    /// <param name="other">An <see cref="LogixEnum{TEnum, TValue}"/> value to compare to this instance.</param>
    /// <returns><c>true</c> if <paramref name="other"/> has the same value as this instance; otherwise, <c>false</c>.</returns>
    public virtual bool Equals(LogixEnum<TEnum, TValue>? other)
    {
        if (ReferenceEquals(this, other)) return true;
        return other is not null && Value.Equals(other.Value);
    }

    /// <summary>
    /// Performs equality check on the provided <see cref="LogixEnum{TEnum,TValue}"/> types.
    /// </summary>
    /// <param name="left">An instance of the enumeration to check.</param>
    /// <param name="right">An instance of the enumeration to check.</param>
    /// <returns><c>true</c> if the types are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(LogixEnum<TEnum, TValue> left, LogixEnum<TEnum, TValue> right) =>
        Equals(left, right);

    /// <summary>
    /// Performs equality check on the provided <see cref="LogixEnum{TEnum,TValue}"/> types.
    /// </summary>
    /// <param name="left">An instance of the enumeration to check.</param>
    /// <param name="right">An instance of the enumeration to check.</param>
    /// <returns><c>true</c> if the types are NOT equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(LogixEnum<TEnum, TValue> left, LogixEnum<TEnum, TValue> right) =>
        !Equals(left, right);

    /// <summary>
    /// Compares this instance to a specified <see cref="LogixEnum{TEnum, TValue}"/> and returns an indication of
    /// their relative values.
    /// </summary>
    /// <param name="other">An <see cref="LogixEnum{TEnum, TValue}"/> value to compare to this instance.</param>
    /// <returns>A signed number indicating the relative values of this instance and <paramref name="other"/>.</returns>
    public virtual int CompareTo(LogixEnum<TEnum, TValue>? other) =>
        other is not null ? Value.CompareTo(other.Value) : -1;

    /// <summary>
    /// Compares this instance to a specified <see cref="LogixEnum{TEnum, TValue}"/> and returns an indication if
    /// <c>left</c> is less than <c>right</c>.  
    /// </summary>
    /// <param name="left">An instance of <see cref="LogixEnum{TEnum,TValue}"/> to compare.</param>
    /// <param name="right">An instance of <see cref="LogixEnum{TEnum,TValue}"/> to compare.</param>
    /// <returns><c>true</c> if left is less than <c>right</c>.</returns>
    public static bool operator <(LogixEnum<TEnum, TValue> left, LogixEnum<TEnum, TValue> right) =>
        left.CompareTo(right) < 0;


    /// <summary>
    /// Compares this instance to a specified <see cref="LogixEnum{TEnum, TValue}"/> and returns an indication if
    /// <c>left</c> is less than or equal to <c>right</c>.  
    /// </summary>
    /// <param name="left">An instance of <see cref="LogixEnum{TEnum,TValue}"/> to compare.</param>
    /// <param name="right">An instance of <see cref="LogixEnum{TEnum,TValue}"/> to compare.</param>
    /// <returns><c>true</c> if left is less than or equal to <c>right</c>.</returns>
    public static bool operator <=(LogixEnum<TEnum, TValue> left, LogixEnum<TEnum, TValue> right) =>
        left.CompareTo(right) <= 0;

    /// <summary>
    /// Compares this instance to a specified <see cref="LogixEnum{TEnum, TValue}"/> and returns an indication if
    /// <c>left</c> is greater than <c>right</c>.  
    /// </summary>
    /// <param name="left">An instance of <see cref="LogixEnum{TEnum,TValue}"/> to compare.</param>
    /// <param name="right">An instance of <see cref="LogixEnum{TEnum,TValue}"/> to compare.</param>
    /// <returns><c>true</c> if left is greater than <c>right</c>.</returns>
    public static bool operator >(LogixEnum<TEnum, TValue> left, LogixEnum<TEnum, TValue> right) =>
        left.CompareTo(right) > 0;

    /// <summary>
    /// Compares this instance to a specified <see cref="LogixEnum{TEnum, TValue}"/> and returns an indication if
    /// <c>left</c> is greater than or equal to <c>right</c>.  
    /// </summary>
    /// <param name="left">An instance of <see cref="LogixEnum{TEnum,TValue}"/> to compare.</param>
    /// <param name="right">An instance of <see cref="LogixEnum{TEnum,TValue}"/> to compare.</param>
    /// <returns><c>true</c> if left is greater than or equal to <c>right</c>.</returns>
    public static bool operator >=(LogixEnum<TEnum, TValue> left, LogixEnum<TEnum, TValue> right) =>
        left.CompareTo(right) >= 0;

    /// <summary>
    /// Implicitly converts the provided <see cref="LogixEnum{TEnum,TValue}"/> to the underlying value. 
    /// </summary>
    /// <param name="enumeration">The enumeration type.</param>
    /// <returns>A value representing the enumeration</returns>
    public static implicit operator TValue(LogixEnum<TEnum, TValue> enumeration) => enumeration.Value;

    /// <summary>
    /// Implicitly converts the provided value to a <see cref="LogixEnum{TEnum,TValue}"/>. 
    /// </summary>
    /// <param name="value">The enumeration value.</param>
    /// <returns>A enumeration value representing the value type.</returns>
    public static explicit operator LogixEnum<TEnum, TValue>(TValue value) => Parse(value.ToString()!);
}