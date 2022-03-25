using System;
using System.Collections.Generic;
using L5Sharp.Types;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp
{
    /// <summary>
    /// Represents a String Defined Logix data type.
    /// </summary>
    public interface IStringType : IComplexType, IEnumerable<char>
    {
        /// <summary>
        /// Gets the current string value of the <see cref="IStringType"/>.
        /// </summary>
        string Value { get; }

        /// <summary>
        /// Gets the current length of the string <see cref="Value"/> property.
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Gets the LEN member of the <see cref="IStringType"/>
        /// </summary>
        /// <value>
        /// Member that holds the decimal format value representing the maximum length of the string value. 
        /// </value>
        IMember<DINT> LEN { get; }

        /// <summary>
        /// Gets the DATA member of the <see cref="IStringType"/>.
        /// </summary>
        /// <value>
        /// Member that holds the ASCII format sequence of characters that comprise the string value.
        /// </value>
        IMember<IArrayType<SINT>> DATA { get; }
        
        /// <summary>
        /// Sets <see cref="Value"/> to the provided string value. 
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <exception cref="ArgumentNullException">value is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// value length is larger than the predefined length (LEN Member value).
        /// </exception>
        void SetValue(string value);

        /// <summary>
        /// Clears the text of the <see cref="Value"/> property to an empty string.
        /// </summary>
        void ClearValue();
    }
}