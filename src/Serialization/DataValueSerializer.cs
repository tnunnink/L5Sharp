using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Extensions;
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
            dataValue.AddValue(obj.Name, L5XName.DataType);
            dataValue.AddValue(obj.ToString(), L5XName.Value);

            return dataValue;
        }

        /// <inheritdoc />
        public AtomicType Deserialize(XElement element)
        {
            Check.NotNull(element);
            
            var name = element.Value<string>(L5XName.DataType);
            var radix = element.ValueOrDefault<Radix>(L5XName.Radix);
            var value = element.Value<string>(L5XName.Value);

            return Atomic.Parse(name, value, radix);
        }
    }
}