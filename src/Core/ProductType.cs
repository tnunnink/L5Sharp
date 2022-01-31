using System;

namespace L5Sharp.Core
{
    /// <summary>
    /// An entity that represents the Product type of a given <see cref="IModule"/>.
    /// </summary>
    /// <remarks>
    /// Product types are defined by Rockwell and assigned unique Id
    /// </remarks>
    public class ProductType : IEquatable<ProductType>
    {
        private ProductType(ushort id, string? name = null)
        {
            Id = id;
            Name = name ?? string.Empty;
        }
        
        /// <summary>
        /// Gets the value that uniquely identifies the <see cref="ProductType"/>. 
        /// </summary>
        public ushort Id { get; }
        
        /// <summary>
        /// Gets the value that represents the <see cref="ProductType"/> name.
        /// </summary>
        public string Name { get; }
        
        public static implicit operator ushort(ProductType vendor) => vendor.Id;
        
        public static implicit operator ProductType(ushort vendorId) => new(vendorId);

        /// <inheritdoc />
        public bool Equals(ProductType? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as ProductType);

        /// <inheritdoc />
        public override int GetHashCode() => Id.GetHashCode();

        /// <summary>
        /// Determines if the provided objects are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are equal; otherwise, false.</returns>
        public static bool operator ==(ProductType? left, ProductType? right) => Equals(left, right);

        /// <summary>
        /// Determines if the provided objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are not equal; otherwise, false.</returns>
        public static bool operator !=(ProductType? left, ProductType? right) => !Equals(left, right);
    }
}