using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

[assembly: InternalsVisibleTo("L5Sharp.Serialization.Tests")]

namespace L5Sharp.Serialization
{
    internal class TagSerializer : IComponentSerializer<ITag>
    {
        public XElement Serialize(ITag component)
        {
            var element = new XElement(nameof(Tag));
            element.Add(new XAttribute(nameof(component.Name), component.Name));
            element.Add(new XAttribute(nameof(component.TagType), component.TagType));
            element.Add(new XAttribute(nameof(component.DataType), component.DataType));
            if (component.Dimensions.Length > 0)
                element.Add(new XAttribute(LogixNames.Attributes.Dimensions, component.Dimensions.ToString()));
            if (component.Radix != Radix.Null)
                element.Add(new XAttribute(nameof(component.Radix), component.Radix));
            element.Add(new XAttribute(nameof(component.Constant), component.Constant));
            element.Add(new XAttribute(nameof(component.ExternalAccess), component.ExternalAccess));

            /*if (!string.IsNullOrEmpty(component.AliasFor))
                element.Add(new XAttribute(nameof(component.AliasFor), component.AliasFor));*/

            if (component.Usage != TagUsage.Null)
                element.Add(new XAttribute(nameof(component.Usage), component.Usage));

            if (!string.IsNullOrEmpty(component.Description))
                element.Add(new XElement(nameof(component.Description), component.Description));

            var data = GenerateDataElement(component);
            element.Add(data);

            return element;
        }

        private static XElement GenerateDataElement(ITagMember tag)
        {
            var data = new XElement(LogixNames.Elements.Data);
            data.Add(new XAttribute("Format", "Decorated"));

            if (tag.IsValueMember)
            {
                var dataValue = new XElement(LogixNames.Elements.DataValue);
                dataValue.Add(new XAttribute(LogixNames.Attributes.DataType, tag.DataType));
                dataValue.Add(new XAttribute(LogixNames.Attributes.Radix, tag.Radix.Name));
                dataValue.Add(new XAttribute(LogixNames.Attributes.Value, tag.Value));
                data.Add(dataValue);
                return data;
            }

            if (tag.IsArrayMember)
            {
                var array = new XElement(LogixNames.Elements.Array);
                array.Add(new XAttribute(LogixNames.Attributes.DataType, tag.DataType));
                array.Add(new XAttribute(LogixNames.Attributes.Dimensions, tag.Dimensions.ToString()));
                array.Add(new XAttribute(LogixNames.Attributes.Radix, tag.Radix.Name));
                array.Add(tag.Members.Select(m => m.Serialize()));
                data.Add(array);
                return data;
            }

            if (!tag.IsStructureMember)
                throw new InvalidOperationException();

            var structure = new XElement(LogixNames.Elements.Structure);
            structure.Add(new XAttribute(LogixNames.Attributes.DataType, tag.DataType));
            structure.Add(tag.Members.Select(m => m.Serialize()));
            data.Add(structure);
            return data;
        }
    }
}