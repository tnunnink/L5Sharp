using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Serialization;
using L5Sharp.Serialization.Components;
using L5Sharp.Serialization.Data;

namespace L5Sharp.L5X
{
    public class L5XSerializers : IEnumerable<IL5XSerializer>
    {
        private readonly Dictionary<L5XElement, IL5XSerializer> _serializers;

        public L5XSerializers(L5XContext context)
        {
            _serializers = new Dictionary<L5XElement, IL5XSerializer>
            {
                { L5XElement.Controller, new ControllerSerializer() },
                { L5XElement.DataType, new UserDefinedSerializer(context) },
                { L5XElement.Member, new MemberSerializer(context) },
                { L5XElement.Module, new ModuleSerializer() },
                { L5XElement.Tag, new TagSerializer() },
                { L5XElement.Program, new ProgramSerializer() },
                { L5XElement.Routine, new RoutineSerializer() },
                { L5XElement.Task, new TaskSerializer() },
                { L5XElement.Structure, new StructureSerializer() },
                { L5XElement.DataValue, new DataValueSerializer() },
                { L5XElement.Array, new ArraySerializer() },
                { L5XElement.DataValueMember, new DataValueMemberSerializer() },
                { L5XElement.ArrayMember, new ArrayMemberSerializer() },
                { L5XElement.StructureMember, new StructureMemberSerializer() }
            };
        }

        public IL5XSerializer<TComponent> GetSerializer<TComponent>()
            where TComponent : ILogixComponent => GetSerializer<TComponent>(L5XNames.GetComponentName<TComponent>());

        public IL5XSerializer<TComponent> GetSerializer<TComponent>(Type serializerType)
        {
            var serializer = _serializers.Values.FirstOrDefault(s => s.GetType() == serializerType);

            if (serializer is not IL5XSerializer<TComponent> typed)
                throw new InvalidOperationException(
                    $"Could not obtain serializer for type {serializerType} as type {typeof(TComponent)}");

            return typed;
        }

        public IL5XSerializer<TComponent> GetSerializer<TComponent>(XElement element) =>
            GetSerializer<TComponent>(element.Name.ToString());

        private IL5XSerializer<TComponent> GetSerializer<TComponent>(string name)
        {
            if (!Enum.TryParse<L5XElement>(name, out var l5XElement))
                throw new ArgumentException($"No {typeof(L5XElement)} defined for {name}.");

            var serializer = _serializers.FirstOrDefault(t => t.Key == l5XElement).Value;

            if (serializer is not IL5XSerializer<TComponent> typed)
                throw new InvalidOperationException(
                    $"Could not obtain typed serializer for element '{name}' as type {typeof(TComponent)}");

            return typed;
        }

        public IEnumerator<IL5XSerializer> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}