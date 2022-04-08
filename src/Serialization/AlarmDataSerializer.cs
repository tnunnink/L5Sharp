using System;
using System.Xml.Linq;
using L5Sharp.L5X;
using L5Sharp.Types;

namespace L5Sharp.Serialization
{
    internal class AlarmDataSerializer : L5XSerializer<IDataType>
    {
        private readonly L5XDocument? _document;

        private AlarmDigitalParametersSerializer AlarmDigitalParametersSerializer => _document is not null
            ? _document.Serializers.Get<AlarmDigitalParametersSerializer>()
            : new AlarmDigitalParametersSerializer();
        
        private AlarmAnalogParametersSerializer AlarmAnalogParametersSerializer => _document is not null
            ? _document.Serializers.Get<AlarmAnalogParametersSerializer>()
            : new AlarmAnalogParametersSerializer();
        
        public AlarmDataSerializer(L5XDocument? document = null)
        {
            _document = document;
        }

        public override XElement Serialize(IDataType component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            return component switch
            {
                ALARM_DIGITAL digital => AlarmDigitalParametersSerializer.Serialize(digital),
                ALARM_ANALOG analog => AlarmAnalogParametersSerializer.Serialize(analog),
                _ => throw new ArgumentException(
                    $"Data type {component.GetType()} is not valid for the serializer {GetType()}")
            };
        }

        public override IDataType Deserialize(XElement element)
        {
            if (element is null)
                throw new ArgumentNullException(nameof(element));

            var name = Enum.Parse<L5XElement>(element.Name.ToString());

            // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
            // Only following element names are valid for the alarm format.
            return name switch
            {
                L5XElement.AlarmDigitalParameters => AlarmDigitalParametersSerializer.Deserialize(element),
                L5XElement.AlarmAnalogParameters => AlarmAnalogParametersSerializer.Deserialize(element),
                _ => throw new ArgumentException($"Element '{name}' not valid for the serializer {GetType()}.")
            };
        }
    }
}