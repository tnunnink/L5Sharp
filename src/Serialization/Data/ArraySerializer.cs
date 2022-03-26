using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization.Data
{
    internal class ArraySerializer : L5XSerializer<IArrayType<IDataType>>
    {
        private readonly L5XDocument? _document;
        private static readonly XName ElementName = L5XElement.Array.ToString();

        private ArrayElementSerializer ArrayElementSerializer => _document is not null
            ? _document.Serializers.Get<ArrayElementSerializer>()
            : new ArrayElementSerializer(_document);

        public ArraySerializer(L5XDocument? document = null)
        {
            _document = document;
        }

        public override XElement Serialize(IArrayType<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.Add(new XAttribute(L5XElement.DataType.ToString(), component.Name));
            element.Add(new XAttribute(L5XAttribute.Dimensions.ToString(), component.Dimensions));
            if (component.First().Radix != Radix.Null)
                element.Add(new XAttribute(L5XAttribute.Radix.ToString(), component.First().Radix));
            
            var elements = component.Select(e => ArrayElementSerializer.Serialize(e));
            element.Add(elements);

            return element;
        }

        public override IArrayType<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var dimensions = Dimensions.Parse(element.Attribute(L5XAttribute.Dimensions.ToString())?.Value!);
            Radix.TryFromValue(element.Attribute(L5XAttribute.Radix.ToString())?.Value!, out var radix);
            
            var members = element.Elements().Select(e => ArrayElementSerializer.Deserialize(e));
            
            return new ArrayType<IDataType>(dimensions!, members.Select(m => m.DataType).ToList(), radix);
        }
    }
}