using System;
using System.Collections.Generic;
using L5Sharp.Exceptions;

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
    /// <seealso cref="IReadOnlyMembers{TMember}"/>
    public interface IMemberCollection<TMember> : IComponentCollection<TMember> where TMember : IMember<IDataType>
    {
        /// <summary>
        /// Adds a collection of <c>IMember</c> objects to the <c>IMembers</c> collection.
        /// </summary>
        /// <param name="members">The collection of <c>IMember</c> objects to add to the collection.</param>
        /// <exception cref="ArgumentNullException">When members is null.</exception>
        /// <exception cref="ComponentNameCollisionException">When a member name already exists in the collection.</exception>
        /// <exception cref="CircularReferenceException">When a member type is equivalent to the parent type of the collection.</exception>
        void AddRange(IEnumerable<TMember> members);
    }
}