using System;
using System.ComponentModel;
using L5Sharp.Converters;
using L5Sharp.Enums;

namespace L5Sharp.Types
{
    /// <summary>
    /// Represents a INT Logix atomic data type.
    /// </summary>
    [TypeConverter(typeof(LintConverter))]
    public sealed class Lint : IAtomicType<long>, IEquatable<Lint>, IComparable<Lint>
    {
        /// <summary>
        /// Creates a new default instance of a Lint type.
        /// </summary>
        public Lint()
        {
            Name = nameof(Lint).ToUpper();
            Value = default;
        }

        /// <summary>
        /// Creates a new instance of a Lint with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        public Lint(long value) : this()
        {
            Value = value;
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description => $"Logix representation of a {typeof(long)}";

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.Atomic;

        /// <inheritdoc />
        public long Value { get; private set; }

        object IAtomicType.Value => Value;

        /// <inheritdoc />
        public void SetValue(long value) => Value = value;

        /// <inheritdoc />
        public void SetValue(object value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));
            
            var converter = TypeDescriptor.GetConverter(GetType());

            if (!converter.CanConvertFrom(value.GetType()))
                throw new ArgumentException($"Value of type '{value.GetType()}' is not a valid for {GetType()}");

            Value = (Lint)converter.ConvertFrom(value)!;
        }

        /// <inheritdoc />
        public string Format(Radix? radix = null) =>
            radix is not null ? radix.Format(this) : Radix.Default(this).Format(this);

        /// <inheritdoc />
        public IDataType Instantiate() => new Lint();

        /// <summary>
        /// Converts the provided <see cref="long"/> to a <see cref="Lint"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="Lint"/> value.</returns>
        public static implicit operator Lint(long value) => new(value);

        /// <summary>
        /// Converts the provided <see cref="Lint"/> to a <see cref="long"/> value.
        /// </summary>
        /// <param name="atomic">The value to convert.</param>
        /// <returns>A <see cref="long"/> type value.</returns>
        public static implicit operator long(Lint atomic) => atomic.Value;

        /// <summary>
        /// Converts the provided <see cref="string"/> to a <see cref="Lint"/> value. 
        /// </summary>
        /// <param name="input">The string value to convert.</param>
        /// <returns>
        /// If the string value is able to be parsed, a new instance of a <see cref="Lint"/> with the value
        /// provided. If not, then a default instance value.
        /// </returns>
        public static implicit operator Lint(string input) => Radix.ParseValue<Lint>(input);

        /// <inheritdoc />
        public bool Equals(Lint? other)
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
            return obj.GetType() == GetType() && Equals((Lint)obj);
        }

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
        public static bool operator ==(Lint left, Lint right) => Equals(left, right);

        /// <summary>
        /// Determines whether the objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are not equal, otherwise, false.</returns>
        public static bool operator !=(Lint left, Lint right) => !Equals(left, right);

        /// <inheritdoc />
        public int CompareTo(Lint? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : Value.CompareTo(other.Value);
        }
    }
}