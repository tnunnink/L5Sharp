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
        private static readonly XName ElementName = L5XElement.StructureMember.ToString();
        private readonly IL5XSerializer<IMember<IDataType>> _dataValueMemberSerializer;
        private readonly IL5XSerializer<IMember<IDataType>> _arrayMemberSerializer;
        private readonly IL5XSerializer<IMember<IDataType>> _structureMemberSerializer;

        public StructureMemberSerializer(L5XContext? context = null)
        {
            _dataValueMemberSerializer = context is not null
                ? context.Serializers.GetSerializer<IMember<IDataType>>(typeof(DataValueMemberSerializer))
                : new DataValueMemberSerializer();

            _arrayMemberSerializer = context is not null
                ? context.Serializers.GetSerializer<IMember<IDataType>>(typeof(ArrayMemberSerializer))
                : new ArrayMemberSerializer(context);

            _structureMemberSerializer = context is not null
                ? context.Serializers.GetSerializer<IMember<IDataType>>(typeof(StructureMemberSerializer))
                : new StructureMemberSerializer(context);
        }

        public XElement Serialize(IMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.AddAttribute(component, m => m.Name);
            element.AddAttribute(component, m => m.DataType);

            var members = component.DataType.GetMembers()
                .Select(m => m.IsValueMember ? _dataValueMemberSerializer.Serialize(m) 
                : m.IsArrayMember ? _arrayMemberSerializer.Serialize(m) 
                : _structureMemberSerializer.Serialize(m));

            element.Add(members);

            return element;
        }

        public IMember<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.ComponentName();

            var typeName = element.DataTypeName();
            var members = element.Elements().Select(e => GetSerializer(e).Deserialize(e)).ToList();

            var dataType = new StructureType(typeName, DataTypeClass.Unknown, members);

            return new Member<IDataType>(name, dataType);
        }

        private IL5XSerializer<IMember<IDataType>> GetSerializer(XElement element)
        {
            return element.Name == L5XElement.DataValueMember.ToString() ? _dataValueMemberSerializer
                : element.Name == L5XElement.ArrayMember.ToString() ? _dataValueMemberSerializer
                : element.Name == L5XElement.StructureMember.ToString() ? _structureMemberSerializer
                : throw new ArgumentException();
        }
    }
}