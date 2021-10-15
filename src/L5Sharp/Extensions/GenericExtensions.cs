using System;
using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Serialization;

namespace L5Sharp.Extensions
{
    public static class GenericExtensions
    {
        private static readonly Dictionary<Type, IComponentSerializer> Serializers = new Dictionary<Type, IComponentSerializer>
        {
            { typeof(IDataType), new DataTypeSerializer() },
            { typeof(DataType), new DataTypeSerializer() },
            { typeof(IMember), new MemberSerializer() },
            { typeof(Member), new MemberSerializer() },
            { typeof(ITag), new TagSerializer() },
            { typeof(IProgram), new ProgramSerializer() },
            { typeof(ITask), new TaskSerializer() }
        };
        
        public static XElement Serialize<T>(this T component) where T : IComponent
        {
            var type = typeof(T);

            if (!Serializers.ContainsKey(type))
                throw new InvalidOperationException($"Serializer not defined for component of type '{type}'");

            var serializer = (IComponentSerializer<T>)Serializers[type];

            return serializer.Serialize(component);
        }
    }
}