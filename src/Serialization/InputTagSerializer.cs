using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.L5X;

namespace L5Sharp.Serialization
{
    internal class InputTagSerializer : L5XSerializer<ITag<IDataType>>
    {
        private readonly L5XContent? _document;
        private const string DefaultSuffix = "I";
        private static readonly XName ElementName = L5XName.InputTag;
        
        private FormattedDataSerializer FormattedDataSerializer => _document is not null
            ? _document.Serializers.Get<FormattedDataSerializer>()
            : new FormattedDataSerializer(_document);

        public InputTagSerializer(L5XContent? document = null)
        {
            _document = document;
        }
        
        public override XElement Serialize(ITag<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            
            element.Add(new XAttribute(L5XName.ExternalAccess, component.ExternalAccess));
            
            element.Add(FormattedDataSerializer.Serialize(component.DataType));

            return element;
        }
        
        public override ITag<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var tagName = DetermineTagName(element);
            
            var access = element.Attribute(L5XName.ExternalAccess)?.Value?.Parse<ExternalAccess>();
            
            var data = element
                .Descendants(L5XName.Data)
                .First(e => e.Attribute(L5XName.Format)?.Value == TagDataFormat.Decorated.Name);
            
            var dataType = FormattedDataSerializer.Deserialize(data);

            var commentSerializer = new TagPropertySerializer(L5XName.Comment, tagName);
            var comments = new TagPropertyCollection<string>(element.Descendants(L5XName.Comment)
                .Select(e => commentSerializer.Deserialize(e)));

            var unitsSerializer = new TagPropertySerializer(L5XName.EngineeringUnit, tagName);
            var units = new TagPropertyCollection<string>(element.Descendants(L5XName.EngineeringUnit)
                .Select(e => unitsSerializer.Deserialize(e)));

            return new Tag<IDataType>(tagName, dataType, externalAccess: access, comments: comments, units: units);
        }
        
        private string DetermineTagName(XNode element)
        {
            var suffix = element.Ancestors(L5XName.Connection)
                .First().Attribute(L5XName.InputTagSuffix)?.Value ?? DefaultSuffix;

            var moduleName = element.Ancestors(L5XName.Module)
                .FirstOrDefault()?.Attribute(L5XName.Name)?.Value;

            var parentName = element.Ancestors(L5XName.Module)
                .FirstOrDefault()?.Attribute(L5XName.ParentModule)?.Value;

            var slot = element
                .Ancestors(L5XName.Module)
                .Descendants(L5XName.Port)
                .Where(p => bool.Parse(p.Attribute(L5XName.Upstream)?.Value!)
                            && p.Attribute(L5XName.Type)?.Value != "Ethernet"
                            && int.TryParse(p.Attribute(L5XName.Address)?.Value, out _))
                .Select(p => p.Attribute(L5XName.Address)?.Value)
                .FirstOrDefault();

            return slot is not null ? $"{parentName}:{slot}:{suffix}" : $"{moduleName}:{suffix}";
        }
    }
}