using System;
using System.Collections.Generic;
using L5Sharp.Serialization;

namespace L5Sharp.L5X
{
    /// <summary>
    /// A class that contains instances of <see cref="IL5XSerializer{T}"/>, which make available the
    /// current <see cref="L5XContext"/> object, so that the serializers may use the information to necessary
    /// to instantiate objects efficiently. 
    /// </summary>
    internal class L5XSerializers
    {
        private readonly Dictionary<Type, IL5XSerializer> _serializers;

        private readonly Dictionary<Type, Type> _componentLookup = new()
        {
            { typeof(IComplexType), typeof(DataTypeSerializer) },
            { typeof(IModule), typeof(ModuleSerializer) },
            { typeof(IAddOnInstruction), typeof(AddOnInstructionSerializer) },
            { typeof(ITag<IDataType>), typeof(TagSerializer) },
            { typeof(IProgram), typeof(ProgramSerializer) },
            { typeof(ITask), typeof(TaskSerializer) },
        };

        private readonly Dictionary<string, Type> _elementLookup = new()
        {
            { L5XElement.DataType.ToString(), typeof(DataTypeSerializer) },
            { L5XElement.Structure.ToString(), typeof(StructureSerializer) },
            { L5XElement.AddOnInstructionDefinition.ToString(), typeof(AddOnInstructionSerializer) }
        };

        public L5XSerializers(L5XDocument document)
        {
            _serializers = new Dictionary<Type, IL5XSerializer>
            {
                { typeof(AddOnInstructionSerializer), new AddOnInstructionSerializer(document) },
                { typeof(ConfigTagSerializer), new ConfigTagSerializer(document) },
                { typeof(ConnectionSerializer), new ConnectionSerializer(document) },
                { typeof(ControllerSerializer), new ControllerSerializer() },
                { typeof(DataTypeSerializer), new DataTypeSerializer(document) },
                { typeof(InputTagSerializer), new InputTagSerializer(document) },
                { typeof(LadderLogicSerializer), new LadderLogicSerializer(document) },
                { typeof(LocalTagSerializer), new LocalTagSerializer(document) },
                { typeof(MemberSerializer), new MemberSerializer(document) },
                { typeof(ModuleSerializer), new ModuleSerializer(document) },
                { typeof(OutputTagSerializer), new OutputTagSerializer(document) },
                { typeof(ParameterSerializer), new ParameterSerializer(document) },
                { typeof(PortSerializer), new PortSerializer() },
                { typeof(ProgramSerializer), new ProgramSerializer(document) },
                { typeof(RoutineSerializer), new RoutineSerializer(document) },
                { typeof(RungSerializer), new RungSerializer() },
                { typeof(TagSerializer), new TagSerializer(document) },
                { typeof(TaskSerializer), new TaskSerializer() },
                { typeof(AlarmAnalogParametersSerializer), new AlarmAnalogParametersSerializer() },
                { typeof(AlarmDataSerializer), new AlarmDataSerializer(document) },
                { typeof(AlarmDigitalParametersSerializer), new AlarmDigitalParametersSerializer() },
                { typeof(ArrayElementSerializer), new ArrayElementSerializer(document) },
                { typeof(ArrayMemberSerializer), new ArrayMemberSerializer(document) },
                { typeof(ArraySerializer), new ArraySerializer(document) },
                { typeof(DataValueMemberSerializer), new DataValueMemberSerializer() },
                { typeof(DataValueSerializer), new DataValueSerializer() },
                { typeof(DecoratedDataSerializer), new DecoratedDataSerializer(document) },
                { typeof(FormattedDataSerializer), new FormattedDataSerializer(document) },
                { typeof(StructureMemberSerializer), new StructureMemberSerializer(document) },
                { typeof(StructureSerializer), new StructureSerializer(document) }
            };
        }

        public TSerializer Get<TSerializer>() where TSerializer : IL5XSerializer
        {
            _serializers.TryGetValue(typeof(TSerializer), out var result);

            if (result is not TSerializer serializer)
                throw new InvalidOperationException($"No serializer defined for type '{typeof(TSerializer)}'");

            return serializer;
        }

        public IL5XSerializer<TComponent> ForComponent<TComponent>() where TComponent : ILogixComponent
        {
            _componentLookup.TryGetValue(typeof(TComponent), out var type);

            if (type is null)
                throw new InvalidOperationException(
                    $"No serializer defined for component of type '{typeof(TComponent)}'");

            _serializers.TryGetValue(type, out var result);

            if (result is not IL5XSerializer<TComponent> serializer)
                throw new InvalidOperationException($"The configured serializer is not a valid '{type}'");

            return serializer;
        }


        public IL5XSerializer<T> ForElement<T>(string name)
        {
            _elementLookup.TryGetValue(name, out var type);

            if (type is null)
                throw new InvalidOperationException($"No serializer defined for element '{name}'");

            _serializers.TryGetValue(type, out var result);

            if (result is not IL5XSerializer<T> serializer)
                throw new InvalidOperationException($"No serializer defined for type '{type}'");

            return serializer;
        }
    }
}