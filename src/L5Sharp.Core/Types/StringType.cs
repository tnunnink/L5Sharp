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
/// A <see cref="StringType"/> will not pass the provided element to the base class but rather extract
/// the data type name and string value and act as a wrapper over that data. The element will be reconstructed
/// when <see cref="Serialize"/> or <see cref="SerializeStructure"/> is called. We need to do all this due to how
/// strings are serialized in an L5X which is in a special format. Also we want to treat strings similar to atomics
/// as if they are immutable values types.
/// </para>
/// </remarks>
public class StringType : StructureType, IEnumerable<char>
{
    private static readonly Regex LogixAsciiPattern = new(@"\$[A-Fa-f0-9]{2}|\$[tlpr'$]{1}|[\x00-\x7F]");
    private readonly SINT[] _data;

    /// <inheritdoc />
    public StringType(XElement element) : base(new XElement(L5XName.Data))
    {
        Name = element.DataType() ?? string.Empty;
        _data = GenerateData(ToArray(GetStringValue(element)));
    }

    /// <summary>
    /// Creates a new <see cref="StringType"/> initialized with the provided string type name and data value. 
    /// </summary>
    /// <param name="value">The value of the string data type. If null then defaults to an empty string.</param>
    /// <remarks>
    /// This constructor allows the caller to create a generic string type instance that could represent a
    /// user defined string type class without having to actually create a custom class.
    /// </remarks>
    public StringType(string? value) : base(new XElement(L5XName.Data))
    {
        Name = string.Empty;
        _data = GenerateData(ToArray(value ?? string.Empty));
    }

    /// <summary>
    /// Creates a new <see cref="StringType"/> initialized with the provided string type name and data value. 
    /// </summary>
    /// <param name="name">The type name of this string data type. If null then defaults to an empty string.</param>
    /// <param name="value">The value of the string data type. If null then defaults to an empty string.</param>
    /// <remarks>
    /// This constructor allows the caller to create a generic string type instance that could represent a
    /// user defined string type class without having to actually create a custom class.
    /// </remarks>
    public StringType(string? name, string? value) : base(new XElement(L5XName.Data))
    {
        Name = name ?? string.Empty;
        _data = GenerateData(ToArray(value ?? string.Empty));
    }

    /// <inheritdoc />
    public override string Name { get; }

    /// <inheritdoc />
    public override IEnumerable<Member> Members => GenerateVirtualMembers();

    /// <summary>
    /// Gets the current length of the string value.
    /// </summary>
    /// <value>A <see cref="DINT"/> value representing the integer length of the string value.</value>
    public DINT LEN => ToString().Length;

    /// <summary>
    /// Gets the array of <see cref="SINT"/> characters that represent the ASCII encoded string value.
    /// </summary>
    /// <returns>An array of <see cref="SINT"/> logix atomic values representing the bytes of the string.</returns>
    public ArrayType<SINT> DATA => new(_data);

    /// <summary>
    /// The maximum length of characters the <see cref="StringType"/> derivative can hold in <see cref="DATA"/>.
    /// </summary>
    /// <remarks>
    /// By default this will be '0' since we can't know the max length for some arbitrary string type. The code internally
    /// will construct the array to the size of the incoming string value if set to <c>0</c>. If greater than zero then
    /// <see cref="DATA"/> will be initialized to that length and filled with the incoming value array.
    /// </remarks>
    protected virtual ushort MaxLength => 0;

    /// <summary>
    /// Returns the actual string value of the <see cref="StringType"/> object without single quotes and special
    /// Logix escape character(s) ('$').
    /// </summary>
    /// <returns>The <see cref="string"/> containing the value of the string type object.</returns>
    public override string ToString()
    {
        var ascii = _data.Where(s => s > 0).Select(s => s.ToString(Radix.Ascii).TrimStart('\'').TrimEnd('\''));
        return $"{string.Join(string.Empty, ascii)}";
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

    /// <inheritdoc />
    public IEnumerator<char> GetEnumerator() => ToString().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Converts the provided <see cref="StringType"/> to a <see cref="string"/> value.
    /// </summary>
    /// <param name="input">The value to convert.</param>
    /// <returns>A <see cref="string"/> type value.</returns>
    public static implicit operator string(StringType input) => input.ToString();

    /// <inheritdoc />
    public override XElement Serialize()
    {
        var element = new XElement(L5XName.Data);
        element.Add(new XAttribute(L5XName.Format, DataFormat.String));
        element.Add(new XAttribute(L5XName.Length, LEN));
        element.Add(new XCData($"'{ToString()}'"));
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
        len.Add(new XAttribute(L5XName.Value, LEN));
        element.Add(len);

        var data = new XElement(L5XName.DataValueMember);
        data.Add(new XAttribute(L5XName.Name, nameof(DATA)));
        data.Add(new XAttribute(L5XName.DataType, Name));
        data.Add(new XAttribute(L5XName.Radix, Radix.Ascii));
        data.Add(new XCData($"'{ToString()}'"));
        element.Add(data);

        return element;
    }

    /// <summary>
    /// Generates the "virtual" data members for the <see cref="StringType"/>. All string types have
    /// the LEN and DATA member which contain the length and array of ASCII characters. We will allow these to be
    /// retrieved as nested data members, but they have no setter. This is because of how strings are serialized. Strings
    /// are treated as immutable types like Atomics. You only update their value on a tag or member which will know
    /// how to update the corresponding data element. You can not update the member values. This is actually how
    /// Logix Designer works as well.
    /// </summary>
    private IEnumerable<Member> GenerateVirtualMembers()
    {
        const string message = "Set the string value for the parent tag to update this value.";

        yield return new Member(nameof(LEN), () => LEN,
            _ => throw new InvalidOperationException(
                $"The LEN member for {typeof(StringType)} objects is read only. {message}."));
        yield return new Member(nameof(DATA), () => DATA,
            _ => throw new InvalidOperationException(
                $"The DATA member for {typeof(StringType)} objects is read only. {message}."));
    }

    /// <summary>
    /// Generates an array type with the predefined length and initializes the array to the provided array. Will fill
    /// remaining array with default '0' SINT values with ASCII format.
    /// </summary>
    // ReSharper disable once SuggestBaseTypeForParameter I don't care.
    private SINT[] GenerateData(SINT[] array)
    {
        //If there is no specified MaxLength (meaning this is a generic StringType) then use the provided array length.
        var length = MaxLength > 0 ? MaxLength : array.Length;

        var data = new SINT[length];

        //Populate the generated DATA array with the provided array.
        for (var i = 0; i < length; i++)
        {
            data[i] = i < array.Length ? new SINT(array[i], Radix.Ascii) : new SINT(Radix.Ascii);
        }

        return data;
    }

    /// <summary>
    /// Gets the value of the string form the provided element object. Where this value exists depends on if the
    /// element is a root data element or a nested structure or structure member element. If neither is found this
    /// returns and empty string.
    /// </summary>
    private static string GetStringValue(XElement element)
    {
        if (element.Name == L5XName.Data && element.Attribute(L5XName.Format)?.Value == DataFormat.String)
            return element.Value;

        var data = element.Elements().FirstOrDefault(e => e.MemberName() == nameof(DATA));
        return data is not null ? data.Value : string.Empty;
    }

    /// <summary>
    /// Converts the provided string value to a SINT array. Handles empty or null string by returning empty array.
    /// Trims start and end single quotes. Matches characters using known <see cref="LogixAsciiPattern"/>.
    /// </summary>
    private static SINT[] ToArray(string value)
    {
        var result = new List<SINT>();
        
        //Logix encloses strings in single quotes so we need to remove those if the are present.
        value = value.TrimStart('\'').TrimEnd('\'');

        //If we get a null or empty string then return an empty array.
        if (string.IsNullOrEmpty(value)) return Array.Empty<SINT>();

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