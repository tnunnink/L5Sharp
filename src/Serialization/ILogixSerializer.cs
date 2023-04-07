using System.Xml.Linq;

namespace L5Sharp.Serialization;

/// <summary>
/// A generic interface for serializing and deserializing objects of type T.
/// </summary>
/// <typeparam name="T">The type of object, must be a class.</typeparam>
public interface ILogixSerializer<T>
{
    /// <summary>
    /// Serializes an object of type T to an XElement.
    /// </summary>
    /// <param name="obj">The object to serialize.</param>
    /// <returns>The XElement representation of the object.</returns>
    XElement Serialize(T obj);
        
    /// <summary>
    /// Deserializes an XElement to an object of type T.
    /// </summary>
    /// <param name="element">The XElement to deserialize.</param>
    /// <returns>The deserialized object of type T.</returns>
    T Deserialize(XElement element);
}