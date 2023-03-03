using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization.Data
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
            dataValue.AddValue(obj.Radix, L5XName.Radix);
            dataValue.AddValue(obj, L5XName.Value);

            return dataValue;
        }

        /// <inheritdoc />
        public AtomicType Deserialize(XElement element)
        {
            Check.NotNull(element);
            
            var name = element.GetValue<string>(L5XName.DataType);
            var radix = element.TryGetValue<Radix>(L5XName.Radix);
            var value = element.GetValue<string>(L5XName.Value);

            if (value == "1.#QNAN")
                Logix.Atomic(name, radix);

                //There is an issue where some data values format does not match the indicated radix attribute value (Date/Time (ns)).
            //To get around this, I will just infer the format from the value, and use that if it doesn't match what
            //Rockwell is saying it is.
            var format = Radix.Infer(value);

            return Logix.Atomic(name, value, radix != format ? format : radix);
        }
    }
}