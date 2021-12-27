using System;
using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Serialization;

namespace L5Sharp.Extensions
{
    internal static class SerializerExtensions
    {
        private static readonly Dictionary<XName, IXSerializer> Serializers = new()
        {
            { LogixNames.Controller, new ControllerSerializer() },
            { LogixNames.DataType, new UserDefinedSerializer() },
            { LogixNames.Member, new UserDefinedMemberSerializer() },
            { LogixNames.Tag, new TagSerializer() },
            { LogixNames.Task, new TaskSerializer() },
            { LogixNames.DataValueMember, new DataValueMemberSerializer() },
            { LogixNames.ArrayMember, new ArrayMemberSerializer() },
            { LogixNames.StructureMember, new StructureMemberSerializer() },
            { LogixNames.Structure, new StructureSerializer() },
        };
        
        public static XElement Serialize<T>(this T component, XName? serializerName = null)
        {
            var name = serializerName ?? LogixNames.GetComponentName<T>();
            
            var serializer = GetSerializer<T>(name);
            
            return serializer.Serialize(component);
        }

        public static T Deserialize<T>(this XElement element, XName? serializerName = null)
        {
            var name = serializerName ?? LogixNames.GetComponentName<T>();
            
            var serializer = GetSerializer<T>(name);
            
            return serializer.Deserialize(element);
        }

        private static IXSerializer<T> GetSerializer<T>(XName name)
        {
            if (!Serializers.TryGetValue(name, out var serializer))
                throw new InvalidOperationException($"Serializer not defined for component '{name}'");

            return (IXSerializer<T>)serializer;
        }
    }
}