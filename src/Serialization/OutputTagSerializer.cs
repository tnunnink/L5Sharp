using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.L5X;

namespace L5Sharp.Serialization
{
    internal class OutputTagSerializer : L5XSerializer<ITag<IDataType>>
    {
        private readonly L5XContent? _document;
        private const string DefaultSuffix = "O";
        private static readonly XName ElementName = L5XElement.OutputTag.ToString();
        
        private FormattedDataSerializer FormattedDataSerializer => _document is not null
            ? _document.Serializers.Get<FormattedDataSerializer>()
            : new FormattedDataSerializer(_document);

        public OutputTagSerializer(L5XContent? document = null)
        {
            _document = document;
        }
        
        public override XElement Serialize(ITag<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            
            element.Add(new XAttribute(L5XAttribute.ExternalAccess.ToString(), component.ExternalAccess));
            
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
            
            var access = element.Attribute(L5XAttribute.ExternalAccess.ToString())?.Value?.Parse<ExternalAccess>();
            
            var data = element
                .Descendants(L5XElement.Data.ToString())
                .First(e => e.Attribute(L5XAttribute.Format.ToString())?.Value == TagDataFormat.Decorated.Name);
            
            var dataType = FormattedDataSerializer.Deserialize(data);

            var commentSerializer = new TagPropertySerializer(L5XElement.Comment, tagName);
            var comments = new TagPropertyCollection<string>(element.Descendants(L5XElement.Comment.ToString())
                .Select(e => commentSerializer.Deserialize(e)));

            var unitsSerializer = new TagPropertySerializer(L5XElement.EngineeringUnit, tagName);
            var units = new TagPropertyCollection<string>(element.Descendants(L5XElement.EngineeringUnit.ToString())
                .Select(e => unitsSerializer.Deserialize(e)));

            return new Tag<IDataType>(tagName, dataType, externalAccess: access, comments: comments, units: units);
        }
        
        private string DetermineTagName(XNode element)
        {
            var suffix = element.Ancestors(L5XElement.Connection.ToString())
                .First().Attribute(L5XAttribute.OutputTagSuffix.ToString())?.Value ?? DefaultSuffix;

            var moduleName = element.Ancestors(L5XElement.Module.ToString())
                .FirstOrDefault()?.Attribute(L5XAttribute.Name.ToString())?.Value;

            var parentName = element.Ancestors(L5XElement.Module.ToString())
                .FirstOrDefault()?.Attribute(L5XAttribute.ParentModule.ToString())?.Value;

            var slot = element
                .Ancestors(L5XElement.Module.ToString())
                .Descendants(L5XElement.Port.ToString())
                .Where(p => bool.Parse(p.Attribute(L5XAttribute.Upstream.ToString())?.Value!)
                            && p.Attribute(L5XAttribute.Type.ToString())?.Value != "Ethernet"
                            && int.TryParse(p.Attribute(L5XAttribute.Address.ToString())?.Value, out _))
                .Select(p => p.Attribute(L5XAttribute.Address.ToString())?.Value)
                .FirstOrDefault();

            return slot is not null ? $"{parentName}:{slot}:{suffix}" : $"{moduleName}:{suffix}";
        }
    }
}