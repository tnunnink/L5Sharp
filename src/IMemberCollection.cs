using System.Collections.Generic;
using L5Sharp.Core;

namespace L5Sharp
{
    /// <summary>
    /// A collection of <see cref="IMember{TDataType}"/>
    /// </summary>
    public interface IMemberCollection<TMember> : IEnumerable<TMember> where TMember : IMember<IDataType>
    {
        /// <summary>
        /// Gets the number of members in the collection.
        /// </summary>
        int Count { get; }
        
        /// <summary>
        /// Indicates that the component with the provided name exists in the collection.
        /// </summary>
        /// <param name="name">The name of the member to find.</param>
        /// <returns>true if the collection contains a member instance with the specified name.</returns>
        bool Contains(ComponentName name);
        
        /// <summary>
        /// Gets a member from the collection by the provided name.
        /// </summary>
        /// <param name="name">the name of the member to get.</param>
        /// <returns>The instance of the member with the specified name if found, null if not.</returns>
        TMember Get(ComponentName name);
        
        /// <summary>
        /// Adds a member to the collection.
        /// </summary>
        /// <param name="member">The member to add.</param>
        void Add(TMember member);

        /// <summary>
        /// Adds a collection of members to the collection
        /// </summary>
        /// <param name="members">The enumerable collection of members to add.</param>
        void AddRange(IEnumerable<TMember> members);
        
        /// <summary>
        /// Updates an existing member with the provided member instance if found, adds the member if not.
        /// </summary>
        /// <param name="member">The member to update.</param>
        void Update(TMember member);

        /// <summary>
        /// Updates a collection of members on the collection.
        /// </summary>
        /// <param name="members">The enumerable collection of members to update.</param>
        void UpdateRange(IEnumerable<TMember> members);
        
        /// <summary>
        /// Inserts the provided member at the specified index of the collection.
        /// </summary>
        /// <param name="index">The index at which to insert the member.</param>
        /// <param name="member">The member to insert.</param>
        void Insert(int index, TMember member);

        /// <summary>
        /// Renames a member in the current collection from the current name to the new name.
        /// </summary>
        /// <param name="current">The current member name of the instance to rename.</param>
        /// <param name="name">The name to rename the member to.</param>
        void Rename(ComponentName current, ComponentName name);
        
        /// <summary>
        /// Removes a member from the collection.
        /// </summary>
        /// <param name="name">The name of the member to remove from the collection.</param>
        void Remove(ComponentName name);
    }
}