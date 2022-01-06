using System;
using System.Collections.Generic;

namespace L5Sharp
{
    /// <summary>
    /// A read-only collection of <c>IMember</c> objects that are children of a given <c>IComplexType</c>.
    /// </summary>
    /// <remarks>
    /// The <c>IReadOnlyMembers</c> collection exposes methods that will recursively traverse the nested hierarchy of
    /// members  in order to examine all descendant <c>IMember</c> objects. This makes finding and getting instance
    /// of nested <c>IMember</c> objects easier for the user. As the name implies, this collection only exposes methods
    /// for reading from the collection, as some <c>IComplexTypes</c> members are not mutable. For a mutable collection
    /// of <c>IMembers</c>, use <see cref="IMemberCollection{TMember}"/>.
    /// </remarks>
    /// <typeparam name="TMember">The <see cref="IMember{TDataType}"/> type that the collection represents.</typeparam>
    /// <seealso cref="IMemberCollection{TMember}"/>
    public interface IReadOnlyMembers<out TMember> : IEnumerable<TMember> where TMember : IMember<IDataType>
    {
        /// <summary>
        /// Gets the number of <c>IMember</c> objects in the <c>IReadOnlyMembers</c> collection.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Determines if this collection, or nested member collections, contain the provided member name.
        /// </summary>
        /// <remarks>
        /// This method will recursively traverse the nested hierarchy of data type members to find the provided name.
        /// </remarks>
        /// <param name="name">The name of the member to search for.
        /// This parameter can contain the full path to a nested member or simply the immediate name of a member object.
        /// </param>
        /// <example>
        /// To determine if a nested member with the name "Pump.Indy.Running" exists:
        /// <code>
        /// var exists = members.Contains("Pump.Indy.Running");
        /// </code>
        /// </example>
        /// <returns>
        /// true if the provided member name exists either in the immediate or nested member collections; otherwise, false.
        /// </returns>
        bool Contains(string? name);
        
        /// <summary>
        /// Gets an <c>IMember</c> object with the provided name from the current collection or nested member collection.
        /// </summary>
        /// <remarks>
        /// This method will recursively traverse the nested hierarchy of data type members to find the provided name.
        /// Users can specify the full path to a specific member (e.g. Pump.Indy.Running),
        /// or simply the name of an immediate child member (e.g. Pump). 
        /// </remarks>
        /// <param name="name">The name of the member to get.</param>
        /// <returns>
        /// If the member with the specified name is found as an immediate or nested object of the collection,
        /// the <c>IMember</c> instance with the specified name; otherwise, null.
        /// </returns>
        /// <exception cref="ArgumentNullException">name is null.</exception>
        /// <exception cref="ArgumentException">name is empty.</exception>
        /// <example>
        /// To get a nested member with the name "Pump.Indy.Running":
        /// <code>
        /// var member = members.GetMember("Pump.Indy.Running");
        /// </code>
        /// </example>
        TMember? GetMember(string name);

        /// <summary>
        /// Gets all <c>IMember</c> objects, including nested members, in the current <c>IMemberCollection</c>.
        /// </summary>
        /// <remarks>
        /// This method will recursively traverse the nested hierarchy of <c>IComplexType</c> members to get a deep collection
        /// of member instance for the current <c>IComplexType</c>.
        /// </remarks>
        /// <returns>
        /// A collection of <c>IMember</c> objects that represent all immediate and nested child members of the current
        /// <c>IMemberCollection</c> if any exists.
        /// If none exist, an empty collection of <c>IMember</c>.
        /// </returns>
        IEnumerable<TMember> GetMembers();

        /// <summary>
        /// Gets all <c>IMember</c> names, including nested members, in the <c>IReadOnlyMembers</c> collection.
        /// </summary>
        /// <remarks>
        /// This method will recursively traverse the nested hierarchy of data type members to get a deep collection
        /// of names for each <c>IMember</c> object.
        /// </remarks>
        /// <returns>
        /// A collection of string names that represent the full operand name of each member as seen from the current .
        /// </returns>
        IEnumerable<string> GetNames();
    }
}