using System;
using System.Reflection;
using System.Xml.Linq;
using L5Sharp.Attributes;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A static serialization class that provides the ability to retrieve <see cref="ILogixSerializer{T}"/> instances
    /// for specified type, as well as perform serialization and deserialization of the type.
    /// </summary>
    public static class LogixSerializer
    {
        /// <summary>
        /// Serializes the provided object using the preconfigured <see cref="ILogixSerializer{T}"/> for the type.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <typeparam name="T">The type of the object being serialized. Type must have configured
        /// <see cref="LogixSerializerAttribute"/> so that the serializer can retrieve the correct implementation
        /// to use for serialization.</typeparam>
        /// <returns>An <see cref="XElement"/> representing the serialized object.</returns>
        public static XElement Serialize<T>(T obj) => GetSerializer<T>().Serialize(obj);

        /// <summary>
        /// Deserialized the provided element into the specified object type using the preconfigured
        /// <see cref="ILogixSerializer{T}"/> for the type.
        /// </summary>
        /// <param name="element">The XML element to deserialize.</param>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <returns>A new object of the specified type representing the deserialized object.</returns>
        public static T Deserialize<T>(XElement element) => GetSerializer<T>().Deserialize(element);

        /// <summary>
        /// Gets a <see cref="ILogixSerializer{T}"/> instance for the specified type. 
        /// </summary>
        /// <typeparam name="T">The type for which to get the preconfigured serializer for.</typeparam>
        /// <returns>A new <see cref="ILogixSerializer{T}"/> instance.</returns>
        /// <exception cref="InvalidOperationException">The specified type does not have a
        /// <see cref="LogixSerializerAttribute"/> configured on the type -or- the returned serializer is not a
        /// serializer of the specified generic type.</exception>
        public static ILogixSerializer<T> GetSerializer<T>()
        {
            var serializerAttribute = typeof(T).GetCustomAttribute<LogixSerializerAttribute>();

            if (serializerAttribute is null)
                throw new InvalidOperationException(
                    @$"No type defined for type {typeof(T)}.
                     Class must specify LogixSerializerAttribute to be deserialized.");

            var serializer = Activator.CreateInstance(serializerAttribute.Type);

            if (serializer is not ILogixSerializer<T> logixSerializer)
                throw new InvalidOperationException(
                    @$"The serializer {serializerAttribute.Type} does not serialize objects of type {typeof(T)}.
                    Either specify correct LogixSerializerAttribute for type ");

            return logixSerializer;
        }
    }
}