using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization.Data
{
    internal class StructureMemberSerializer : IL5XSerializer<IMember<IDataType>>
    {
        private static readonly XName ElementName = L5XElement.StructureMember.ToXName();

        public XElement Serialize(IMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.AddAttribute(component, m => m.Name);
            element.AddAttribute(component, m => m.DataType);

            var members = component.DataType.GetMembers().Select(m =>
            {
                if (m.IsValueMember)
                {
                    var dataValueMemberSerializer = new DataValueMemberSerializer();
                    return dataValueMemberSerializer.Serialize(m);
                }

                if (m.IsArrayMember)
                {
                    var arrayMemberSerializer = new ArrayMemberSerializer();
                    return arrayMemberSerializer.Serialize(m);
                }

                var structureMemberSerializer = new StructureMemberSerializer();
                return structureMemberSerializer.Serialize(m);
            });
            
            element.Add(members);

            return element;
        }

        public IMember<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.GetComponentName();

            var typeName = element.GetDataTypeName();
            var members = element.Elements().Select(e =>
            {
                var l5XElement = Enum.Parse<L5XElement>(e.Name.ToString());
                return ((IL5XSerializer<IMember<IDataType>>)l5XElement.GetSerializer()).Deserialize(e);
            });

            var dataType = new StructureType(typeName, DataTypeClass.Unknown, members);

            return new Member<IDataType>(name, dataType);
        }
    }
}