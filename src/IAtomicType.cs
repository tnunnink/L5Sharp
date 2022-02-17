using System;
using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// A fundamental <see cref="IDataType"/> that contains a unit of value.
    /// </summary>
    /// <remarks>
    /// Logix atomic types are types that have value (i.e. BOOL, SINT, INT, DINT, REAL, etc.).
    /// These type are synonymous with value types in .NET. Hence, the <see cref="IAtomicType"/> adds a non generic
    /// member that will hold the reference to the data type value, as well as methods for setting and formatting
    /// the value. Atomic types, though classes, should be considered value types as they simply wrap primitive .NET CLR
    /// types. 
    /// </remarks>
    /// <seealso cref="IAtomicType{T}"/>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer> 
    public interface IAtomicType : IDataType
    {
        /// <summary>
        /// Gets the value of the <c>IAtomicType</c>.
        /// </summary>
        /// <value>
        /// Returns the .NET value type that the current <c>IAtomicType</c> represents as an object.
        /// for a strongly typed value, use <see cref="IAtomicType{T}.Value"/>.
        /// </value>
        object Value { get; }

        /// <summary>
        /// Sets the <see cref="Value"/> of the <c>IAtomicType</c>.
        /// </summary>
        /// <param name="value">The object value to set.</param>
        /// <exception cref="ArgumentNullException">
        /// When the provided object value is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// When the provided object type is not able to be converted or parsed to the current <c>IAtomicType</c> value.
        /// </exception>
        void SetValue(object value);

        /// <summary>
        /// Formats the <see cref="Value"/> of the <see cref="IAtomicType"/> using the provided <see cref="Enums.Radix"/>.  
        /// </summary>
        /// <param name="radix">
        /// The <see cref="Enums.Radix"/> format of the value.
        /// If not provided, will use the default Radix for the current data type.
        /// </param>
        /// <returns>A string value representing the current atomic value formatted in the specified Radix.</returns>
        string Format(Radix? radix = null);
    }

    /// <summary>
    /// Generic implementation of the IAtomic to specify the struct value that the atomic represents.
    /// </summary>
    /// <typeparam name="T">The struct type that the atomic data type represents.</typeparam>
    public interface IAtomicType<T> : IAtomicType where T : struct
    {
        /// <summary>
        /// Gets the typed value of the <c>IAtomicType</c>.
        /// </summary>
        /// <value>
        /// Returns the .NET value type that the current <c>IAtomicType</c> represents, where T is the struct type of
        /// the <c>IAtomicType</c>.
        /// </value>
        new T Value { get; }

        /// <summary>
        /// Sets the <see cref="Value"/> of the <c>IAtomicType</c>.
        /// </summary>
        /// <param name="value">The typed value to set.</param>
        void SetValue(T value);
    }
}