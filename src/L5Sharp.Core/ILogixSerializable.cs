﻿using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// An interface defining the method for serialization of an object to a <see cref="XElement"/>.
/// </summary>
public interface ILogixSerializable
{
    /// <summary>
    /// Returns a <see cref="XElement"/> representing the serialized L5X data for a given object.
    /// </summary>
    /// <returns>A <see cref="XElement"/> containing the XML data.</returns>
    XElement Serialize();
}