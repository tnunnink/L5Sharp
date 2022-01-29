using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Helpers;

namespace L5Sharp.Serialization
{
    internal class CommentSerializer : IXSerializer<Comments>
    {
        private readonly TagName _tagName;
        private static readonly XName ElementName = LogixNames.Comments;

        public CommentSerializer(TagName tagName)
        {
            _tagName = tagName;
        }
        
        public XElement Serialize(Comments component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            var comments = component.Select(c =>
            {
                var comment = new XElement(LogixNames.Comment);
                comment.Add(new XAttribute(nameof(c.Key.Operand), c.Key.Operand));
                comment.Add(new XCData(c.Value));
                return comment;
            });
            
            element.Add(comments);

            return element;
        }

        public Comments Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var comments = element.Elements().Select(e =>
            {
                var tagName = TagName.Combine(_tagName, e.Attribute(nameof(_tagName.Operand))?.Value!);
                var comment = e.Value;
                return new KeyValuePair<TagName, string>(tagName, comment);
            });

            return new Comments(comments);
        }
    }
}