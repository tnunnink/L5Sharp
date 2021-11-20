namespace L5Sharp
{
    /// <summary>
    /// Defines a method for performing a deep copy of all
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPrototype<out T>
    {
        /// <summary>
        /// Performs a deep copy of the current type and returns a new instance of equal value.
        /// </summary>
        /// <returns>A new instance of the specified type of equal value.</returns>
        T Copy();
    }
}