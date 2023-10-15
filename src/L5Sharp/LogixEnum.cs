using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace L5Sharp;

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
public abstract class LogixEnum<TEnum, TValue> :
    IEquatable<LogixEnum<TEnum, TValue>>,
    IComparable<LogixEnum<TEnum, TValue>>
    where TEnum : LogixEnum<TEnum, TValue>
    where TValue : IEquatable<TValue>, IComparable<TValue>
{
    /// <summary>
    /// Creates an enumeration with the specified name and value.
    /// </summary>
    /// <param name="name">The common name of the enumeration option.</param>
    /// <param name="value">The corresponding value of the enumeration option.</param>
    /// <exception cref="ArgumentNullException"><c>name</c> or <c>value</c> are null.</exception>
    protected LogixEnum(string name, TValue value)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// The display name of the enumeration type.
    /// </summary>
    /// <value>A <see cref="string"/> common enumeration field name.</value>
    public string Name { get; }

    /// <summary>
    /// The value of the enumeration type.
    /// </summary>
    /// <value>A value of the specified enumeration type.</value>
    public TValue Value { get; }

    /// <summary>
    /// Returns all enumeration options for the specified enumeration type.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{T}"/> containing all enumeration values of the specified type.</returns>
    public static IEnumerable<TEnum> All() => FromNamOptions.Value.Values.ToList().AsReadOnly();

    /// <summary>
    /// Gets the item associated with the specified name.
    /// </summary>
    /// <param name="name">The name of the item to get.</param>
    /// <param name="ignoreCase"><c>true</c> to ignore case during the comparison; otherwise, <c>false</c>.</param>
    /// <returns>
    /// The item associated with the specified name. 
    /// If the specified name is not found, throws a <see cref="KeyNotFoundException"/>.
    /// </returns>
    /// <exception cref="ArgumentException"><paramref name="name"/> is <c>null</c>.</exception> 
    /// <exception cref="KeyNotFoundException"><paramref name="name"/> does not exist.</exception> 
    /// <seealso cref="LogixEnum{TEnum, TValue}.TryFromName(string, out TEnum)"/>
    /// <seealso cref="LogixEnum{TEnum, TValue}.TryFromName(string, bool, out TEnum)"/>
    public static TEnum FromName(string name, bool ignoreCase = false)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Argument can not be null or empty.", name);

        return GetFromName(ignoreCase ? FromNameIgnoreCaseOptions.Value : FromNamOptions.Value);

        TEnum GetFromName(IReadOnlyDictionary<string, TEnum> dictionary)
        {
            if (!dictionary.TryGetValue(name, out var result))
                throw new KeyNotFoundException($"No {typeof(TEnum).Name} with Name \\\"{name}\\\" found.");

            return result;
        }
    }

    /// <summary>
    /// Gets the item associated with the specified name.
    /// </summary>
    /// <param name="name">The name of the item to get.</param>
    /// <param name="result">
    /// When this method returns, contains the item associated with the specified name, if the key is found; 
    /// otherwise, <c>null</c>. This parameter is passed uninitialized.</param>
    /// <returns>
    /// <c>true</c> if the <see cref="LogixEnum{TEnum, TValue}"/> contains an item with the specified name; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentException"><paramref name="name"/> is <c>null</c>.</exception> 
    /// <seealso cref="LogixEnum{TEnum, TValue}.FromName(string, bool)"/>
    /// <seealso cref="LogixEnum{TEnum, TValue}.TryFromName(string, bool, out TEnum)"/>
    public static bool TryFromName(string? name, out TEnum? result) => TryFromName(name, false, out result);

    /// <summary>
    /// Gets the item associated with the specified name.
    /// </summary>
    /// <param name="name">The name of the item to get.</param>
    /// <param name="ignoreCase"><c>true</c> to ignore case during the comparison; otherwise, <c>false</c>.</param>
    /// <param name="result">
    /// When this method returns, contains the item associated with the specified name, if the name is found; 
    /// otherwise, <c>null</c>. This parameter is passed uninitialized.</param>
    /// <returns>
    /// <c>true</c> if the <see cref="LogixEnum{TEnum, TValue}"/> contains an item with the specified name; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentException"><paramref name="name"/> is <c>null</c>.</exception> 
    /// <seealso cref="LogixEnum{TEnum, TValue}.FromName(string, bool)"/>
    /// <seealso cref="LogixEnum{TEnum, TValue}.TryFromName(string, out TEnum)"/>
    public static bool TryFromName(string? name, bool ignoreCase, out TEnum? result)
    {
        if (!string.IsNullOrEmpty(name))
            return ignoreCase
                ? FromNameIgnoreCaseOptions.Value.TryGetValue(name, out result)
                : FromNamOptions.Value.TryGetValue(name, out result);

        result = default;
        return false;
    }

    /// <summary>
    /// Gets an item associated with the specified value.
    /// </summary>
    /// <param name="value">The value of the item to get.</param>
    /// <returns>
    /// The first item found that is associated with the specified value.
    /// If the specified value is not found, throws a <see cref="KeyNotFoundException"/>.
    /// </returns>
    /// <exception cref="KeyNotFoundException"><paramref name="value"/> does not exist.</exception> 
    /// <seealso cref="LogixEnum{TEnum, TValue}.FromValue(TValue, TEnum)"/>
    /// <seealso cref="LogixEnum{TEnum, TValue}.TryFromValue(TValue, out TEnum)"/>
    public static TEnum FromValue(TValue value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        if (!FromValueOptions.Value.TryGetValue(value, out var result))
            throw new KeyNotFoundException($"No {typeof(TEnum).Name} with Value {value} found.");

        return result;
    }

    /// <summary>
    /// Gets an item associated with the specified value.
    /// </summary>
    /// <param name="value">The value of the item to get.</param>
    /// <param name="defaultValue">The value to return when item not found.</param>
    /// <returns>
    /// The first item found that is associated with the specified value.
    /// If the specified value is not found, returns <paramref name="defaultValue"/>.
    /// </returns>
    /// <seealso cref="LogixEnum{TEnum, TValue}.FromValue(TValue)"/>
    /// <seealso cref="LogixEnum{TEnum, TValue}.TryFromValue(TValue, out TEnum)"/>
    public static TEnum FromValue(TValue value, TEnum defaultValue)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        return !FromValueOptions.Value.TryGetValue(value, out var result) ? defaultValue : result;
    }

    /// <summary>
    /// Gets an item associated with the specified value.
    /// </summary>
    /// <param name="value">The value of the item to get.</param>
    /// <param name="result">
    /// When this method returns, contains the item associated with the specified value, if the value is found; 
    /// otherwise, <c>null</c>. This parameter is passed uninitialized.</param>
    /// <returns>
    /// <c>true</c> if the <see cref="LogixEnum{TEnum, TValue}"/> contains an item with the specified name; otherwise, <c>false</c>.
    /// </returns>
    /// <seealso cref="LogixEnum{TEnum, TValue}.FromValue(TValue)"/>
    /// <seealso cref="LogixEnum{TEnum, TValue}.FromValue(TValue, TEnum)"/>
    public static bool TryFromValue(TValue? value, out TEnum? result)
    {
        if (value is not null)
            return FromValueOptions.Value.TryGetValue(value, out result);

        result = default;
        return false;
    }


    /// <inheritdoc />
    public override string ToString() => Value.ToString();

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
    public virtual int CompareTo(LogixEnum<TEnum, TValue> other) =>
        Value.CompareTo(other.Value);


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
    /// <param name="logixEnum">The enumeration type.</param>
    /// <returns>A value representing the </returns>
    public static implicit operator TValue(LogixEnum<TEnum, TValue> logixEnum) => logixEnum.Value;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static explicit operator LogixEnum<TEnum, TValue>(TValue value) => FromValue(value);


    private static readonly Lazy<TEnum[]> EnumOptions = new(GetOptions, LazyThreadSafetyMode.ExecutionAndPublication);

    private static readonly Lazy<Dictionary<string, TEnum>> FromNamOptions = new(() =>
        EnumOptions.Value.ToDictionary(item => item.Name));

    private static readonly Lazy<Dictionary<string, TEnum>> FromNameIgnoreCaseOptions = new(() =>
        EnumOptions.Value.ToDictionary(item => item.Name, StringComparer.OrdinalIgnoreCase));

    private static readonly Lazy<Dictionary<TValue, TEnum>> FromValueOptions = new(() =>
    {
        var dictionary = new Dictionary<TValue, TEnum>();
        foreach (var item in EnumOptions.Value)
        {
            dictionary.TryAdd(item.Value, item);
        }

        return dictionary;
    });


    private static TEnum[] GetOptions()
    {
        var baseType = typeof(TEnum);
        return Assembly.GetAssembly(baseType)
            .GetTypes()
            .Where(t => baseType.IsAssignableFrom(t))
            .SelectMany(GetFieldsOfType<TEnum>)
            .OrderBy(t => t.Name)
            .ToArray();
    }

    private static List<TFieldType> GetFieldsOfType<TFieldType>(Type type)
    {
        return type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(f => type.IsAssignableFrom(f.FieldType))
            .Select(f => (TFieldType)f.GetValue(null))
            .ToList();
    }
}