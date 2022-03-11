using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Atomics;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.L5X;
using String = L5Sharp.Predefined.String;

namespace L5Sharp.Serialization.Data
{
    internal class StringMembersSerializer : IL5XSerializer<IStringType>
    {
        private static readonly XName ElementName = L5XElement.Structure.ToString();

        public XElement Serialize(IStringType component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
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

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.DataTypeName();

            var members = new List<IMember<IDataType>>();

            var length = element.Elements().First(e => e.Attribute(L5XAttribute.Name.ToString())?.Value == "LEN")
                .Attribute(L5XAttribute.Value.ToString())?.Value.Parse<int>() ?? default;

            var value = element.Elements().First(e => e.Attribute(L5XAttribute.Name.ToString())?.Value == "DATA").Value;

            members.Add(new Member<Dint>("LEN", new Dint(length)));

            throw new NotImplementedException();
        }
    }
}