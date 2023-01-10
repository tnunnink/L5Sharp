using System;
using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    internal class TagPropertySerializer : L5XSerializer<KeyValuePair<TagName, string>>
    {
        private readonly string _element;
        private readonly string _baseTagName;

        public TagPropertySerializer(string element, string baseTagName)
        {
            _element = element;
            _baseTagName = baseTagName;
        }

        public override XElement Serialize(KeyValuePair<TagName, string> component)
        {
            var element = new XElement(_element);
            
            var (key, value) = component;
            element.Add(new XAttribute(L5XName.Operand, key.Operand));
            element.Add(new XCData(value));
            
            return element;
        }

        public override KeyValuePair<TagName, string> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != _element)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var tagName = TagName.Combine(_baseTagName, element.Attribute(L5XName.Operand)?.Value!);
            var value = element.Value;
            
            return new KeyValuePair<TagName, string>(tagName, value);
        }
    }
}