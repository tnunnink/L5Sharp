using System;
using System.Collections.Generic;

namespace L5Sharp
{
    /// <summary>
    /// A read only collection of <c>IMember</c> components for a given <c>IComplexType</c>.
    /// </summary>
    /// <remarks>
    /// <c>IMemberCollection</c> inherits <c>IComponentCollection</c> and adds methods for recursively traversing a complex
    /// member hierarchy to get members not just in the current collection but in descendent complex member collections.
    /// </remarks>
    /// <typeparam name="TMember">The <see cref="IMember{TDataType}"/> type that the collection represents.</typeparam>
    public interface IMemberCollection<out TMember> : IComponentCollection<TMember> where TMember : IMember<IDataType>
    {
        /// <summary>
        /// Determines if the current collection, or nested member collections, contain the provided member name.
        /// </summary>
        /// <remarks>
        /// This method will recursively traverse the nested hierarchy of data type members to find the provided name.
        /// </remarks>
        /// <param name="name">The name of the member to search for.</param>
        /// <returns>true if the provided member name exists either in the immediate or nested member collections.</returns>
        bool DeepContains(string? name);
        
        /// <summary>
        /// Gets an <c>IMember</c> object with the provided name.
        /// </summary>
        /// <remarks>
        /// This method will recursively traverse the nested hierarchy of data type members to find the provided name.
        /// Users can specify the full path to a specific member (e.g. Pump.Indy.Timer.DN),
        /// or simply the name of an immediate child member (e.g. Pump). 
        /// </remarks>
        /// <param name="name">The name of the member to get.</param>
        /// <returns>
        /// If the member with the specified name is found as an immediate or nested child of the <c>IComplexType</c>,
        /// the <c>IMember</c> instance with the specified name; otherwise, null.
        /// </returns>
        /// <exception cref="ArgumentNullException">name is null.</exception>
        /// <exception cref="ArgumentException">name is empty.</exception>
        TMember? DeepGet(string name);

        /// <summary>
        /// Gets all <c>IMember</c> objects, including nested members, in the current <c>IMemberCollection</c>.
        /// </summary>
        /// <remarks>
        /// This method will recursively traverse the nested hierarchy of <c>IComplexType</c> members to get a deep collection
        /// of member instance for the current <c>IComplexType</c>.
        /// For immediate objects only, use <see cref="IComponentCollection{TComponent}.Get"/>.
        /// </remarks>
        /// <returns>
        /// A collection of <c>IMember</c> objects that represent all immediate and nested child members of the current
        /// <c>IMemberCollection</c> if any exists.
        /// If none exist, an empty collection of <c>IMember</c>.
        /// </returns>
        IEnumerable<TMember> DeepGetAll();

        /// <summary>
        /// Gets all <c>IMember</c> names, including nested members, for the current <c>IComplexType</c>.
        /// </summary>
        /// <remarks>
        /// This method will recursively traverse the nested hierarchy of data type members to get a deep collection
        /// of names for each <c>IMember</c> object.
        /// </remarks>
        /// <returns>A collection of string names.</returns>
        IEnumerable<string> DeepNames();
    }
}