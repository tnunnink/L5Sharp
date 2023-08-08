using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Types;

/// <summary>
/// A <see cref="L5Sharp.LogixType"/> that represents a string or collection of ASCII characters.
/// </summary>
/// <remarks>
/// <para>
/// A logix string type has predefined members <see cref="LEN"/> and <see cref="DATA"/>, which contain the
/// current string length and set of ASCII characters representing the string value, respectively.
/// This class is inherited by <see cref="Predefined.STRING"/>, which is Rockwell's built in base string type.
/// </para>
/// <para>
/// StringType has special cases in terms of it's L5X structure. Rockwell treats strings sort of like a value type,
/// giving it a special <see cref="DataFormat"/>. However, when serialized as a member of a complex structure, the data
/// looks more like a generic structure type.</para>
/// </remarks>
public class StringType : StructureType, IEnumerable<char>
{
    private const string LogixAsciiPattern = @"\$[A-Fa-f0-9]{2}|\$[tlpr'$]{1}|[\x00-\x7F]";

    /// <summary>
    /// Creates a new <see cref="StringType"/> initialized from the provided <see cref="XElement"/> data.
    /// </summary>
    /// <param name="element">The element to parse as the new member object.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    /// <exception cref="InvalidOperationException"><c>element</c> does not have required attributes or child elements.</exception>
    public StringType(XElement element) : base(DetermineName(element), GenerateMembers(DetermineValue(element)))
    {
    }

    /// <summary>
    /// Creates a new <see cref="StringType"/> initialized with default name and data.
    /// </summary>
    /// <remarks>This creates a default instance named "StringType" with an empty string.</remarks>
    public StringType() : base(nameof(StringType), GenerateMembers(string.Empty))
    {
    }

    /// <summary>
    /// Creates a new <see cref="StringType"/> initialized with default name and provided string data.
    /// </summary>
    /// <param name="value">The string value to initialize the type with.</param>
    /// <remarks>This creates a instance named "StringType" with the provided string value.</remarks>
    public StringType(string value) : base(nameof(StringType), GenerateMembers(value))
    {
    }

    /// <summary>
    /// Creates a new <see cref="StringType"/> instance with the provided data.
    /// </summary>
    /// <param name="name">The name of the string type.</param>
    /// <param name="value">The string value of the type.</param>
    /// <exception cref="ArgumentException"><c>name</c> is null or empty.</exception>
    public StringType(string name, string value) : base(name, GenerateMembers(value))
    {
    }

    /// <summary>
    /// Creates a new <see cref="StringType"/> object with the provided name and string value.
    /// </summary>
    /// <param name="name">The name of the string type.</param>
    /// <param name="value">The string value of the type.</param>
    /// <param name="length">The length to initialize DATA with.
    /// This should be greater or equal than the length of <c>value</c>.</param>
    /// <exception cref="ArgumentException"><c>name</c> is null or empty.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><c>value</c> is longer than <c>length</c>.</exception>
    /// <remarks>
    /// This constructor allows you to instantiate a string type with a specified DATA length so that values
    /// of different lengths may be assigned. This is meant to be used by deriving classes such as the predefined
    /// Rockwell <c>STRING</c> type and any other user defined string type.
    /// </remarks>
    protected StringType(string name, string value, ushort length) : base(name, GenerateMembers(value, length))
    {
    }

    /// <summary>
    /// Creates a new <see cref="StringType"/> initialized from the provided <see cref="XElement"/> and data length value.
    /// </summary>
    /// <param name="element">The element to parse as the new member object.</param>
    /// <param name="length">The length to initialize DATA with.
    /// This should be greater or equal than the length of the value found on the provided element object.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    /// <exception cref="InvalidOperationException"><c>element</c> does not have required attributes or child elements.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><c>element</c> contains a value longer than <c>length</c>.</exception>
    /// <remarks>
    /// This constructor allows you to instantiate a string type with a specified DATA length so that values
    /// of different lengths may be assigned. This is meant to be used by deriving classes such as the predefined
    /// Rockwell <c>STRING</c> type and any other user defined string type.
    /// </remarks>
    protected StringType(XElement element, ushort length) : base(DetermineName(element),
        GenerateMembers(DetermineValue(element), length))
    {
    }

    /// <inheritdoc />
    public sealed override DataTypeFamily Family => DataTypeFamily.String;

    /// <summary>
    /// Gets the LEN member of the string type structure. 
    /// </summary>
    /// <value>A <see cref="DINT"/> logix atomic value representing the integer length of the string.</value>
    /// <remarks>
    /// Setting this value will do nothing. The LEN member of a string type is a computed value based on the
    /// length of <see cref="DATA"/> (non-zero characters only). Internally, the data changed event is captured to sync
    /// this property value with that of the string length.
    /// </remarks>
    public DINT LEN
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the array of bytes that represent the ASCII encoded string value.
    /// </summary>
    /// <returns>An array of <see cref="SINT"/> logix atomic values representing the bytes of the string.</returns>
    public ArrayType<SINT> DATA
    {
        get => GetMember<ArrayType<SINT>>();
        set => SetMember(value);
    }

    /// <inheritdoc />
    public IEnumerator<char> GetEnumerator() => ToString().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <inheritdoc />
    public override string ToString() => ToString(DATA);

    /// <inheritdoc />
    public override XElement Serialize()
    {
        var element = new XElement(L5XName.Data);
        element.Add(new XAttribute(L5XName.Format, DataFormat.String));
        element.Add(new XAttribute(L5XName.Length, ToString().Length));
        element.Add(new XCData(ToString()));
        return element;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            StringType value => ToString() == value.ToString(),
            string value => ToString() == value,
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode() => ToString().GetHashCode();

    /// <summary>
    /// Converts the provided <see cref="string"/> to a <see cref="StringType"/> value.
    /// </summary>
    /// <param name="input">The value to convert.</param>
    /// <returns>A <see cref="StringType"/> type value.</returns>
    public static implicit operator StringType(string input) => new(input);

    /// <summary>
    /// Converts the provided <see cref="StringType"/> to a <see cref="string"/> value.
    /// </summary>
    /// <param name="input">The value to convert.</param>
    /// <returns>A <see cref="string"/> type value.</returns>
    public static implicit operator string(StringType input) => input.ToString();

    /// <summary>
    /// A custom serialization method that returns the string type as a structure element, instead of the string formatted
    /// L5X structure. 
    /// </summary>
    /// <returns>A <see cref="XElement"/> object representing the serialized string structure data.</returns>
    public XElement SerializeStructure()
    {
        var value = ToString();

        var element = new XElement(L5XName.Structure);
        element.Add(new XAttribute(L5XName.DataType, Name));

        var len = new XElement(L5XName.DataValueMember);
        len.Add(new XAttribute(L5XName.Name, nameof(LEN)));
        len.Add(new XAttribute(L5XName.DataType, LEN.Name));
        len.Add(new XAttribute(L5XName.Radix, Radix.Decimal));
        len.Add(new XAttribute(L5XName.Value, value.Length));
        element.Add(len);

        var data = new XElement(L5XName.DataValueMember);
        data.Add(new XAttribute(L5XName.Name, nameof(DATA)));
        data.Add(new XAttribute(L5XName.DataType, Name));
        data.Add(new XAttribute(L5XName.Radix, Radix.Ascii));
        data.Add(new XCData(value));
        element.Add(data);

        return element;
    }

    /// <inheritdoc />
    /// <remarks>
    /// The <see cref="LEN"/> member of the string type structure should be determined by the length of the string.
    /// There is really no way to enforce or sync it's value other than conveniently intercepting any data changed event
    /// for the string type members and setting/resetting LEN to avoid any inconsistencies.
    /// </remarks>
    protected override void OnMemberDataChanged(object sender, EventArgs e)
    {
        var length = ToString().Length;
        if (LEN != length) LEN = length;
        base.OnMemberDataChanged(sender, e);
    }

    /// <summary>
    /// Determines the string type name from a given element. This is slightly tricky because we have different places
    /// to look depending on if this is a string formatted element or a nested string structure.
    /// </summary>
    private static string DetermineName(XElement element)
    {
        if (element.Attribute(L5XName.Format)?.Value == DataFormat.String)
            return element.Ancestors().FirstOrDefault()?.Attribute(L5XName.DataType)?.Value ?? nameof(StringType);

        return element.Get<string>(L5XName.DataType);
    }

    /// <summary>
    /// Determines the string type name from a given element. This is slightly tricky because we have different places
    /// to look depending on if this is a string formatted element or a nested string structure. 
    /// </summary>
    private static string DetermineValue(XElement element)
    {
        if (element.Attribute(L5XName.Format)?.Value == DataFormat.String)
            return element.Value;

        return element.Elements(L5XName.DataValueMember)
                   .FirstOrDefault(e => e.Attribute(L5XName.Name)?.Value == nameof(DATA))?.Value
               ?? string.Empty;
    }

    /// <summary>
    /// Generates the static string type logix members given the string data.
    /// </summary>
    private static IEnumerable<LogixMember> GenerateMembers(string value)
    {
        var array = ToArray(value);
        var len = new LogixMember(nameof(LEN), new DINT(array.Length));
        var data = new LogixMember(nameof(DATA), new ArrayType<SINT>(array));
        return new List<LogixMember> { len, data };
    }

    /// <summary>
    /// Generates the static string type logix member given the string data and the predefined
    /// DATA array length. This method will create the SINT array of the specified length and then
    /// assign the provided value.
    /// </summary>
    private static IEnumerable<LogixMember> GenerateMembers(string value, ushort length)
    {
        var array = ToArray(value);

        if (array.Length > length)
            throw new ArgumentOutOfRangeException(nameof(value),
                $"The string value '{value}' length {value.Length} is greater than the predefined length {length}.");

        var len = new LogixMember(nameof(LEN), new DINT(array.Length));
        var data = new LogixMember(nameof(DATA), ArrayType.New<SINT>(length)) { DataType = array };

        return new List<LogixMember> { len, data };
    }

    /// <summary>
    /// Converts the provided string value to a SINT array. Handles empty or null string. SINT array can not be empty.
    /// </summary>
    private static SINT[] ToArray(string value)
    {
        if (string.IsNullOrEmpty(value)) return new SINT[] { new() };
        value = value.TrimStart('\'').TrimEnd('\'');
        var matches = Regex.Matches(value, LogixAsciiPattern, RegexOptions.Compiled);
        return matches.Select(m => SINT.Parse(m.Value)).ToArray();
    }

    /// <summary>
    /// Converts the provided SINT array into a string value. Will trim bytes that are zero.
    /// </summary>
    private static string ToString(IEnumerable<SINT> array)
    {
        var ascii = array.Where(s => s > 0)
            .Select(s => s.ToString(Radix.Ascii).TrimStart('\'').TrimEnd('\'')).ToArray();
        return $"'{string.Join(string.Empty, ascii)}'";
    }
}