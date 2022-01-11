using System;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc cref="IMember{TDataType}" />
    public class Member<TDataType> : IMember<TDataType> where TDataType : IDataType
    {
        internal Member(string name, TDataType dataType, Radix? radix = null, ExternalAccess? externalAccess = null,
            string? description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            DataType = dataType ?? throw new ArgumentNullException(nameof(dataType));
            Dimensions = dataType is IArrayType<IDataType> arrayType ? arrayType.Dimensions : Dimensions.Empty;
            Radix = radix is not null && radix.SupportsType(DataType) ? radix : Radix.Default(DataType);
            ExternalAccess = externalAccess ?? ExternalAccess.ReadWrite;
            Description = description ?? string.Empty;
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description { get; }

        /// <inheritdoc />
        public TDataType DataType { get; }

        /// <inheritdoc />
        public Dimensions Dimensions { get; }

        /// <inheritdoc />
        public Radix Radix { get; }

        /// <inheritdoc />
        public ExternalAccess ExternalAccess { get; }

        /// <inheritdoc />
        public bool IsValueMember => DataType is IAtomicType;

        /// <inheritdoc />
        public bool IsStructureMember => DataType is IComplexType;

        /// <inheritdoc />
        public bool IsArrayMember => DataType is IArrayType<IDataType>;
    }
}