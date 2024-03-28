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
    private readonly string _value;

    /// <summary>
    /// Creates a new <see cref="StringType"/> initialized from the provided <see cref="XElement"/> data.
    /// </summary>
    /// <param name="element">The element to parse as the new member object.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    /// <exception cref="InvalidOperationException"><c>element</c> does not have required attributes or child elements.</exception>
    public StringType(XElement element) : base(element)
    {
        Name = element.DataType() ?? nameof(StringType);
        _value = GetStringValue();
    }

    /// <summary>
    /// Creates a new <see cref="StringType"/> initialized with default name and data.
    /// </summary>
    /// <remarks>This creates a default instance named "StringType" with an empty string.</remarks>
    public StringType() : base(new XElement(L5XName.Data))
    {
        Name = nameof(StringType);
        _value = string.Empty;
    }

    /// <summary>
    /// Creates a new <see cref="StringType"/> initialized with default name and provided string data.
    /// </summary>
    /// <param name="value">The string value to initialize the type with.</param>
    /// <remarks>This creates a instance named "StringType" with the provided string value.</remarks>
    public StringType(string value) : base(new XElement(L5XName.Data))
    {
        Name = nameof(StringType);
        _value = value;
    }

    /// <summary>
    /// Creates a new <see cref="StringType"/> initialized with the provided string type name and data value. 
    /// </summary>
    /// <param name="name">The type name of this string data type.</param>
    /// <param name="value">The value of the string data type.</param>
    public StringType(string? name, string? value) : base(new XElement(L5XName.Data))
    {
        Name = name ?? nameof(StringType);
        _value = value ?? string.Empty;
    }

    /// <inheritdoc />
    public override string Name { get; }

    /// <inheritdoc />
    public override IEnumerable<Member> Members => GenerateVirtualMembers();

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
    /// Generates the "virtual" data members for the <see cref="StringType"/>. All string types have
    /// the LEN and DATA member which contain the length and array of ASCII characters. We will allow these to be
    /// retrieved as nested data member, but they have no setter. This is because of how string are serialized. Strings
    /// are treated as immutable types like Atomics. You only update their value on a tag or member which will know
    /// how to update the corresponding data element. You can not update the member values. This is actually how
    /// Logix Designer works as well.
    /// </summary>
    private IEnumerable<Member> GenerateVirtualMembers()
    {
        yield return new Member(nameof(LEN), () => LEN, _ => {});
        yield return new Member(nameof(DATA), () => DATA, _ => {});
    }

    /// <summary>
    /// Converts the provided string value to a SINT array. Handles empty or null string. SINT array can not be empty
    /// to invalid initialization of the <c>ArrayType</c> object.
    /// </summary>
    private static SINT[] ToArray(string value)
    {
        //If we get a null or empty string then return an empty array.
        if (string.IsNullOrEmpty(value) || value.All(c => c == '\''))
            return Array.Empty<SINT>();

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
    /// Serializes this current <see cref="StringType"/> as a structure data element. This is a custom serialization
    /// method which is need to format a string as a nested structure within an array or other structure type.
    /// </summary>
    /// <returns>A <see cref="XElement"/> containing the serialized data.</returns>
    /// <remarks>
    /// This is a special serialization overload since <see cref="StringType"/> can be different depending
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
        len.Add(new XAttribute(L5XName.Value, _value.Length));
        element.Add(len);

        var data = new XElement(L5XName.DataValueMember);
        data.Add(new XAttribute(L5XName.Name, nameof(DATA)));
        data.Add(new XAttribute(L5XName.DataType, Name));
        data.Add(new XAttribute(L5XName.Radix, Radix.Ascii));
        data.Add(new XCData($"'{_value}'"));
        element.Add(data);

        return element;
    }
}