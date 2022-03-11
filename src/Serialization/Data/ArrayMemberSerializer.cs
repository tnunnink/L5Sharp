using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization.Data
{
    internal class ArrayMemberSerializer : IL5XSerializer<IMember<IDataType>>
    {
        private static readonly XName ElementName = L5XElement.ArrayMember.ToString();
        private readonly ArrayElementSerializer _arrayElementSerializer;

        public ArrayMemberSerializer(StructureSerializer structureSerializer)
        {
            _arrayElementSerializer = new ArrayElementSerializer(structureSerializer);
        }

        public XElement Serialize(IMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            if (component.DataType is not IArrayType<IDataType> arrayType)
                throw new ArgumentException("Provided component is not an array type.");

            var element = new XElement(ElementName);

            element.Add(new XAttribute(L5XAttribute.Name.ToString(), component.Name));
            element.Add(new XAttribute(L5XAttribute.DataType.ToString(), component.DataType.Name));
            element.Add(new XAttribute(L5XAttribute.Dimensions.ToString(), component.Dimensions));
            if (component.Radix != Radix.Null)
                element.Add(new XAttribute(L5XAttribute.Radix.ToString(), component.Radix));
            
            var elements = arrayType.Select(m => _arrayElementSerializer.Serialize(m));
            element.Add(elements);

            return element;
        }

        public IMember<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.ComponentName();
            var dimensions = element.Attribute(L5XAttribute.Dimensions.ToString())?.Value.Parse<Dimensions>();
            var radix = element.Attribute(L5XAttribute.Radix.ToString())?.Value.Parse<Radix>();
            var members = element.Elements().Select(e => _arrayElementSerializer.Deserialize(e));
            
            var arrayType = new ArrayType<IDataType>(dimensions!, members.Select(m => m.DataType).ToList(), radix);

            return new Member<IArrayType<IDataType>>(name, arrayType, radix);
        }
    }
}