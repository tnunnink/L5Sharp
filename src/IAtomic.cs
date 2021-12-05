namespace L5Sharp
{
    /// <summary>
    /// Represents a Logix data type that is a fundamental value type, or a type that has value.
    /// </summary>
    /// <remarks>
    /// These types represent CLR types such as bool, short, int, long, float. All atomics are predefined...
    /// </remarks>
    public interface IAtomic : IDataType
    {
        /// <summary>
        /// Gets the value of the atomic type.
        /// </summary>
        object Value { get; }

        /// <summary>
        /// Gets a new instance of the atomic type with the provided value.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <returns>A new instance of the atomic type with the provided value.</returns>
        IAtomic Update(object value);
    }

    /// <summary>
    /// Generic implementation of the IAtomic to specify the struct value that the atomic represents.
    /// </summary>
    /// <typeparam name="T">The struct type that the atomic data type represents.</typeparam>
    public interface IAtomic<T> : IAtomic where T : struct
    {
        /// <summary>
        /// Gets the value of the atomic type.
        /// </summary>
        new T Value { get; }
        
        /// <summary>
        /// Gets a new instance of the atomic type with the provided value.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <returns>A new instance of the atomic type with the provided value.</returns>
        IAtomic<T> Update(T value);
    }
}