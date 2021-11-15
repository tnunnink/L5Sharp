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
        /// The <see cref="Dimensions"/> of the <see cref="IMember{TDataType}"/> Logix Component.
        /// </summary>
        Dimensions Dimensions { get; }

        /// <summary>
        /// The <see cref="L5Sharp.Enums.Radix"/> of the <see cref="IMember{TDataType}"/> Logix Component.
        /// </summary>
        Radix Radix { get; }

        /// <summary>
        /// The <see cref="ExternalAccess"/> of the <see cref="IMember{TDataType}"/> Logix Component.
        /// </summary>
        ExternalAccess ExternalAccess { get; }

        /// <summary>
        /// Sets the property <see cref="Radix"/> of the <see cref="IMember{TDataType}"/> Logix Component.
        /// </summary>
        /// <param name="radix">The value to set <see cref="Radix"/></param>
        void SetRadix(Radix radix);

        /// <summary>
        /// Sets the Description property of the <see cref="IMember{TDataType}"/> Logix Component.
        /// </summary>
        /// <param name="description"></param>
        void SetDescription(string description);
    }
}