using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Utilities;
using String = L5Sharp.Types.String;

namespace L5Sharp.Core
{
    public class Predefined : IPredefined, IEquatable<Predefined>
    {
        private const string ResourceNamespace = "Resources";
        private const string ResourceFileName = "Predefined.xml";
        private static readonly ResourceReader Resources = new ResourceReader(typeof(Predefined));
        private static readonly XDocument PredefinedData = LoadPredefined();

        private static readonly Dictionary<string, IPredefined> RegisteredTypes =
            new Dictionary<string, IPredefined>(StringComparer.OrdinalIgnoreCase);

        private readonly Dictionary<string, Member> _members =
            new Dictionary<string, Member>(StringComparer.OrdinalIgnoreCase);

        protected Predefined(string name, DataTypeFamily family, IEnumerable<Member> members = null)
        {
            Validate.Name(name);
            Name = name;
            Family = family ?? throw new ArgumentNullException(nameof(family), "Family can not be null");

            members ??= Array.Empty<Member>();

            foreach (var member in members)
            {
                if (_members.ContainsKey(member.Name))
                    throw new ComponentNameCollisionException(member.Name, typeof(Member));

                _members.Add(member.Name, member);
            }

            if (!RegisteredTypes.ContainsKey(name))
                RegisteredTypes.Add(name, this);
        }

        internal Predefined(XElement element)
        {
            Validate.Name(element.GetName());

            Name = element.GetName();
            Family = element.GetValue<IDataType>(d => d.Family)
                     ?? throw new ArgumentNullException(nameof(element), "Family can not be null");

            var members = element.Descendants(LogixNames.GetComponentName<IMember>());

            foreach (var e in members)
            {
                var typeName = e.GetDataTypeName();
                if (typeName == null)
                    throw new ArgumentNullException(nameof(typeName), "DataType can not be null");

                if (!RegisteredTypes.ContainsKey(typeName))
                    throw new InvalidOperationException(
                        $"Type '{typeName}' has not been defined. Register dependent types before parent type");

                var type = RegisteredTypes[typeName];

                var name = e.GetName();
                var description = e.GetDescription();
                var dimension = e.GetValue<IMember>(m => m.Dimensions);
                var radix = e.GetValue<IMember>(m => m.Radix);
                var access = e.GetValue<IMember>(m => m.ExternalAccess);

                var member = new Member(name, type, dimension, radix, access, description);

                _members.Add(member.Name, member);
            }

            if (!RegisteredTypes.ContainsKey(Name))
                RegisteredTypes.Add(Name, this);
        }

        public static readonly Undefined Undefined = new Undefined();
        public static readonly Bool Bit = new Bool();
        public static readonly Bool Bool = new Bool();
        public static readonly Sint Sint = new Sint();
        public static readonly Int Int = new Int();
        public static readonly Dint Dint = new Dint();
        public static readonly Lint Lint = new Lint();
        public static readonly Real Real = new Real();
        public static readonly String String = new String();
        public static readonly Timer Timer = new Timer();
        public static readonly Counter Counter = new Counter();
        public static readonly Alarm Alarm = new Alarm();

        public string Name { get; }
        public string Description => string.Empty;
        public DataTypeFamily Family { get; }
        public DataTypeClass Class => DataTypeClass.Predefined;
        public virtual TagDataFormat DataFormat => TagDataFormat.Decorated;
        public IEnumerable<IMember> Members => _members.Values.AsEnumerable();
        
        public IMember GetMember(string name)
        {
            _members.TryGetValue(name, out var member);
            return member;
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