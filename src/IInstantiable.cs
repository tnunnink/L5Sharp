namespace L5Sharp
{
    /// <summary>
    /// Defines a method for instantiating new instance of a given data type. 
    /// </summary>
    /// <typeparam name="TDataType">The type of the <see cref="IDataType"/> to instantiate.</typeparam>
    public interface IInstantiable<out TDataType> where TDataType : IDataType
    {
        /// <summary>
        /// Created a new instance of the <c>DataType</c> component.
        /// </summary>
        /// <returns></returns>
        TDataType Instantiate();
    }
}