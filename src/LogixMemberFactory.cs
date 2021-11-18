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
        /// Creates an instance of <see cref="IMember{IDataType}"/> with the specified arguments.
        /// </summary>
        /// <param name="name">The <see cref="ComponentName"/> value of the member.</param>
        /// <param name="dataType">The <see cref="IDataType"/> of the member.</param>
        /// <param name="radix">The <see cref="Radix"/> option of the member.</param>
        /// <param name="externalAccess">The <see cref="ExternalAccess"/> option of the member.</param>
        /// <param name="description">The member description value.</param>
        /// <returns>A new instance of <see cref="IMember{IDataType}"/></returns>
        public static IMember<IDataType> Create(ComponentName name, IDataType dataType,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null)
        {
            return new Member<IDataType>(name, dataType, radix, externalAccess, description);
        }

        /// <summary>
        /// Creates a strongly typed instance of <see cref="IMember{TDataType}"/> with the specified arguments.
        /// </summary>
        /// <param name="name">The <see cref="ComponentName"/> value of the member.</param>
        /// <param name="radix">The <see cref="Radix"/> option of the member.</param>
        /// <param name="externalAccess">The <see cref="ExternalAccess"/> option of the member.</param>
        /// <param name="description">The member description value.</param>
        /// <typeparam name="TDataType">The <see cref="IDataType"/> of the member.
        /// TDataType must implement IDataType and have parameterless constructor</typeparam>
        /// <returns>A new instance of <see cref="IMember{TDataType}"/></returns>
        public static IMember<TDataType> Create<TDataType>(ComponentName name, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null)
            where TDataType : IDataType, new()
        {
            return new Member<TDataType>(name, new TDataType(), radix, externalAccess, description);
        }

        /// <summary>
        /// Creates a strongly typed instance of <see cref="IMember{TDataType}"/> with the specified arguments.
        /// </summary>
        /// <param name="name">The <see cref="ComponentName"/> value of the member.</param>
        /// <param name="dataType">The <see cref="TDataType"/> value of the member.</param>
        /// <param name="radix">The <see cref="Radix"/> option of the member.</param>
        /// <param name="externalAccess">The <see cref="ExternalAccess"/> option of the member.</param>
        /// <param name="description">The member description value.</param>
        /// <typeparam name="TDataType">The <see cref="IDataType"/> of the member.
        /// TDataType must implement IDataType and have parameterless constructor</typeparam>
        /// <returns>A new instance of <see cref="IMember{TDataType}"/></returns>
        public static IMember<TDataType> Create<TDataType>(ComponentName name, TDataType dataType, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null)
            where TDataType : IDataType
        {
            return new Member<TDataType>(name, dataType, radix, externalAccess, description);
        }

        public static IArrayMember<IDataType> Create(ComponentName name, IDataType dataType, Dimensions dimensions,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null)
        {
            return new ArrayMember<IDataType>(name, dataType, dimensions, radix, externalAccess, description);
        }

        public static IArrayMember<TDataType> Create<TDataType>(ComponentName name, Dimensions dimensions,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null)
            where TDataType : IDataType, new()
        {
            return new ArrayMember<TDataType>(name, new TDataType(), dimensions, radix, externalAccess, description);
        }

        public static IArrayMember<TDataType> Create<TDataType>(ComponentName name, TDataType dataType,
            Dimensions dimensions, Radix radix = null, ExternalAccess externalAccess = null, string description = null)
            where TDataType : IDataType
        {
            return new ArrayMember<TDataType>(name, dataType, dimensions, radix, externalAccess, description);
        }
    }
}