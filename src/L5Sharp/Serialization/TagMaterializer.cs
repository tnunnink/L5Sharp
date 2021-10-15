using System.Xml.Linq;
using L5Sharp.Abstractions;

namespace L5Sharp.Serialization
{
    public class TagMaterializer : IComponentMaterializer<ITag>
    {
        public ITag Materialize(XElement element)
        {
            /*var dataType = element.GetDataType();
            var tagType = element.GetTagType();

            var tag = tagType.Create(element);

            var formatted = element
                .Descendants(LogixNames.Elements.Data).FirstOrDefault(x =>
                    x.HasAttributes && x.Attribute("Format") != null && x.Attribute("Format")?.Value != "L5K");

            //todo what about other formats?

            if (tag.IsValueMember && dataType is Predefined predefined)
                UpdateTagValue(formatted?.Elements().First(), tag, predefined);
            else
                UpdateTagMember(formatted?.Elements().First(), tag);

            //todo comments?
            //todo units?

            return tag;*/
            return null;
        }
        
        /*
        private static void UpdateTagValue(XElement element, ITagMember tag, Predefined type)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));

            if (element.Name != LogixNames.Elements.DataValue)
                throw new InvalidOperationException(
                    $"Current element is not the expected name '{LogixNames.Elements.DataValue}");

            tag.Value = type.ParseValue(element.Attribute(LogixNames.Attributes.Value)?.Value);
        }

        private static void UpdateTagMember(XElement element, ITagMember tag)
        {
            if (element.Name == LogixNames.Elements.Array || element.Name == LogixNames.Elements.Structure)
            {
                var children = element.Elements();
                foreach (var child in children)
                    UpdateTagMember(child, tag);
            }

            if (!element.Name.ToString().Contains(LogixNames.Components.Member) &&
                !element.Name.ToString().Contains(LogixNames.Elements.Element)) return;

            var name = element.FirstAttribute.Value;
            var member = tag.GetMember(name);
            if (!(member is TagMember tagMember)) return;

            if (element.GetRadix() != null)
                tagMember.Radix = element.GetRadix();
            
            if (element.GetValue() != null)
                tagMember.Value = element.GetValue();

            if (element.Name.ToString() != LogixNames.Elements.StructureMember &&
                element.Name.ToString() != LogixNames.Elements.ArrayMember) return;

            foreach (var child in element.Elements())
                UpdateTagMember(child, tagMember);
        }*/
    }
}