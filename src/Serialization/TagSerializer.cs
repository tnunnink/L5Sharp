using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Helpers;

namespace L5Sharp.Serialization
{
    internal class TagSerializer : IXSerializer<ITag<IDataType>>
    {
        private readonly LogixContext _context;
        private static readonly XName ElementName = LogixNames.Tag;

        public TagSerializer(LogixContext context)
        {
            _context = context;
        }
        
        public XElement Serialize(ITag<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            
            element.AddAttribute(component, c => c.Name);
            element.AddElement(component, c => c.Description);
            element.AddAttribute(component, c => c.TagType);
            element.AddAttribute(component, c => c.DataType);
            element.AddAttribute(component, c => c.Dimensions);
            element.AddAttribute(component, c => c.Radix, t => t.IsValueMember);
            element.AddAttribute(component, c => c.Constant);
            element.AddAttribute(component, c => c.ExternalAccess);
            
            //todo comments.
            //todo eng unit?

            var decoratedSerializer = new DecoratedDataSerializer();
            var data = decoratedSerializer.Serialize(component.DataType);
            element.Add(data);

            return element;
        }

        public ITag<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var name = element.GetComponentName();
            var dataType = _context.Types.GetDataType(element.GetDataTypeName());
            var dimensions = element.GetAttribute<Tag<IDataType>, Dimensions>(t => t.Dimensions);
            var radix = element.GetAttribute<Tag<IDataType>, Radix>(t => t.Radix);
            var access = element.GetAttribute<Tag<IDataType>, ExternalAccess>(t => t.ExternalAccess);
            var usage = element.GetAttribute<Tag<IDataType>, TagUsage>(m => m.Usage);
            var constant = element.GetAttribute<Tag<IDataType>, bool>(m => m.Constant);
            var description = element.GetAttribute<Tag<IDataType>, string>(m => m.Description);
            
            var type = dimensions is not null && dimensions.AreEmpty 
                ? new ArrayType<IDataType>(dimensions, dataType, radix, access, description)
                : dataType;

            var tag = new Tag<IDataType>(name, type, radix, access, description, usage, constant);

            //todo set tag data.

            return tag;
        }
    }
}