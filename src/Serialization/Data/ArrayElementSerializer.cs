using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization.Data
{
    internal class ArrayElementSerializer : IL5XSerializer<IMember<IDataType>>
    {
        private static readonly XName ElementName = L5XElement.Element.ToString();
        private readonly IL5XSerializer<IComplexType> _structureSerializer;

        public ArrayElementSerializer(StructureSerializer structureSerializer)
        {
            _structureSerializer = structureSerializer;
        }

        public XElement Serialize(IMember<IDataType> component)
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
                    element.Add(_structureSerializer.Serialize(complexType));
                    break;
                }
            }

            return element;
        }

        public IMember<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var index = element.Attribute(L5XAttribute.Index.ToString())?.Value!;
            var value = element.Attribute(L5XAttribute.Value.ToString())?.Value;

            IDataType dataType = value is not null
                ? DataType.Atomic(element.Parent?.DataTypeName()!, value)
                : _structureSerializer.Deserialize(element.Element(L5XElement.Structure.ToString())!);

            return new Member<IDataType>(index, dataType);
        }
    }
}