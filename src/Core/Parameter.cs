using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc cref="L5Sharp.IParameter{TDataType}" />
    public sealed class Parameter<TDataType> : Member<TDataType>, IParameter<TDataType> where TDataType : IDataType
    {
        internal Parameter(string name, TDataType dataType, Radix? radix = null,
            ExternalAccess? externalAccess = null, TagUsage? usage = null, TagName? alias = null,
            bool required = false, bool visible = false, bool constant = false,
            string? description = null) : base(name, dataType, radix, externalAccess, description)
        {
            Usage = usage ?? TagUsage.Default(dataType);
            Alias = alias ?? TagName.Empty;
            Default = DataType is IAtomicType atomicType ? atomicType : default;
            Required = Usage == TagUsage.InOut || required;
            Visible = Required || visible;
            Constant = constant;
        }

        /// <summary>
        /// Creates a new <see cref="Parameter{TDataType}"/> object with the provided name, data type instance, and
        /// optional parameters for configuring the parameter.
        /// </summary>
        /// <param name="name">The name of the parameter to create.</param>
        /// <param name="dataType">The <see cref="IDataType"/> instance of the parameter.</param>
        /// <param name="usage">The <see cref="TagUsage"/> of the parameter.
        /// If not provided, will default based on the provided data type.</param>
        /// <param name="radix">The <see cref="Enums.Radix"/> of the parameter.
        /// If not provided, will default based on the provided data type.</param>
        /// <param name="externalAccess">The <see cref="Enums.ExternalAccess"/> of the parameter.
        /// If not provided, will default to Read/Write.</param>
        /// <param name="required">The value indicating whether the parameter is required by the instruction.
        /// Required parameters are those that show up in the instruction arguments. All non-atomic parameters
        /// are required by default since the user must provide a tag reference.
        /// All required parameters are also visible. If not provided, will default to false.
        /// </param>
        /// <param name="visible">The value indicating whether the parameter is visible on the instruction interface.
        /// Visible parameters are those that will be displayed on the instruction interface. All non-atomic parameters
        /// are visible by default. If not provided, will default to false. 
        /// </param>
        /// <param name="alias"></param>
        /// <param name="constant">A value indicating whether the parameter is a constant value.
        /// If not provided, will default to false.</param>
        /// <param name="description">A string description of the parameter.
        /// If not provided, will default to an empty string.</param>
        public Parameter(ComponentName name, TDataType dataType, TagUsage? usage = null,
            Radix? radix = null, ExternalAccess? externalAccess = null,
            bool required = default, bool visible = default, TagName? alias = null, bool constant = default,
            string? description = null) 
            : this(name, dataType, radix, externalAccess, usage, alias, required, visible, constant, description)
        {
        }

        /// <inheritdoc />
        public TagType TagType => Alias.IsEmpty ? TagType.Base : TagType.Alias;

        /// <inheritdoc />
        public TagUsage Usage { get; }

        /// <inheritdoc />
        public bool Required { get; }

        /// <inheritdoc />
        public bool Visible { get; }

        /// <inheritdoc />
        public TagName Alias { get; }

        /// <inheritdoc />
        public IAtomicType? Default { get; }

        /// <inheritdoc />
        public bool Constant { get; }
    }
}