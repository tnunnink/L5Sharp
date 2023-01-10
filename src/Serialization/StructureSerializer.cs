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
    internal class StructureSerializer : L5XSerializer<IComplexType>
    {
        private readonly LogixInfo? _document;
        private static readonly XName ElementName = L5XName.Structure;

        private DataValueMemberSerializer DataValueMemberSerializer => _document is not null
            ? _document.Serializers.Get<DataValueMemberSerializer>()
            : new DataValueMemberSerializer();

        private ArrayMemberSerializer ArrayMemberSerializer => _document is not null
            ? _document.Serializers.Get<ArrayMemberSerializer>()
            : new ArrayMemberSerializer(_document);

        private StructureMemberSerializer StructureMemberSerializer => _document is not null
            ? _document.Serializers.Get<StructureMemberSerializer>()
            : new StructureMemberSerializer(_document);
        
        private StringStructureSerializer StringStructureSerializer => _document is not null
            ? _document.Serializers.Get<StringStructureSerializer>()
            : new StringStructureSerializer();
        
        private StringMemberSerializer StringMemberSerializer => _document is not null
            ? _document.Serializers.Get<StringMemberSerializer>()
            : new StringMemberSerializer();

        public StructureSerializer(LogixInfo? document = null)
        {
            _document = document;
        }

        public override XElement Serialize(IComplexType component)
        {
            switch (component)
            {
                case null:
                    throw new ArgumentNullException(nameof(component));
                case IStringType stringType:
                    return StringStructureSerializer.Serialize(stringType);
            }

            var element = new XElement(ElementName);
            element.Add(new XAttribute(L5XName.DataType, component.Name));

            var members = component.Members.Select(m =>
                m.MemberType == MemberType.ValueMember ? DataValueMemberSerializer.Serialize(m)
                : m.MemberType == MemberType.ArrayMember ? ArrayMemberSerializer.Serialize(m)
                : m.MemberType == MemberType.StructureMember ? StructureMemberSerializer.Serialize(m)
                : m.MemberType == MemberType.StringMember ? StringMemberSerializer.Serialize(m)
                : throw new InvalidOperationException($"Could not determine member type for member '{m.Name}'."));

            element.Add(members);

            return element;
        }

        public override IComplexType Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.DataTypeName();

            //The only way to know if this is a string type that needs special treatment is if there is a member with
            //the same data type name (which otherwise should be impossible).
            if (element.Elements().Any(e => string.Equals(e.Attribute(L5XName.DataType)?.Value, name,
                    StringComparison.OrdinalIgnoreCase)))
            {
                return StringStructureSerializer.Deserialize(element);
            }

            var members = element.Elements().Select(e => GetSerializer(e).Deserialize(e)).ToList();

            return new StructureType(name, members);
        }

        private IL5XSerializer<IMember<IDataType>> GetSerializer(XElement element)
        {
            return element.Name == L5XName.DataValueMember ? DataValueMemberSerializer
                : element.Name == L5XName.ArrayMember ? ArrayMemberSerializer
                : element.Name == L5XName.StructureMember ? StructureMemberSerializer
                : throw new ArgumentException();
        }
    }
}