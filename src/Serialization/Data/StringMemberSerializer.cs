using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization.Data
{
    internal class StringStructureSerializer : IL5XSerializer<IStringType>
    {
        private readonly XName _elementName;

        public StringStructureSerializer(XName elementName)
        {
            _elementName = elementName;
        }

        public XElement Serialize(IStringType component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(_elementName);
            
            if (_elementName == L5XElement.StructureMember.ToString())
                element.AddComponentName(component.Name);
            element.Add(new XAttribute(L5XAttribute.DataType.ToString(), component.Name));

            var lengthElement = new XElement(L5XElement.DataValueMember.ToString());
            lengthElement.Add(new XAttribute(L5XAttribute.Name.ToString(), component.LEN.Name));
            lengthElement.Add(new XAttribute(L5XAttribute.DataType.ToString(), component.LEN.DataType));
            lengthElement.Add(new XAttribute(L5XAttribute.Radix.ToString(), component.LEN.Radix));
            lengthElement.Add(new XAttribute(L5XAttribute.Value.ToString(), component.LEN.DataType.Value));

            var dataElement = new XElement(L5XElement.DataValueMember.ToString());
            dataElement.Add(new XAttribute(L5XAttribute.Name.ToString(), component.DATA.Name));
            dataElement.Add(new XAttribute(L5XAttribute.DataType.ToString(), component.Name));
            dataElement.Add(new XAttribute(L5XAttribute.Radix.ToString(), component.DATA.Radix));
            dataElement.Add(new XCData(component.Value));

            element.Add(lengthElement);
            element.Add(dataElement);

            return element;
        }

        public IStringType Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != _elementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.DataTypeName();
            var value = element.Elements()
                .First(e => e.Attribute(L5XAttribute.Name.ToString())?.Value == "DATA")
                .Value;
            
            return new StringDefined(name, value);
        }
    }
}