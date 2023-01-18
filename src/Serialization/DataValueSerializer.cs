using System;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A <see cref="ILogixSerializer{T}"/> that serializes <see cref="AtomicType"/> or data value elements.
    /// </summary>
    public class DataValueSerializer : ILogixSerializer<AtomicType>
    {
        /// <inheritdoc />
        public XElement Serialize(AtomicType obj)
        {
            Check.NotNull(obj);

            var dataValue = new XElement(L5XName.DataValue);
            dataValue.Add(new XAttribute(L5XName.DataType, obj.Name));
            dataValue.Add(new XAttribute(L5XName.Radix, obj.Radix.Value));
            dataValue.Add(new XAttribute(L5XName.Value, obj.ToString()));
            
            return dataValue;
        }

        /// <inheritdoc />
        public AtomicType Deserialize(XElement element)
        {
            Check.NotNull(element);
            
            var name = element.Attribute(L5XName.DataType)?.Value
                       ?? throw new ArgumentException($"Element must have {L5XName.DataType} attribute.");
            var radix = element.Attribute(L5XName.Radix)?.Value.Parse<Radix>() ?? Radix.Decimal;
            var value = element.Attribute(L5XName.Value)?.Value
                        ?? throw new ArgumentException($"Element must have {L5XName.Value} attribute.");

            return Atomic.Parse(name, value, radix);
        }
    }
}