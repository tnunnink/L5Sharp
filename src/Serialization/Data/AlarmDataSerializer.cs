using System;
using System.Xml.Linq;
using L5Sharp.L5X;
using L5Sharp.Types;

namespace L5Sharp.Serialization.Data
{
    internal class AlarmDataSerializer : IL5XSerializer<IDataType>
    {
        private readonly AlarmDigitalParametersSerializer _digitalParametersSerializer;
        private readonly AlarmAnalogParametersSerializer _analogParametersSerializer;

        public AlarmDataSerializer()
        {
            _digitalParametersSerializer = new AlarmDigitalParametersSerializer();
            _analogParametersSerializer = new AlarmAnalogParametersSerializer();
        }

        public XElement Serialize(IDataType component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            return component switch
            {
                ALARM_DIGITAL digital => _digitalParametersSerializer.Serialize(digital),
                ALARM_ANALOG analog => _analogParametersSerializer.Serialize(analog),
                _ => throw new ArgumentException(
                    $"Data type {component.GetType()} is not valid for the serializer {GetType()}")
            };
        }

        public IDataType Deserialize(XElement element)
        {
            if (element is null)
                throw new ArgumentNullException(nameof(element));

            var name = Enum.Parse<L5XElement>(element.Name.ToString());

            // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
            // Only following element names are valid for the alarm format.
            return name switch
            {
                L5XElement.AlarmDigitalParameters => _digitalParametersSerializer.Deserialize(element),
                L5XElement.AlarmAnalogParameters => _analogParametersSerializer.Deserialize(element),
                _ => throw new ArgumentException($"Element '{name}' not valid for the serializer {GetType()}.")
            };
        }
    }
}