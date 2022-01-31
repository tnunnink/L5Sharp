using System;

namespace L5Sharp.Core
{
    /// <summary>
    /// An entity that identifies a Logix Vendor. 
    /// </summary>
    /// <remarks>
    /// The L5X only exports the Vendor Id.
    /// </remarks>
    public class Vendor : IEquatable<Vendor>
    {
        private Vendor(ushort id, string? name = null)
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

        public static implicit operator ushort(Vendor vendor) => vendor.Id;
        
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