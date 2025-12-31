using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A <see cref="LogixData"/> type that represents a string or collection of ASCII characters.
/// </summary>
/// <remarks>
/// <para>
/// This data type corresponds to any variable length string type value in .NET. 
/// This class is inherited by <see cref="STRING"/>, which is Rockwell's built-in base string type.
/// Users can derive from this base class to create user-defined string types if desired.
/// </para>
/// <para>
/// Strings are weird in L5X format.
/// </para>
/// </remarks>
public class StringData : LogixData, IEnumerable<char>
{
    /// <inheritdoc />
    public StringData(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new <see cref="StringData"/> instance initialized with the provided type name and string value.
    /// </summary>
    /// <param name="name">The name of the string data type instance.</param>
    /// <param name="value">The value of the string data instance.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> is null or empty.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
    public StringData(string name, string value) : base(new XElement(L5XName.Data))
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty");

        if (value is null)
            throw new ArgumentNullException(nameof(value));

        Element.SetAttributeValue(L5XName.DataType, name);
        Element.SetAttributeValue(L5XName.Format, DataFormat.String);
        Element.SetAttributeValue(L5XName.Length, value.Length);
        Element.Add(new XCData($"'{value}'"));
    }

    /// <summary>
    /// Creates a new empty <see cref="StringData"/> instance with the default type name 'StringData'.
    /// </summary>
    public StringData() : this(nameof(StringData), string.Empty)
    {
    }

    /// <summary>
    /// Creates a new <see cref="StringData"/> instance initialized with the provided string value. 
    /// </summary>
    /// <param name="value">The string value to initialize the data object with.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
    public StringData(string value) : this(nameof(StringData), value)
    {
    }

    /// <summary>
    /// Gets the maximum number of characters this string type can hold.
    /// </summary>
    /// <remarks>
    /// Deriving classes must override this property to correctly updated or read data streams.
    /// </remarks>
    public virtual int Capacity => 0;

    /// <inheritdoc />
    /// <remarks>
    /// For strings, the size is the 4-byte DINT length plus the SINT array capacity.
    /// </remarks>
    public override int Size => (sizeof(int) + Capacity + 3) & ~3;

    /// <summary>
    /// Gets the length of the string represented by the current <see cref="StringData"/> instance.
    /// </summary>
    /// <remarks>
    /// This property returns the number of characters in the string. It is equivalent to
    /// accessing the <see cref="string.Length"/> property of the underlying string value.
    /// This is not tied to the underlying element value for the LEN member when present.
    /// </remarks>
    public int Length => GetLength();

    /// <inheritdoc />
    public override void Update(LogixData data)
    {
        if (data is null)
            throw new ArgumentNullException(nameof(data));

        if (data is not StringData stringData)
            throw new ArgumentException($"Can not update string with data of type '{data.GetType()}'.");

        SetLength(stringData.Length);
        SetString(stringData);
    }

    /// <inheritdoc />
    public override int Update(byte[] data, int offset)
    {
        // If we have not defined the size of the string type we can't know how many bytes to read.
        // We have to rely on the serializer instantiating conrete types and the user defining custom one (either
        // manually or with source generator)
        if (Capacity <= 0)
            throw new InvalidOperationException(
                $"Cannot hydrate string '{Name}' because its capacity is unknown. Ensure the type is registered or generated.");

        // Align to the DINT offset to read the length data.
        // Default to the capacity if lower than the string length to ensure valid mapping.
        offset = (offset + 3) & ~3;
        var rawLength = BitConverter.ToInt32(data, offset);
        var safeLength = Math.Min(rawLength, Capacity);
        SetLength(safeLength);

        //Read the remaing bytes to get the string value.
        var value = System.Text.Encoding.ASCII.GetString(data, offset + sizeof(int), safeLength);
        SetString(value);

        //Size will depend on the Capacity property getting set correctly.
        return offset + Size;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            StringData value => GetString() == value.GetString(),
            string value => GetString() == value,
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode() => GetString().GetHashCode();

    /// <inheritdoc />
    public override string ToString() => GetString();

    /// <inheritdoc />
    public IEnumerator<char> GetEnumerator() => GetString().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Defines an implicit conversion from a <see cref="string"/> to a <see cref="StringData"/> object.
    /// </summary>
    /// <param name="value">The string value to be converted into a <see cref="StringData"/>.</param>
    /// <returns>A new instance of <see cref="StringData"/> initialized with the specified string value.</returns>
    public static implicit operator StringData(string value) => new(value);

    /// <summary>
    /// Defines an implicit conversion from a <see cref="StringData"/> instance to a <see cref="string"/>.
    /// </summary>
    /// <param name="value">The <see cref="StringData"/> instance to be converted.</param>
    /// <returns>A <see cref="string"/> representation of the <see cref="StringData"/> value.</returns>
    public static implicit operator string(StringData value) => value.ToString();

    /// <summary>
    /// Retrieves the string value from the underlying XElement. Since this could be either a Data or Structure element,
    /// we will just look for the first descendent CDATA node and get the value.
    /// Trims surrounding single quotes if present.
    /// </summary>
    private string GetString()
    {
        return Element.DescendantNodes().OfType<XCData>().First()?.Value.Trim('\'') ?? string.Empty;
    }

    /// <summary>
    /// Sets the string value within the underlying XML element.
    /// </summary>
    /// <param name="value">The string value to be assigned to the XML element.</param>
    private void SetString(string value)
    {
        Element.DescendantNodes().OfType<XCData>().First().ReplaceWith(new XCData($"'{value}'"));
    }

    /// <summary>
    /// Retrieves the length of the string data from the underlying XML element.
    /// </summary>
    /// <returns>The length of the string data as an integer.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the length attribute or element is not found in the XML.</exception>
    private int GetLength()
    {
        var value = Element.Name.LocalName is L5XName.Data
            ? Element.Attribute(L5XName.Length)?.Value
            : Element.Elements().FirstOrDefault(e => e.MemberName() == "LEN")?.Attribute(L5XName.Value)?.Value;

        if (value is null)
            throw Element.L5XError(L5XName.Length);

        return int.Parse(value);
    }

    /// <summary>
    /// Sets the length attribute for the underlying XML element of the StringData instance.
    /// </summary>
    /// <param name="length">The new length to be set for the string representation within the XML structure.</param>
    private void SetLength(int length)
    {
        if (Element.Name.LocalName is L5XName.Data)
        {
            Element.SetAttributeValue(L5XName.Length, length);
            return;
        }

        Element.Elements().Single(e => e.MemberName() == "LEN").SetAttributeValue(L5XName.Value, length);
    }

    /// <summary>
    /// Converts the current StringData instance into an XElement representation
    /// structured according to the L5X specification.
    /// </summary>
    /// <returns>An XElement object representing the structure of the StringData instance.</returns>
    public XElement ToStructureElement()
    {
        var value = GetString();

        var element = new XElement(L5XName.Structure);
        element.Add(new XAttribute(L5XName.DataType, Name));

        var len = new XElement(L5XName.DataValueMember);
        len.Add(new XAttribute(L5XName.Name, "LEN"));
        len.Add(new XAttribute(L5XName.DataType, "DINT"));
        len.Add(new XAttribute(L5XName.Radix, Radix.Decimal));
        len.Add(new XAttribute(L5XName.Value, value.Length));
        element.Add(len);

        var data = new XElement(L5XName.DataValueMember);
        data.Add(new XAttribute(L5XName.Name, "DATA"));
        data.Add(new XAttribute(L5XName.DataType, Name));
        data.Add(new XAttribute(L5XName.Radix, Radix.Ascii));
        data.Add(new XCData($"'{value}'"));
        element.Add(data);

        return element;
    }
}