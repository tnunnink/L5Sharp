using L5Sharp.Types;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp
{
    /// <summary>
    /// Represents a String Defined Logix data type.
    /// </summary>
    public interface IStringDefined : IDataType
    {
        /// <summary>
        /// Gets the string value of the type.
        /// </summary>
        string Value { get; }

        /// <summary>
        /// Gets the Length member of the string defined type.
        /// </summary>
        /// <remarks>
        /// This member represents the length of the string defined type
        /// </remarks>
        IMember<Dint> LEN { get; }

        /// <summary>
        /// Gets the data member of the string defined type.
        /// </summary>
        IArrayMember<Sint> DATA { get; }

        /// <summary>
        /// Generates a new string defined instance with the specified value. 
        /// </summary>
        /// <param name="value">The value to be updated.</param>
        /// <returns>A new StringDefined instance that represents the previous instance with the provided value.</returns>
        IStringDefined Update(string value);
    }
}