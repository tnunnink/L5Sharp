using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization.Data
{
    internal class StructureMemberSerializer : L5XSerializer<IMember<IDataType>>
    {
        private readonly L5XDocument? _document;
        private static readonly XName ElementName = L5XElement.StructureMember.ToString();

        private DataValueMemberSerializer DataValueMemberSerializer => _document is not null
            ? _document.Serializers.Get<DataValueMemberSerializer>()
            : new DataValueMemberSerializer();
        
        private ArrayMemberSerializer ArrayMemberSerializer => _document is not null
            ? _document.Serializers.Get<ArrayMemberSerializer>()
            : new ArrayMemberSerializer(_document);
        
        //todo non standard..do we care?
        private StringStructureSerializer StringStructureSerializer => new(ElementName);

        public StructureMemberSerializer(L5XDocument? document = null)
        {
            _document = document;
        }

        public override XElement Serialize(IMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            //String types are treated differently than other types.
            if (component.DataType is IStringType stringType)
                return StringStructureSerializer.Serialize(stringType);

            var element = new XElement(ElementName);

            element.Add(new XAttribute(L5XAttribute.Name.ToString(), component.Name));
            element.Add(new XAttribute(L5XAttribute.DataType.ToString(), component.DataType));

            var members = component.DataType.GetMembers()
                .Select(m => m.IsValueMember ? DataValueMemberSerializer.Serialize(m)
                    : m.IsArrayMember ? ArrayMemberSerializer.Serialize(m)
                    : Serialize(m));

            element.Add(members);

            return element;
        }

        public override IMember<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.ComponentName();
            var typeName = element.DataTypeName();

            //The only way to know if this is a string type that needs special treatment is if there is a member with
            //the same data type name (which otherwise should be impossible).
            if (element.Elements().Any(e => string.Equals(e.Attribute(L5XAttribute.DataType.ToString())?.Value,
                    typeName, StringComparison.OrdinalIgnoreCase)))
            {
                return new Member<IDataType>(name, StringStructureSerializer.Deserialize(element));
            }

            var members = element.Elements().Select(e => GetSerializer(e).Deserialize(e)).ToList();

            var dataType = new StructureType(typeName, members);

            return new Member<IDataType>(name, dataType);
        }

        private IL5XSerializer<IMember<IDataType>> GetSerializer(XElement element)
        {
            return element.Name == L5XElement.DataValueMember.ToString() ? DataValueMemberSerializer
                : element.Name == L5XElement.ArrayMember.ToString() ? ArrayMemberSerializer
                : element.Name == L5XElement.StructureMember.ToString() ? this
                : throw new ArgumentException();
        }
    }
}