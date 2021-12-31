using System.Xml.Linq;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A base class used for collections of serializers. 
    /// </summary>
    internal interface IXSerializer
    {
    }
    
    /// <summary>
    /// A typed serializer that defines the methods for serializing and deserializing objects to <see cref="XElement"/>.
    /// </summary>
    /// <typeparam name="T">The type of object used in serialization.</typeparam>
    internal interface IXSerializer<T> : IXSerializer
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
        /// <param name="element">The <see cref="XElement"/> object to deserialize.</param>
        /// <returns>A new object instance of the specified type.</returns>
        T Deserialize(XElement element);
    }
}