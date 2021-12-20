using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc cref="IMember{TDataType}" />
    public sealed class Member<TDataType> : IMember<TDataType>, IEquatable<Member<TDataType>>
        where TDataType : IDataType
    {
        private readonly List<IMember<TDataType>> _elements = new();
        
        internal Member(string name, TDataType dataType, Dimensions? dimension,
            Radix? radix, ExternalAccess? externalAccess, string? description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            DataType = dataType ?? throw new ArgumentNullException(nameof(dataType));
            Dimension = dimension ?? Dimensions.Empty;
            Radix = radix is not null && radix.SupportsType(DataType) ? radix : Radix.Default(DataType);
            ExternalAccess = externalAccess ?? ExternalAccess.ReadWrite;
            Description = description ?? string.Empty;
            
            _elements.AddRange(Dimension.GenerateMembers(DataType, Radix, ExternalAccess, Description));
        }
        
        internal Member(string name, List<TDataType> elements, Dimensions? dimension,
            Radix? radix, ExternalAccess? externalAccess, string? description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            DataType = elements.Count > 0 ? elements[0] : throw new ArgumentNullException(nameof(elements));
            Dimension = dimension ?? Dimensions.Empty;
            Radix = radix is not null && radix.SupportsType(DataType) ? radix : Radix.Default(DataType);
            ExternalAccess = externalAccess ?? ExternalAccess.ReadWrite;
            Description = description ?? string.Empty;
            
            _elements.AddRange(Dimension.GenerateMembers(elements, Radix, ExternalAccess, Description));
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description { get; }

        /// <inheritdoc />
        public TDataType DataType { get; }

        /// <inheritdoc />
        public Dimensions Dimension { get; }

        /// <inheritdoc />
        public Radix Radix { get; }

        /// <inheritdoc />
        public ExternalAccess ExternalAccess { get; }
        
        /// <inheritdoc />
        public IMember<TDataType> this[int index] => _elements[index];

        /// <inheritdoc />
        public IMember<TDataType> Copy() =>
            new Member<TDataType>(string.Copy(Name), (TDataType)DataType.Instantiate(), Dimension.Copy(),
                Radix, ExternalAccess, string.Copy(Description));

        /// <inheritdoc />
        public bool Equals(Member<TDataType>? other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name
                   && Equals(DataType, other.DataType)
                   && Equals(Dimension, other.Dimension)
                   && Equals(Radix, other.Radix)
                   && Equals(ExternalAccess, other.ExternalAccess)
                   && Description == other.Description
                   && _elements.SequenceEqual(other._elements);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Member<TDataType>)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode() =>
            HashCode.Combine(Name, DataType, Dimension, ExternalAccess, Description, _elements);

        /// <summary>
        /// Determines whether two objects are equal. 
        /// </summary>
        /// <param name="left">The left object to compare.</param>
        /// <param name="right">The right object to compare.</param>
        /// <returns>True if the left object is equal to the right object. Otherwise, False</returns>
        public static bool operator ==(Member<TDataType> left, Member<TDataType> right) => Equals(left, right);

        /// <summary>
        /// Determines whether two objects are not equal. 
        /// </summary>
        /// <param name="left">The left object to compare.</param>
        /// <param name="right">The right object to compare.</param>
        /// <returns>True if the left object is not equal to the right object. Otherwise, False</returns>
        public static bool operator !=(Member<TDataType> left, Member<TDataType> right) => !Equals(left, right);
        
        /// <inheritdoc />
        public IEnumerator<IMember<TDataType>> GetEnumerator() => _elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}