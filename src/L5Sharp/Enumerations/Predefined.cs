using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Ardalis.SmartEnum;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Utilities;
using String = L5Sharp.Types.String;

namespace L5Sharp.Enumerations
{
    public class Predefined : SmartEnum<Predefined, string>, IDataType
    {
        private const string ResourceNamespace = "Resources";
        private const string PredefinedFileName = "Predefined.xml";
        private static readonly ResourceReader Resources = new ResourceReader(typeof(Predefined));
        private static readonly XDocument PredefinedData = LoadPredefined();
        private static readonly string[] AtomicNames = { "BIT", "BOOL", "SINT", "INT", "DINT", "LINT", "REAL" };
        private readonly Dictionary<string, ReadOnlyMember> _members = new Dictionary<string, ReadOnlyMember>();

        internal Predefined(string name, string value) : base(name, value)
        {
        }

        internal Predefined(XElement element) : base(element.GetName(), element.GetName())
        {
            Family = DataTypeFamily.FromName(element.Attribute(nameof(Family))?.Value);

            var members = element.Element(nameof(Members))?.Descendants().Select(e => new ReadOnlyMember(e));
            if (members == null) return;
            foreach (var member in members)
                _members.Add(member.Name, member);
        }

        public static readonly Predefined Null = new Null();
        public static readonly Predefined Bit = new Bool(nameof(Bit).ToUpper());
        public static readonly Predefined Bool = new Bool();
        public static readonly Predefined Sint = new Sint();
        public static readonly Predefined Int = new Int();
        public static readonly Predefined Dint = new Dint();
        public static readonly Predefined Lint = new Lint();
        public static readonly Predefined Real = new Real();
        public static readonly Predefined String = new String();
        public static readonly Predefined Timer = new Timer();
        public static readonly Predefined Counter = new Counter();
        public static readonly Predefined Alarm = new Alarm();

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
            return List.Any(type => type.Name == name);
        }

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

        protected static XElement LoadElement(string name)
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
    }
}