using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Types;

/// <summary>
/// A <see cref="LogixType"/> that represents a string or collection of ASCII characters.
/// </summary>
/// <remarks>
/// <para>
/// A logix string type has predefined members <see cref="LEN"/> and <see cref="DATA"/>, which contain the
/// current string length and set of ASCII characters, respectively. This class is inherited by
/// <see cref="Predefined.STRING"/>, which is Rockwell's built in base string type. You may also create instance of
/// this class providing a string value and type name.
/// </para>
/// <para>
/// StringType has special cases in terms of it's L5X structure. Rockwell treats strings sort of like a value type,
/// giving it it's own <see cref="DataFormat"/>. However, when serialized as a member of a complex structure, the data
/// looks more like a generic structure type...</para>
/// </remarks>
public class StringType : StructureType, IEnumerable<char>, IEquatable<StringType>, IComparable<StringType>
{
    /// <summary>
    /// Creates a new <see cref="StringType"/> instance with the provided data.
    /// </summary>
    /// <param name="value">The string value of the type.</param>
    /// <remarks>This constructor will set <see cref="Name"/> to the name of the class <c>StingType</c>.</remarks>
    public StringType(string value) : base(GenerateElement(value))
    {
        Name = nameof(StringType);
        Value = value ?? throw new ArgumentNullException();
        //DATA = !value.IsEmpty() ? value.ToArrayType() : ArrayType.New<SINT>(1);
    }

    /// <summary>
    /// Creates a new <see cref="StringType"/> instance with the provided data.
    /// </summary>
    /// <param name="name">The name of the string type.</param>
    /// <param name="value">The string value of the type.</param>
    public StringType(string name, string value) : base(GenerateElement(value))
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        //DATA = !value.IsEmpty() ? value.ToArrayType() : ArrayType.New<SINT>(1);
        Value = value ?? throw new ArgumentNullException();
    }

    /// <inheritdoc />
    public StringType(XElement element) : base(element)
    {
        if (element.Name == L5XName.Data && element.Attribute(L5XName.Format)?.Value == DataFormat.String)
        {
            Name = element.Ancestors(L5XName.Tag).FirstOrDefault()?.Attribute(L5XName.DataType)?.Value
                   ?? throw new L5XException(L5XName.DataType, element);
            Value = element.Value;
            return;
        }

        Name = element.Attribute(L5XName.DataType)?.Value ?? throw new L5XException(L5XName.DataType, element);
        Value = element.Elements(L5XName.DataValueMember)
                    .FirstOrDefault(e => e.Attribute(L5XName.Name)?.Value == nameof(DATA))?.Value ??
                throw new L5XException(L5XName.DataValueMember, element);
    }

    /// <summary>
    /// Creates a new <see cref="StringType"/> given the name, pred
    /// </summary>
    /// <param name="name"></param>
    /// <param name="length"></param>
    /// <param name="value"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    protected StringType(string name, ushort length, string value) : base(GenerateElement(value))
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));

        if (value.Length > length)
            throw new ArgumentOutOfRangeException(
                $"The length {value.Length} of value can not be greater than {length} characters.");

        Value = value;
        //DATA = !value.IsEmpty() ? value.ToArrayType() : ArrayType.New<SINT>(1);
    }

    /// <inheritdoc />
    public override string Name { get; }

    /// <inheritdoc />
    public override DataTypeFamily Family => DataTypeFamily.String;

    /// <inheritdoc />
    // We are manually assigning these since they don't exist for string formatted data.  
    public override IEnumerable<Member> Members => new List<Member> { new(nameof(LEN), LEN), new(nameof(DATA), DATA) };

    /// <summary>
    /// The string value of the type.
    /// </summary>
    protected readonly string Value;

    /// <summary>
    /// Gets the character length value of the string. 
    /// </summary>
    /// <returns>A <see cref="DINT"/> logix atomic value representing the integer length of the string.</returns>
    public DINT LEN => Value.Length;

    /// <summary>
    /// Gets the array of bytes that represent the ASCII encoded string value.
    /// </summary>
    /// <returns>An array of <see cref="SINT"/> logix atomic values representing the bytes of the string.</returns>
    public ArrayType<SINT> DATA => new(Encoding.ASCII.GetBytes(Value).Select(b => new SINT((sbyte)b, Radix.Ascii)).ToArray());

    /// <inheritdoc />
    public IEnumerator<char> GetEnumerator() => Value.GetEnumerator();

    /// <inheritdoc />
    public override string ToString() => Value;

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <inheritdoc />
    public bool Equals(StringType? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => Equals(obj as StringType);

    /// <inheritdoc />
    public override int GetHashCode() => Value.GetHashCode();

    /// <summary>
    /// Determines if the provided objects are equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the provided objects are equal; otherwise, false.</returns>
    public static bool operator ==(StringType left, StringType right) => Equals(left, right);

    /// <summary>
    /// Determines if the provided objects not are equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the provided objects are not equal; otherwise, false.</returns>
    public static bool operator !=(StringType left, StringType right) => !Equals(left, right);

    /// <inheritdoc />
    public int CompareTo(StringType? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        return ReferenceEquals(other, null)
            ? 1
            : string.Compare(Value, other.Value, StringComparison.Ordinal);
    }

    /// <summary>
    /// Generates the default backing element for the <see cref="StringType"/>.
    /// </summary>
    /// <param name="value">The value of the string.</param>
    /// <returns>A new <see cref="XElement"/> containing the default L5X data.</returns>
    private static XElement GenerateElement(string value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        var element = new XElement(L5XName.Data);
        element.Add(new XAttribute(L5XName.Format, DataFormat.String));
        element.Add(new XAttribute(L5XName.Length, value.Length.ToString()));
        element.Add(new XCData(value));
        return element;
    }
}