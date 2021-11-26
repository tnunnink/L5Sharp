using System;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// Static factory class that creates instances of components that implement <see cref="IMember{TDataType}"/>. 
    /// </summary>
    public static class Member
    {
        /// <summary>
        /// Creates a new generic typed single <c>Member</c> with the specified arguments.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="dataType">The data type of the member.</param>
        /// <param name="dimensions">The dimensions of the member.</param>
        /// <param name="radix">The radix of the member.</param>
        /// <param name="externalAccess">The external access of the member.</param>
        /// <param name="description">The description of the member.</param>
        /// <returns>A new member instance with the specified arguments.</returns>
        /// <exception cref="ArgumentNullException">Thrown if name is null.</exception>
        public static IMember<IDataType> Create(ComponentName name, IDataType dataType, Dimensions dimensions = null,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (dimensions != null && !dimensions.AreEmpty)
                return new ArrayMember<IDataType>(name, dataType, dimensions, radix, externalAccess, description);

            return new Member<IDataType>(name, dataType, radix, externalAccess, description);
        }

        /// <summary>
        /// Creates a new strongly typed single <c>Member</c> with the specified arguments.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="dimensions">The dimensions of the member.</param>
        /// <param name="radix">The radix of the member.</param>
        /// <param name="externalAccess">The external access of the member.</param>
        /// <param name="description">The description of the member.</param>
        /// <typeparam name="TDataType">
        /// The DataType of the member.
        /// TDataType must implement IDataType and have parameterless constructor for it to be instantiated.
        /// </typeparam>
        /// <returns>A new <c>Member</c> instance with the specified arguments.</returns>
        /// <exception cref="ArgumentNullException">Thrown if name is null.</exception>
        public static IMember<TDataType> Create<TDataType>(ComponentName name, Dimensions dimensions = null,
            Radix radix = null,
            ExternalAccess externalAccess = null, string description = null)
            where TDataType : IDataType, new()
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (dimensions != null && !dimensions.AreEmpty)
                return new ArrayMember<TDataType>(name, new TDataType(), dimensions, radix, externalAccess,
                    description);

            return new Member<TDataType>(name, new TDataType(), radix, externalAccess, description);
        }

        /// <summary>
        /// Creates a new strongly typed single <c>Member</c> with the specified arguments.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="dataType">The data type of the member.</param>
        /// <param name="dimensions">The dimensions of the member.</param>
        /// <param name="radix">The radix of the member.</param>
        /// <param name="externalAccess">The external access of the member.</param>
        /// <param name="description">The description of the member.</param>
        /// <typeparam name="TDataType">The DataType of the member.</typeparam>
        /// <returns>A new <c>Member</c> instance with the specified arguments.</returns>
        /// <exception cref="ArgumentNullException">Thrown if name is null.</exception>
        public static IMember<TDataType> Create<TDataType>(ComponentName name, TDataType dataType,
            Dimensions dimensions = null, Radix radix = null, ExternalAccess externalAccess = null,
            string description = null) where TDataType : IDataType
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (dimensions != null && !dimensions.AreEmpty)
                return new ArrayMember<TDataType>(name, dataType, dimensions, radix, externalAccess, description);

            return new Member<TDataType>(name, dataType, radix, externalAccess, description);
        }


        /// <summary>
        /// Creates a new generic typed <c>ArrayMember</c> with the specified arguments.
        /// </summary>
        /// <param name="name">The <c>ComponentName</c> of the member.</param>
        /// <param name="dataType">The <c>DataType</c> of the member.</param>
        /// <param name="dimensions">The <c>Dimensions</c> of the member.</param>
        /// <param name="radix">The <c>Radix</c> option of the member.</param>
        /// <param name="externalAccess">The <c>ExternalAccess</c> option of the member.</param>
        /// <param name="description">The description of the member.</param>
        /// <returns>A new <c>Member</c> instance with the specified arguments.</returns>
        /// <exception cref="ArgumentNullException">Thrown if name is null.</exception>
        public static IArrayMember<IDataType> Array(ComponentName name, IDataType dataType, Dimensions dimensions,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            return new ArrayMember<IDataType>(name, dataType, dimensions, radix, externalAccess, description);
        }

        /// <summary>
        /// Creates a new strongly typed <c>ArrayMember</c> with the specified arguments.
        /// </summary>
        /// <param name="name">The <c>ComponentName</c> of the member.</param>
        /// <param name="dimensions">The <c>Dimensions</c> of the member.</param>
        /// <param name="radix">The <c>Radix</c> option of the member.</param>
        /// <param name="externalAccess">The <c>ExternalAccess</c> option of the member.</param>
        /// <param name="description">The Description of the member.</param>
        /// <typeparam name="TDataType">
        /// The DataType of the member.
        /// TDataType must implement IDataType and have parameterless constructor for it to be instantiated.
        /// </typeparam>
        /// <returns>A new <c>Member</c> instance with the specified arguments.</returns>
        /// <exception cref="ArgumentNullException">Thrown if name is null.</exception>
        public static IArrayMember<TDataType> Array<TDataType>(ComponentName name, Dimensions dimensions,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null)
            where TDataType : IDataType, new()
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            return new ArrayMember<TDataType>(name, new TDataType(), dimensions, radix, externalAccess, description);
        }

        /// <summary>
        /// Creates a new strongly typed <c>ArrayMember</c> with the specified arguments.
        /// </summary>
        /// <param name="name">The <c>ComponentName</c> of the member.</param>
        /// <param name="dataType">The default <c>DataType</c> instance of the member.</param>
        /// /// <param name="dimensions">The <c>Dimensions</c> of the member.</param>
        /// <param name="radix">The <c>Radix</c> option of the member.</param>
        /// <param name="externalAccess">The <c>ExternalAccess</c> option of the member.</param>
        /// <param name="description">The Description of the member.</param>
        /// <typeparam name="TDataType">The DataType of the member.</typeparam>
        /// <returns>A new <c>Member</c> instance with the specified arguments.</returns>
        /// <exception cref="ArgumentNullException">Thrown if name is null.</exception>
        public static IArrayMember<TDataType> Array<TDataType>(ComponentName name, TDataType dataType,
            Dimensions dimensions, Radix radix = null, ExternalAccess externalAccess = null, string description = null)
            where TDataType : IDataType
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            return new ArrayMember<TDataType>(name, dataType, dimensions, radix, externalAccess, description);
        }
    }
}