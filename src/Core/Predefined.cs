using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class Predefined : IPredefined, IEquatable<Predefined>
    {
        private const string ResourceNamespace = "Resources";
        private const string ResourceFileName = "Predefined.xml";
        private static readonly ResourceReader Resources = new ResourceReader(typeof(Predefined));
        private static readonly XDocument PredefinedData = LoadPredefined();

        private readonly Dictionary<string, Member<IDataType>> _members =
            new Dictionary<string, Member<IDataType>>(StringComparer.OrdinalIgnoreCase);

        protected Predefined(string name, DataTypeFamily family, IEnumerable<Member<IDataType>> members = null)
        {
            Validate.Name(name);
            Name = name;
            Family = family ?? throw new ArgumentNullException(nameof(family), "Family can not be null");

            members ??= Array.Empty<Member<IDataType>>();

            foreach (var member in members)
            {
                if (_members.ContainsKey(member.Name))
                    throw new ComponentNameCollisionException(member.Name, typeof(Member<IDataType>));

                _members.Add(member.Name, member);
            }
        }

        internal Predefined(XElement element)
        {
            Validate.Name(element.GetName());

            Name = element.GetName();
            Family = element.GetValue<IDataType>(d => d.Family)
                     ?? throw new ArgumentNullException(nameof(element), "Family can not be null");

            var members = element.Descendants(LogixNames.GetComponentName<IMember<IDataType>>());

            foreach (var e in members)
            {
                var typeName = e.GetDataTypeName();
                if (typeName == null)
                    throw new ArgumentNullException(nameof(typeName), "DataType can not be null");

                if (!Logix.DataType.Contains(typeName))
                    throw new InvalidOperationException(
                        $"Type '{typeName}' has not been defined. Register dependent types before parent type");

                var type = Logix.DataType.Create(typeName);

                var name = e.GetName();
                var description = e.GetDescription();
                var dimension = e.GetValue<IMember<IDataType>>(m => m.Dimensions);
                var radix = e.GetValue<IMember<IDataType>>(m => m.Radix);
                var access = e.GetValue<IMember<IDataType>>(m => m.ExternalAccess);

                var member = new Member<IDataType>(name, type, dimension, radix, access, description);

                _members.Add(member.Name, member);
            }
        }

        public string Name { get; }
        public string Description => string.Empty;
        public DataTypeFamily Family { get; }
        public DataTypeClass Class => DataTypeClass.Predefined;
        public virtual TagDataFormat DataFormat => TagDataFormat.Decorated;
        public IEnumerable<IMember<IDataType>> Members => _members.Values.AsEnumerable();

        public IMember<IDataType> GetMember(string name)
        {
            _members.TryGetValue(name, out var member);
            return member;
        }

        public IMember<TType> GetMember<TType>(string name) where TType : IDataType
        {
            _members.TryGetValue(name, out var member);

            if (member == null)
                return null;

            var type = (TType)member.DataType ?? throw new InvalidOperationException();

            return new Member<TType>(member.Name, type, member.Dimensions, member.Radix,
                member.ExternalAccess, member.Description);
        }

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(Predefined other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Predefined)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_members, Name, Family);
        }

        public static bool operator ==(Predefined left, Predefined right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Predefined left, Predefined right)
        {
            return !Equals(left, right);
        }

        internal static XElement LoadElement(string name)
        {
            var element = PredefinedData.Descendants(nameof(DataType))
                .SingleOrDefault(x => x.Attribute(nameof(Name))?.Value == name);

            return element;
        }

        private static XDocument LoadPredefined()
        {
            using var stream = Resources.GetStream(ResourceFileName, ResourceNamespace);
            return XDocument.Load(stream);
        }
    }
}