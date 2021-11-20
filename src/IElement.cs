namespace L5Sharp
{
    /// <summary>
    /// Represents a member of an array.
    /// </summary>
    /// <remarks>An element is effectively a <see cref="IMember{TDataType}"/> but overrides the name with a simple
    /// <see cref="string"/> so that it can have the correct index naming that is not subject to Logix name constraints.
    /// </remarks>
    /// <typeparam name="TDataType">The <see cref="IDataType"/> of the member.</typeparam>
    public interface IElement<out TDataType> : IMember<TDataType> where TDataType : IDataType
    {
        /// <summary>
        /// The zero based index name of the array element.
        /// </summary>
        new string Name { get; }
    }
}