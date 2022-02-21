using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Helpers;

namespace L5Sharp.Serialization.Components
{
    internal class CommentSerializer : IL5XSerializer<Comments>
    {
        private static readonly XName ElementName = L5XElement.Comments.ToXName();

        public XElement Serialize(Comments component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            var comments = component.Select(c =>
            {
                var comment = new XElement(L5XElement.Comment.ToXName());
                comment.Add(new XAttribute(L5XAttribute.Operand.ToXName(), c.Key.Operand));
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
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var baseName = element.Ancestors(L5XElement.Tag.ToXName()).FirstOrDefault()?.GetComponentName();

            if (baseName is null)
                throw new ArgumentException("The provided comments do not have a base tag name");

            var comments = element.Elements().Select(e =>
            {
                var tagName = TagName.Combine(baseName, e.Attribute(L5XAttribute.Operand.ToXName())?.Value!);
                var comment = e.Value;
                return new KeyValuePair<TagName, string>(tagName, comment);
            });

            return new Comments(comments);
        }
    }
}