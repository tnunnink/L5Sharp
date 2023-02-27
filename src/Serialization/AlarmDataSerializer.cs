using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types.Predefined;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A logix serializer that forwards serialization to the specified alarm data type.
    /// </summary>
    public class AlarmDataSerializer : ILogixSerializer<ILogixType>
    {
        private readonly AlarmAnalogSerializer _analogSerializer = new();
        private readonly AlarmDigitalSerializer _digitalSerializer = new();


        /// <inheritdoc />
        public XElement Serialize(ILogixType obj)
        {
            Check.NotNull(obj);

            var element = new XElement(L5XName.Data);
            element.AddValue(DataFormat.Alarm, L5XName.Format);

            var data = obj switch
            {
                ALARM_DIGITAL digital => _digitalSerializer.Serialize(digital),
                ALARM_ANALOG analog => _analogSerializer.Serialize(analog),
                _ => throw new ArgumentException(
                    $"Data type {obj.GetType()} is not valid for the serializer {GetType()}")
            };
            
            element.Add(data);
            return element;
        }

        /// <inheritdoc />
        public ILogixType Deserialize(XElement element)
        {
            if (element is null)
                throw new ArgumentNullException(nameof(element));

            var name = element.Elements().First().Name.ToString();

            // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
            // Only following element names are valid for the alarm format.
            return name switch
            {
                L5XName.AlarmDigitalParameters => _digitalSerializer.Deserialize(element),
                L5XName.AlarmAnalogParameters => _analogSerializer.Deserialize(element),
                _ => throw new ArgumentException($"Element '{name}' not valid for the serializer {GetType()}.")
            };
        }
    }
}