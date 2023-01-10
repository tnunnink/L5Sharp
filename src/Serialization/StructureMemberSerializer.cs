using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    internal class StructureMemberSerializer : L5XSerializer<IMember<IDataType>>
    {
        private readonly LogixInfo? _document;
        private static readonly XName ElementName = L5XName.StructureMember;

        private DataValueMemberSerializer DataValueMemberSerializer => _document is not null
            ? _document.Serializers.Get<DataValueMemberSerializer>()
            : new DataValueMemberSerializer();

        private ArrayMemberSerializer ArrayMemberSerializer => _document is not null
            ? _document.Serializers.Get<ArrayMemberSerializer>()
            : new ArrayMemberSerializer(_document);
        
        private StringMemberSerializer StringMemberSerializer => _document is not null
            ? _document.Serializers.Get<StringMemberSerializer>()
            : new StringMemberSerializer();

        public StructureMemberSerializer(LogixInfo? document = null)
        {
            _document = document;
        }

        public override XElement Serialize(IMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));
            
            if (component.DataType is IStringType)
                return StringMemberSerializer.Serialize(component);

            var element = new XElement(ElementName);

            element.Add(new XAttribute(L5XName.Name, component.Name));
            element.Add(new XAttribute(L5XName.DataType, component.DataType));

            var members = component.DataType.GetMembers()
                .Select(m => m.MemberType == MemberType.ValueMember ? DataValueMemberSerializer.Serialize(m)
                    : m.MemberType == MemberType.ArrayMember ? ArrayMemberSerializer.Serialize(m)
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
            
            if (element.Elements().Any(e => string.Equals(e.Attribute(L5XName.DataType)?.Value,
                    typeName, StringComparison.OrdinalIgnoreCase)))
            {
                return StringMemberSerializer.Deserialize(element);
            }

            var members = element.Elements().Select(e => GetSerializer(e).Deserialize(e)).ToList();

            var dataType = new StructureType(typeName, members);

            return new Member<IDataType>(name, dataType);
        }

        private IL5XSerializer<IMember<IDataType>> GetSerializer(XElement element)
        {
            return element.Name == L5XName.DataValueMember ? DataValueMemberSerializer
                : element.Name == L5XName.ArrayMember ? ArrayMemberSerializer
                : element.Name == L5XName.StructureMember ? this
                : throw new ArgumentException();
        }
    }
}