using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc cref="L5Sharp.IArrayMember{TDataType}" />
    public class ArrayMember<TDataType> : IArrayMember<TDataType>, IEquatable<ArrayMember<TDataType>>
        where TDataType : IDataType
    {
        private readonly List<IElement<TDataType>> _elements;

        internal ArrayMember(ComponentName name, TDataType dataType, Dimensions dimensions, Radix radix,
            ExternalAccess externalAccess, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            DataType = dataType;
            Dimension = dimensions ?? throw new ArgumentNullException(nameof(dimensions));
            ExternalAccess = externalAccess ?? ExternalAccess.ReadWrite;
            Description = description;

            if (DataType is IAtomic atomic && radix != null)
                atomic.SetRadix(radix);

            _elements = new List<IElement<TDataType>>(dimensions.Length);
            var indices = Dimension.GenerateIndices().ToList();

            for (var i = 0; i < Dimension; i++)
            {
                var element = new Element<TDataType>(indices[i], (TDataType)DataType.Instantiate(),
                    Radix, ExternalAccess, Description);
                _elements.Add(element);
            }
        }

        /// <inheritdoc />
        public IElement<TDataType> this[int index] => _elements[index];

        /// <inheritdoc />
        public ComponentName Name { get; }

        /// <inheritdoc />
        public TDataType DataType { get; }

        /// <inheritdoc />
        public Dimensions Dimension { get; }

        /// <inheritdoc />
        public Radix Radix => DataType.Radix;

        /// <inheritdoc />
        public ExternalAccess ExternalAccess { get; }

        /// <inheritdoc />
        public string Description { get; }

        /// <inheritdoc />
        public IEnumerator<IElement<TDataType>> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public bool Equals(ArrayMember<TDataType> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Name, other.Name)
                   && EqualityComparer<TDataType>.Default.Equals(DataType, other.DataType)
                   && Equals(Dimension, other.Dimension)
                   && Equals(ExternalAccess, other.ExternalAccess)
                   && Description == other.Description
                   && _elements.SequenceEqual(other._elements);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ArrayMember<TDataType>)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(_elements, Name, DataType, Dimension, ExternalAccess, Description);
        }

        /// <summary>
        /// Indicates whether one object is equal to another object of the same type.
        /// </summary>
        /// <param name="left">The left instance of the object.</param>
        /// <param name="right">The right instance of the object.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public static bool operator ==(ArrayMember<TDataType> left, ArrayMember<TDataType> right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Indicates whether one object is not equal to another object of the same type.
        /// </summary>
        /// <param name="left">The left instance of the object.</param>
        /// <param name="right">The right instance of the object.</param>
        /// <returns>True if the two objects are not equal, otherwise false.</returns>
        public static bool operator !=(ArrayMember<TDataType> left, ArrayMember<TDataType> right)
        {
            return !Equals(left, right);
        }
    }
}