using L5Sharp.Types;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp
{
    /// <summary>
    /// Represents a String Defined Logix data type.
    /// </summary>
    public interface IStringType : IDataType
    {
        /// <summary>
        /// Gets the value of the string type.
        /// </summary>
        string Value { get; }

        /// <summary>
        /// Gets the Length Member of the string type.
        /// </summary>
        /// <remarks>
        /// This member represents the length of the string defined type
        /// </remarks>
        IMember<Dint> LEN { get; }

        /// <summary>
        /// Gets the Data Member of the string type.
        /// </summary>
        IMember<Sint> DATA { get; }
        
        /// <summary>
        /// Sets the value of the string to the provided string value. 
        /// </summary>
        /// <param name="value">The value to set.</param>
        void SetValue(string value);
    }
}