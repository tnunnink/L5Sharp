using System;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Common;

/// <summary>
/// Represents an argument to an instruction, which could be a tag name reference or an immediate atomic value.
/// </summary>
public class Argument
{
    private readonly object _value;

    /// <summary>
    /// Creates a new <see cref="Argument"/> wrapping the provided immediate atomic value.
    /// </summary>
    /// <param name="value">An <see cref="AtomicType"/> representing the argument.</param>
    /// <exception cref="ArgumentNullException"><c>value</c> is <c>null</c>.</exception>
    private Argument(AtomicType value)
    {
        _value = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// Creates a new <see cref="Argument"/> wrapping the provided tag name reference.
    /// </summary>
    /// <param name="value">A <see cref="TagName"/> representing the argument.</param>
    /// <exception cref="ArgumentNullException"><c>value</c> is <c>null</c>.</exception>
    private Argument(TagName value)
    {
        _value = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// Indicates whether the argument is an immediate atomic value.
    /// </summary>
    /// <value><c>true</c> if the underlying value is an <see cref="AtomicType"/> object; Otherwise, <c>false</c>.</value>
    public bool IsImmediate => _value is AtomicType;

    /// <summary>
    /// Indicates whether the argument is an tag name reference.
    /// </summary>
    /// <value><c>true</c> if the underlying value is a <see cref="TagName"/> object; Otherwise, <c>false</c>.</value>
    public bool IsTag => _value is TagName;

    /// <summary>
    /// Implicitly converts the provided <see cref="TagName"/> to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="tagName">The <see cref="TagName"/> object to convert.</param>
    /// <returns>A <see cref="Argument"/> object containing the value of the tag name.</returns>
    public static implicit operator Argument(TagName tagName) => new(tagName);

    /// <summary>
    /// Implicitly converts the provided <see cref="TagName"/> to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="tagName">The <see cref="TagName"/> object to convert.</param>
    /// <returns>A <see cref="Argument"/> object containing the value of the tag name.</returns>
    public static implicit operator Argument(string tagName) => new(new TagName(tagName));

    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(bool value) => new(new BOOL(value));

    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(sbyte value) => new(new SINT(value));
    
    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(byte value) => new(new USINT(value));
    
    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(short value) => new(new INT(value));

    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(ushort value) => new(new UINT(value));

    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(int value) => new(new DINT(value));
    
    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(uint value) => new(new UDINT(value));
    
    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(long value) => new(new LINT(value));
    
    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(ulong value) => new(new ULINT(value));
    
    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(float value) => new(new REAL(value));
    
    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(double value) => new(new LREAL(value));

    /// <summary>
    /// Explicitly converts the provided <see cref="Argument"/> to a <see cref="TagName"/>.
    /// </summary>
    /// <param name="argument">The <see cref="Argument"/> object to convert.</param>
    /// <returns>A <see cref="TagName"/> object representing the value of the argument.</returns>
    public static explicit operator TagName(Argument argument) => (TagName) argument._value;

    /// <summary>
    /// Explicitly converts the provided <see cref="Argument"/> to an <see cref="AtomicType"/>.
    /// </summary>
    /// <param name="argument">The <see cref="Argument"/> object to convert.</param>
    /// <returns>A <see cref="AtomicType"/> object representing the value of the argument.</returns>
    public static explicit operator AtomicType(Argument argument) => (AtomicType) argument._value;

    /// <inheritdoc />
    public override bool Equals(object? obj) => _value.Equals(obj);

    /// <inheritdoc />
    public override int GetHashCode() => _value.GetHashCode();

    /// <inheritdoc />
    public override string ToString() => _value.ToString();
}