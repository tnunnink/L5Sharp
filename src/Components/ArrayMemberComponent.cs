using System;
using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Components
{
    /// <summary>
    /// Static factory class that creates instances of components that implement <see cref="IArrayMember{TDataType}"/>. 
    /// </summary>
    public static class ArrayMember
    {
        /// <summary>
        /// Creates a new generic typed ArrayMember with the specified arguments.
        /// </summary>
        /// <param name="name">The ComponentName of the member.</param>
        /// <param name="dataType">The DataType of the member.</param>
        /// <param name="dimensions">The Dimensions of the member.</param>
        /// <param name="radix">The Radix of the member.
        /// If not provided, will default to <see cref="Radix.Default(IDataType)"/>.</param>
        /// <param name="externalAccess">The external access of the member.
        /// If not provided, will default to <see cref="ExternalAccess.ReadWrite"/>.</param>
        /// <param name="description"> The description of the member.
        /// If not provided, will default to <see cref="string.Empty"/>.</param>
        /// <returns>A new Member instance with the specified arguments.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <see cref="name"/> is null.</exception>
        public static IArrayMember<IDataType> Create(ComponentName name, IDataType dataType, Dimensions dimensions,
            Radix? radix = null, ExternalAccess? externalAccess = null, string? description = null)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            return new ArrayMember<IDataType>(name, dataType, dimensions, radix, externalAccess, description);
        }

        /// <summary>
        /// Creates a new generic typed ArrayMember with the specified arguments.
        /// </summary>
        /// <param name="name">The ComponentName of the member.</param>
        /// <param name="elements">The collection of data type elements to initialize the array with.</param>
        /// <param name="dimensions">The dimensions of the member
        /// If not provided, will default to length of <see cref="elements"/> collection.</param>
        /// <param name="radix">The radix of the member.
        /// If not provided, will default to <see cref="Radix.Default(IDataType)"/>.</param>
        /// <param name="externalAccess">The external access of the member.
        /// If not provided, will default to <see cref="ExternalAccess.ReadWrite"/>.</param>
        /// <param name="description"> The description of the member.
        /// If not provided, will default to <see cref="string.Empty"/>.</param>
        /// <returns>A new Member instance with the specified arguments.</returns>
        /// <exception cref="ArgumentNullException">Thrown if name is null.</exception>
        public static IArrayMember<IDataType> Create(ComponentName name, List<IDataType> elements,
            Dimensions? dimensions = null, Radix? radix = null, ExternalAccess? externalAccess = null,
            string? description = null)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            return new ArrayMember<IDataType>(name, elements, dimensions, radix, externalAccess, description);
        }

        /// <summary>
        /// Creates a new strongly typed ArrayMember with the specified arguments.
        /// </summary>
        /// <param name="name">The ComponentName of the member.</param>
        /// <param name="dimensions">The Dimensions of the member.</param>
        /// <param name="radix">The Radix option of the member.</param>
        /// <param name="externalAccess">The ExternalAccess option of the member.</param>
        /// <param name="description">The Description of the member.</param>
        /// <typeparam name="TDataType">
        /// The DataType of the member.
        /// TDataType must implement IDataType and have parameterless constructor for it to be instantiated.
        /// </typeparam>
        /// <returns>A new Member instance with the specified arguments.</returns>
        /// <exception cref="ArgumentNullException">Thrown if name is null.</exception>
        public static IArrayMember<TDataType> Create<TDataType>(ComponentName name, Dimensions dimensions,
            Radix? radix = null, ExternalAccess? externalAccess = null, string? description = null)
            where TDataType : IDataType, new()
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            return new ArrayMember<TDataType>(name, new TDataType(), dimensions, radix, externalAccess, description);
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
        /// <typeparam name="TDataType">The DataType of the member.</typeparam>
        /// <returns>A new Member instance with the specified arguments.</returns>
        /// <exception cref="ArgumentNullException">Thrown if name is null.</exception>
        public static IArrayMember<TDataType> Create<TDataType>(ComponentName name, TDataType dataType,
            Dimensions dimensions, Radix? radix = null, ExternalAccess? externalAccess = null,
            string? description = null)
            where TDataType : IDataType
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            return new ArrayMember<TDataType>(name, dataType, dimensions, radix, externalAccess, description);
        }
    }
}