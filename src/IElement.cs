namespace L5Sharp
{
    /// <summary>
    /// An <c>Element</c> represents a member of an array.
    /// </summary>
    /// <remarks>An element is effectively a <see cref="IMember{TDataType}"/> but overrides the name with a simple
    /// <see cref="string"/> os that if can have the correct index naming that is not subject to Logix name constraints/>
    /// </remarks>
    /// <typeparam name="TDataType"></typeparam>
    public interface IElement<out TDataType> : IMember<TDataType> where TDataType : IDataType
    {
        /// <summary>
        /// The zero based index name of the array element.
        /// </summary>
        /// <example>[0]</example>
        new string Name { get; }
    }
}