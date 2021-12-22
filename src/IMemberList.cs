namespace L5Sharp
{
    /// <summary>
    /// A mutable collection of <c>IMember</c> components for a given <c>IComplexType</c>.
    /// </summary>
    /// <remarks>
    /// <c>IMemberList</c> is similar to <c>IMemberCollection</c> but it adds the ability to mutate the collection.
    /// This allows the user to configure types such as <see cref="IUserDefined"/> as they would in Logix.
    /// <c>IMember</c> instances of any given <c>IMemberList</c> must have unique names and not self reference
    /// the parent <c>IComplexType</c>.
    /// </remarks>
    /// <typeparam name="TMember">The <see cref="IMember{TDataType}"/> type that the collection represents.</typeparam>
    public interface IMemberList<TMember> : IMemberCollection<TMember>, IComponentsList<TMember>
        where TMember : IMember<IDataType>
    {
        
    }
}