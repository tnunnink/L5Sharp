using System;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A component of a <see cref="Module"/> that represents the properties and data of the connection to the field device.
/// </summary>
public class Communications : LogixElement
{
    /// <summary>
    /// Creates a new <see cref="Communications"/> with default values.
    /// </summary>
    public Communications() : base(L5XName.Communications)
    {
        Element.Add(new XElement(L5XName.ConfigTag));
        Connections = [];
    }

    /// <summary>
    /// Creates a new <see cref="Communications"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public Communications(XElement element) : base(element)
    {
    }

    /// <summary>
    /// A Tag component containing the configuration data for the module.
    /// </summary>
    /// <value>A <see cref="Tag"/> component representing the complex module defined data structure.</value>
    public Tag? ConfigTag
    {
        get => GetComplex<Tag>();
        set => SetComplex(value?.Convert<Tag>(L5XName.ConfigTag));
    }

    /// <summary>
    /// A collection of <see cref="Connection"/> defining the input and output connection specific to the module.
    /// </summary>
    /// <value>A <see cref="LogixContainer{TElement}"/> of <see cref="Connection"/> objects.</value>
    /// <remarks>
    /// Each connection may contain input or output tag structures, as well as several other configuration properties.
    /// </remarks>
    public LogixContainer<Connection> Connections
    {
        get => GetContainer<Connection>();
        set => SetContainer(value);
    }
}