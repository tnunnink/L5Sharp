using System;
using System.Collections;
using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public class ArrayMember<TDataType> : IArrayMember<TDataType> where TDataType : IDataType
    {
        private readonly IElement<TDataType>[] _elements;

        internal ArrayMember(ComponentName name, TDataType dataType, Dimensions dimensions, Radix radix,
            ExternalAccess externalAccess, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            DataType = dataType;
            Dimension = dimensions ?? throw new ArgumentNullException(nameof(dimensions));
            if (Dimension.AreMultiDimensional)
                throw new ArgumentException("Member can only have single dimensional arrays");
            if (DataType is IAtomic atomic && radix != null)
                atomic.SetRadix(radix);
            ExternalAccess = externalAccess ?? ExternalAccess.ReadWrite;
            Description = description;
            _elements = new IElement<TDataType>[dimensions];
            for (var i = 0; i < dimensions; i++)
                _elements[i] = new Element<TDataType>(i, (TDataType)dataType.Instantiate(), radix, externalAccess,
                    description);
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
            return (IEnumerator<IElement<TDataType>>)_elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}