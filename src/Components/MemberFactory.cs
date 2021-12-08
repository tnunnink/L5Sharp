using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Creates a new generic typed single Member with the specified arguments.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="dataType">The data type of the member.</param>
        /// <param name="dimensions">The dimensions of the member.
        /// If not provided, will default to <see cref="Dimensions.Empty"/>.</param>
        /// <param name="radix">The radix of the member.
        /// If not provided, will default to <see cref="Radix.Default"/>.</param>
        /// <param name="externalAccess">The external access of the member.
        /// If not provided, will default to <see cref="ExternalAccess.ReadWrite"/>.</param>
        /// <param name="description"> The description of the member.
        /// If not provided, will default to <see cref="string.Empty"/>.</param>
        /// <returns>A new member instance with the specified arguments.</returns>
        /// <exception cref="ArgumentNullException">Thrown if name is null.</exception>
        public static IMember<IDataType> Create(ComponentName name, IDataType dataType, Dimensions? dimensions = null,
            Radix? radix = null, ExternalAccess? externalAccess = null, string? description = null)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            if (dimensions is not null && !dimensions.AreEmpty)
                return new ArrayMember<IDataType>(name, dataType, dimensions, radix, externalAccess, description,
                    Enumerable.Empty<IMember<IDataType>>());

            return new Member<IDataType>(name, dataType, radix, externalAccess, description);
        }

        /// <summary>
        /// Creates a new strongly typed single Member with the specified arguments.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="dimensions">The dimensions of the member.
        /// If not provided, will default to <see cref="Dimensions.Empty"/>.</param>
        /// <param name="radix">The radix of the member.
        /// If not provided, will default to <see cref="Radix.Default"/>.</param>
        /// <param name="externalAccess">The external access of the member.
        /// If not provided, will default to <see cref="ExternalAccess.ReadWrite"/>.</param>
        /// <param name="description"> The description of the member.
        /// If not provided, will default to <see cref="string.Empty"/>.</param>
        /// <typeparam name="TDataType">
        /// The DataType of the member.
        /// TDataType must implement IDataType and have parameterless constructor for it to be instantiated.
        /// </typeparam>
        /// <returns>A new Member instance with the specified arguments.</returns>
        /// <exception cref="ArgumentNullException">Thrown if name is null.</exception>
        public static IMember<TDataType> Create<TDataType>(ComponentName name, Dimensions? dimensions = null,
            Radix? radix = null, ExternalAccess? externalAccess = null, string? description = null)
            where TDataType : IDataType, new()
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            if (dimensions is not null && !dimensions.AreEmpty)
                return new ArrayMember<TDataType>(name, new TDataType(), dimensions, radix, externalAccess,
                    description, Enumerable.Empty<IMember<IDataType>>());

            return new Member<TDataType>(name, new TDataType(), radix, externalAccess, description);
        }

        /// <summary>
        /// Creates a new strongly typed single Member with the specified arguments.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="dataType">The data type of the member.</param>
        /// <param name="dimensions">The dimensions of the member.
        /// If not provided, will default to <see cref="Dimensions.Empty"/>.</param>
        /// <param name="radix">The radix of the member.
        /// If not provided, will default to <see cref="Radix.Default"/>.</param>
        /// <param name="externalAccess">The external access of the member.
        /// If not provided, will default to <see cref="ExternalAccess.ReadWrite"/>.</param>
        /// <param name="description"> The description of the member.
        /// If not provided, will default to <see cref="string.Empty"/>.</param>
        /// <typeparam name="TDataType">The DataType of the member.</typeparam>
        /// <returns>A new Member instance with the specified arguments.</returns>
        /// <exception cref="ArgumentNullException">Thrown if name is null.</exception>
        public static IMember<TDataType> Create<TDataType>(ComponentName name, TDataType dataType,
            Dimensions? dimensions = null, Radix? radix = null, ExternalAccess? externalAccess = null,
            string? description = null) where TDataType : IDataType
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            if (dimensions is not null && !dimensions.AreEmpty)
                return new ArrayMember<TDataType>(name, dataType, dimensions, radix, externalAccess, description,
                    Enumerable.Empty<IMember<IDataType>>());

            return new Member<TDataType>(name, dataType, radix, externalAccess, description);
        }


        /// <summary>
        /// Creates a new generic typed ArrayMember with the specified arguments.
        /// </summary>
        /// <param name="name">The ComponentName of the member.</param>
        /// <param name="dataType">The DataType of the member.</param>
        /// <param name="dimensions">The dimensions of the member.
        /// If not provided, will default to <see cref="Dimensions.Empty"/>.</param>
        /// <param name="radix">The radix of the member.
        /// If not provided, will default to <see cref="Radix.Default"/>.</param>
        /// <param name="externalAccess">The external access of the member.
        /// If not provided, will default to <see cref="ExternalAccess.ReadWrite"/>.</param>
        /// <param name="description"> The description of the member.
        /// If not provided, will default to <see cref="string.Empty"/>.</param>
        /// <param name="members">A collection of members to initialize the array with.</param>
        /// <returns>A new Member instance with the specified arguments.</returns>
        /// <exception cref="ArgumentNullException">Thrown if name is null.</exception>
        public static IArrayMember<IDataType> Array(ComponentName name, IDataType dataType, Dimensions dimensions,
            Radix? radix = null, ExternalAccess? externalAccess = null, string? description = null,
            IEnumerable<IMember<IDataType>>? members = null)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            return new ArrayMember<IDataType>(name, dataType, dimensions, radix, externalAccess, description, members);
        }

        /// <summary>
        /// Creates a new strongly typed ArrayMember with the specified arguments.
        /// </summary>
        /// <param name="name">The ComponentName of the member.</param>
        /// <param name="dimensions">The Dimensions of the member.</param>
        /// <param name="radix">The Radix option of the member.</param>
        /// <param name="externalAccess">The ExternalAccess option of the member.</param>
        /// <param name="description">The Description of the member.</param>
        /// <param name="members">A collection of members to initialize the array with.</param>
        /// <typeparam name="TDataType">
        /// The DataType of the member.
        /// TDataType must implement IDataType and have parameterless constructor for it to be instantiated.
        /// </typeparam>
        /// <returns>A new Member instance with the specified arguments.</returns>
        /// <exception cref="ArgumentNullException">Thrown if name is null.</exception>
        public static IArrayMember<TDataType> Array<TDataType>(ComponentName name, Dimensions dimensions,
            Radix? radix = null, ExternalAccess? externalAccess = null, string? description = null,
            IEnumerable<IMember<IDataType>>? members = null)
            where TDataType : IDataType, new()
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            return new ArrayMember<TDataType>(name, new TDataType(), dimensions, radix, externalAccess, description,
                members);
        }

        /// <summary>
        /// Creates a new strongly typed ArrayMember with the specified arguments.
        /// </summary>
        /// <param name="name">The ComponentName of the member.</param>
        /// <param name="dataType">The default DataType instance of the member.</param>
        /// <param name="dimensions">The Dimensions of the member.</param>
        /// <param name="radix">The Radix option of the member.</param>
        /// <param name="externalAccess">The ExternalAccess option of the member.</param>
        /// <param name="description">The Description of the member.</param>
        /// <param name="members">A collection of members to initialize the array with.</param>
        /// <typeparam name="TDataType">The DataType of the member.</typeparam>
        /// <returns>A new Member instance with the specified arguments.</returns>
        /// <exception cref="ArgumentNullException">Thrown if name is null.</exception>
        public static IArrayMember<TDataType> Array<TDataType>(ComponentName name, TDataType dataType,
            Dimensions dimensions, Radix? radix = null, ExternalAccess? externalAccess = null,
            string? description = null, IEnumerable<IMember<IDataType>>? members = null)
            where TDataType : IDataType
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            return new ArrayMember<TDataType>(name, dataType, dimensions, radix, externalAccess, description, members);
        }
    }
}