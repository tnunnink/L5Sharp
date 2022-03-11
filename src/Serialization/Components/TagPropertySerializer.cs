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

        public TagPropertySerializer(XName elementName)
        {
            _elementName = elementName;
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
            
            var baseName = element.Ancestors(L5XElement.Tag.ToString()).FirstOrDefault()?.ComponentName();

            var tagName = TagName.Combine(baseName!, element.Attribute(L5XAttribute.Operand.ToString())?.Value!);
            var value = element.Value;
            
            return new KeyValuePair<TagName, string>(tagName, value);
        }
    }
}