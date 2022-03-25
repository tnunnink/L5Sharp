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
        private static readonly XName ElementName = L5XElement.Array.ToString();
        private readonly ArrayElementSerializer _elementSerializer;

        public ArraySerializer(StructureSerializer structureSerializer)
        {
            _elementSerializer = new ArrayElementSerializer(structureSerializer);
        }

        public ArraySerializer(L5XDocument document)
        {
            _elementSerializer = document.Serializers().Get<ArrayElementSerializer>();
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
            
            var elements = component.Select(e => _elementSerializer.Serialize(e));
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
            
            var members = element.Elements().Select(e => _elementSerializer.Deserialize(e));
            
            return new ArrayType<IDataType>(dimensions!, members.Select(m => m.DataType).ToList(), radix);
        }
    }
}