using System;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp
{
    /// <summary>
    /// Represents a String Defined Logix data type.
    /// </summary>
    public interface IStringType : IDataType
    {
        /// <summary>
        /// Gets the current string value of the <c>IStringType</c>.
        /// </summary>
        string Value { get; }

        /// <summary>
        /// Gets the LEN <c>IMember</c> of the <c>IStringType</c>.
        /// </summary>
        /// <value>
        /// Member that holds the decimal format value representing the maximum length of the string value. 
        /// </value>
        IMember<Dint> LEN { get; }

        /// <summary>
        /// Gets the DATA <c>IMember</c> of the <c>IStringType</c>.
        /// </summary>
        /// <value>
        /// Member that holds the ASCII format sequence of characters that comprise the string value.
        /// </value>
        IMember<IArrayType<Sint>> DATA { get; }
        
        /// <summary>
        /// Sets <see cref="Value"/> to the provided string value for the <c>IStringType</c>. 
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <exception cref="ArgumentNullException">When value is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// When the length of value is longer than the predefined length (LEN Member value).
        /// </exception>
        void SetValue(string value);
    }
}