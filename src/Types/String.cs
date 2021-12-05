using System;
using System.Linq;
using System.Text;
using L5Sharp.Abstractions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;

// ReSharper disable InconsistentNaming RSLogix Naming

namespace L5Sharp.Types
{
    public sealed class String : ComplexType, IStringDefined, IEquatable<String>, IComparable<String>
    {
        private const int PredefinedLength = 82; //This is the built in length of string types in RSLogix

        public String() : base(nameof(String).ToUpper(), $"RSLogix representation of a {typeof(string)}")
        {
        }

        public String(string value) : this()
        {
            if (!string.IsNullOrEmpty(value))
                SetValue(value);
        }

        /// <inheritdoc />
        public override DataTypeFamily Family => DataTypeFamily.String;

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;

        /// <inheritdoc />
        public string Value => GetValue();

        /// <inheritdoc />
        public IMember<Dint> LEN { get; } = Member.Create(nameof(LEN), new Dint(PredefinedLength));

        /// <inheritdoc />
        public IArrayMember<Sint> DATA { get; } =
            Member.Array<Sint>(nameof(DATA), new Dimensions(PredefinedLength), Radix.Ascii);

        /// <inheritdoc />
        protected override IDataType New()
        {
            return new String();
        }

        /// <inheritdoc />
        public void SetValue(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Value can not be null");

            var bytes = Encoding.ASCII.GetBytes(value);

            if (bytes.Length > LEN.DataType.Value)
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"Value length is {bytes.Length}. The value length must be less than the predefined length {PredefinedLength}");

            ClearData();

            for (var i = 0; i < bytes.Length; i++)
                DATA[i].DataType.Update(bytes[i]);
        }

        public static implicit operator String(string input)
        {
            return new String(input);
        }

        public static implicit operator string(String input)
        {
            return input.GetValue();
        }

        /// <inheritdoc />
        public bool Equals(String? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value &&
                   Members.SequenceEqual(other.Members) &&
                   Equals(LEN, other.LEN)
                   && Equals(DATA, other.DATA);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((String)obj);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public int CompareTo(String other)
        {
            return string.Compare(Value, other.Value, StringComparison.Ordinal);
        }

        private string GetValue()
        {
            var bytes = DATA.Where(d => d.DataType.Value > 0)
                .Select(d => d.DataType.Value).ToArray();
            return Encoding.ASCII.GetString(bytes);
        }

        private void ClearData()
        {
            foreach (var dataElement in DATA)
                dataElement.DataType.Update(0);
        }
    }
}