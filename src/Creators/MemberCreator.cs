using System;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Creators
{
    /// <summary>
    /// Static factory class that creates instances of components that implement <see cref="IMember{TDataType}"/>. 
    /// </summary>
    public static class Member
    {
        /// <summary>
        /// Creates a new generic typed Member instance with the specified arguments.
        /// </summary>
        /// <param name="name">The <see cref="ComponentName"/> of the member.</param>
        /// <param name="dataType">The <see cref="IDataType"/> instance of the member.</param>
        /// <param name="radix">The optional <see cref="Enums.Radix"/> of the member.
        /// If not provided, will default to <see cref="Radix.Default(IDataType)"/>.</param>
        /// <param name="externalAccess">The optional <see cref="Enums.ExternalAccess"/> of the member.
        /// If not provided, will default to <see cref="ExternalAccess.ReadWrite"/>.</param>
        /// <param name="description">The optional description of the member.
        /// If not provided, will default to <see cref="string.Empty"/>.</param>
        /// <returns>A generic <see cref="IMember{TDataType}"/> instance with the specified arguments.</returns>
        /// <exception cref="ArgumentNullException">name or dataType is null.</exception>
        public static IMember<IDataType> Create(ComponentName name, IDataType dataType,
            Radix? radix = null, ExternalAccess? externalAccess = null, string? description = null) =>
            Create<IDataType>(name, dataType, radix, externalAccess, description);

        /// <summary>
        /// Creates a new strongly typed <see cref="IMember{TDataType}"/> with the provided name and optional parameters.
        /// </summary>
        /// <param name="name">The <see cref="ComponentName"/> of the member.</param>
        /// <param name="radix">The optional <see cref="Enums.Radix"/> of the member.
        /// If not provided, will default to <see cref="Radix.Default(IDataType)"/>.</param>
        /// <param name="externalAccess">The optional <see cref="Enums.ExternalAccess"/> of the member.
        /// If not provided, will default to <see cref="ExternalAccess.ReadWrite"/>.</param>
        /// <param name="description">The optional description of the member.
        /// If not provided, will default to <see cref="string.Empty"/>.</param>
        /// <typeparam name="TDataType">
        /// The DataType of the member.
        /// Must implement IDataType and have parameterless constructor for it to be instantiated.
        /// </typeparam>
        /// <returns>A generic <see cref="IMember{TDataType}"/> instance with the specified arguments.</returns>
        /// <exception cref="ArgumentNullException">name or dataType is null.</exception>
        public static IMember<TDataType> Create<TDataType>(ComponentName name, 
            Radix? radix = null, ExternalAccess? externalAccess = null, string? description = null)
            where TDataType : IDataType, new() =>
            Create(name, new TDataType(), radix, externalAccess, description);

        /// <summary>
        /// Creates a new strongly typed <see cref="IMember{TDataType}"/> with the provided name and data type instance.
        /// </summary>
        /// <param name="name">The <see cref="ComponentName"/> of the member. Must satisfy Logix naming constraints.</param>
        /// <param name="dataType">The <see cref="IDataType"/> of the member.</param>
        /// <param name="radix">The optional <see cref="Enums.Radix"/> of the member.
        /// If not provided, will default to <see cref="Radix.Default(IDataType)"/>.</param>
        /// <param name="externalAccess">The optional <see cref="Enums.ExternalAccess"/> of the member.
        /// If not provided, will default to <see cref="ExternalAccess.ReadWrite"/>.</param>
        /// <param name="description">The optional description of the member.
        /// If not provided, will default to <see cref="string.Empty"/>.</param>
        /// 
        /// <returns>A generic <see cref="IMember{TDataType}"/> instance with the specified arguments.</returns>
        /// <exception cref="ArgumentNullException">name or dataType is null.</exception>
        public static IMember<TDataType> Create<TDataType>(ComponentName name, TDataType dataType,
            Radix? radix = null, ExternalAccess? externalAccess = null, string? description = null)
            where TDataType : IDataType
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name), "ComponentName can not be null.");

            if (dataType is null)
                throw new ArgumentNullException(nameof(dataType), "DataType can not be null.");

            return new Member<TDataType>(name, dataType, radix, externalAccess, description);
        }


        /// <summary>
        /// Creates a new generic array <see cref="IMember{TDataType}"/> with the provided name, data type, and dimensions. 
        /// </summary>
        /// <param name="name">The <see cref="Core.ComponentName"/> of the array member.</param>
        /// <param name="dataType">The <see cref="IDataType"/> that the array member wraps.</param>
        /// <param name="dimensions">The <see cref="Core.Dimensions"/> of the array member.</param>
        /// <param name="radix">The optional <see cref="Enums.Radix"/> of the member.
        /// If not provided, will default to <see cref="Radix.Default(IDataType)"/>.</param>
        /// <param name="externalAccess">The optional <see cref="Enums.ExternalAccess"/> of the member.
        /// If not provided, will default to <see cref="ExternalAccess.ReadWrite"/>.</param>
        /// <param name="description">The optional description of the member.
        /// If not provided, will default to <see cref="string.Empty"/>.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">name, dataType, or dimensions are null.</exception>
        public static IMember<IArrayType<IDataType>> Create(ComponentName name, IDataType dataType, Dimensions dimensions,
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
        public static IMember<IArrayType<TDataType>> Create<TDataType>(ComponentName name, Dimensions dimensions,
            Radix? radix = null, ExternalAccess? externalAccess = null, string? description = null) 
            where TDataType : IDataType, new() =>
            Create(name, new TDataType(), dimensions, radix, externalAccess, description);

        /// <summary>
        /// Creates a new strongly typed array <see cref="IMember{TDataType}"/> with the provided name, data type, and dimensions. 
        /// </summary>
        /// <param name="name">The <see cref="Core.ComponentName"/> of the array member.</param>
        /// <param name="dataType">The <see cref="IDataType"/> that the array member wraps.</param>
        /// <param name="dimensions">The <see cref="Core.Dimensions"/> of the array member.</param>
        /// <param name="radix">The optional <see cref="Enums.Radix"/> of the member.
        /// If not provided, will default to <see cref="Radix.Default(IDataType)"/>.</param>
        /// <param name="externalAccess">The optional <see cref="Enums.ExternalAccess"/> of the member.
        /// If not provided, will default to <see cref="ExternalAccess.ReadWrite"/>.</param>
        /// <param name="description">The optional description of the member.
        /// If not provided, will default to <see cref="string.Empty"/>.</param>
        /// <typeparam name="TDataType">The DataType of the member.</typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">name, dataType, or dimensions are null.</exception>
        public static IMember<IArrayType<TDataType>> Create<TDataType>(ComponentName name, TDataType dataType,
            Dimensions dimensions, Radix? radix = null, ExternalAccess? externalAccess = null,
            string? description = null) where TDataType : IDataType
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name), "ComponentName can not be null.");

            if (dataType is null)
                throw new ArgumentNullException(nameof(dataType), "DataType can not be null.");

            var arrayType = new ArrayType<TDataType>(dimensions, dataType, radix, externalAccess, description);

            return new Member<IArrayType<TDataType>>(name, arrayType, radix, externalAccess, description);
        }
    }
}