using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;

[assembly: InternalsVisibleTo("L5Sharp.Serialization.Tests")]

namespace L5Sharp.Serialization
{
    internal class TagSerializer : IComponentSerializer<ITag<IDataType>>
    {
        public const string Data = nameof(Data);
        public const string DataValue = nameof(DataValue);
        public const string Array = nameof(Array);
        public const string Index = nameof(Index);
        public const string Element = nameof(Element);
        public const string Structure = nameof(Structure);
        public const string ArrayMember = nameof(ArrayMember);
        public const string DataValueMember = nameof(DataValueMember);
        public const string StructureMember = nameof(StructureMember);
        
        public XElement Serialize(ITag<IDataType> component)
        {
            var element = new XElement(nameof(Tag));
            element.Add(component.ToAttribute(t => t.Name));
            element.Add(component.ToAttribute(t => t.TagType));
            element.Add(component.ToAttribute(t => t.DataType));
            
            if (component.Dimensions.Length > 0)
                element.Add(component.ToAttribute(t => t.Dimensions));
            
            if (component.Radix != Radix.Null)
                element.Add(component.ToAttribute(t => t.Radix));
            
            element.Add(component.ToAttribute(t => t.Constant));
            element.Add(component.ToAttribute(t => t.ExternalAccess));

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

        private static XElement GenerateDataElement(ITagMember<IDataType> tag)
        {
            var data = new XElement(Data);
            data.Add(new XAttribute("Format", "Decorated"));

            if (tag.IsValueMember)
            {
                var dataValue = new XElement(DataValue);
                dataValue.Add(tag.ToAttribute(t => t.DataType));
                dataValue.Add(tag.ToAttribute(t => t.Radix));
                dataValue.Add(tag.ToAttribute(t => t.Value));
                data.Add(dataValue);
                return data;
            }

            if (tag.IsArrayMember)
            {
                var array = new XElement(Array);
                array.Add(tag.ToAttribute(t => t.DataType));
                array.Add(tag.ToAttribute(t => t.Dimensions));
                array.Add(tag.ToAttribute(t => t.Radix));
                array.Add(tag.Members.Select(m => m.Serialize()));
                data.Add(array);
                return data;
            }

            if (!tag.IsStructureMember)
                throw new InvalidOperationException();

            var structure = new XElement(Structure);
            structure.Add(tag.ToAttribute(t => t.DataType));
            structure.Add(tag.Members.Select(m => m.Serialize()));
            data.Add(structure);
            return data;
        }
    }
}