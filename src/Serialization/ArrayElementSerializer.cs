using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization
{
    internal class ArrayElementSerializer : L5XSerializer<IMember<IDataType>>
    {
        private readonly L5XDocument? _document;
        private static readonly XName ElementName = L5XElement.Element.ToString();

        private StructureSerializer StructureSerializer => _document is not null
            ? _document.Serializers.Get<StructureSerializer>()
            : new StructureSerializer(_document);

        public ArrayElementSerializer(L5XDocument? document = null)
        {
            _document = document;
        }

        public override XElement Serialize(IMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.Add(new XAttribute(L5XAttribute.Index.ToString(), component.Name));

            switch (component.DataType)
            {
                case IAtomicType atomic:
                    element.Add(new XAttribute(L5XAttribute.Value.ToString(), atomic.Format(component.Radix)));
                    break;
                case IComplexType complexType:
                {
                    element.Add(StructureSerializer.Serialize(complexType));
                    break;
                }
            }

            return element;
        }

        public override IMember<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var index = element.Attribute(L5XAttribute.Index.ToString())?.Value!;
            var value = element.Attribute(L5XAttribute.Value.ToString())?.Value?.TryParse<IAtomicType>();

            IDataType dataType = value is not null
                ? DataType.Atomic(element.Parent?.DataTypeName()!, value)
                : StructureSerializer.Deserialize(element.Element(L5XElement.Structure.ToString())!);

            return new Member<IDataType>(index, dataType);
        }
    }
}