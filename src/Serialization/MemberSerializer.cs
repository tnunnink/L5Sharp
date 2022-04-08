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
        private static readonly XName ElementName = L5XElement.Member.ToString();
        private readonly L5XDocument? _document;
        
        public MemberSerializer(L5XDocument? document = null)
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
            element.Add(new XAttribute(L5XAttribute.DataType.ToString(), component.DataType.Name));
            element.Add(new XAttribute(L5XAttribute.Dimension.ToString(), component.Dimensions));
            element.Add(new XAttribute(L5XAttribute.Radix.ToString(), component.Radix));
            element.Add(new XAttribute(L5XAttribute.Hidden.ToString(), false));
            element.Add(new XAttribute(L5XAttribute.ExternalAccess.ToString(), component.ExternalAccess));

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
            var dimensions = element.Attribute(L5XAttribute.Dimension.ToString())?.Value.Parse<Dimensions>();
            var radix = element.Attribute(L5XAttribute.Radix.ToString())?.Value.Parse<Radix>();
            var access = element.Attribute(L5XAttribute.ExternalAccess.ToString())?.Value.Parse<ExternalAccess>();

            if (dimensions is null || dimensions.IsEmpty)
                return new Member<IDataType>(name, dataType, radix, access, description);

            var arrayType = new ArrayType<IDataType>(dimensions, dataType, radix, access, description);
            return new Member<IDataType>(name, arrayType, radix, access, description);
        }
    }
}