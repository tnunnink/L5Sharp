using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Serialization
{
    internal class TagSerializer : IXSerializer<ITag<IDataType>>
    {
        private static readonly XName ElementName = LogixNames.Tag;
        
        public XElement Serialize(ITag<IDataType> component)
        {
            throw new NotImplementedException();
        }

        public ITag<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var name = element.GetComponentName();
            var dataType = element.GetDataType();
            var dimensions = element.GetAttribute<Tag<IDataType>, Dimensions>(t => t.Dimensions);
            var radix = element.GetAttribute<Tag<IDataType>, Radix>(t => t.Radix);
            var access = element.GetAttribute<Tag<IDataType>, ExternalAccess>(t => t.ExternalAccess);
            var usage = element.GetAttribute<Tag<IDataType>, TagUsage>(m => m.Usage);
            var constant = element.GetAttribute<Tag<IDataType>, bool>(m => m.Constant);
            var description = element.GetAttribute<Tag<IDataType>, string>(m => m.Description);
            
            //todo with how this is now we would need to determine if data type is an array type.
            //todo also we would need to deserialize the tag data and apply it.

            return new Tag<IDataType>(name, dataType, radix, access, description, usage, constant);
        }
    }
}