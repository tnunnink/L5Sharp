using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    internal class ArraySerializer : L5XSerializer<IArrayType<IDataType>>
    {
        private readonly LogixInfo? _document;
        private static readonly XName ElementName = L5XName.Array;

        private ArrayElementSerializer ArrayElementSerializer => _document is not null
            ? _document.Serializers.Get<ArrayElementSerializer>()
            : new ArrayElementSerializer(_document);

        public ArraySerializer(LogixInfo? document = null)
        {
            _document = document;
        }

        public override XElement Serialize(IArrayType<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.Add(new XAttribute(L5XName.DataType, component.Name));
            element.Add(new XAttribute(L5XName.Dimensions, component.Dimensions));
            if (component.First().Radix != Radix.Null)
                element.Add(new XAttribute(L5XName.Radix, component.First().Radix));
            
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

            var dimensions = Dimensions.Parse(element.Attribute(L5XName.Dimensions)?.Value!);
            Radix.TryFromValue(element.Attribute(L5XName.Radix)?.Value!, out var radix);
            
            var members = element.Elements().Select(e => ArrayElementSerializer.Deserialize(e));
            
            return new ArrayType<IDataType>(dimensions, members.Select(m => m.DataType).ToList(), radix);
        }
    }
}