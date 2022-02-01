using System;

namespace L5Sharp.Core
{
    /// <summary>
    /// An entity that identifies a Logix Module Vendor. 
    /// </summary>
    /// <remarks>
    /// The L5X only exports the Vendor Id.
    /// </remarks>
    public class Vendor : IEquatable<Vendor>
    {
        internal Vendor(ushort id, string? name = null)
        {
            Id = id;
            Name = name ?? string.Empty;
        }
        
        /// <summary>
        /// Gets the value that uniquely identifies the <see cref="Vendor"/>. 
        /// </summary>
        /// <remarks>
        /// This value is exported in the L5X and is used for Vendor name lookups.
        /// </remarks>
        public ushort Id { get; }
        
        /// <summary>
        /// Gets the value that represents the <see cref="Vendor"/> name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Creates an instance of an unknown <see cref="Vendor"/>
        /// </summary>
        public static Vendor Unknown => new(0);

        /// <summary>
        /// Converts a <see cref="Vendor"/> object to a <see cref="ushort"/> that represents the Id.
        /// </summary>
        /// <param name="vendor">The <see cref="Vendor"/> object to convert.</param>
        /// <returns>A <see cref="ushort"/> representing the value of the Vendor Id.</returns>
        public static implicit operator ushort(Vendor vendor) => vendor.Id;
        
        /// <summary>
        /// Converts a <see cref="ushort"/> value to a <see cref="Vendor"/> object that represents the Id.
        /// </summary>
        /// <param name="vendorId">The <see cref="ushort"/> value to convert.</param>
        /// <returns>A <see cref="Vendor"/> with the Id of the converted value.</returns>
        public static implicit operator Vendor(ushort vendorId) => new(vendorId);

        /// <inheritdoc />
        public bool Equals(Vendor? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as Vendor);

        /// <inheritdoc />
        public override int GetHashCode() => Id.GetHashCode();

        /// <summary>
        /// Determines if the provided objects are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are equal; otherwise, false.</returns>
        public static bool operator ==(Vendor? left, Vendor? right) => Equals(left, right);

        /// <summary>
        /// Determines if the provided objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are not equal; otherwise, false.</returns>
        public static bool operator !=(Vendor? left, Vendor? right) => !Equals(left, right);
    }
}