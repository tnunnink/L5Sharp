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
    /// Gets the length of the string represented by the current <see cref="StringData"/> instance.
    /// </summary>
    /// <remarks>
    /// This property returns the number of characters in the string. It is equivalent to
    /// accessing the <see cref="string.Length"/> property of the underlying string value.
    /// This is not tied to the underlying element value for the LEN member when present.
    /// </remarks>
    public int Length => GetString().Length;

    /// <inheritdoc />
    public override void Update(LogixData data)
    {
        if (data is null)
            throw new ArgumentNullException(nameof(data));

        if (data is not StringData stringData)
            throw new ArgumentException($"Can not update string with data of type '{data.GetType()}'.");

        if (Element.Name.LocalName is L5XName.Data)
        {
            Element.SetAttributeValue(L5XName.Length, stringData.Length);
            Element.ReplaceNodes(new XCData($"'{stringData}'"));
            return;
        }

        Element.Elements()
            .First(e => e.MemberName() == "LEN")
            .SetAttributeValue(L5XName.Value, stringData.Length);

        Element.Elements()
            .First(e => e.MemberName() == "DATA")
            .ReplaceNodes(new XCData($"'{stringData}'"));
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
        var text = Element.DescendantNodes().OfType<XCData>().FirstOrDefault()?.Value ?? string.Empty;
        return text.TrimStart('\'').TrimEnd('\'');
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