using System;
using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Serialization;

namespace L5Sharp.Extensions
{
    internal static class SerializerExtensions
    {
        private static readonly Dictionary<string, IXSerializer> Serializers = new()
        {
            { LogixNames.Controller, new ControllerSerializer() },
            { LogixNames.DataType, new DataTypeSerializer() },
            { LogixNames.Member, new UserDefinedMemberSerializer() },
            { LogixNames.Module, new ModuleSerializer() },
            { LogixNames.Port, new PortSerializer() },
            { LogixNames.Connection, new ConnectionSerializer() },
            { LogixNames.Tag, new TagSerializer() },
            { LogixNames.Rung, new RungSerializer() },
            { LogixNames.RllContent, new LadderLogicSerializer() },
            { LogixNames.Task, new TaskSerializer() },
            { LogixNames.DataValueMember, new DataValueMemberSerializer() },
            { LogixNames.ArrayMember, new ArrayMemberSerializer() },
            { LogixNames.StructureMember, new StructureMemberSerializer() },
            { LogixNames.Structure, new StructureSerializer() }
        };

        public static XElement Serialize<T>(this T component, string? serializerName = null)
        {
            var name = serializerName ?? LogixNames.GetComponentName<T>();

            var serializer = GetSerializer<T>(name);

            return serializer.Serialize(component);
        }

        public static T Deserialize<T>(this XElement element, string? serializerName = null)
        {
            var name = serializerName ?? LogixNames.GetComponentName<T>();

            var serializer = GetSerializer<T>(name);

            return serializer.Deserialize(element);
        }

        private static IXSerializer<T> GetSerializer<T>(string name) => 
            Serializers.TryGetValue(name, out var serializer) 
                ? (IXSerializer<T>)serializer 
                : throw new InvalidOperationException($"No serializer has been defined for'{name}'");
    }
}