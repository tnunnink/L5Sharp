using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A <see cref="ILogixSerializer{T}"/> that serializes <see cref="Member"/> whose data type is a <see cref="AtomicType"/> object.
    /// </summary>
    public class DataValueMemberSerializer : ILogixSerializer<Member>
    {
        /// <inheritdoc />
        public XElement Serialize(Member obj)
        {
            Check.NotNull(obj);
            
            var atomicType = (AtomicType)obj.DataType;
            
            var element = new XElement(L5XName.DataValueMember);
            element.Add(new XAttribute(L5XName.Name, obj.Name));
            element.Add(new XAttribute(L5XName.DataType, obj.DataType.Name));

            if (atomicType is not BOOL)
                element.Add(new XAttribute(L5XName.Radix, atomicType.Radix));
            
            element.Add(new XAttribute(L5XName.Value, atomicType.ToString()));

            return element;
        }

        /// <inheritdoc />
        public Member Deserialize(XElement element)
        {
            Check.NotNull(element);

            var name = element.Attribute(L5XName.Name)?.Value ?? throw new ArgumentException();
            var dataType = element.Attribute(L5XName.DataType)?.Value
                           ?? throw new ArgumentException($"Element must have {L5XName.DataType} attribute.");
            var radix = element.Attribute(L5XName.Radix)?.Value.Parse<Radix>() ?? Radix.Decimal;
            var value = element.Attribute(L5XName.Value)?.Value
                        ?? throw new ArgumentException($"Element must have {L5XName.Value} attribute.");
            
            
            var atomic = Atomic.Parse(dataType, value, radix);

            return new Member(name, atomic);
        }
    }
}