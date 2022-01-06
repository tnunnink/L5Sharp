using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc cref="L5Sharp.IParameter{TDataType}" />
    public class Parameter<TDataType> : Member<TDataType>, IParameter<TDataType> where TDataType : IDataType
    {
        internal Parameter(string name, TDataType dataType, Dimensions? dimensions = null, Radix? radix = null,
            ExternalAccess? externalAccess = null, string? description = null, TagUsage? usage = null, 
            string? alias = null, bool required = false, bool visible = false, bool constant = false)
            : base(name, dataType, radix, externalAccess, description)
        {
            
            Usage = usage is not null && usage.SupportsType(DataType.GetType()) 
                ? usage :
                DataType is IAtomicType ? TagUsage.Input : TagUsage.InOut;
            Alias = alias;
            Default = DataType is IAtomicType atomicType ? atomicType : default;
            Required = Usage == TagUsage.InOut || required;
            Visible = Required || visible;
            Constant = constant;
        }

        /// <inheritdoc />
        public TagType TagType => Alias is null ? TagType.Base : TagType.Alias;

        /// <inheritdoc />
        public TagUsage Usage { get; }

        /// <inheritdoc />
        public bool Required { get; }

        /// <inheritdoc />
        public bool Visible { get; }

        /// <inheritdoc />
        public string? Alias { get; }

        /// <inheritdoc />
        public IAtomicType? Default { get; }

        /// <inheritdoc />
        public bool Constant { get; }
    }
}