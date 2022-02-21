using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Helpers;

namespace L5Sharp.Serialization.Data
{
    internal class StructureSerializer : IL5XSerializer<IComplexType>
    {
        private static readonly XName ElementName = L5XElement.Structure.ToXName();

        public XElement Serialize(IComplexType component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            
            element.AddAttribute(component, c => c.Name, nameOverride: L5XElement.DataType.ToString());
            
            var members = component.Members.Select(m => GetSerializer(m).Serialize(m));
            element.Add(members); 

            return element;
        }

        public IComplexType Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName && element.Name != L5XElement.StructureMember.ToXName())
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.Attribute(L5XElement.DataType.ToXName())?.Value;
            
            var members = element.Elements().Select(e => GetSerializer(e).Deserialize(e));
            
            return new StructureType(name!, DataTypeClass.Unknown, members);
        }

        private static IL5XSerializer<IMember<IDataType>> GetSerializer(IMember<IDataType> member)
        {
            if (member.IsValueMember)
                return new DataValueMemberSerializer();
            
            if (member.IsStructureMember)
                return new StructureMemberSerializer();
            
            if (member.IsArrayMember)
                return new ArrayMemberSerializer();

            throw new NotSupportedException($"No data member serializer is defined for member {member.Name}");
        }
        
        private static IL5XSerializer<IMember<IDataType>> GetSerializer(XElement element)
        {
            if (element.Name == L5XElement.DataValueMember.ToXName())
                return new DataValueMemberSerializer();
            
            if (element.Name == L5XElement.StructureMember.ToXName())
                return new StructureMemberSerializer();
            
            if (element.Name == L5XElement.ArrayMember.ToXName())
                return new ArrayMemberSerializer();

            throw new NotSupportedException($"No data member serializer is defined for element {element.Name}");
        }
    }
}