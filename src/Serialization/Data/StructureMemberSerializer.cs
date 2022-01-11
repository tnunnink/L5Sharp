using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Core;
using L5Sharp.Extensions;

namespace L5Sharp.Serialization.Data
{
    internal class StructureMemberSerializer : IXSerializer<IMember<IDataType>>
    {
        private static readonly XName ElementName = LogixNames.StructureMember;

        public XElement Serialize(IMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            if (component.DataType is not IComplexType complex)
                throw new InvalidOperationException("StructureMembers must have a complex data type.");

            var element = new XElement(ElementName);

            element.AddAttribute(component, m => m.Name);
            element.AddAttribute(component, m => m.DataType);

            var members = complex.Members.Select(m => GetSerializer(m).Serialize(m));
            element.Add(members); 

            return element;
        }

        public IMember<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException(
                    $"Expecting element with name {LogixNames.DataValueMember} but got {element.Name}");

            var name = element.GetComponentName();
            
            var serializer = new StructureSerializer();
            var dataType = serializer.Deserialize(element);

            return new Member<IDataType>(name, dataType);
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
        
    }
}