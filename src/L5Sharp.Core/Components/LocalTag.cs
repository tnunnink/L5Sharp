using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A <see cref="Tag"/> that is defined locally to an <see cref="AddOnInstruction"/> component.
/// </summary>
/// <remarks>
/// This class does not add any new functionality but simply overrides the default L5XType to LocalTag. Logix
/// uses a different element name for tags in an AOI, so to match this our L5XTypeAttribute implementation, we are
/// deriving a new specific class.
/// </remarks>
[L5XType(L5XName.LocalTag)]
public class LocalTag : Tag, ILogixParsable<LocalTag>
{
    /// <inheritdoc />
    protected override List<string> ElementOrder =>
    [
        L5XName.Description,
        L5XName.DefaultData,
    ];

    /// <summary>
    /// Creates a new <see cref="LocalTag"/> with default values.
    /// </summary>
    public LocalTag() : base(L5XName.LocalTag)
    {
    }

    /// <summary>
    /// Creates a new <see cref="Tag"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public LocalTag(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new <see cref="LocalTag"/> initialized with the provided name and value.
    /// </summary>
    /// <param name="name">The name of the LocalTag.</param>
    /// <param name="value">The <see cref="LogixData"/> value of the LocalTag.</param>
    /// <param name="description">the optional description of the LocalTag.</param>
    public LocalTag(string name, LogixData value, string? description = null) : base(L5XName.LocalTag)
    {
        Element.SetAttributeValue(L5XName.Name, name);
        Value = value;
        SetProperty(description, nameof(Description));
    }

    /// <summary>
    /// Returns a new deep-cloned instance as the specified <see cref="LogixElement"/> type.
    /// </summary>
    /// <returns>A new instance of the specified element type with the same property values.</returns>
    public new LocalTag Clone() => new XElement(Serialize()).Deserialize<LocalTag>();

    /// <summary>
    /// Parses the provided string and returned the strongly typed component object.
    /// </summary>
    /// <param name="value">The XML string value to parse.</param>
    /// <returns>A new <see cref="LogixComponent"/> instance that represents the parsed value.</returns>
    /// <remarks>
    /// Internally this uses XElement.Parse along with our <see cref="LogixSerializer"/> to instantiate the concrete instance.
    /// This means the user can use the <see cref="LogixParser"/> extensions to also parse XML into strongly typed logix objects.
    /// Also note that since this uses internal XElement and casts the type, this method can throw exceptions for invalid
    /// XML or XML that is parsed to a different type than the one specified here.
    /// </remarks>
    public new static LocalTag Parse(string value)
    {
        var element = XElement.Parse(value);
        return element.Deserialize<LocalTag>();
    }

    /// <summary>
    /// Attempts to parse the provided string and returned the strongly typed component object.
    /// If unsuccessful, then this method returns <c>null</c>.
    /// </summary>
    /// <param name="value">The XML string value to parse.</param>
    /// <returns>A new <see cref="LogixComponent"/> instance that represents the parsed value if successful, otherwise, <c>null</c>.</returns>
    /// <remarks>
    /// Internally this uses XElement.Parse along with our <see cref="LogixSerializer"/> to instantiate the concrete instance.
    /// This means the user can use the <see cref="LogixParser"/> extensions to also parse XML into strongly typed logix objects.
    /// Note that this method will just return null if any exception is caught. This could be for invalid XML formats
    /// of invalid type casts.
    /// </remarks>
    public new static LocalTag? TryParse(string? value)
    {
        if (value is null || value.IsEmpty()) //this satisfies the .NET 2.0 compiler warnings about null.
            return null;

        var trimmed = value.Trim();
        if (trimmed.Length == 0 || trimmed[0] != '<') return null;

        try
        {
            var element = XElement.Parse(trimmed);
            return element.Deserialize<LocalTag>();
        }
        catch (Exception)
        {
            return null;
        }
    }
}