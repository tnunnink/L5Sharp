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
            var element = new XElement(nameof(Tag));
            element.Add(new XAttribute(nameof(component.Name), component.Name));
            element.Add(new XAttribute(nameof(component.TagType), component.TagType));
            element.Add(new XAttribute(nameof(component.DataType), component.DataType));
            if (component.Dimension.Length > 0)
                element.Add(new XAttribute(L5XNames.Attributes.Dimensions, component.Dimension.ToString()));
            if (component.Radix != Radix.Null)
                element.Add(new XAttribute(nameof(component.Radix), component.Radix));
            element.Add(new XAttribute(nameof(component.Constant), component.Constant));
            element.Add(new XAttribute(nameof(component.ExternalAccess), component.ExternalAccess));

            if (!string.IsNullOrEmpty(component.AliasFor))
                element.Add(new XAttribute(nameof(component.AliasFor), component.AliasFor));

            if (component.Usage != TagUsage.Null)
                element.Add(new XAttribute(nameof(component.Usage), component.Usage));

            if (!string.IsNullOrEmpty(component.Description))
                element.Add(new XElement(nameof(component.Description), component.Description));

            /*var data = GenerateDataElement();
            element.Add(data);*/

            return element;
        }

        public Tag Deserialize(XElement element)
        {
            var type = new Bool();
            if (type == null) return null;

            var tag = new Tag(element.GetName(), type, element.GetDimensions(), element.GetRadix(),
                element.GetExternalAccess(), element.GetTagType(), element.GetUsage(), element.GetDescription(),
                element.GetScope(), element.GetAliasFor(), element.GetConstant());

            var formatted = element
                .Descendants(L5XNames.Elements.Data).FirstOrDefault(x =>
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

            if (element.Name != L5XNames.Elements.DataValue)
                throw new InvalidOperationException($"Current element is not the expected name '{L5XNames.Elements.DataValue}");

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

            tagMember.Radix = element.GetRadix();
            tagMember.Value = element.GetValue();

            if (element.Name.ToString() != L5XNames.Elements.StructureMember &&
                element.Name.ToString() != L5XNames.Elements.ArrayMember) return;

            foreach (var child in element.Elements())
                UpdateTagMember(child, tagMember);
        }
        
        /*private XElement GenerateDataElement()
        {
            var data = new XElement(L5XNames.Data);
            data.Add(new XAttribute("Format", "Decorated"));

            if (IsValueMember)
            {
                var dataValue = new XElement(L5XNames.DataValue);
                dataValue.Add(new XAttribute(L5XNames.DataType, DataType));
                dataValue.Add(new XAttribute(L5XNames.Radix, Radix.Name));
                dataValue.Add(new XAttribute(L5XNames.Value, Value));
                data.Add(dataValue);
                return data;
            }

            if (IsArrayMember)
            {
                var array = new XElement(L5XNames.Array);
                array.Add(new XAttribute(L5XNames.DataType, DataType));
                array.Add(new XAttribute(L5XNames.Dimensions, Dimension.ToString()));
                array.Add(new XAttribute(L5XNames.Radix, Radix.Name));
                array.Add(_members.Values.Select(m => m.Serialize()));
                data.Add(array);
                return data;
            }

            if (!IsStructureMember) return null;

            var structure = new XElement(L5XNames.Structure);
            structure.Add(new XAttribute(L5XNames.DataType, DataType));
            structure.Add(_members.Values.Select(m => m.Serialize()));
            data.Add(structure);
            return data;
        }*/
        
        /*private XElement SerializeValueMember()
        {
            var element = new XElement(L5XNames.DataValueMember);
            element.Add(new XAttribute(L5XNames.Name, Name));
            element.Add(new XAttribute(L5XNames.DataType, DataType));
            if (Radix != null) element.Add(new XAttribute(nameof(Radix), Radix.Name));
            element.Add(new XAttribute(L5XNames.Value, Value.ToString()));
            return element;
        }

        private XElement SerializeArrayMember()
        {
            var element = new XElement(L5XNames.ArrayMember);
            element.Add(new XAttribute(L5XNames.Name, Name));
            element.Add(new XAttribute(L5XNames.DataType, DataType));
            element.Add(new XAttribute(L5XNames.Dimensions, Dimension.Length));
            element.Add(new XAttribute(L5XNames.Radix, Radix.Name));
            return element;
        }

        private XElement SerializeArrayElement()
        {
            var element = new XElement(L5XNames.Element);
            element.Add(new XAttribute(L5XNames.Index, Name));

            if (Value != null)
                element.Add(new XAttribute(L5XNames.Value, Value));

            if (IsStructureMember)
                element.Add(new XElement(L5XNames.Structure, new XAttribute(L5XNames.DataType, DataType)));

            return element;
        }

        private XElement SerializeStructureMember()
        {
            var element = new XElement(L5XNames.StructureMember);
            element.Add(new XAttribute(L5XNames.Name, Name));
            element.Add(new XAttribute(L5XNames.DataType, DataType));
            return element;
        }*/
        
        /*private Tag(XElement element, IController controller)
        {
            _name = element.Attribute(nameof(Name))?.Value;

            //we have to get reference to existing data type
            var typeName = element.Attribute(nameof(DataType))?.Value;
            _dataType = controller.GetDataType(typeName);
            if (_dataType == null) Throw.DataTypeNotFoundException(typeName, _name);
            
            _tagType = element.GetTagType() ?? TagType.Base;
            _dimensions = element.GetDimensions() ?? Dimensions.Empty;
            _radix = element.GetRadix() ?? Radix.Default(_dataType);
            _externalAccess = element.GetExternalAccess() ?? ExternalAccess.None;
            _aliasFor = element.GetAliasFor() ?? string.Empty;
            _usage = element.GetUsage() ?? TagUsage.Null;
            _description = element.GetDescription() ?? string.Empty;
            Constant = element.GetConstant();

            //if data has data value element then it is atomic and we just set the value
            var dataValue = element.Descendants(L5XNames.DataValue).SingleOrDefault();
            if (dataValue != null)
            {
                if (!(_dataType is Predefined predefined))
                    throw new InvalidOperationException();

                _value = predefined.ParseValue(dataValue.Attribute(L5XNames.Value)?.Value);
                return;
            }

            //otherwise use transforms to construct members
            var formatted = element
                .Descendants(L5XNames.Data).FirstOrDefault(x =>
                    x.HasAttributes && x.Attribute("Format") != null && x.Attribute("Format")?.Value != "L5K");

            var transform = new FormattedDataTransform();
            var members = transform.TransformMany(formatted);
            _members.AddRange(members.Select(m => TagMember.Materialize(m, controller)));
        }*/
    }
}