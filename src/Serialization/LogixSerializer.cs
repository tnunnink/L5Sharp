using System;
using System.Reflection;
using System.Xml.Linq;
using L5Sharp.Attributes;

namespace L5Sharp.Serialization
{
    public static class LogixSerializer
    {
        public static XElement Serialize<T>(T obj) => GetSerializer<T>().Serialize(obj);

        public static T Deserialize<T>(XElement element) => GetSerializer<T>().Deserialize(element);

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