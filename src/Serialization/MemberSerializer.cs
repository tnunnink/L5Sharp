using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization
{
    internal class MemberSerializer : L5XSerializer<IMember<IDataType>>
    {
        private static readonly XName ElementName = L5XName.Member;
        private readonly L5XContent? _document;
        
        public MemberSerializer(L5XContent? document = null)
        {
            _document = document;
        }

        public override XElement Serialize(IMember<IDataType> component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.AddComponentName(component.Name);
            element.AddComponentDescription(component.Description);
            element.Add(new XAttribute(L5XName.DataType, component.DataType.Name));
            element.Add(new XAttribute(L5XName.Dimension, component.Dimensions));
            element.Add(new XAttribute(L5XName.Radix, component.Radix));
            element.Add(new XAttribute(L5XName.Hidden, false));
            element.Add(new XAttribute(L5XName.ExternalAccess, component.ExternalAccess));

            return element;
        }

        public override IMember<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.ComponentName();
            var description = element.ComponentDescription();
            var dataType = _document is not null
                ? _document.Index.LookupType(element.DataTypeName())
                : DataType.Create(element.DataTypeName());
            var dimensions = element.Attribute(L5XName.Dimension)?.Value.Parse<Dimensions>();
            var radix = element.Attribute(L5XName.Radix)?.Value.Parse<Radix>();
            var access = element.Attribute(L5XName.ExternalAccess)?.Value.Parse<ExternalAccess>();

            if (dimensions is null || dimensions.IsEmpty)
                return new Member<IDataType>(name, dataType, radix, access, description);

            var arrayType = new ArrayType<IDataType>(dimensions, dataType, radix, access, description);
            return new Member<IDataType>(name, arrayType, radix, access, description);
        }
    }
}