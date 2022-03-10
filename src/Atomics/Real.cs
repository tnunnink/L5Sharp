using System;
using System.ComponentModel;
using L5Sharp.Converters;
using L5Sharp.Enums;

namespace L5Sharp.Atomics
{
    /// <summary>
    /// Represents a <b>REAL</b> Logix atomic data type, or a type analogous to a <see cref="float"/>.
    /// </summary>
    [TypeConverter(typeof(RealConverter))]
    public sealed class Real : IAtomicType<float>, IEquatable<Real>, IComparable<Real>
    {
        /// <summary>
        /// Creates a new default <see cref="Real"/> type.
        /// </summary>
        public Real()
        {
            Name = nameof(Real).ToUpper();
            Value = default;
        }

        /// <summary>
        /// Creates a new <see cref="Real"/> with the provided value.
        /// </summary>
        /// <param name="value">The value to initialize the type with.</param>
        public Real(float value) : this()
        {
            Value = value;
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description => $"Logix representation of a {typeof(float)}";

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.Atomic;

        /// <inheritdoc />
        public float Value { get; private set; }

        object IAtomicType.Value => Value;

        /// <inheritdoc />
        public void SetValue(float value) => Value = value;

        /// <inheritdoc />
        public void SetValue(object value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            var converter = TypeDescriptor.GetConverter(GetType());

            if (!converter.CanConvertFrom(value.GetType()))
                throw new ArgumentException($"Value of type '{value.GetType()}' is not a valid for {GetType()}");

            Value = (Real)converter.ConvertFrom(value)!;
        }

        /// <inheritdoc />
        public bool TrySetValue(object? value)
        {
            var converter = TypeDescriptor.GetConverter(GetType());

            if (value is null || !converter.CanConvertFrom(value.GetType()))
                return false;

            Value = (converter.ConvertFrom(value) as Real)!;
            return true;
        }

        /// <inheritdoc />
        public string Format(Radix? radix = null) =>
            radix is not null ? radix.Format(this) : Radix.Default(this).Format(this);

        /// <inheritdoc />
        public IDataType Instantiate() => new Real();

        /// <summary>
        /// Converts the provided <see cref="float"/> to a <see cref="Real"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="Real"/> value.</returns>
        public static implicit operator Real(float value) => new(value);

        /// <summary>
        /// Converts the provided <see cref="Real"/> to a <see cref="float"/> value.
        /// </summary>
        /// <param name="atomic">The value to convert.</param>
        /// <returns>A <see cref="float"/> type value.</returns>
        public static implicit operator float(Real atomic) => atomic.Value;

        /// <summary>
        /// Converts the provided <see cref="string"/> to a <see cref="Real"/> value. 
        /// </summary>
        /// <param name="input">The string value to convert.</param>
        /// <returns>
        /// If the string value is able to be parsed, a new instance of a <see cref="Real"/> with the value
        /// provided. If not, then a default instance value.
        /// </returns>
        public static implicit operator Real(string input) => Radix.ParseValue<Real>(input);

        /// <inheritdoc />
        public bool Equals(Real? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Math.Abs(Value - other.Value) < float.Epsilon;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Real)obj);
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
        public static bool operator ==(Real left, Real right) => Equals(left, right);

        /// <summary>
        /// Determines whether the objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the objects are not equal, otherwise, false.</returns>
        public static bool operator !=(Real left, Real right) => !Equals(left, right);

        /// <inheritdoc />
        public int CompareTo(Real? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : Value.CompareTo(other.Value);
        }
    }
}