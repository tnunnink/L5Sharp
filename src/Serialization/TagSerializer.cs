using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization
{
    internal class TagSerializer : L5XSerializer<ITag<IDataType>>
    {
        private readonly L5XContent? _document;
        private static readonly XName ElementName = L5XName.Tag;

        private FormattedDataSerializer FormattedDataSerializer => _document is not null
            ? _document.Serializers.Get<FormattedDataSerializer>()
            : new FormattedDataSerializer(_document);

        public TagSerializer(L5XContent? document = null)
        {
            _document = document;
        }

        public override XElement Serialize(ITag<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.AddComponentName(component.Name);
            element.AddComponentDescription(component.Description);
            element.Add(new XAttribute(L5XName.TagType, component.TagType));
            if (!component.Alias.IsEmpty)
                element.Add(new XAttribute(L5XName.AliasFor, component.Alias));
            element.Add(new XAttribute(L5XName.DataType, component.DataType.Name));
            if (!component.Dimensions.IsEmpty)
                element.Add(new XAttribute(L5XName.Dimensions, component.Dimensions));
            if (component.MemberType == MemberType.ValueMember)
                element.Add(new XAttribute(L5XName.Radix, component.Radix));
            element.Add(new XAttribute(L5XName.Constant, component.Constant));
            element.Add(new XAttribute(L5XName.ExternalAccess, component.ExternalAccess));

            if (component.Comments.Any())
            {
                var commentSerializer = new TagPropertySerializer(L5XName.Comment, component.TagName);
                element.Add(component.Comments.Select(c => commentSerializer.Serialize(c)));
            }
            
            if (component.EngineeringUnits.Any())
            {
                var unitsSerializer = new TagPropertySerializer(L5XName.EngineeringUnit, component.TagName);
                element.Add(component.EngineeringUnits.Select(u => unitsSerializer.Serialize(u)));
            }
            
            var data = FormattedDataSerializer.Serialize(component.DataType);
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
            var dimensions = element.Attribute(L5XName.Dimensions)?.Value.Parse<Dimensions>();
            var radix = element.Attribute(L5XName.Radix)?.Value.Parse<Radix>();
            var access = element.Attribute(L5XName.ExternalAccess)?.Value.Parse<ExternalAccess>();
            var usage = element.Attribute(L5XName.Usage)?.Value.Parse<TagUsage>();
            var alias = element.Attribute(L5XName.AliasFor)?.Value.Parse<TagName>();
            var constant = element.Attribute(L5XName.Constant)?.Value.Parse<bool>() ?? default;

            var commentSerializer = new TagPropertySerializer(L5XName.Comment, name);
            var comments = new TagPropertyCollection<string>(element.Descendants(L5XName.Comment)
                .Select(e => commentSerializer.Deserialize(e)));

            var unitsSerializer = new TagPropertySerializer(L5XName.EngineeringUnit, name);
            var units = new TagPropertyCollection<string>(element.Descendants(L5XName.EngineeringUnit)
                .Select(e => unitsSerializer.Deserialize(e)));

            var type = dimensions is not null && !dimensions.IsEmpty
                ? new ArrayType<IDataType>(dimensions, dataType, radix, access, description)
                : dataType;

            var tag = new Tag<IDataType>(name, type, radix, access, description,
                usage, alias, constant, comments, units);

            var formattedData = element.Descendants(L5XName.Data)
                .FirstOrDefault(e => e.Attribute(L5XName.Format)?.Value != TagDataFormat.L5K);

            if (formattedData is null) return tag;

            var data = FormattedDataSerializer.Deserialize(formattedData);
            tag.SetData(data);
            return tag;
        }
    }
}