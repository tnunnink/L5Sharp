using System;

namespace L5Sharp.Core
{
    /// <summary>
    /// A value type that represents a Logix Bulletin number,
    /// or a four digit identifier for the Logix controller family.
    /// </summary>
    public sealed class Bulletin : IEquatable<Bulletin>, IComparable<Bulletin>
    {
        private readonly short _identifier;

        /// <summary>
        /// Creates a new <see cref="Bulletin"/> number with the provided short identifier.
        /// </summary>
        /// <param name="identifier">The 4 digit identifier that represents the <see cref="Bulletin"/> value.</param>
        /// <exception cref="ArgumentException">identifier is not 4 digits.</exception>
        public Bulletin(short identifier)
        {
            if (identifier.ToString().Length != 4)
                throw new ArgumentException(
                    $"The provided identifier '{identifier}' does not have a valid length. Bulletin numbers must be 4 digits.");
            
            _identifier = identifier;
        }
        
        /// <summary>
        /// Creates a new <see cref="Bulletin"/> number with the provided string identifier.
        /// </summary>
        /// <param name="identifier">A 4 digit string identifier that represents the <see cref="Bulletin"/> value.</param>
        /// <exception cref="ArgumentException">identifier is null -or- not able to be parsed to a short type.</exception>
        public Bulletin(string identifier)
        {
            if (string.IsNullOrEmpty(identifier))
                throw new ArgumentException("The provided identifier can not be null or empty.");
            
            if (identifier.Length != 4)
                throw new ArgumentException(
                    $"The provided identifier '{identifier}' does not have a valid length. Bulletin numbers must be 4 digits.");

            if (!short.TryParse(identifier, out var result))
                throw new ArgumentException(
                    $"The provided identifier '{identifier}' was not able to be parsed to a {typeof(short)}");
            
            _identifier = result;
        }

        /// <summary>
        /// A <see cref="Bulletin"/> instance of the ControlLogix family (Number = 1756).
        /// </summary>
        public static Bulletin ControlLogix = new(1756);
        
        /// <summary>
        /// A <see cref="Bulletin"/> instance of the CompactLogix family (Number = 1769).
        /// </summary>
        public static Bulletin CompactLogix = new(1769);
        
        /// <summary>
        /// A <see cref="Bulletin"/> instance of the SoftLogix family (Number = 1789).
        /// </summary>
        public static Bulletin SoftLogix = new(1789);

        /// <summary>
        /// Converts a <see cref="Bulletin"/> value to a <see cref="short"/>.
        /// </summary>
        /// <param name="value">The <see cref="Bulletin"/> value to convert.</param>
        /// <returns>A <see cref="short"/> value representing the converted value.</returns>
        public static implicit operator short(Bulletin value) => value._identifier;
        
        /// <summary>
        /// Converts a <see cref="short"/> value to a <see cref="Bulletin"/> identifier.
        /// </summary>
        /// <param name="value">The <see cref="short"/> value to convert.</param>
        /// <returns>A <see cref="Bulletin"/> identifier representing the converted value.</returns>
        public static implicit operator Bulletin(short value) => new(value);
        
        /// <summary>
        /// Converts a <see cref="string"/> value to a <see cref="Bulletin"/> identifier.
        /// </summary>
        /// <param name="value">The <see cref="string"/> value to convert.</param>
        /// <returns>A <see cref="Bulletin"/> identifier representing the converted value.</returns>
        public static implicit operator Bulletin(string value) => new(value);

        /// <inheritdoc />
        public override string ToString() => _identifier.ToString();

        /// <inheritdoc />
        public bool Equals(Bulletin? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _identifier == other._identifier;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as Bulletin);

        /// <inheritdoc />
        public override int GetHashCode() => _identifier.GetHashCode();

        /// <summary>
        /// Determines if the provided objects are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are equal; otherwise, false.</returns>
        public static bool operator ==(Bulletin? left, Bulletin? right) => Equals(left, right);

        /// <summary>
        /// Determines if the provided objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are not equal; otherwise, false.</returns>
        public static bool operator !=(Bulletin? left, Bulletin? right) => !Equals(left, right);

        /// <inheritdoc />
        public int CompareTo(Bulletin? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : _identifier.CompareTo(other._identifier);
        }
    }
}