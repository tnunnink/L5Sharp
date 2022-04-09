using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Creators;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;
using L5Sharp.Types;

namespace L5Sharp.Serialization
{
    internal class DataValueMemberSerializer : L5XSerializer<IMember<IDataType>>
    {
        private static readonly XName ElementName = L5XElement.DataValueMember.ToString();

        public override XElement Serialize(IMember<IDataType> component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));
            
            if (component.DataType is not IAtomicType atomic)
                throw new ArgumentException($"{ElementName} must have an atomic data type.");

            var element = new XElement(ElementName);
            element.Add(new XAttribute(L5XAttribute.Name.ToString(), component.Name));
            element.Add(new XAttribute(L5XAttribute.DataType.ToString(), component.DataType));
            if (!string.Equals(component.DataType.Name, nameof(BOOL), StringComparison.OrdinalIgnoreCase))
                element.Add(new XAttribute(L5XAttribute.Radix.ToString(), component.Radix));
            element.Add(new XAttribute(L5XAttribute.Value.ToString(), atomic.Format(component.Radix)));

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
            var radix = element.Attribute(L5XAttribute.Radix.ToString())?.Value?.Parse<Radix>() ?? Radix.Decimal;
            var value = element.Attribute(L5XAttribute.Value.ToString())?.Value;
            
            var dataType = DataType.Atomic(type, value);
            
            return Member.Create(name, dataType, radix);
        }
    }
}