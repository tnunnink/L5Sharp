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
        private ProductType(ushort id, ushort code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }
        
        /// <summary>
        /// Gets the value that uniquely identifies the <see cref="ProductType"/>. 
        /// </summary>
        public ushort Id { get; }
        
        public ushort Code { get; }
        public string Name { get; }
        
        public static ProductType Unkown => new(0, 0, string.Empty);

        /// <inheritdoc />
        public bool Equals(ProductType? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ProductType)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(ProductType? left, ProductType? right) => Equals(left, right);

        public static bool operator !=(ProductType? left, ProductType? right) => !Equals(left, right);
    }
}