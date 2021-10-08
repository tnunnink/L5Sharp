using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Utilities;
using String = L5Sharp.Types.String;

namespace L5Sharp.Core
{
    public class Predefined : IDataType
    {
        private const string ResourceNamespace = "Resources";
        private const string PredefinedFileName = "Predefined.xml";
        private static readonly ResourceReader Resources = new ResourceReader(typeof(Predefined));
        private static readonly XDocument PredefinedData = LoadPredefined();
        private static readonly string[] AtomicNames = { "BOOL", "SINT", "INT", "DINT", "LINT", "REAL" };
        private readonly Dictionary<string, ReadOnlyMember> _members = new Dictionary<string, ReadOnlyMember>();

        protected Predefined(string name, DataTypeFamily family, IEnumerable<ReadOnlyMember> members)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "Name can not be null");
            Family = family ?? throw new ArgumentNullException(nameof(family), "Family can not be null");

            if (members == null) return;

            foreach (var member in members)
            {
                if (_members.ContainsKey(member.Name))
                    Throw.ComponentNameCollisionException(member.Name, typeof(ReadOnlyMember));
                
                _members.Add(member.Name, member);
            }
        }

        internal Predefined(XElement element)
        {
            Name = element.GetName() ?? throw new ArgumentNullException(nameof(element), "Name can not be null");
            Family = element.GetFamily() ?? throw new ArgumentNullException(nameof(element), "Family can not be null");

            var members = element.Descendants(L5XNames.Components.Member);

            foreach (var me in members)
            {
                var typeName = me.GetDataTypeName();
                if (typeName == null)
                    throw new ArgumentNullException(nameof(typeName), "DataType can not be null");

                var type = ParseType(typeName);

                var member = new ReadOnlyMember(me.GetName(), type, me.GetDimension(), me.GetRadix(),
                    me.GetExternalAccess(), me.GetDescription());

                _members.Add(member.Name, member);
            }
        }

        public static readonly Null Null = new Null();
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
        public string Description { get; }
        public DataTypeFamily Family { get; }
        public DataTypeClass Class => DataTypeClass.Predefined;
        public bool IsAtomic => AtomicNames.Contains(Name);
        public virtual object DefaultValue => null;
        public virtual Radix DefaultRadix => Radix.Null;
        public virtual TagDataFormat DataFormat => TagDataFormat.Decorated;
        public IEnumerable<IMember> Members => _members.Values.AsEnumerable();

        public virtual bool SupportsRadix(Radix radix)
        {
            if (!IsAtomic) return radix.Equals(Radix.Null);

            return radix == Radix.Binary
                   || radix == Radix.Octal
                   || radix == Radix.Decimal
                   || radix == Radix.Hex
                   || radix == Radix.Ascii;
        }

        public static bool ContainsType(string name)
        {
            return GetStaticTypes().Any(type => type.Name == name);
        }

        public static IEnumerable<IDataType> Types() => GetStaticTypes();

        public static IDataType ParseType(string name)
        {
            var field = typeof(Predefined)
                .GetField(name, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);

            return (Predefined)field?.GetValue(null);
        }

        public virtual object ParseValue(string value)
        {
            return null;
        }

        public virtual bool IsValidValue(object value)
        {
            return !IsAtomic && value == null;
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

        private static IEnumerable<IDataType> GetStaticTypes()
        {
            var fields =
                typeof(Predefined).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);

            return fields.Select(f => (Predefined)f.GetValue(null)).Distinct();
        }
    }
}