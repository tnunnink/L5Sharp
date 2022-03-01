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
    internal class TagSerializer : IL5XSerializer<ITag<IDataType>>
    {
        private static readonly XName ElementName = L5XElement.Tag.ToXName();

        public XElement Serialize(ITag<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.AddAttribute(component, c => c.Name);
            element.AddElement(component, c => c.Description);
            element.AddAttribute(component, c => c.TagType);
            element.AddAttribute(component, c => c.DataType.Name, nameOverride: nameof(component.DataType));
            element.AddAttribute(component, c => c.Dimensions, t => !t.Dimensions.AreEmpty);
            element.AddAttribute(component, c => c.Radix, t => t.IsValueMember);
            element.AddAttribute(component, c => c.Constant);
            element.AddAttribute(component, c => c.ExternalAccess);

            if (component.Comments.Any())
            {
                var commentSerializer = new CommentSerializer();
                element.Add(commentSerializer.Serialize(component.Comments));    
            }
            

            //todo same with engineering units and perhaps ranges min/max

            var formattedDataSerializer = new FormattedDataSerializer();
            var data = formattedDataSerializer.Serialize(component.DataType);
            element.Add(data);

            return element;
        }

        public ITag<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.ComponentName();
            var description = element.ComponentDescription();
            var dataType = element.DataType();
            var dimensions = element.GetAttribute<Tag<IDataType>, Dimensions>(t => t.Dimensions);
            var radix = element.GetAttribute<Tag<IDataType>, Radix>(t => t.Radix);
            var access = element.GetAttribute<Tag<IDataType>, ExternalAccess>(t => t.ExternalAccess);
            var usage = element.GetAttribute<Tag<IDataType>, TagUsage>(m => m.Usage);
            var constant = element.GetAttribute<Tag<IDataType>, bool>(m => m.Constant);

            var commentSerializer = new CommentSerializer();
            var commentsElement = element.Elements(L5XElement.Comments.ToXName()).FirstOrDefault();
            var comments = commentsElement is not null ? commentSerializer.Deserialize(commentsElement) : null; 
                
            var type = dimensions is not null && dimensions.AreEmpty
                ? new ArrayType<IDataType>(dimensions, dataType, radix, access, description)
                : dataType;

            var tag = new Tag<IDataType>(name, type, radix, access, description, usage, constant, comments);

            var formattedData = element.Descendants(L5XElement.Data.ToXName())
                .FirstOrDefault(e => e.Attribute(L5XAttribute.Format.ToXName())?.Value != TagDataFormat.L5K);

            if (formattedData is null) return tag;

            var dataSerializer = new FormattedDataSerializer();
            var data = dataSerializer.Deserialize(formattedData);
            tag.SetData(data);
            return tag;
        }
    }
}