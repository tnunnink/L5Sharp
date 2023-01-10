using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Creators;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    internal class DataValueMemberSerializer : L5XSerializer<IMember<IDataType>>
    {
        private static readonly XName ElementName = L5XName.DataValueMember;

        public override XElement Serialize(IMember<IDataType> component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));
            
            if (component.DataType is not IAtomicType atomic)
                throw new ArgumentException($"{ElementName} must have an atomic data type.");

            var element = new XElement(ElementName);
            element.Add(new XAttribute(L5XName.Name, component.Name));
            element.Add(new XAttribute(L5XName.DataType, component.DataType.Name));
            if (!string.Equals(component.DataType.Name, nameof(BOOL), StringComparison.OrdinalIgnoreCase))
                element.Add(new XAttribute(L5XName.Radix, component.Radix));
            element.Add(new XAttribute(L5XName.Value, atomic.Format(component.Radix)));

            return element;
        }

        public override IMember<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.ComponentName();
            var type = element.DataTypeName();
            var radix = element.Attribute(L5XName.Radix)?.Value?.Parse<Radix>() ?? Radix.Decimal;
            var value = element.Attribute(L5XName.Value)?.Value.TryParse<IAtomicType>();
            
            var dataType = LogixType.Atomic(type, value);

            return Member.Create(name, dataType, radix);
        }
    }
}