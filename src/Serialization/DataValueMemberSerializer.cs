using System;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
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
            var radix = element.ValueOrDefault<Radix>(L5XName.Radix);
            var value = element.GetValue<string>(L5XName.Value);

            var atomic = Atomic.Parse(dataType, value, radix);

            return new Member(name, atomic);
        }
    }
}