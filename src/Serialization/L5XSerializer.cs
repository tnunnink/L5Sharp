using System;
using System.Xml.Linq;

namespace L5Sharp.Serialization
{
    /// <inheritdoc />
    public abstract class L5XSerializer<T> : IL5XSerializer<T>
    {
        /// <inheritdoc />
        public virtual XElement Serialize(T component)
        {
            throw new NotSupportedException(
                $"The abstract serializer {typeof(L5XSerializer<T>)} does not support typed serialization. " +
                "Method must be implemented on derived type.");
        }

        /// <inheritdoc />
        public XElement Serialize(object component)
        {
            if (component is not T typed)
                throw new ArgumentException($"The provided component of type is not expected type '{typeof(T)}'");

            return Serialize(typed);
        }

        /// <inheritdoc />
        object IL5XSerializer.Deserialize(XElement element)
        {
            return Deserialize(element) ??
                   throw new ArgumentException($"Could not deserialize the provided element '{element.Name}'");
        }

        /// <inheritdoc />
        public virtual T Deserialize(XElement element)
        {
            throw new NotSupportedException(
                $"The abstract serializer {typeof(L5XSerializer<T>)} does not support typed deserialization. " +
                "Method must be implemented on derived type.");
        }
    }
}