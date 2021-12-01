using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// Represents a Logix data type that is a fundamental value type,  or a type that has value.
    /// </summary>
    /// <remarks>
    /// These types correlate to CLR types such as bool, int, long, float. Type that implement this interface must wrap
    /// a .NET value type. IAtomic is the only Logix data type with value.
    /// You can therefore set the value and radix for the type.
    /// </remarks>
    public interface IAtomic : IDataType
    {
        /// <summary>
        /// Get the value of the current atomic data type. 
        /// </summary>
        object Value { get; }
        
        /// <summary>
        /// Gets the formatted value based on the current <see cref="Radix"/> of the data type. 
        /// </summary>
        string FormattedValue { get; }
        
        /// <summary>
        /// Sets the value of the current atomic type with the provided value.
        /// </summary>
        /// <param name="value">THe object value to set.</param>
        void SetValue(object value);
        
        /// <summary>
        /// Sets the Radix of the current atomic data type. 
        /// </summary>
        /// <param name="radix">The radix value to set.</param>
        void SetRadix(Radix radix);

        /// <summary>
        /// Determines if the provided radix is supported by the current atomic type.
        /// </summary>
        /// <param name="radix">The value of the radix to determine support for.</param>
        /// <returns>true if the provided radix value is valid for the current atomic type. Otherwise, false.</returns>
        bool SupportsRadix(Radix radix);
    }

    /// <summary>
    /// Generic implementation of the IAtomic to specify the struct value that the atomic represents.
    /// </summary>
    /// <typeparam name="T">The struct type that the atomic data type represents.</typeparam>
    public interface IAtomic<T> : IAtomic where T : struct
    {
        /// <summary>
        /// Gets the strongly type current value of the atomic type. 
        /// </summary>
        new T Value { get; }
        
        /// <summary>
        /// Sets the value of the current atomic type with the provided value.
        /// </summary>
        /// <param name="value">The typed value to set.</param>
        void SetValue(T value);
    }
}