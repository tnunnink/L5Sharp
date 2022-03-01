using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc cref="L5Sharp.IParameter{TDataType}" />
    public class Parameter<TDataType> : Member<TDataType>, IParameter<TDataType> where TDataType : IDataType
    {
        internal Parameter(string name, TDataType dataType, Radix? radix = null,
            ExternalAccess? externalAccess = null, TagUsage? usage = null, string? alias = null,
            bool required = false, bool visible = false, bool constant = false,
            string? description = null) : base(name, dataType, radix, externalAccess, description)
        {
            Usage = usage ?? TagUsage.Default(dataType);
            Alias = alias;
            Default = DataType is IAtomicType atomicType ? atomicType : default;
            Required = Usage == TagUsage.InOut || required;
            Visible = Required || visible;
            Constant = constant;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">The name of the parameter to create.</param>
        /// <param name="dataType">The <see cref="IDataType"/> instance of the parameter.</param>
        /// <param name="usage">The <see cref="TagUsage"/> of the parameter.
        /// If not provided will default based on the provided data type.</param>
        /// <param name="radix">The <see cref="Enums.Radix"/> of the parameter.
        /// If not provided will default based on the provided data type.</param>
        /// <param name="externalAccess"></param>
        /// <param name="required"></param>
        /// <param name="visible"></param>
        /// <param name="alias"></param>
        /// <param name="constant"></param>
        /// <param name="description"></param>
        public Parameter(ComponentName name, TDataType dataType, TagUsage? usage = null,
            Radix? radix = null, ExternalAccess? externalAccess = null,
            bool required = default, bool visible = default, string? alias = null, bool constant = default,
            string? description = null) 
            : this(name, dataType, radix, externalAccess, usage, alias, required, visible, constant, description)
        {
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