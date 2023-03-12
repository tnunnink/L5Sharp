using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization.Data
{
    /// <summary>
    /// A logix serializer that performs serialization of <see cref="Member"/> value types.
    /// </summary>
    public class DataValueMemberSerializer : ILogixSerializer<Member>
    {
        /// <inheritdoc />
        public XElement Serialize(Member obj)
        {
            Check.NotNull(obj);

            var element = new XElement(L5XName.DataValueMember);
            element.AddValue(obj.Name, L5XName.Name);
            element.AddValue(obj.DataType.Name, L5XName.DataType);

            var atomicType = (AtomicType)obj.DataType;
            if (atomicType is not BOOL)
                element.AddValue(atomicType, a => a.Radix);
            
            element.AddValue(atomicType, L5XName.Value);

            return element;
        }

        /// <inheritdoc />
        public Member Deserialize(XElement element)
        {
            Check.NotNull(element);

            var name = element.LogixName();
            var dataType = element.GetValue<string>(L5XName.DataType);
            var radix = element.TryGetValue<Radix>(L5XName.Radix);
            var value = element.GetValue<string>(L5XName.Value);
            
            //Not really sure how to handle this not a number value other than set to default for now.
            if (value == "1.#QNAN")
                value = dataType == "REAL" ? "0.0" : "0";
            
            //There is an issue where some data values format does not match the indicated radix attribute value (Date/Time (ns)).
            //To get around this, I will just infer the format from the value, and use that if it doesn't match what
            //Rockwell is saying it is.
            var format = Radix.Infer(value);

            var atomic = Logix.Atomic(dataType, value, !format.Equals(radix) ? format : radix);

            return new Member(name, atomic);
        }
    }
}