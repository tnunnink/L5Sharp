using System;
using System.ComponentModel;
using L5Sharp.Converters;
using L5Sharp.Enums;

namespace L5Sharp.Types.Atomics
{
    /// <summary>
    /// Represents a <b>USint</b> Logix atomic data type, or a type analogous to a <see cref="byte"/>.
    /// </summary>
    [TypeConverter(typeof(USintConverter))]
    public sealed class USint  : IAtomicType<byte>, IEquatable<USint>, IComparable<USint>
    {
        /// <summary>
        /// Creates a new default <see cref="USint"/> type.
        /// </summary>
        public USint()
        {
            Name = nameof(USint).ToUpper();
            Value = default;
        }

        /// <summary>
        /// Creates a new <see cref="USint"/> with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        public USint(byte value) : this()
        {
            Value = value;
        }
        
        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description => $"Logix representation of a {typeof(byte)}";

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.Atomic;

        /// <inheritdoc />
        public byte Value { get; private set; }

        object IAtomicType.Value => Value;

        /// <inheritdoc />
        public void SetValue(byte value) => Value = value;

        /// <inheritdoc />
        public void SetValue(object value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));
            
            var converter = TypeDescriptor.GetConverter(GetType());

            if (!converter.CanConvertFrom(value.GetType()))
                throw new ArgumentException($"Value of type '{value.GetType()}' is not a valid for {GetType()}");

            Value = (USint)converter.ConvertFrom(value)!;
        }

        /// <inheritdoc />
        public string Format(Radix? radix = null) =>
            radix is not null ? radix.Format(this) : Radix.Default(this).Format(this);

        /// <inheritdoc />
        public IDataType Instantiate() => new USint();

        /// <summary>
        /// Converts the provided <see cref="byte"/> to a <see cref="Sint"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="Sint"/> value.</returns>
        public static implicit operator USint(byte value) => new(value);

        /// <summary>
        /// Converts the provided <see cref="Sint"/> to a <see cref="byte"/> value.
        /// </summary>
        /// <param name="atomic">The value to convert.</param>
        /// <returns>A <see cref="byte"/> type value.</returns>
        public static implicit operator byte(USint atomic) => atomic.Value;

        /// <summary>
        /// Converts the provided <see cref="string"/> to a <see cref="Sint"/> value. 
        /// </summary>
        /// <param name="input">The string value to convert.</param>
        /// <returns>
        /// If the string value is able to be parsed, a new instance of a <see cref="Sint"/> with the value
        /// provided. If not, then a default instance value.
        /// </returns>
        public static implicit operator USint(string input) => 
            byte.TryParse(input, out var result) ? new USint(result) : Radix.ParseValue<USint>(input);

        /// <inheritdoc />
        public bool Equals(USint? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as USint);
        
        /// <inheritdoc />
        public override int GetHashCode() => Name.GetHashCode();
        
        /// <inheritdoc />
        public override string ToString() => Name;

        /// <summary>
        /// Determines whether the objects are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are equal, otherwise, false.</returns>
        public static bool operator ==(USint left, USint right) => Equals(left, right);

        /// <summary>
        /// Determines whether the objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are not equal, otherwise, false.</returns>
        public static bool operator !=(USint left, USint right) => !Equals(left, right);

        /// <inheritdoc />
        public int CompareTo(USint? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : Value.CompareTo(other.Value);
        }
    }
}