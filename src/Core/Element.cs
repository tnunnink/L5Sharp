using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public class Element<TDataType> : IElement<TDataType> where TDataType : IDataType
    {
        internal Element(int index, TDataType dataType, Radix radix, ExternalAccess access, string comment)
        {
            Index = $"[{index}]";
            DataType = dataType;
            if (DataType is IAtomic atomic)
                atomic.SetRadix(radix);
            ExternalAccess = access;
            Comment = comment;
        }

        /// <inheritdoc />
        public string Index { get; }

        /// <inheritdoc />
        public TDataType DataType { get; }

        /// <inheritdoc />
        public Radix Radix => DataType.Radix;

        /// <inheritdoc />
        public ExternalAccess ExternalAccess { get; }

        /// <inheritdoc />
        public string Comment { get; }
    }
}