using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Serialization.Data
{
    internal class StructureSerializer : IXSerializer<IComplexType>
    {
        private static readonly XName ElementName = LogixNames.Structure;

        public XElement Serialize(IComplexType component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            
            element.AddAttribute(component, c => c.Name, nameOverride: LogixNames.DataType);
            
            var members = component.Members.Select(m => GetSerializer(m).Serialize(m));
            element.Add(members); 

            return element;
        }

        public IComplexType Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName && element.Name != LogixNames.StructureMember)
                throw new ArgumentException(
                    $"Element name '{element.Name}' invalid. Expecting '{ElementName}' or '{LogixNames.StructureMember}'");

            var name = element.Attribute(LogixNames.DataType)?.Value;
            
            var members = element.Elements().Select(e => GetSerializer(e).Deserialize(e));
            
            return new StructureType(name!, DataTypeClass.Unknown, members);
        }

        private static IXSerializer<IMember<IDataType>> GetSerializer(IMember<IDataType> member)
        {
            if (member.IsValueMember)
                return new DataValueMemberSerializer();
            
            if (member.IsStructureMember)
                return new StructureMemberSerializer();
            
            if (member.IsArrayMember)
                return new ArrayMemberSerializer();

            throw new NotSupportedException($"No data member serializer is defined for member {member.Name}");
        }
        
        private static IXSerializer<IMember<IDataType>> GetSerializer(XElement element)
        {
            if (element.Name == LogixNames.DataValueMember)
                return new DataValueMemberSerializer();
            
            if (element.Name == LogixNames.StructureMember)
                return new StructureMemberSerializer();
            
            if (element.Name == LogixNames.ArrayMember)
                return new ArrayMemberSerializer();

            throw new NotSupportedException($"No data member serializer is defined for element {element.Name}");
        }
    }
}