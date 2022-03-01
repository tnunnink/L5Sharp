using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;

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

            var members = component.Members.Select(m =>
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

        public IComplexType Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.DataTypeName();
            var members = element.Elements().Select(e =>
            {
                var l5XElement = Enum.Parse<L5XElement>(e.Name.ToString());
                return ((IL5XSerializer<IMember<IDataType>>)l5XElement.GetSerializer()).Deserialize(e);
            });

            return new StructureType(name, DataTypeClass.Unknown, members);
        }
    }
}