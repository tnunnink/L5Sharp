using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// Represents a member component.
    /// </summary>
    /// <remarks>
    /// Members define complex data structures like <see cref="IUserDefined"/>, <see cref="IPredefined"/>, or <see cref="IAddOnDefined"/>.
    /// </remarks>
    /// <typeparam name="TDataType">The <see cref="IDataType"/> that is the member's type</typeparam>
    public interface IMember<out TDataType> : ILogixComponent where TDataType : IDataType
    {
        /// <summary>
        /// The data type <see cref="TDataType"/> of the <see cref="IMember{TDataType}"/> Logix Component.
        /// </summary>
        TDataType DataType { get; }

        /// <summary>
        /// The <see cref="Dimension"/> of the <see cref="IMember{TDataType}"/> Logix Component.
        /// </summary>
        Dimensions Dimension { get; }

        /// <summary>
        /// The <see cref="L5Sharp.Enums.Radix"/> of the <see cref="IMember{TDataType}"/> Logix Component.
        /// </summary>
        Radix Radix { get; }

        /// <summary>
        /// The <see cref="ExternalAccess"/> of the <see cref="IMember{TDataType}"/> Logix Component.
        /// </summary>
        ExternalAccess ExternalAccess { get; }
    }
}