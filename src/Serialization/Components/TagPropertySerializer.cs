using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization.Components
{
    internal class TagPropertySerializer : IL5XSerializer<KeyValuePair<TagName, string>>
    {
        private readonly XName _elementName;
        private string _baseTagName;

        public TagPropertySerializer(XName elementName)
        {
            _elementName = elementName;
            _baseTagName = string.Empty;
        }

        public void SetBaseName(string baseTagName)
        {
            _baseTagName = baseTagName;
        }

        public XElement Serialize(KeyValuePair<TagName, string> component)
        {
            var element = new XElement(_elementName);
            
            var (key, value) = component;
            element.Add(new XAttribute(L5XAttribute.Operand.ToString(), key.Operand));
            element.Add(new XCData(value));
            
            return element;
        }

        public KeyValuePair<TagName, string> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != _elementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var tagName = TagName.Combine(_baseTagName, element.Attribute(L5XAttribute.Operand.ToString())?.Value!);
            var value = element.Value;
            
            return new KeyValuePair<TagName, string>(tagName, value);
        }
    }
}