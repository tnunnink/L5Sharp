using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public class Element<TDataType> : IElement<TDataType> where TDataType : IDataType
    {
        internal Element(int index, TDataType dataType, Radix radix, ExternalAccess access, string description)
        {
            Name = $"[{index}]";
            DataType = dataType;
            if (DataType is IAtomic atomic && radix != null)
                atomic.SetRadix(radix);
            ExternalAccess = access ?? ExternalAccess.ReadWrite;
            Description = description;
        }

        /// <inheritdoc />
        public string Name { get; }

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
    }
}