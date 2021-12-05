using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc cref="L5Sharp.IArrayMember{TDataType}" />
    public class ArrayMember<TDataType> : IArrayMember<TDataType>, IEquatable<ArrayMember<TDataType>>
        where TDataType : IDataType
    {
        private readonly List<IMember<TDataType>> _elements;

        internal ArrayMember(string name, TDataType dataType, Dimensions dimensions, Radix? radix,
            ExternalAccess? externalAccess, string? description, IEnumerable<IMember<IDataType>>? elements)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            DataType = dataType;
            Dimensions = dimensions ?? throw new ArgumentNullException(nameof(dimensions));
            Radix = radix ?? Radix.Default(dataType);
            ExternalAccess = externalAccess ?? ExternalAccess.ReadWrite;
            Description = description ?? string.Empty;

            _elements = new List<IMember<TDataType>>(dimensions.Length);
            
            var indices = Dimensions.GenerateIndices().ToList();
            
            for (var i = 0; i < Dimensions; i++)
            {
                var element = new Member<TDataType>(indices[i], (TDataType)DataType.Instantiate(),
                    Radix, ExternalAccess, string.Copy(Description));
                _elements.Add(element);
            }
            
            if (elements != null) return;
            
            //todo initialize elements / assign value.
        }

        /// <inheritdoc />
        public IMember<TDataType> this[int index] => _elements[index];

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description { get; }

        /// <inheritdoc />
        public TDataType DataType { get; }

        /// <inheritdoc />
        [XmlAttribute("Dimension")] // accounts for naming difference between data type members and tags.
        public Dimensions Dimensions { get; }

        /// <inheritdoc />
        public Radix Radix { get; }

        /// <inheritdoc />
        public ExternalAccess ExternalAccess { get; }

        /// <inheritdoc />
        public IEnumerator<IMember<TDataType>> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public IMember<TDataType> Copy()
        {
            return new ArrayMember<TDataType>(string.Copy(Name), (TDataType)DataType.Instantiate(), Dimensions.Copy(),
                Radix, ExternalAccess, string.Copy(Description), Enumerable.Empty<IMember<IDataType>>());
        }

        /// <inheritdoc />
        public bool Equals(ArrayMember<TDataType>? other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Name, other.Name)
                   && EqualityComparer<TDataType>.Default.Equals(DataType, other.DataType)
                   && Equals(Dimensions, other.Dimensions)
                   && Equals(ExternalAccess, other.ExternalAccess)
                   && Description == other.Description
                   && _elements.SequenceEqual(other._elements);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ArrayMember<TDataType>)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(_elements, Name, DataType, Dimensions, ExternalAccess, Description);
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