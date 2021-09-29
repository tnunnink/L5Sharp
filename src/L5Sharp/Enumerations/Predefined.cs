using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Ardalis.SmartEnum;
using L5Sharp.Abstractions;
using L5Sharp.Primitives;
using L5Sharp.Utilities;

namespace L5Sharp.Enumerations
{
    public class Predefined : SmartEnum<Predefined, string>, IDataType
    {
        private const string ResourceNamespace = "Resources";
        private const string PredefinedFileName = "Predefined.xml";
        private static readonly ResourceReader Resources = new ResourceReader(typeof(Predefined));
        private static readonly XDocument PredefinedData = LoadPredefined();
        private static readonly string[] AtomicNames = { "BOOL", "SINT", "INT", "DINT", "LINT", "REAL" };
        private readonly Dictionary<string, Member> _members = new Dictionary<string, Member>();

        private Predefined(XElement element) :
            base(element.Attribute(nameof(Name))?.Value, element.Attribute(nameof(Name))?.Value)
        {
            Family = DataTypeFamily.FromName(element.Attribute(nameof(Family))?.Value);

            var members = element.Element(nameof(Members))?.Descendants().Select(Member.Materialize);
            if (members == null) return;
            foreach (var member in members)
                _members.Add(member.Name, member);
        }

        public static readonly Predefined Bool = Load(nameof(Bool).ToUpper());

        public static readonly Predefined Sint = new SintType(LoadElement(nameof(Sint).ToUpper()));

        public static readonly Predefined Int = Load(nameof(Int).ToUpper());

        public static readonly Predefined Dint = Load(nameof(Dint).ToUpper());

        public static readonly Predefined Lint = Load(nameof(Lint).ToUpper());

        public static readonly Predefined Real = Load(nameof(Real).ToUpper());

        public static readonly Predefined String = Load(nameof(String).ToUpper());

        public static readonly Predefined Timer = Load(nameof(Timer).ToUpper());

        public static readonly Predefined Counter = Load(nameof(Counter).ToUpper());

        public DataTypeFamily Family { get; }
        public DataTypeClass Class => DataTypeClass.Predefined;
        public bool IsAtomic => AtomicNames.Contains(Name);
        public IEnumerable<IMember> Members => _members.Values.AsEnumerable();

        public bool SupportsRadix(Radix radix)
        {
            if (this == Real)
            {
                return radix == Radix.Float || radix == Radix.Exponential;
            }

            if (this == Bool)
            {
                return radix == Radix.Binary || radix == Radix.Octal || radix == Radix.Decimal || radix == Radix.Hex;
            }

            if (this == Lint)
            {
                return radix == Radix.Binary || radix == Radix.Octal || radix == Radix.Decimal || radix == Radix.Hex
                       || radix == Radix.Ascii || radix == Radix.DateTime || radix == Radix.DateTimeNs;
            }

            return radix == Radix.Binary || radix == Radix.Octal || radix == Radix.Decimal || radix == Radix.Hex ||
                   radix == Radix.Ascii;
        }

        public static bool ContainsType(string name)
        {
            return List.Any(type => type.Name == name);
        }

        public static IDataType ParseType(string name)
        {
            var field = typeof(Predefined)
                .GetField(name, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
            
            return (Predefined) field?.GetValue(null);
        }

        public virtual object ParseValue(string value)
        {
            if (!IsAtomic)
                throw new InvalidOperationException($"Type {Name} is not atomic and does not represent an value type");

            if (Equals(Bool))
                return bool.Parse(value);
            if (Equals(Sint))
                return byte.Parse(value);
            if (Equals(Int))
                return short.Parse(value);
            if (Equals(Dint))
                return int.Parse(value);
            if (Equals(Lint))
                return long.Parse(value);
            if (Equals(Real))
                return float.Parse(value);

            return null;
        }

        private static Predefined Load(string name)
        {
            var element = PredefinedData.Descendants(nameof(DataType))
                .SingleOrDefault(x => x.Attribute(nameof(Name))?.Value == name);

            return new Predefined(element);
        }
        
        private static XElement LoadElement(string name)
        {
            var element = PredefinedData.Descendants(nameof(DataType))
                .SingleOrDefault(x => x.Attribute(nameof(Name))?.Value == name);

            return element;
        }

        private static XDocument LoadPredefined()
        {
            using var stream = Resources.GetStream(PredefinedFileName, ResourceNamespace);
            if (stream == null)
                throw new InvalidOperationException(
                    $"Could not load template resource file {PredefinedFileName} from {ResourceNamespace}");

            return XDocument.Load(stream);
        }
        
        private class BoolType : Predefined
        {
            public BoolType(XElement element) : base(element)
            {
            }

            public override object ParseValue(string value)
            {
                return value == "1" || value == "Yes" || value == "true";
            }
        }
        
        private class SintType : Predefined
        {
            public SintType(XElement element) : base(element)
            {
            }
        }
    }
}