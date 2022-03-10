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

        public ArrayElementSerializer(L5XContext? context = null)
        {
            _structureSerializer = context is not null
                ? context.Serializers.GetSerializer<IComplexType>(typeof(StructureSerializer))
                : new StructureSerializer(context);
        }

        public XElement Serialize(IMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.AddAttribute(component, c => c.Name, nameOverride: L5XAttribute.Index.ToString());

            switch (component.DataType)
            {
                case IAtomicType atomic:
                    element.Add(new XAttribute(L5XAttribute.Value.ToString(), atomic.Format(component.Radix)));
                    break;
                case IComplexType complexType:
                {
                    var structure = _structureSerializer.Serialize(complexType);
                    element.Add(structure);
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
            var dataType = GetElementType(element);

            return new Member<IDataType>(index, dataType);
        }

        private static IDataType GetElementType(XElement element)
        {
            var structure = element.Element(L5XElement.Structure.ToString());

            if (structure is not null)
            {
                var serializer = new StructureSerializer();
                return serializer.Deserialize(structure);
            }

            var name = element.Parent?.DataTypeName()!;
            var value = element.Attribute(L5XAttribute.Value.ToString())?.Value!;
            return DataType.Atomic(name, value);
        }
    }
}