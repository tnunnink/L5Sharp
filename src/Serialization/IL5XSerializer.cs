using System.Xml.Linq;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// Defines to mapping to and from <see cref="XElement"/> objects so that a component of a given object can be
    /// properly serialized or deserialized. 
    /// </summary>
    public interface IL5XSerializer
    {
        /// <summary>
        /// Converts the provided object into a <see cref="XElement"/> instance. 
        /// </summary>
        /// <param name="component">The object to serialize.</param>
        /// <returns>An <see cref="XElement"/> that represents the serialized component.</returns>
        XElement Serialize(object component);
        
        /// <summary>
        /// Converts the provided <see cref="XElement"/> to an object instance.
        /// </summary>
        /// <param name="element">The xml element to deserialize.</param>
        /// <returns>A new <see cref="object"/> that represents the deserialized element.</returns>
        object Deserialize(XElement element);
    }
    
    /// <summary>
    /// A typed <see cref="IL5XSerializer"/> that defines the methods for serializing and deserializing objects to/from
    /// <see cref="XElement"/> for the specified type.
    /// </summary>
    /// <typeparam name="T">The type of object being serialized and deserialized.</typeparam>
    public interface IL5XSerializer<T> : IL5XSerializer
    {
        /// <summary>
        /// Serializes the provided objet to an <see cref="XElement"/> object.
        /// </summary>
        /// <param name="component">The object to serialize.</param>
        /// <returns>A <see cref="XElement"/> that represents the serialized component object.</returns>
        XElement Serialize(T component);
        
        /// <summary>
        /// Deserialized the provided <see cref="XElement"/> to an object of the specified type.
        /// </summary>
        /// <param name="element">The xml element to deserialize.</param>
        /// <returns>A new object instance of the specified type.</returns>
        new T Deserialize(XElement element);
    }
}