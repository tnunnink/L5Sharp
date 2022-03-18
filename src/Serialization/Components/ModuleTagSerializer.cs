using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.L5X;
using L5Sharp.Serialization.Data;

namespace L5Sharp.Serialization.Components
{
    internal class ModuleTagSerializer : IL5XSerializer<ITag<IDataType>>
    {
        private readonly XName _elementName;
        private readonly string _defaultSuffix;
        private readonly L5XAttribute? _suffixAttributeName;
        private readonly FormattedDataSerializer _formattedDataSerializer;
        private readonly TagPropertySerializer _commentSerializer;
        private readonly TagPropertySerializer _unitsSerializer;

        public ModuleTagSerializer(XName elementName, string defaultSuffix)
        {
            _elementName = elementName;
            _defaultSuffix = defaultSuffix;
            _suffixAttributeName = _elementName == L5XElement.InputTag.ToString() ? L5XAttribute.InputTagSuffix
                : _elementName == L5XElement.OutputTag.ToString() ? L5XAttribute.OutputTagSuffix
                : null;
            _formattedDataSerializer = new FormattedDataSerializer();
            _commentSerializer = new TagPropertySerializer(L5XElement.Comment.ToString());
            _unitsSerializer = new TagPropertySerializer(L5XElement.EngineeringUnit.ToString());
        }

        public XElement Serialize(ITag<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(_elementName);
            element.Add(new XAttribute(L5XAttribute.ExternalAccess.ToString(), component.ExternalAccess));
            element.Add(_formattedDataSerializer.Serialize(component.DataType));

            return element;
        }

        public ITag<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != _elementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var tagName = DetermineTagName(element);
            var access = element.Attribute(L5XAttribute.ExternalAccess.ToString())?.Value?.Parse<ExternalAccess>();
            var data = element
                .Descendants(L5XElement.Data.ToString())
                .First(e => e.Attribute(L5XAttribute.Format.ToString())?.Value == TagDataFormat.Decorated.Name);
            var dataType = _formattedDataSerializer.Deserialize(data);
            
            _commentSerializer.SetBaseName(tagName);
            var comments = new TagPropertyCollection<string>(element.Descendants(L5XElement.Comment.ToString())
                .Select(e => _commentSerializer.Deserialize(e)));
            
            _unitsSerializer.SetBaseName(tagName);
            var units = new TagPropertyCollection<string>(element.Descendants(L5XElement.EngineeringUnit.ToString())
                .Select(e => _unitsSerializer.Deserialize(e)));

            return new Tag<IDataType>(tagName, dataType, externalAccess: access, comments: comments, units: units);
        }

        private string DetermineTagName(XNode element)
        {
            var suffix = _suffixAttributeName is not null
                ? element.Ancestors(L5XElement.Connection.ToString()).First().Attribute(_suffixAttributeName.ToString())
                    ?.Value ?? _defaultSuffix
                : _defaultSuffix;

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