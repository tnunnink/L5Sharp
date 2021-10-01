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
        private readonly Dictionary<string, ReadOnlyMember> _members = new Dictionary<string, ReadOnlyMember>();

        private Predefined(string name, string value) : base(name, value)
        {
        }

        private Predefined(XElement element) : base(element.GetName(), element.GetName())
        {
            Family = DataTypeFamily.FromName(element.Attribute(nameof(Family))?.Value);

            var members = element.Element(nameof(Members))?.Descendants().Select(ReadOnlyMember.Materialize);
            if (members == null) return;
            foreach (var member in members)
                _members.Add(member.Name, member);
        }

        public static readonly Predefined Bit = new BoolType(nameof(Bit).ToUpper());
        public static readonly Predefined Bool = new BoolType();
        public static readonly Predefined Sint = new SintType();
        public static readonly Predefined Int = new IntType();
        public static readonly Predefined Dint = new DintType();
        public static readonly Predefined Lint = new LintType();
        public static readonly Predefined Real = new RealType();
        public static readonly Predefined String = new StringType();
        public static readonly Predefined Timer = Load(nameof(Timer).ToUpper());
        public static readonly Predefined Counter = Load(nameof(Counter).ToUpper());

        public DataTypeFamily Family { get; }
        public DataTypeClass Class => DataTypeClass.Predefined;
        public bool IsAtomic => AtomicNames.Contains(Name);
        public virtual object Default => null;
        public IEnumerable<IMember> Members => _members.Values.AsEnumerable();

        public virtual bool SupportsRadix(Radix radix)
        {
            if (!IsAtomic) return radix.Equals(Radix.Null);
            
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

        internal virtual void UpdateMembers(ITagMember member)
        {
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


        #region Classes

        private class BoolType : Predefined
        {
            internal BoolType() : base(LoadElement(nameof(Bool).ToUpper()))
            {
            }
            
            internal BoolType(string name) : base(name, LoadElement(nameof(Bool).ToUpper()).GetName())
            {
            }

            public override object Default => default(bool);

            public override bool SupportsRadix(Radix radix)
            {
                return radix == Radix.Binary || radix == Radix.Octal || radix == Radix.Decimal || radix == Radix.Hex;
            }

            public override object ParseValue(string value)
            {
                if (bool.TryParse(value, out var result))
                    return result;

                if (string.Equals(value, "1", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(value, "True", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(value, "Yes", StringComparison.OrdinalIgnoreCase))
                    return true;

                if (string.Equals(value, "0", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(value, "False", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(value, "No", StringComparison.OrdinalIgnoreCase))
                    return false;

                return null;
            }

            public override bool IsValidValue(object value)
            {
                return value is bool;
            }
        }

        private class SintType : Predefined
        {
            internal SintType() : base(LoadElement(nameof(Sint).ToUpper()))
            {
            }

            public override object Default => default(byte);

            public override object ParseValue(string value)
            {
                if (byte.TryParse(value, out var result))
                    return result;
                return null;
            }
            
            public override bool IsValidValue(object value)
            {
                return value is byte;
            }
        }

        private class IntType : Predefined
        {
            internal IntType() : base(LoadElement(nameof(Int).ToUpper()))
            {
            }

            public override object Default => default(short);

            public override object ParseValue(string value)
            {
                if (short.TryParse(value, out var result))
                    return result;
                return null;
            }
            
            public override bool IsValidValue(object value)
            {
                return value is short;
            }
        }

        private class DintType : Predefined
        {
            internal DintType() : base(LoadElement(nameof(Dint).ToUpper()))
            {
            }

            public override object Default => default(int);

            public override object ParseValue(string value)
            {
                if (int.TryParse(value, out var result))
                    return result;
                return null;
            }
            
            public override bool IsValidValue(object value)
            {
                return value is int;
            }
        }

        private class LintType : Predefined
        {
            internal LintType() : base(LoadElement(nameof(Lint).ToUpper()))
            {
            }

            public override object Default => default(long);

            public override bool SupportsRadix(Radix radix)
            {
                return radix == Radix.Binary || radix == Radix.Octal || radix == Radix.Decimal || radix == Radix.Hex
                       || radix == Radix.Ascii || radix == Radix.DateTime || radix == Radix.DateTimeNs;
            }

            public override object ParseValue(string value)
            {
                if (long.TryParse(value, out var result))
                    return result;
                return null;
            }
            
            public override bool IsValidValue(object value)
            {
                return value is long;
            }
        }

        private class RealType : Predefined
        {
            internal RealType() : base(LoadElement(nameof(Real).ToUpper()))
            {
            }

            public override object Default => default(float);

            public override bool SupportsRadix(Radix radix)
            {
                return radix == Radix.Float || radix == Radix.Exponential;
            }

            public override object ParseValue(string value)
            {
                if (float.TryParse(value, out var result))
                    return result;
                return null;
            }
            
            public override bool IsValidValue(object value)
            {
                return value is float;
            }
        }

        private class StringType : Predefined
        {
            internal StringType() : base(LoadElement(nameof(String).ToUpper()))
            {
                
            }

            public override object Default => string.Empty;

            public override object ParseValue(string value)
            {
                return value;
            }

            public override bool IsValidValue(object value)
            {
                return value is string;
            }
        }
        
        #endregion
    }
}