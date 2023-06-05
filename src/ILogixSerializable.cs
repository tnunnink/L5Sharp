using System.Xml.Linq;

namespace L5Sharp;

/// <summary>
/// A interface defining the method for serialization of an object to a <see cref="XElement"/>.
/// </summary>
public interface ILogixSerializable
{
    /// <summary>
    /// Returns a <see cref="XElement"/> representing the serialized L5X data for a given entity or component.
    /// </summary>
    /// <returns>A <see cref="XElement"/> containing the L5X/XML data.</returns>
    XElement Serialize();
}