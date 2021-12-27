using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Components;
using L5Sharp.Extensions;

namespace L5Sharp.Serialization
{
    internal class StructureMemberSerializer : IXSerializer<IMember<IDataType>>
    {
        private static readonly XName ElementName = LogixNames.StructureMember;

        public XElement Serialize(IMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));
            
            if (!(component.DataType is IComplexType complex))
                throw new InvalidOperationException("StructureMembers must have a complex data type.");
            
            var element = new XElement(ElementName);
            
            element.Add(component.ToAttribute(m => m.Name));
            element.Add(component.ToAttribute(m => m.DataType));
            
            var elements = complex.Members.Select(m => m.Serialize());
            element.Add(elements);

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

            return Member.Create(name, dataType);
        }
    }
}