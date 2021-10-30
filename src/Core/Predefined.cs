using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using L5Sharp.Abstractions;
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
        private const string PredefinedFileName = "Predefined.xml";
        private static readonly ResourceReader Resources = new ResourceReader(typeof(Predefined));
        private static readonly XDocument PredefinedData = LoadPredefined();
        private static readonly string[] AtomicNames = { "BOOL", "SINT", "INT", "DINT", "LINT", "REAL" };

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
                    Throw.ComponentNameCollisionException(member.Name, typeof(Member));

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
                    throw new DataTypeNotFoundException($"Type '{typeName}' has not been defined");

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
        public bool IsAtomic => AtomicNames.Contains(Name);
        public virtual object DefaultValue => null;
        public virtual Radix DefaultRadix => Radix.Null;
        public virtual TagDataFormat DataFormat => TagDataFormat.Decorated;
        public IEnumerable<IMember> Members => _members.Values.AsEnumerable();
        public static IEnumerable<IDataType> Types => RegisteredTypes.Values.ToList();
        public static IEnumerable<IDataType> Atomics => RegisteredTypes.Values.Where(t => t.IsAtomic).ToList();

        public virtual bool SupportsRadix(Radix radix)
        {
            if (!IsAtomic) return radix.Equals(Radix.Null);

            return radix == Radix.Binary
                   || radix == Radix.Octal
                   || radix == Radix.Decimal
                   || radix == Radix.Hex
                   || radix == Radix.Ascii;
        }

        public virtual bool IsValidValue(object value)
        {
            return value == null;
        }

        public IMember GetMember(string name)
        {
            _members.TryGetValue(name, out var member);
            return member;
        }

        public IEnumerable<IDataType> GetDependentTypes() => GetUniqueMemberTypes(this);

        public static bool ContainsType(string name)
        {
            return RegisteredTypes.ContainsKey(name)
                   || FindFieldType(name) != null
                   || FindAssemblyType(name) != null;
        }

        public static IDataType ParseType(string name)
        {
            if (RegisteredTypes.ContainsKey(name))
                return RegisteredTypes[name];

            var fieldType = FindFieldType(name);
            if (fieldType != null)
                return fieldType;

            var assemblyType = FindAssemblyType(name);

            try
            {
                return (IDataType)Activator.CreateInstance(assemblyType);
            }
            catch (Exception)
            {
                return Undefined;
            }
        }

        public virtual object ParseValue(string value)
        {
            return null;
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

        private static IDataType FindFieldType(string name)
        {
            var field = typeof(Predefined).GetField(name, BindingFlags.Public
                                                          | BindingFlags.Static
                                                          | BindingFlags.IgnoreCase);

            return (Predefined)field?.GetValue(null);
        }

        private static Type FindAssemblyType(string name)
        {
            return GetAssemblyTypes().SingleOrDefault(t => t.Name == name);
        }

        private static IEnumerable<Type> GetAssemblyTypes()
        {
            return from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                where type.IsSubclassOf(typeof(Predefined))
                      && !type.IsAbstract
                      && type.GetConstructor(Type.EmptyTypes) != null
                select type;
        }

        internal static XElement LoadElement(string name)
        {
            var element = PredefinedData.Descendants(nameof(DataType))
                .SingleOrDefault(x => x.Attribute(nameof(Name))?.Value == name);

            return element;
        }

        private static XDocument LoadPredefined()
        {
            using var stream = Resources.GetStream(PredefinedFileName, ResourceNamespace);
            return XDocument.Load(stream);
        }

        private static IEnumerable<IDataType> GetUniqueMemberTypes(IDataType dataType)
        {
            var types = new List<IDataType>();

            foreach (var member in dataType.Members)
            {
                types.Add(member.DataType);
                types.AddRange(GetUniqueMemberTypes(member.DataType));
            }

            return types.Distinct();
        }
    }
}