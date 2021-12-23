using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using L5Sharp.Abstractions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;

// ReSharper disable InconsistentNaming Logix Naming

namespace L5Sharp.Types
{
    /// <summary>
    /// Represents a predefined String Logix data type.
    /// </summary>
    public sealed class String : ComplexType, IStringType, IEquatable<String>, IComparable<String>
    {
        private const int PredefinedLength = 82; //This is the built in length of string types in RSLogix

        /// <summary>
        /// Creates a new instance of a String Logix data type with default values.
        /// </summary>
        public String() : base(nameof(String).ToUpper(), $"Logix representation of a {typeof(string)}",
            GenerateMembers())
        {
            LEN = (IMember<Dint>)Members.Single(m => m.Name == nameof(LEN));
            DATA = (IMember<Sint>)Members.Single(m => m.Name == nameof(DATA));
        }

        /// <summary>
        /// Creates a new instance of a String Logix data type with the provided string value.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public String(string value) : this()
        {
            SetData(value);
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
        public IMember<Sint> DATA { get; }

        /// <inheritdoc />
        protected override IDataType New() => new String();

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">
        /// Thrown when the provided value is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the length of the provided string is longer than the predefined length (LEN Member value).
        /// </exception>
        public void SetValue(string value) => SetData(value);

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
        public override int GetHashCode() => Value.GetHashCode();

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
        public int CompareTo(String? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(other, null) ? 1 : string.Compare(Value, other.Value, StringComparison.Ordinal);
        }

        /// <summary>
        /// Helper to set the underlying DATA member with the provided string value.
        /// </summary>
        /// <param name="value">The string value to parse and set DATA with.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the length of the provided value is longer than the predefined length.</exception>
        private void SetData(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var bytes = Encoding.ASCII.GetBytes(value);

            if (bytes.Length > LEN.DataType.Value)
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"Value length '{bytes.Length}' must be less than the predefined length '{PredefinedLength}'");
            
            foreach (var element in DATA)
                element.DataType.SetValue(0);

            for (var i = 0; i < bytes.Length; i++)
                DATA[i].DataType.SetValue(bytes[i]);
        }

        private static IEnumerable<IMember<IDataType>> GenerateMembers()
        {
            return new List<IMember<IDataType>>
            {
                Member.Create(nameof(LEN), new Dint(PredefinedLength)),
                Member.Create<Sint>(nameof(DATA), new Dimensions(PredefinedLength), Radix.Ascii)
            };
        }
    }
}