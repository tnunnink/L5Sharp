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
                element.Add(new XAttribute(L5XNames.Attributes.Dimensions, component.Dimensions.ToString()));
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

        public ITag Deserialize(XElement element)
        {
            var dataType = element.GetDataType();
            var tagType = element.GetTagType();

            var tag = tagType.Create(element);

            var formatted = element
                .Descendants(L5XNames.Elements.Data).FirstOrDefault(x =>
                    x.HasAttributes && x.Attribute("Format") != null && x.Attribute("Format")?.Value != "L5K");

            //todo what about other formats?

            if (tag.IsValueMember && dataType is Predefined predefined)
                UpdateTagValue(formatted?.Elements().First(), tag, predefined);
            else
                UpdateTagMember(formatted?.Elements().First(), tag);

            //todo comments?
            //todo units?

            return tag;
        }

        private static void UpdateTagValue(XElement element, ITagMember tag, Predefined type)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));

            if (element.Name != L5XNames.Elements.DataValue)
                throw new InvalidOperationException(
                    $"Current element is not the expected name '{L5XNames.Elements.DataValue}");

            tag.Value = type.ParseValue(element.Attribute(L5XNames.Attributes.Value)?.Value);
        }

        private static void UpdateTagMember(XElement element, ITagMember tag)
        {
            if (element.Name == L5XNames.Elements.Array || element.Name == L5XNames.Elements.Structure)
            {
                var children = element.Elements();
                foreach (var child in children)
                    UpdateTagMember(child, tag);
            }

            if (!element.Name.ToString().Contains(L5XNames.Components.Member) &&
                !element.Name.ToString().Contains(L5XNames.Elements.Element)) return;

            var name = element.FirstAttribute.Value;
            var member = tag.GetMember(name);
            if (!(member is TagMember tagMember)) return;

            if (element.GetRadix() != null)
                tagMember.Radix = element.GetRadix();
            
            if (element.GetValue() != null)
                tagMember.Value = element.GetValue();

            if (element.Name.ToString() != L5XNames.Elements.StructureMember &&
                element.Name.ToString() != L5XNames.Elements.ArrayMember) return;

            foreach (var child in element.Elements())
                UpdateTagMember(child, tagMember);
        }

        private static XElement GenerateDataElement(ITagMember tag)
        {
            var data = new XElement(L5XNames.Elements.Data);
            data.Add(new XAttribute("Format", "Decorated"));

            if (tag.IsValueMember)
            {
                var dataValue = new XElement(L5XNames.Elements.DataValue);
                dataValue.Add(new XAttribute(L5XNames.Attributes.DataType, tag.DataType));
                dataValue.Add(new XAttribute(L5XNames.Attributes.Radix, tag.Radix.Name));
                dataValue.Add(new XAttribute(L5XNames.Attributes.Value, tag.Value));
                data.Add(dataValue);
                return data;
            }

            if (tag.IsArrayMember)
            {
                var array = new XElement(L5XNames.Elements.Array);
                array.Add(new XAttribute(L5XNames.Attributes.DataType, tag.DataType));
                array.Add(new XAttribute(L5XNames.Attributes.Dimensions, tag.Dimensions.ToString()));
                array.Add(new XAttribute(L5XNames.Attributes.Radix, tag.Radix.Name));
                array.Add(tag.Members.Select(m => m.Serialize()));
                data.Add(array);
                return data;
            }

            if (!tag.IsStructureMember)
                throw new InvalidOperationException();

            var structure = new XElement(L5XNames.Elements.Structure);
            structure.Add(new XAttribute(L5XNames.Attributes.DataType, tag.DataType));
            structure.Add(tag.Members.Select(m => m.Serialize()));
            data.Add(structure);
            return data;
        }
    }
}