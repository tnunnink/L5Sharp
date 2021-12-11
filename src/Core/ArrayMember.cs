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
        private readonly List<IMember<TDataType>> _elements = new();

        internal ArrayMember(string name, TDataType seedType, Dimensions dimensions,
            Radix? radix, ExternalAccess? externalAccess, string? description)
        {
            if (seedType is null)
                throw new ArgumentNullException(nameof(seedType));

            Name = name ?? throw new ArgumentNullException(nameof(name));
            Dimensions = dimensions ?? throw new ArgumentNullException(nameof(dimensions));
            Radix = radix is not null && radix.SupportsType(seedType) ? radix : Radix.Default(seedType);
            ExternalAccess = externalAccess ?? ExternalAccess.ReadWrite;
            Description = description ?? string.Empty;
            _elements.AddRange(Dimensions.GenerateMembers(seedType, Radix, ExternalAccess, Description));
        }

        internal ArrayMember(string name, List<TDataType> elements, Dimensions? dimensions,
            Radix? radix, ExternalAccess? externalAccess, string? description)
        {
            if (elements is null)
                throw new ArgumentNullException(nameof(elements));

            if (elements.Count > ushort.MaxValue)
                throw new ArgumentException(
                    $"Elements length '{elements.Count}' must be less than '{ushort.MaxValue}'.");

            Name = name ?? throw new ArgumentNullException(nameof(name));
            Dimensions = dimensions ?? new Dimensions((ushort)elements.Count);
            Radix = radix is not null && radix.SupportsType(typeof(TDataType)) 
                ? radix : Radix.Default(typeof(TDataType));
            ExternalAccess = externalAccess ?? ExternalAccess.ReadWrite;
            Description = description ?? string.Empty;
            _elements.AddRange(Dimensions.GenerateMembers(elements, Radix, ExternalAccess, Description));
        }

        /// <inheritdoc />
        public IMember<TDataType> this[int index] => _elements[index];

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description { get; }

        /// <inheritdoc />
        /// <remarks>
        /// The data type instance of the array member will always be the first index of the array.
        /// </remarks>
        public TDataType DataType => _elements[0].DataType;

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
            return new ArrayMember<TDataType>(string.Copy(Name), DataType, Dimensions.Copy(),
                Radix, ExternalAccess, string.Copy(Description));
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
            return HashCode.Combine(Name, DataType, Dimensions, ExternalAccess, Description, _elements);
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