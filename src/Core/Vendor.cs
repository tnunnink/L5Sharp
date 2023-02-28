using System;
using L5Sharp.Components;
using L5Sharp.Rockwell;

namespace L5Sharp.Core
{
    /// <summary>
    /// An entity that represents the vendor of a given <see cref="Module"/>.
    /// </summary>
    /// <remarks>
    /// This object is a simple entity type wrapper that groups the vendor id and name.
    /// Vendor's are defined by Rockwell and assigned unique Id and name.
    /// This information is obtained from the L5X or from the <see cref="ModuleCatalog"/> service by lookup of a given
    /// <see cref="CatalogNumber"/>. Use <see cref="Rockwell"/> as it is the most common vendor for compatible devices.
    /// </remarks>
    public class Vendor : IEquatable<Vendor>
    {
        /// <summary>
        /// Creates a new <see cref="Vendor"/> value with the provided id and name.
        /// </summary>
        /// <param name="id">The unique Id of the Vendor.</param>
        /// <param name="name">The name of the Vendor. Will default to empty if not provided.</param>
        public Vendor(ushort id, string? name = null)
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
        /// Represents an Unknown <see cref="Vendor"/> with no id or name.
        /// </summary>
        public static Vendor Unknown => new(0);

        /// <summary>
        /// Gets the Rockwell Automation Vendor instance (id=1).
        /// </summary>
        public static Vendor Rockwell => new(1, "Rockwell Automation/Allen-Bradley");

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

        /// <summary>
        /// Creates a new <see cref="Vendor"/> using the provided vendor Id.
        /// </summary>
        /// <param name="vendorId">The unique valid that identifies the Vendor.</param>
        /// <returns>A new <see cref="Vendor"/> object with the provided Id.</returns>
        public static Vendor Parse(string vendorId) => ushort.TryParse(vendorId, out var id) ? new Vendor(id) : Unknown;

        /// <inheritdoc />
        public override string ToString() => Name;

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