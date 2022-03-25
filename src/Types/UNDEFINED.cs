using System;
using L5Sharp.Enums;

namespace L5Sharp.Types
{
    /// <summary>
    /// Represents a data type that is undefined.
    /// </summary>
    /// <remarks>
    /// This is a replacement for null type. If the data type that is read from a L5X is unknown and can not be created,
    /// Undefined will stand in as the placeholder for the data type.
    /// </remarks>
    public sealed class UNDEFINED : IDataType
    {
        /// <summary>
        /// Creates a new <see cref="UNDEFINED"/> type.
        /// </summary>
        public UNDEFINED()
        {
            Name = nameof(UNDEFINED);
        }
        
        /// <summary>
        /// Creates a new <see cref="UNDEFINED"/> type with the provided type name.
        /// </summary>
        /// <param name="name">Name of the type for which no definition has been found.</param>
        public UNDEFINED(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name can not be null or empty.");
            
            Name = name;
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description => string.Empty;

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.Unknown;

        /// <inheritdoc />
        public IDataType Instantiate() => new UNDEFINED(Name);
    }
}