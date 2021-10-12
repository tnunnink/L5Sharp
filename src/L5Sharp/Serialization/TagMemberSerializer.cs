using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Utilities;

[assembly: InternalsVisibleTo("L5Sharp.Serialization.Tests")]

namespace L5Sharp.Serialization
{
    internal class TagMemberSerializer : IComponentSerializer<TagMember>
    {
        private readonly ITagMember _parent;

        public TagMemberSerializer(ITagMember parent)
        {
            _parent = parent;
        }
        
        public XElement Serialize(TagMember component)
        {
            if (component.IsValueMember)
                return GenerateDataValueMember(component);
            
            var member = component.IsArrayMember
                ? GenerateArrayMember(component)
                : component.IsArrayElement
                    ? GenerateArrayElement(component)
                    : component.IsStructureMember
                        ? GenerateStructureMember(component)
                        : null;

            if (member == null)
                throw new InvalidOperationException(
                    $"Could not generate member element for tag member '{component.Name}'");
            
            member.Add(component.Members.Select(m => Serialize((TagMember)m)));

            return member;
        }

        public TagMember Deserialize(XElement element)
        {
            throw new NotImplementedException();
        }

        private static XElement GenerateDataValueMember(ITagMember member)
        {
            var element = new XElement(LogixNames.Elements.DataValueMember);
            element.Add(new XAttribute(LogixNames.Attributes.Name, member.DataType));
            element.Add(new XAttribute(LogixNames.Attributes.DataType, member.DataType));
            if (member.Radix != Radix.Null) element.Add(new XAttribute(LogixNames.Attributes.Radix, member.Radix.Name));
            element.Add(new XAttribute(LogixNames.Attributes.Value, member.Value));
            return element;
        }

        private static XElement GenerateArrayMember(ITagMember member)
        {
            var element = new XElement(LogixNames.Elements.ArrayMember);
            element.Add(new XAttribute(LogixNames.Attributes.Name, member.Name));
            element.Add(new XAttribute(LogixNames.Attributes.DataType, member.DataType));
            element.Add(new XAttribute(LogixNames.Attributes.Dimensions, member.Dimensions.ToString()));
            element.Add(new XAttribute(LogixNames.Attributes.Radix, member.Radix));
            return element;
        }

        private static XElement GenerateArrayElement(ITagMember member)
        {
            var element = new XElement(LogixNames.Elements.Element);
            element.Add(new XAttribute(LogixNames.Elements.Index, member.Name));
            if (!member.IsStructureMember)
                element.Add(new XAttribute(LogixNames.Attributes.Value, member.Value));
            return element;
        }

        private static XElement GenerateStructureMember(ITagMember member)
        {
            var element = new XElement(LogixNames.Elements.Structure);
            element.Add(new XAttribute(LogixNames.Attributes.Name, member.Name));
            element.Add(new XAttribute(LogixNames.Attributes.DataType, member.DataType));
            return element;
        }
    }
}