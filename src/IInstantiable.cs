namespace L5Sharp
{
    /// <summary>
    /// Defines a method for instantiating new instance of a given data type. 
    /// </summary>
    /// <typeparam name="TDataType">The type of the <see cref="IDataType"/> to instantiate.</typeparam>
    public interface IInstantiable<out TDataType> where TDataType : IDataType
    {
        /// <summary>
        /// Creates a new instance of the <c>DataType</c> component with default values.
        /// </summary>
        /// <returns></returns>
        TDataType Instantiate();
    }
}