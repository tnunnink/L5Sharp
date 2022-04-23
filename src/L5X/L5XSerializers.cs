using System;
using System.Collections.Generic;
using L5Sharp.Serialization;

namespace L5Sharp.L5X
{
    /// <summary>
    /// A class that contains instances of <see cref="IL5XSerializer{T}"/>, which make available the
    /// current <see cref="LogixContext"/> object, so that the serializers may use the information to necessary
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

        public L5XSerializers(L5XContent content)
        {
            _serializers = new Dictionary<Type, IL5XSerializer>
            {
                { typeof(AddOnInstructionSerializer), new AddOnInstructionSerializer(content) },
                { typeof(ConfigTagSerializer), new ConfigTagSerializer(content) },
                { typeof(ConnectionSerializer), new ConnectionSerializer(content) },
                { typeof(ControllerSerializer), new ControllerSerializer() },
                { typeof(DataTypeSerializer), new DataTypeSerializer(content) },
                { typeof(InputTagSerializer), new InputTagSerializer(content) },
                { typeof(LadderLogicSerializer), new LadderLogicSerializer(content) },
                { typeof(LocalTagSerializer), new LocalTagSerializer(content) },
                { typeof(MemberSerializer), new MemberSerializer(content) },
                { typeof(ModuleSerializer), new ModuleSerializer(content) },
                { typeof(OutputTagSerializer), new OutputTagSerializer(content) },
                { typeof(ParameterSerializer), new ParameterSerializer(content) },
                { typeof(PortSerializer), new PortSerializer() },
                { typeof(ProgramSerializer), new ProgramSerializer(content) },
                { typeof(RoutineSerializer), new RoutineSerializer(content) },
                { typeof(RungSerializer), new RungSerializer() },
                { typeof(TagSerializer), new TagSerializer(content) },
                { typeof(TaskSerializer), new TaskSerializer() },
                { typeof(AlarmAnalogParametersSerializer), new AlarmAnalogParametersSerializer() },
                { typeof(AlarmDataSerializer), new AlarmDataSerializer(content) },
                { typeof(AlarmDigitalParametersSerializer), new AlarmDigitalParametersSerializer() },
                { typeof(ArrayElementSerializer), new ArrayElementSerializer(content) },
                { typeof(ArrayMemberSerializer), new ArrayMemberSerializer(content) },
                { typeof(ArraySerializer), new ArraySerializer(content) },
                { typeof(DataValueMemberSerializer), new DataValueMemberSerializer() },
                { typeof(DataValueSerializer), new DataValueSerializer() },
                { typeof(DecoratedDataSerializer), new DecoratedDataSerializer(content) },
                { typeof(FormattedDataSerializer), new FormattedDataSerializer(content) },
                { typeof(StringStructureSerializer), new StringStructureSerializer() },
                { typeof(StringMemberSerializer), new StringMemberSerializer() },
                { typeof(StructureMemberSerializer), new StructureMemberSerializer(content) },
                { typeof(StructureSerializer), new StructureSerializer(content) }
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
    }
}