using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Primitives;
using L5Sharp.Types;
using L5Sharp.Utilities;

[assembly: InternalsVisibleTo("L5Sharp.Loaders.Tests")]

namespace L5Sharp.Serialization
{
    internal class TagSerializer : IL5XSerializer<Tag>
    {
        private readonly DataTypeSerializer _typeSerializer = new DataTypeSerializer();

        public XElement Serialize(Tag component)
        {
            throw new NotImplementedException();
        }

        public Tag Deserialize(XElement element)
        {
            var type = new Bool();
            if (type == null) return null;

            var tag = new Tag(element.GetName(), type, element.GetDimensions(), element.GetRadix(),
                element.GetExternalAccess(), element.GetTagType(), element.GetUsage(), element.GetDescription(),
                element.GetScope(), element.GetAliasFor(), element.GetConstant());

            var formatted = element
                .Descendants(L5XNames.Data).FirstOrDefault(x =>
                    x.HasAttributes && x.Attribute("Format") != null && x.Attribute("Format")?.Value != "L5K");

            //todo what about other formats?

            if (tag.IsValueMember && type is Predefined predefined)
                UpdateTagValue(formatted?.Elements().First(), tag, predefined);
            else
                UpdateTagMember(formatted?.Elements().First(), tag);

            //todo comments?
            //todo units?

            return tag;
        }

        private static void UpdateTagValue(XElement element, Tag tag, Predefined type)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));

            if (element.Name != L5XNames.DataValue)
                throw new InvalidOperationException($"Current element is not the expected name '{L5XNames.DataValue}");

            tag.Value = type.ParseValue(element.Attribute(L5XNames.Value)?.Value);
        }

        private static void UpdateTagMember(XElement element, ITagMember tag)
        {
            if (element.Name == L5XNames.Array || element.Name == L5XNames.Structure)
            {
                var children = element.Elements();
                foreach (var child in children)
                    UpdateTagMember(child, tag);
            }

            if (!element.Name.ToString().Contains(L5XNames.Member) &&
                !element.Name.ToString().Contains(L5XNames.Element)) return;

            var name = element.FirstAttribute.Value;
            var member = tag.GetMember(name);
            if (!(member is TagMember tagMember)) return;

            tagMember.Radix = element.GetRadix();
            tagMember.Value = element.GetValue();

            if (element.Name.ToString() != L5XNames.StructureMember &&
                element.Name.ToString() != L5XNames.ArrayMember) return;

            foreach (var child in element.Elements())
                UpdateTagMember(child, tagMember);
        }
    }
}