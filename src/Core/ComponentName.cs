using System;
using System.Linq;
using System.Text.RegularExpressions;
using L5Sharp.Exceptions;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents the name of a Logix component.
    /// </summary>
    /// <remarks>
    /// Valid name must contain only alphanumeric or '_', start with a letter, and be between 1 and 40 characters.
    /// </remarks>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer> 
    public class ComponentName : IEquatable<ComponentName>, IComparable<ComponentName>
    {
        private readonly string _name;

        /// <summary>
        /// Creates a new instance of a <see cref="ComponentName"/> with the provided <see cref="string"/>.
        /// </summary>
        /// <remarks>A <see cref="ComponentName"/> is used to uniquely identify any given RSLogix component. </remarks>
        /// <param name="name">The name of the component.
        /// Valid name must contain only alphanumeric or '_', start with a letter, and be less then 40 characters.
        /// </param>
        /// <exception cref="ArgumentException">Throw when the name is null or empty.</exception>
        /// <exception cref="ComponentNameInvalidException">Thrown when the name is invalid.</exception>
        public ComponentName(string name)
        {
            Validate(name);
            /*if (!Regex.IsMatch(name, @"^[a-zA-Z_][a-zA-Z0-9_]{0,39}$", RegexOptions.Compiled))
                throw new ComponentNameInvalidException(name);*/
            _name = name;
        }

        private static void Validate(string name)
        {
            if (string.IsNullOrEmpty(name)) 
                throw new ArgumentException("Name can not be null or empty");
            
            var characters = name.ToCharArray();

            if (!(char.IsLetter(characters.First()) || characters.First() == '_'))
                throw new ComponentNameInvalidException(name);

            if (!characters.All(c => char.IsLetter(c) || char.IsDigit(c) || c == '_'))
                throw new ComponentNameInvalidException(name);

            if (name.Length > 40)
                throw new ComponentNameInvalidException(name);
        }

        /// <summary>
        /// Converts between a <see cref="ComponentName"/> and a <see cref="string"/>
        /// </summary>
        /// <param name="name">The <see cref="ComponentName"/> to instance to convert</param>
        /// <returns>A <see cref="string"/> representing the value of the <see cref="ComponentName"/></returns>
        public static implicit operator string(ComponentName name) =>
            name is not null ? name._name : throw new ArgumentNullException(nameof(name));

        /// <summary>
        /// Converts between a <see cref="string"/> and a <see cref="ComponentName"/>
        /// </summary>
        /// <param name="name">The <see cref="string"/> to value to convert</param>
        /// <returns>A <see cref="ComponentName"/> representing the value </returns>
        public static implicit operator ComponentName(string name) => new(name);

        /// <summary>
        /// Creates a new instance of the current <see cref="ComponentName"/> with the same value.
        /// </summary>
        /// <returns>A new <see cref="ComponentName"/> object with the same value.</returns>
        public ComponentName Copy() => new(string.Copy(_name));

        /// <inheritdoc />
        public override string ToString() => _name;

        /// <summary>
        /// Determines if the current <see cref="ComponentName"/> instance equals another.
        /// </summary>
        /// <param name="other">The other <see cref="ComponentName"/> to compare</param>
        /// <returns><see langword="true"/> when the names are equal or if they refer to the same instance. Otherwise, <see langword="false"/></returns>
        public bool Equals(ComponentName? other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _name == other._name;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ComponentName)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode() => _name.GetHashCode();

        /// <summary>
        /// Determines if two <see cref="ComponentName"/> objects are equal.
        /// </summary>
        /// <param name="left">The left item to compare.</param>
        /// <param name="right">The right item to compare.</param>
        /// <returns>True if they are equal</returns>
        public static bool operator ==(ComponentName left, ComponentName right) => Equals(left, right);

        /// <summary>
        /// Determines if two <see cref="ComponentName"/> objects are not equal.
        /// </summary>
        /// <param name="left">The left item to compare.</param>
        /// <param name="right">The right item to compare.</param>
        /// <returns><see langword="true"/> if they are not equal</returns>
        public static bool operator !=(ComponentName left, ComponentName right) => !Equals(left, right);

        /// <inheritdoc />
        public int CompareTo(ComponentName? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : string.Compare(_name, other._name, StringComparison.Ordinal);
        }
    }
}