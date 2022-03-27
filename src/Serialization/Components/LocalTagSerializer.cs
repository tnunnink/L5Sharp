using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;
using L5Sharp.Serialization.Data;

namespace L5Sharp.Serialization.Components
{
    internal class LocalTagSerializer : L5XSerializer<ITag<IDataType>>
    {
        private readonly L5XDocument? _document;
        private static readonly XName ElementName = L5XElement.LocalTag.ToString();
        private readonly FormattedDataSerializer _formattedDataSerializer;

        public LocalTagSerializer(L5XDocument? document = null)
        {
            _document = document;
            _formattedDataSerializer = new FormattedDataSerializer(_document, L5XElement.DefaultData);
        }

        public override XElement Serialize(ITag<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.AddComponentName(component.Name);
            element.AddComponentDescription(component.Description);
            element.Add(new XAttribute(L5XAttribute.DataType.ToString(), component.DataType.Name));
            if (!component.Dimensions.IsEmpty)
                element.Add(new XAttribute(L5XAttribute.Dimensions.ToString(), component.Dimensions));
            if (component.MemberType == MemberType.ValueMember)
                element.Add(new XAttribute(L5XAttribute.Radix.ToString(), component.Radix));
            element.Add(new XAttribute(L5XAttribute.ExternalAccess.ToString(), component.ExternalAccess));

            var data = _formattedDataSerializer.Serialize(component.DataType);
            element.Add(data);

            return element;
        }

        public override ITag<IDataType> Deserialize(XElement element)
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
            var dimensions = element.Attribute(L5XAttribute.Dimensions.ToString())?.Value.Parse<Dimensions>();
            var radix = element.Attribute(L5XAttribute.Radix.ToString())?.Value.Parse<Radix>();
            var access = element.Attribute(L5XAttribute.ExternalAccess.ToString())?.Value.Parse<ExternalAccess>();

            var type = dimensions is not null && !dimensions.IsEmpty
                ? new ArrayType<IDataType>(dimensions, dataType, radix, access, description)
                : dataType;

            var tag = new Tag<IDataType>(name, type, radix, access, description, TagUsage.Local);

            var formattedData = element.Descendants(L5XElement.DefaultData.ToString())
                .FirstOrDefault(e => e.Attribute(L5XAttribute.Format.ToString())?.Value != TagDataFormat.L5K);

            if (formattedData is null) return tag;

            var data = _formattedDataSerializer.Deserialize(formattedData);
            tag.SetData(data);
            return tag;
        }
    }
}