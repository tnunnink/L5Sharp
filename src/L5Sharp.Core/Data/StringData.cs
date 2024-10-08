using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A <see cref="LogixData"/> type that represents a string or collection of ASCII characters.
/// </summary>
/// <remarks>
/// <para>
/// A logix string type has predefined members <see cref="LEN"/> and <see cref="DATA"/>, which contain the
/// current string length and set of ASCII characters representing the string value, respectively.
/// This class is inherited by <see cref="STRING"/>, which is Rockwell's built-in base string type.
/// Users can derive from this base class to create user defined string types if desired.
/// </para>
/// <para>
/// <see cref="StringData"/> will not pass the provided element to the base class but rather extract
/// the data type name and string value and act as a wrapper over that data. The element will be reconstructed
/// when <see cref="Serialize"/> or <see cref="SerializeStructure"/> is called. We need to do all this due to how
/// strings are serialized in an L5X which is in a special format. Also, we want to treat strings similar to atomics
/// as if they are immutable values types.
/// </para>
/// </remarks>
public class StringData : StructureData, IEnumerable<SINT>
{
    /// <summary>
    /// The pattern that extracts the ASCII character segments from a string value.
    /// </summary>
    private static readonly Regex LogixAsciiPattern = new(@"\$[A-Fa-f0-9]{2}|\$[tlpr'$]{1}|[\x00-\x7F]");

    /// <summary>
    /// The underlying string value for the type.
    /// </summary>
    private readonly string _value;

    /// <inheritdoc />
    public StringData(XElement element) : base(new XElement(L5XName.Data))
    {
        Name = element.DataType() ?? string.Empty;
        _value = GetStringValue(element);
    }

    /// <summary>
    /// Creates a new <see cref="StringData"/> initialized with the provided string type name and data value. 
    /// </summary>
    /// <param name="name">The type name of this string data type. If null then defaults to an empty string.</param>
    /// <param name="value">The value of the string data type. If null then defaults to an empty string.</param>
    /// <remarks>
    /// This constructor allows the caller to create a generic string type instance that could represent a
    /// user defined string type class without having to actually create a custom class.
    /// </remarks>
    public StringData(string? name, string? value) : base(new XElement(L5XName.Data))
    {
        Name = name ?? string.Empty;
        _value = ParseValue(value);
    }

    /// <inheritdoc />
    public override string Name { get; }

    /// <summary>
    /// Gets the current length of the string value.
    /// </summary>
    /// <value>A <see cref="DINT"/> value representing the integer length of the string value.</value>
    /// <remarks>
    /// This member property is not returned in the <c>Members</c> collection because it is not serialized.
    /// It would also generate an extemely large number of member tags, which would consume more memeory.
    /// To reduce space/file size, Rockwell formats strings as a single element value in a Data member element.
    /// This property is only here as a helper to allow the caller to conveniently obtain the length of the
    /// underlying string value.
    /// </remarks>
    public DINT LEN => _value.Length;

    /// <summary>
    /// Gets the array of <see cref="SINT"/> characters that represent the ASCII encoded string value.
    /// </summary>
    /// <returns>An array of <see cref="SINT"/> logix atomic values representing the bytes of the string.</returns>
    /// <remarks>
    /// This member property is not returned in the <c>Members</c> collection because it is not serialized.
    /// It would also generate an extemely large number of member tags, which would consume more memeory.
    /// To reduce space/file size, Rockwell formats strings as a single element value in a Data or DataValueMember element.
    /// This property is only here as a helper to allow the caller to conveniently obtain the SINT array
    /// representation of the underlying string by converting the underlying value.
    /// </remarks>
    public ArrayData<SINT> DATA => ToDataArray(_value);

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            StringData value => _value == value._value,
            string value => _value == value,
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode() => _value.GetHashCode();

    /// <inheritdoc />
    public override string ToString() => _value;

    /// <inheritdoc />
    public IEnumerator<SINT> GetEnumerator() => ToDataArray(_value).ToList().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <inheritdoc />
    public override XElement Serialize()
    {
        var element = new XElement(L5XName.Data);
        element.Add(new XAttribute(L5XName.Format, DataFormat.String));
        element.Add(new XAttribute(L5XName.Length, LEN));
        element.Add(new XCData($"'{_value}'"));
        return element;
    }

    /// <summary>
    /// Serializes this current <see cref="StringData"/> as a structure data element. This is a custom serialization
    /// method which is need to format a string as a nested structure within an array or other structure type.
    /// </summary>
    /// <returns>A <see cref="XElement"/> containing the serialized data.</returns>
    /// <remarks>
    /// This is a special serialization overload since <see cref="StringData"/> can be different depending
    /// on where it exists in the data structure.
    /// </remarks>
    public XElement SerializeStructure()
    {
        var element = new XElement(L5XName.Structure);
        element.Add(new XAttribute(L5XName.DataType, Name));

        var len = new XElement(L5XName.DataValueMember);
        len.Add(new XAttribute(L5XName.Name, nameof(LEN)));
        len.Add(new XAttribute(L5XName.DataType, LEN.Name));
        len.Add(new XAttribute(L5XName.Radix, Radix.Decimal));
        len.Add(new XAttribute(L5XName.Value, LEN));
        element.Add(len);

        var data = new XElement(L5XName.DataValueMember);
        data.Add(new XAttribute(L5XName.Name, nameof(DATA)));
        data.Add(new XAttribute(L5XName.DataType, Name));
        data.Add(new XAttribute(L5XName.Radix, Radix.Ascii));
        data.Add(new XCData($"'{_value}'"));
        element.Add(data);

        return element;
    }

    /// <summary>
    /// Gets the value of the string form the provided element object. Where this value exists depends on if the
    /// element is a root data element or a nested structure or structure member element. If neither is found this
    /// returns and empty string.
    /// </summary>
    private static string GetStringValue(XElement element)
    {
        if (element.Name == L5XName.Data && element.Attribute(L5XName.Format)?.Value == DataFormat.String)
        {
            return ParseValue(element.Value.TrimStart('\'').TrimEnd('\''));
        }

        var data = element.Elements().FirstOrDefault(e => e.MemberName() == nameof(DATA));
        return data is not null ? ParseValue(data.Value.TrimStart('\'').TrimEnd('\'')) : string.Empty;
    }

    /// <summary>
    /// 
    /// </summary>
    private static string ParseValue(string? input)
    {
        if (input is null) return string.Empty;
        var data = ToDataArray(input);
        var ascii = data.Where(s => s > 0).Select(s => s.ToString(Radix.Ascii).TrimSingle('\''));
        return $"{string.Join(string.Empty, ascii)}";
    }

    /// <summary>
    /// Converts the provided string value to a SINT array. Handles empty or null string by returning empty array.
    /// Matches characters using known <see cref="LogixAsciiPattern"/>.
    /// Trims start and end single quotes.
    /// </summary>
    private static SINT[] ToDataArray(string value)
    {
        var result = new List<SINT>();

        //Logix encloses strings in single quotes, so we need to remove those if they are present.
        value = value.TrimStart('\'').TrimEnd('\'');

        //If we get a null or empty string then return an empty array.
        if (string.IsNullOrEmpty(value)) return [];

        //Breaks apart the string into single ASCII characters to be parsed.
        var matches = LogixAsciiPattern.Matches(value);

        foreach (Match match in matches)
        {
            var parsed = (SINT)Radix.Ascii.ParseValue($"'{match.Value}'");
            result.Add(new SINT(parsed, Radix.Ascii));
        }

        return result.ToArray();
    }
}