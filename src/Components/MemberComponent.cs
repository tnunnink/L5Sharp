using System;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Components
{
    /// <summary>
    /// Static factory class that creates instances of components that implement <see cref="IMember{TDataType}"/>. 
    /// </summary>
    public static class Member
    {
        /// <summary>
        /// Creates a new generic typed Member instance with the specified arguments.
        /// </summary>
        /// <param name="name">The <see cref="ComponentName"/> of the member. Must satisfy Logix naming constraints.</param>
        /// <param name="dataType">The data type of the member.</param>
        /// <param name="dimensions">The dimensions of the member.
        /// If not provided, will default to <see cref="Dimensions.Empty"/>.</param>
        /// <param name="radix">The radix of the member.
        /// If not provided, will default to <see cref="Radix.Default(IDataType)"/>.</param>
        /// <param name="externalAccess">The external access of the member.
        /// If not provided, will default to <see cref="ExternalAccess.ReadWrite"/>.</param>
        /// <param name="description"> The description of the member.
        /// If not provided, will default to <see cref="string.Empty"/>.</param>
        /// <returns>A new member instance with the specified arguments.</returns>
        /// <exception cref="ArgumentNullException">Thrown if name is null.</exception>
        public static IMember<IDataType> Create(ComponentName name, IDataType dataType, Dimensions? dimensions = null,
            Radix? radix = null, ExternalAccess? externalAccess = null, string? description = null) =>
            Create<IDataType>(name, dataType, dimensions, radix, externalAccess, description);

        /// <summary>
        /// Creates a new strongly typed Member instance with the specified arguments.
        /// </summary>
        /// <param name="name">The <see cref="ComponentName"/> of the member. Must satisfy Logix naming constraints.</param>
        /// <param name="dimensions">The dimensions of the member.
        /// If not provided, will default to <see cref="Dimensions.Empty"/>.</param>
        /// <param name="radix">The radix of the member.
        /// If not provided, will default to <see cref="Radix.Default(IDataType)"/>.</param>
        /// <param name="externalAccess">The external access of the member.
        /// If not provided, will default to <see cref="ExternalAccess.ReadWrite"/>.</param>
        /// <param name="description"> The description of the member.
        /// If not provided, will default to <see cref="string.Empty"/>.</param>
        /// <typeparam name="TDataType">
        /// The DataType of the member.
        /// Must implement IDataType and have parameterless constructor for it to be instantiated.
        /// </typeparam>
        /// <returns>A new Member instance with the specified arguments.</returns>
        /// <exception cref="ArgumentNullException">Thrown if name is null.</exception>
        public static IMember<TDataType> Create<TDataType>(ComponentName name, Dimensions? dimensions = null,
            Radix? radix = null, ExternalAccess? externalAccess = null, string? description = null)
            where TDataType : IDataType, new() =>
            Create(name, new TDataType(), dimensions, radix, externalAccess, description);

        /// <summary>
        /// Creates a new strongly typed Member instance with the specified arguments.
        /// </summary>
        /// <param name="name">The <see cref="ComponentName"/> of the member. Must satisfy Logix naming constraints.</param>
        /// <param name="dataType">The data type of the member.</param>
        /// <param name="dimensions">The dimensions of the member.
        /// If not provided, will default to <see cref="Dimensions.Empty"/>.</param>
        /// <param name="radix">The radix of the member.
        /// If not provided, will default to <see cref="Radix.Default(IDataType)"/>.</param>
        /// <param name="externalAccess">The external access of the member.
        /// If not provided, will default to <see cref="ExternalAccess.ReadWrite"/>.</param>
        /// <param name="description"> The description of the member.
        /// If not provided, will default to <see cref="string.Empty"/>.</param>
        /// <typeparam name="TDataType">The DataType of the member.</typeparam>
        /// <returns>A new Member instance with the specified arguments.</returns>
        /// <exception cref="ArgumentNullException">Thrown if name or dataType are null.</exception>
        public static IMember<TDataType> Create<TDataType>(ComponentName name, TDataType dataType,
            Dimensions? dimensions = null, Radix? radix = null, ExternalAccess? externalAccess = null,
            string? description = null) where TDataType : IDataType
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            if (dataType is null)
                throw new ArgumentNullException(nameof(dataType));

            return new Member<TDataType>(name, dataType, dimensions, radix, externalAccess, description);
        }
    }
}