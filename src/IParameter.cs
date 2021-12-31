using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// Represents Logix Parameter, or a member of the <see cref="IAddOnInstruction"/> component.
    /// </summary>
    /// <remarks>
    /// <c>IParameter</c> is a specialized type of <c>IMember</c> that adds properties to determine the usage for the
    /// parent <c>IAddOnInstruction</c>.
    /// </remarks>
    /// <typeparam name="TDataType">The <see cref="IDataType"/> that the <c>IParameter</c> represents.</typeparam>
    public interface IParameter<out TDataType> : IMember<TDataType> where TDataType : IDataType
    {
        /// <summary>
        /// Gets the <c>TagType</c> value of the current <c>IParameter</c>.
        /// </summary>
        /// <remarks>
        /// Parameters can only be <see cref="Enums.TagType.Base"/> or <see cref="Enums.TagType.Alias"/>.
        /// </remarks>
        /// <value>
        /// Determined by the value of <see cref="Alias"/>.
        /// If the property is not null, then the <c>IParameter</c> object is an aliased parameter. 
        /// </value>
        TagType TagType { get; }
        
        /// <summary>
        /// Gets the <c>TagUsage</c> value of the current <c>IParameter</c>.
        /// </summary>
        TagUsage Usage { get; }
        
        /// <summary>
        /// Gets the <c>bool</c> indicating if the current <c>IParameter</c> is required.
        /// </summary>
        bool Required { get; }
        
        /// <summary>
        /// Gets the <c>bool</c> indicating if the current <c>IParameter</c> is visible.
        /// </summary>
        bool Visible { get; }
        
        /// <summary>
        /// Gets the <c>ITag</c> that represents the alias of the current <c>IParameter</c>.
        /// </summary>
        string? Alias { get; }
        
        /// <summary>
        /// Gets the <c>IAtomicType</c> value that represents the default value of the <c>IParameter</c>.
        /// </summary>
        IAtomicType? Default { get; }
        
        /// <summary>
        /// Gets the <c>bool</c> indicating if the current <c>IParameter</c> is a constant.
        /// </summary>
        bool Constant { get; }
    }
}