using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Serialization;
using L5Sharp.Serialization.Components;
using L5Sharp.Serialization.Data;

namespace L5Sharp.L5X
{
    /// <summary>
    /// A class that contains instances of <see cref="IL5XSerializer{T}"/>, which make available the
    /// current <see cref="L5XContext"/> object, so that the serializers may use the information to necessary
    /// to instantiate objects efficiently. 
    /// </summary>
    internal class L5XSerializers
    {
        private readonly List<IL5XSerializer> _serializers;

        private readonly Dictionary<string, Type> _lookup = new()
        {
            { L5XElement.DataType.ToString(), typeof(DataTypeSerializer) },
            { L5XElement.Structure.ToString(), typeof(StructureSerializer) },
            { L5XElement.AddOnInstructionDefinition.ToString(), typeof(AddOnInstructionSerializer) }
        };

        public L5XSerializers(L5XDocument document)
        {
            _serializers = new List<IL5XSerializer>
            {
                new ControllerSerializer(),
                new DataTypeSerializer(document),
                new ModuleSerializer(),
                new AddOnInstructionSerializer(document),
                new TagSerializer(document),
                new ProgramSerializer(),
                new TaskSerializer(),
                new StructureSerializer(),
                new FormattedDataSerializer(),
                /*{ L5XElement.Array, new ArraySerializer(document) },
                { L5XElement.Element, new ArrayElementSerializer(document) },
                { L5XElement.DataValueMember, new DataValueMemberSerializer() },
                { L5XElement.ArrayMember, new ArrayMemberSerializer(document) },
                { L5XElement.StructureMember, new StructureMemberSerializer(document) },
                { L5XElement.StructureMember, new StringStructureSerializer(document) },*/
                
            };
        }

        public TSerializer Get<TSerializer>() where TSerializer : IL5XSerializer
        {
            var target = _serializers.FirstOrDefault(t => t.GetType() == typeof(TSerializer));
            
            if (target is not TSerializer serializer)
                throw new InvalidOperationException($"No serializer defined for type '{typeof(TSerializer)}'");

            return serializer;
        }
        
        public IL5XSerializer<T> Get<T>(string name)
        {
            _lookup.TryGetValue(name, out var type);

            if (type is null)
                throw new InvalidOperationException($"No serializer defined for element '{name}'");
            
            var target = _serializers.FirstOrDefault(t => t.GetType() == type);
            
            if (target is not IL5XSerializer<T> serializer)
                throw new InvalidOperationException($"No serializer defined for type '{type}'");

            return serializer;
        }
    }
}