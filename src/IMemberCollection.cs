using System;

namespace L5Sharp
{
    /// <summary>
    /// A mutable collection of <c>IMember</c> components that are children of a given <c>IComplexType</c>.
    /// </summary>
    /// <remarks>
    /// <c>IMembers</c> inherits <c>IReadOnlyMembers</c> and adds the ability to mutate the collection.
    /// This allows the user to configure types such as <see cref="IUserDefined"/> as they would in the Logix.
    /// All <c>IMember</c> instances in the <c>IMembers</c> collection must have unique names and not reference
    /// the parent <c>IComplexType</c>.
    /// </remarks>
    /// <typeparam name="TMember">The <see cref="IMember{TDataType}"/> type that the collection represents.</typeparam>
    public interface IMemberCollection<TMember> : IComponentCollection<TMember> where TMember : IMember<IDataType>
    {
        /// <summary>
        /// Inserts the provided <see cref="IMember{TDataType}"/> at the specified index location of the collection.
        /// </summary>
        /// <param name="index">The index at which to insert the member.</param>
        /// <param name="member">The <see cref="IMember{TDataType}"/> to insert.</param>
        /// <exception cref="ArgumentOutOfRangeException">The provided index is not valid.</exception>
        /// <exception cref="ArgumentNullException">member is null.</exception>
        void Insert(int index, TMember member);
    }
}