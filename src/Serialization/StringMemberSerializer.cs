using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.L5X;
using L5Sharp.Types;

namespace L5Sharp.Serialization
{
    internal class StringMemberSerializer : L5XSerializer<IMember<IStringType>>
    {
        private static readonly XName ElementName = L5XName.StructureMember;

        public override XElement Serialize(IMember<IStringType> component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.AddComponentName(component.Name);
            element.Add(new XAttribute(L5XName.DataType, component.DataType.Name));

            var lengthElement = new XElement(L5XName.DataValueMember);
            lengthElement.Add(new XAttribute(L5XName.Name, component.DataType.LEN.Name));
            lengthElement.Add(new XAttribute(L5XName.DataType, component.DataType.LEN.DataType.Name));
            lengthElement.Add(new XAttribute(L5XName.Radix, component.DataType.LEN.Radix));
            lengthElement.Add(new XAttribute(L5XName.Value, component.DataType.LEN.DataType.Value));

            var dataElement = new XElement(L5XName.DataValueMember);
            dataElement.Add(new XAttribute(L5XName.Name, component.DataType.DATA.Name));
            dataElement.Add(new XAttribute(L5XName.DataType, component.DataType.Name));
            dataElement.Add(new XAttribute(L5XName.Radix, component.DataType.DATA.Radix));
            dataElement.Add(new XCData(component.DataType.Value));

            element.Add(lengthElement);
            element.Add(dataElement);

            return element;
        }

        public override IMember<IStringType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.ComponentName();
            var typeName = element.DataTypeName();
            var value = element.Elements()
                .First(e => e.Attribute(L5XName.Name)?.Value == "DATA")
                .Value;

            IStringType str = string.Equals(nameof(STRING), typeName, StringComparison.OrdinalIgnoreCase)
                ? new STRING(value)
                : value.IsEmpty()
                    ? new STRING()
                    : new StringDefined(typeName, value);

            return new Member<IStringType>(name, str);
        }
    }
}