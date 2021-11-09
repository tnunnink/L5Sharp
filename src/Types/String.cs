using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using L5Sharp.Core;
using L5Sharp.Enums;

// ReSharper disable InconsistentNaming RSLogix Naming

namespace L5Sharp.Types
{
    public sealed class String : IString, IEquatable<String>, IComparable<String>
    {
        private const int PredefinedLength = 82; //This is the built in length of string types in RSLogix
        
        public String()
        {
            LEN = Member.OfType<Dint>(nameof(LEN));
            DATA = Member.OfType<Sint>(nameof(DATA), new Dimensions(PredefinedLength), Radix.Ascii);
            
            Members = new List<IMember<IDataType>>
            {
                (IMember<IDataType>)LEN,
                (IMember<IDataType>)DATA,
            };
        }

        private String(string value = default) : this()
        {
            if (!string.IsNullOrEmpty(value))
                SetValue(value);
        }

        public string Name => nameof(String).ToUpper();
        public string Description => $"RSLogix representation of a {typeof(string)}";
        public Radix Radix => Radix.Null;
        public DataTypeFamily Family => DataTypeFamily.String;
        public DataTypeClass Class => DataTypeClass.Predefined;
        public TagDataFormat DataFormat => TagDataFormat.String;
        public string Value => GetValue();
        public IMember<Dint> LEN { get; }
        public IMember<Sint> DATA { get; }
        public IEnumerable<IMember<IDataType>> Members { get; }

        public void SetValue(string value)
        {
            var bytes = Encoding.ASCII.GetBytes(value);

            if (bytes.Length > LEN.DataType.Value)
                throw new ArgumentOutOfRangeException();
            
            ClearData();
            
            for (var i = 0; i < bytes.Length; i++)
                DATA.Elements[i].DataType.SetValue(bytes[i]);
        }

        public static implicit operator String(string input)
        {
            return new String(input);
        }

        public static implicit operator string(String input)
        {
            return input.GetValue();
        }

        public bool Equals(String other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Members, other.Members) && Equals(LEN, other.LEN) && Equals(DATA, other.DATA);
        }

        public int CompareTo(String other)
        {
            return string.Compare(Value, other.Value, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((String)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Members, LEN, DATA);
        }

        public static bool operator ==(String left, String right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(String left, String right)
        {
            return !Equals(left, right);
        }

        private string GetValue()
        {
            var bytes = DATA.Elements.Select(d => d.DataType.Value).ToArray();
            return Encoding.ASCII.GetString(bytes);
        }

        private void ClearData()
        {
            foreach (var dataElement in DATA.Elements)
                dataElement.DataType.SetValue(0);
        }
    }
}