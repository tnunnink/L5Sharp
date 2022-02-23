using System;
using System.Xml.Linq;
using L5Sharp.L5X;
using L5Sharp.Types;

namespace L5Sharp.Serialization.Data
{
    internal class AlarmDataSerializer : IL5XSerializer<IDataType>
    {
        public XElement Serialize(IDataType component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));
            
            switch (component)
            {
                case AlarmDigital digital:
                    var digitalDataSerializer = new AlarmDigitalDataSerializer();
                    return digitalDataSerializer.Serialize(digital);
                case AlarmAnalog analog:
                    var analogDataSerializer = new AlarmAnalogDataSerializer();
                    return analogDataSerializer.Serialize(analog);
                default:
                    throw new ArgumentException(
                        $"Data type {component.GetType()} is not valid for the serializer {GetType()}");
            }
        }

        public IDataType Deserialize(XElement element)
        {
            if (element is null)
                throw new ArgumentNullException(nameof(element));

            var name = Enum.Parse<L5XElement>(element.Name.ToString());
            
            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            // Only following element names are valid for 
            switch (name)
            {
                case L5XElement.AlarmDigitalParameters:
                    var digitalDataSerializer = new AlarmDigitalDataSerializer();
                    return digitalDataSerializer.Deserialize(element);
                case L5XElement.AlarmAnalogParameters:
                    var analogDataSerializer = new AlarmAnalogDataSerializer();
                    return analogDataSerializer.Deserialize(element);
                default:
                    throw new ArgumentException(
                        $"Element '{name}' not valid for the serializer {GetType()}.");
            }
        }
    }
}