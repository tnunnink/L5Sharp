using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.L5X;
using L5Sharp.Types;

namespace L5Sharp.Serialization
{
    internal class StringStructureSerializer : L5XSerializer<IStringType>
    {
        private static readonly XName ElementName = L5XName.Structure;

        public override XElement Serialize(IStringType component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
           
            element.Add(new XAttribute(L5XName.DataType, component.Name));

            var lengthElement = new XElement(L5XName.DataValueMember);
            lengthElement.Add(new XAttribute(L5XName.Name, component.LEN.Name));
            lengthElement.Add(new XAttribute(L5XName.DataType, component.LEN.DataType.Name));
            lengthElement.Add(new XAttribute(L5XName.Radix, component.LEN.Radix));
            lengthElement.Add(new XAttribute(L5XName.Value, component.LEN.DataType.Value));

            var dataElement = new XElement(L5XName.DataValueMember);
            dataElement.Add(new XAttribute(L5XName.Name, component.DATA.Name));
            dataElement.Add(new XAttribute(L5XName.DataType, component.Name));
            dataElement.Add(new XAttribute(L5XName.Radix, component.DATA.Radix));
            dataElement.Add(new XCData(component.Value));

            element.Add(lengthElement);
            element.Add(dataElement);

            return element;
        }

        public override IStringType Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.DataTypeName();
            var value = element.Elements()
                .First(e => e.Attribute(L5XName.Name)?.Value == "DATA")
                .Value;
            
            return string.Equals(nameof(STRING), name, StringComparison.OrdinalIgnoreCase) 
                ? new STRING(value) : value.IsEmpty() ? new STRING() : new StringDefined(name, value);
        }
    }
}