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
/// A <see cref="L5Sharp.LogixType"/> that represents a string or collection of ASCII characters.
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
/// looks more like a generic structure type.</para>
/// </remarks>
public class StringType : LogixType, IEnumerable<char>
{
    /// <summary>
    /// Creates a new <see cref="StringType"/> instance with the provided data.
    /// </summary>
    /// <param name="name">The name of the string type.</param>
    /// <param name="value">The string value of the type.</param>
    /// <exception cref="ArgumentException"><c>name</c> is null or empty.</exception>
    public StringType(string name, string value)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty for a string type object.");

        Name = name;
        DATA = GetData(value);
        DATA.DataChanged += OnDataChanged;
    }

    /// <summary>
    /// Creates a new <see cref="StringType"/> instance with the provided data.
    /// </summary>
    /// <param name="name">The name of the string type.</param>
    /// <param name="value">The string value of the type.</param>
    /// <param name="length"></param>
    /// <exception cref="ArgumentException"><c>name</c> is null or empty.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><c>value</c> is longer than <c>length</c>.</exception>
    protected StringType(string name, string value, int length)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty for a string type object.");

        if (value.Length > length)
            throw new ArgumentOutOfRangeException(
                $"The length {value.Length} of value can not be greater than {length} characters.");

        Name = name;
        DATA = GetData(value);
        DATA.DataChanged += OnDataChanged;
    }

    /// <summary>
    /// Creates a new <see cref="StructureType"/> initialized from the provided <see cref="XElement"/> data.
    /// </summary>
    /// <param name="element">The element to parse as the new member object.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    /// <exception cref="L5XException"><c>element</c> does not have a data type attribute.</exception>
    public StringType(XElement element)
    {
        if (element is null) throw new ArgumentNullException(nameof(element));

        if (element.Attribute(L5XName.Format)?.Value == DataFormat.String)
        {
            Name = element.Ancestors(L5XName.Tag).FirstOrDefault()?.Attribute(L5XName.DataType)?.Value ??
                   throw new L5XException(L5XName.DataType, element);
            DATA = GetData(element.Value);
            DATA.DataChanged += OnDataChanged;
            return;
        }

        Name = element.Attribute(L5XName.DataType)?.Value ?? throw new L5XException(L5XName.DataType, element);
        var value = element.Elements(L5XName.DataValueMember)
                        .FirstOrDefault(e => e.Attribute(L5XName.Name)?.Value == nameof(DATA))?.Value ??
                    throw new L5XException(L5XName.DataValueMember, element);
        DATA = GetData(value);
        DATA.DataChanged += OnDataChanged;
    }

    /// <inheritdoc />
    public override string Name { get; }

    /// <inheritdoc />
    public sealed override DataTypeFamily Family => DataTypeFamily.String;

    /// <inheritdoc />
    public override DataTypeClass Class => DataTypeClass.Unknown;

    /// <inheritdoc />
    public override IEnumerable<LogixMember> Members => new List<LogixMember> { new(nameof(LEN), LEN), new(nameof(DATA), DATA) };

    /// <summary>
    /// Gets the character length value of the string. 
    /// </summary>
    /// <returns>A <see cref="DINT"/> logix atomic value representing the integer length of the string.</returns>
    public DINT LEN => ToString().Length;

    /// <summary>
    /// Gets the array of bytes that represent the ASCII encoded string value.
    /// </summary>
    /// <returns>An array of <see cref="SINT"/> logix atomic values representing the bytes of the string.</returns>
    public ArrayType<SINT> DATA { get; }

    /// <inheritdoc />
    public IEnumerator<char> GetEnumerator() => ToString().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <inheritdoc />
    public override string ToString() =>
        Encoding.ASCII.GetString(DATA.Cast<SINT>().Where(s => s > 0).Select(b => (byte)(sbyte)b).ToArray());

    /// <inheritdoc />
    public override XElement Serialize()
    {
        var element = new XElement(L5XName.Data);
        element.Add(new XAttribute(L5XName.Format, DataFormat.String));
        element.Add(new XAttribute(L5XName.Length, LEN.ToString()));
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
    /// A custom serialization method that returns the string type as a structure element, instead of the string formatted
    /// L5X structure. 
    /// </summary>
    /// <returns>A <see cref="XElement"/> object representing the serialized string structure data.</returns>
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
        data.Add(new XCData(ToString()));
        element.Add(data);

        return element;
    }

    /// <summary>
    /// Converts the provided string value to a SINT array. Handles empty or null string. SINT array can not be empty.
    /// </summary>
    private static SINT[] GetData(string value)
    {
        return !string.IsNullOrEmpty(value)
            ? Encoding.ASCII.GetBytes(value).Select(b => new SINT((sbyte)b, Radix.Ascii)).ToArray()
            : new SINT[] { new() };
    }
    
    /// <summary>
    /// When the DATA array type data change event fires, forward the call by raising this types data changed event.
    /// </summary>
    private void OnDataChanged(object sender, EventArgs e) => RaiseDataChanged(sender);
}