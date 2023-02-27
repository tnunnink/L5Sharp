using System;
using L5Sharp.Components;
using L5Sharp.Rockwell;

namespace L5Sharp.Common
{
    /// <summary>
    /// An entity that represents the type of product of a given <see cref="Module"/>.
    /// </summary>
    /// <remarks>
    /// This object is a simple entity type wrapper that groups the product type id and name.
    /// Product types are defined by Rockwell and assigned unique Id and name.
    /// This information is obtained from the L5X or from the <see cref="ModuleCatalog"/> service by lookup of a given
    /// <see cref="CatalogNumber"/>. Some known/common types that are available as static members of this class
    /// include <see cref="Discrete"/>, <see cref="Analog"/>, <see cref="Controller"/>, and <see cref="Communications"/>.
    /// </remarks>
    public class ProductType : IEquatable<ProductType>
    {
        /// <summary>
        /// Creates a new <see cref="ProductType"/> entity with the provided id and name.
        /// </summary>
        /// <param name="id">The unique Id of the ProductType.</param>
        /// <param name="name">The name of the ProductType. Will default to empty if not provided.</param>
        public ProductType(ushort id, string? name = null)
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

        /// <summary>
        /// Represents an Unknown <see cref="ProductType"/> with no id or name.
        /// </summary>
        public static ProductType Unknown => new(0);
        
        /// <summary>
        /// Gets the General Purpose Discrete I/O ProductType instance (id=7).
        /// </summary>
        public static ProductType Discrete => new(7, "General Purpose Discrete I/O");
        
        /// <summary>
        /// Gets the General Purpose Analog I/O ProductType instance (id=10).
        /// </summary>
        public static ProductType Analog => new(10, "General Purpose Analog I/O");
        
        /// <summary>
        /// Gets the General Purpose Discrete ProductType instance (id=14).
        /// </summary>
        public static ProductType Controller => new(14, "Programmable Logic Controller");
        
        /// <summary>
        /// Gets the General Purpose Discrete ProductType instance (id=12).
        /// </summary>
        public static ProductType Communications => new(12, "Communications Adapter");

        /// <summary>
        /// Converts a <see cref="ProductType"/> object to a <see cref="ushort"/> that represents the Id.
        /// </summary>
        /// <param name="productType">The <see cref="ProductType"/> object to convert.</param>
        /// <returns>A <see cref="ushort"/> representing the value of the ProductType Id.</returns>
        public static implicit operator ushort(ProductType productType) => productType.Id;

        /// <summary>
        /// Converts a <see cref="ushort"/> value to a <see cref="ProductType"/> object that represents the Id.
        /// </summary>
        /// <param name="productTypeId">The <see cref="ushort"/> value to convert.</param>
        /// <returns>A <see cref="ProductType"/> with the Id of the converted value.</returns>
        public static implicit operator ProductType(ushort productTypeId) => new(productTypeId);

        /// <summary>
        /// Creates a new <see cref="ProductType"/> using the provided product Id.
        /// </summary>
        /// <param name="productId">The unique valid that identifies the Product.</param>
        /// <returns>A new <see cref="ProductType"/> object with the provided Id.</returns>
        public static ProductType Parse(string productId) =>
            ushort.TryParse(productId, out var id) ? new ProductType(id) : Unknown;

        /// <inheritdoc />
        public override string ToString() => Name;

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