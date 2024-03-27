using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A <see cref="LogixType"/> that represents a string or collection of ASCII characters.
/// </summary>
/// <remarks>
/// <para>
/// A logix string type has predefined members <see cref="LEN"/> and <see cref="DATA"/>, which contain the
/// current string length and set of ASCII characters representing the string value, respectively.
/// This class is inherited by <see cref="STRING"/>, which is Rockwell's built in base string type.
/// </para>
/// <para>
/// StringType has special cases in terms of it's L5X structure. Rockwell treats strings sort of like a value type,
/// giving it a special <see cref="DataFormat"/>. However, when serialized as a member of a complex structure, the data
/// looks more like a generic structure type.</para>
/// </remarks>
public class StringType : StructureType, IEnumerable<char>
{
    private const string LogixAsciiPattern = @"\$[A-Fa-f0-9]{2}|\$[tlpr'$]{1}|[\x00-\x7F]";
    private readonly string _name = string.Empty;
    private readonly string _value;

    /// <summary>
    /// Creates a new <see cref="StringType"/> initialized from the provided <see cref="XElement"/> data.
    /// </summary>
    /// <param name="element">The element to parse as the new member object.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    /// <exception cref="InvalidOperationException"><c>element</c> does not have required attributes or child elements.</exception>
    public StringType(XElement element) : base(element)
    {
        _value = GetStringValue();
    }

    /// <summary>
    /// Creates a new <see cref="StringType"/> initialized with default name and data.
    /// </summary>
    /// <remarks>This creates a default instance named "StringType" with an empty string.</remarks>
    public StringType() : base(GenerateDataElement(string.Empty))
    {
        _value = GetStringValue();
    }

    /// <summary>
    /// Creates a new <see cref="StringType"/> initialized with default name and provided string data.
    /// </summary>
    /// <param name="value">The string value to initialize the type with.</param>
    /// <remarks>This creates a instance named "StringType" with the provided string value.</remarks>
    public StringType(string value) : base(GenerateDataElement(value))
    {
        _value = GetStringValue();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public StringType(string name, string value) : base(GenerateDataElement(value))
    {
        _name = name;
        _value = GetStringValue();
    }

    /// <inheritdoc />
    public override string Name => Element.Attribute(L5XName.DataType)?.Value ??
                                   Element.Parent?.Attribute(L5XName.DataType)?.Value ?? _name;

    /// <summary>
    /// Gets the LEN member of the string type structure. 
    /// </summary>
    /// <value>A <see cref="DINT"/> logix atomic value representing the integer length of the string.</value>
    public DINT LEN => _value.Length;

    /// <summary>
    /// Gets the array of bytes that represent the ASCII encoded string value.
    /// </summary>
    /// <returns>An array of <see cref="SINT"/> logix atomic values representing the bytes of the string.</returns>
    public ArrayType<SINT> DATA => ToArray(_value);

    /// <inheritdoc />
    public override string ToString() => _value;

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            StringType value => _value == value._value,
            string value => _value == value,
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode() => _value.GetHashCode();

    /// <inheritdoc />
    public IEnumerator<char> GetEnumerator() => _value.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

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
    public static implicit operator string(StringType input) => input._value;

    /// <summary>
    /// Gets the value of the string form the underlying element object. Where this value exists depends on if the underlying
    /// element is a root data element or a nested structure or structure member element. If neither is found this
    /// returns and empty string. This method will also trim the start and end single quote since that is not technically
    /// part of the string value.
    /// </summary>
    private string GetStringValue()
    {
        if (Element.Name == L5XName.Data && Element.Attribute(L5XName.Format)?.Value == DataFormat.String)
            return Element.Value.TrimStart('\'').TrimEnd('\'');

        var data = Element.Elements().FirstOrDefault(e => e.MemberName() == nameof(DATA));
        return data is not null ? data.Value.TrimStart('\'').TrimEnd('\'') : string.Empty;
    }

    /// <summary>
    /// Converts the provided string value to a SINT array. Handles empty or null string. SINT array can not be empty
    /// to invalid initialization of the <c>ArrayType</c> object.
    /// </summary>
    private static SINT[] ToArray(string value)
    {
        //If we get a null or empty string then we need to return a single element array to avoid exceptions from array type.
        if (string.IsNullOrEmpty(value) || value.All(c => c == '\''))
            return new SINT[] { new(Radix.Ascii) };

        //Logix encloses strings in single quotes so we need to remove those if the are present.
        value = value.TrimStart('\'').TrimEnd('\'');

        //Breaks apart the string into single ASCII characters to be parsed.
        var matches = Regex.Matches(value, LogixAsciiPattern);
        return matches.Select(m =>
        {
            var parsed = (SINT)Radix.Ascii.ParseValue($"'{m.Value}'");
            return new SINT(parsed, Radix.Ascii);
        }).ToArray();
    }

    /// <summary>
    /// Returns the underlying <see cref="XElement"/> object representing the serialized <see cref="StringType"/> data.
    /// </summary>
    /// <param name="name">The name of the root element for which the type will be serialized. Should be Data, Structure,
    /// or StructureMember.</param>
    /// <returns>A <see cref="XElement"/> containing the serialized data.</returns>
    /// <remarks>
    /// This is a special serialization overload since <see cref="StringType"/> can be different depending
    /// on where it exists in the data structure.
    /// </remarks>
    public XElement Serialize(string name)
    {
        return name switch
        {
            L5XName.Data => Element, //this is the default for a string type.
            L5XName.Structure => GenerateStructureElement(L5XName.Structure),
            L5XName.StructureMember => GenerateStructureElement(L5XName.StructureMember),
            _ => throw new ArgumentOutOfRangeException(nameof(name), name,
                $"Name {name} not a supported element name for {GetType()}.")
        };
    }

    /// <summary>
    /// Generates the default data element for a <see cref="StringType"/> object.
    /// </summary>
    private static XElement GenerateDataElement(string value)
    {
        var element = new XElement(L5XName.Data);
        element.Add(new XAttribute(L5XName.Format, DataFormat.String));
        element.Add(new XAttribute(L5XName.Length, value.Length));
        element.Add(new XCData($"'{value}'"));
        return element;
    }

    /// <summary>
    /// Generates the specialized data structure element for a <see cref="StringType"/> object. This format is found if
    /// a string is a member of another data type, either a structure or array member type. This will take the "name"
    /// which should be Structure (if the child or an array element) or StructureMember (if a member of a structure type).  
    /// </summary>
    private XElement GenerateStructureElement(string name)
    {
        var value = ToString();

        var element = new XElement(name);
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
        data.Add(new XCData($"'{value}'"));
        element.Add(data);

        return element;
    }
}