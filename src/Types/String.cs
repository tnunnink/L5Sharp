using System;
using System.Linq;
using System.Text;
using L5Sharp.Abstractions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;

// ReSharper disable InconsistentNaming Logix Naming

namespace L5Sharp.Types
{
    /// <summary>
    /// Represents a predefined String Logix data type.
    /// </summary>
    public sealed class String : ComplexType, IStringDefined, IEquatable<String>, IComparable<String>
    {
        private const int PredefinedLength = 82; //This is the built in length of string types in RSLogix

        public String() : base(nameof(String).ToUpper(), $"Logix representation of a {typeof(string)}")
        {
            LEN = Member.Create(nameof(LEN), new Dint(PredefinedLength));
        }

        public String(string value) : this()
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Value can not be null");

            var bytes = Encoding.ASCII.GetBytes(value);

            if (bytes.Length > LEN.DataType.Value)
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"Value length {bytes.Length} must be less than the predefined length {PredefinedLength}");

            DATA = bytes.Select(b => new Sint(b))
                .ToArrayMember(nameof(DATA), new Dimensions(PredefinedLength), Radix.Ascii);
        }

        /// <inheritdoc />
        public override DataTypeFamily Family => DataTypeFamily.String;

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;

        /// <inheritdoc />
        public string Value =>
            Encoding.ASCII.GetString(DATA.Where(d => d.DataType.Value > 0).Select(d => d.DataType.Value).ToArray());

        /// <inheritdoc />
        public IMember<Dint> LEN { get; }

        /// <inheritdoc />
        public IArrayMember<Sint> DATA { get; }

        /// <inheritdoc />
        protected override IDataType New() => new String();

        /// <inheritdoc />
        public IStringDefined Update(string value) => new String(value);

        /// <summary>
        /// Converts the provided <see cref="string"/> to a <see cref="String"/> value.
        /// </summary>
        /// <param name="input">The value to convert.</param>
        /// <returns>A <see cref="String"/> type value.</returns>
        public static implicit operator String(string input) => new(input);

        /// <summary>
        /// Converts the provided <see cref="String"/> to a <see cref="string"/> value.
        /// </summary>
        /// <param name="input">The value to convert.</param>
        /// <returns>A <see cref="string"/> type value.</returns>
        public static implicit operator string(String input) => input.Value;

        /// <inheritdoc />
        public bool Equals(String? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
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
            return Value.GetHashCode();
        }

        /// <summary>
        /// Determines whether the objects are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are equal, otherwise, false.</returns>
        public static bool operator ==(String left, String right) => Equals(left, right);

        /// <summary>
        /// Determines whether the objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are not equal, otherwise, false.</returns>
        public static bool operator !=(String left, String right) => !Equals(left, right);

        /// <inheritdoc />
        public int CompareTo(String other) => string.Compare(Value, other.Value, StringComparison.Ordinal);
    }
}