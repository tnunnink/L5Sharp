using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using L5Sharp.Enums;

[assembly: InternalsVisibleTo("L5Sharp.Core.Tests")]

namespace L5Sharp.Core
{
    /// <inheritdoc cref="L5Sharp.IElement{TDataType}" />
    public class Element<TDataType> : IElement<TDataType>, IEquatable<Element<TDataType>> where TDataType : IDataType
    {
        internal Element(string index, TDataType dataType, 
            Radix radix = null, ExternalAccess access = null, string description = null)
        {
            Name = index;
            DataType = dataType;
            ExternalAccess = access ?? ExternalAccess.ReadWrite;
            Description = description;

            if (DataType is IAtomic atomic && radix != null)
                atomic.SetRadix(radix);
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <summary>
        /// Just setting this to null since it will throw an exception if we assign the actual element name
        /// </summary>
        ComponentName ILogixComponent.Name => null;

        /// <inheritdoc />
        public TDataType DataType { get; }

        /// <inheritdoc />
        public Dimensions Dimension => Dimensions.Empty;

        /// <inheritdoc />
        public Radix Radix => DataType.Radix;

        /// <inheritdoc />
        public ExternalAccess ExternalAccess { get; }

        /// <inheritdoc />
        public string Description { get; }

        /// <inheritdoc />
        public bool Equals(Element<TDataType> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name
                   && EqualityComparer<TDataType>.Default.Equals(DataType, other.DataType)
                   && Equals(ExternalAccess, other.ExternalAccess)
                   && Description == other.Description;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Element<TDataType>)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, DataType, ExternalAccess, Description);
        }

        /// <summary>
        /// Indicates whether one object is equal to another object of the same type.
        /// </summary>
        /// <param name="left">The left instance of the object.</param>
        /// <param name="right">The right instance of the object.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public static bool operator ==(Element<TDataType> left, Element<TDataType> right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Indicates whether one object is not equal to another object of the same type.
        /// </summary>
        /// <param name="left">The left instance of the object.</param>
        /// <param name="right">The right instance of the object.</param>
        /// <returns>True if the two objects are not equal, otherwise false.</returns>
        public static bool operator !=(Element<TDataType> left, Element<TDataType> right)
        {
            return !Equals(left, right);
        }
    }
}