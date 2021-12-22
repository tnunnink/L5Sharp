using System;
using System.Collections.Generic;

namespace L5Sharp
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMember"></typeparam>
    public interface IMemberCollection<out TMember> : IComponents<TMember> where TMember : IMember<IDataType>
    {
        /// <summary>
        /// Gets an <c>IMember</c> object with the provided name.
        /// </summary>
        /// <remarks>
        /// This method will recursively traverse the nested hierarchy of data type members to find the specified name.
        /// Users can specify the full path to a specific member (e.g. Pump.Indy.Timer.DN),
        /// or simply the name of an immediate child member (e.g. Pump). 
        /// </remarks>
        /// <param name="name">The name of the member to get.</param>
        /// <returns>
        /// If the member with the specified name is found as an immediate or nested member of the <c>IComplexType</c>,
        /// the <c>IMember</c> instance with the specified name; otherwise, null.
        /// </returns>
        /// <exception cref="ArgumentException">name is null or empty.</exception>
        TMember? DeepGet(string name);

        /// <summary>
        /// Gets all <c>IMember</c> objects, including nested members, in the current <c>IMemberCollection</c>.
        /// </summary>
        /// <remarks>
        /// This method will recursively traverse the nested hierarchy of <c>IComplexType</c> members to get a deep collection
        /// of member instance for the current <c>IComplexType</c>.
        /// For immediate objects only, use <see cref="IComponents{TComponent}.Get"/>.
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
        IEnumerable<string> DeepGetNames();

        /// <summary>
        /// Gets all <c>IMember</c> names, including nested members, for the current <c>IComplexType</c>.
        /// </summary>
        /// <remarks>
        /// This method will recursively traverse the nested hierarchy of data type members to get a deep collection
        /// of names for each <c>IMember</c> object.
        /// </remarks>
        /// <returns>A collection of string names.</returns>
        IEnumerable<string> GetNames();

        /// <summary>
        /// Gets all nested dependent <c>IDataType</c> objects for the current <c>IComplexType</c>.
        /// </summary>
        /// <remarks>
        /// This method will recursively traverse the nested hierarchy of data type members to get a deep collection
        /// of dependent data types for the current <c>IComplexType</c>.
        /// </remarks>
        /// <returns>
        /// A collection of unique data types that the current <c>IComplexType</c> depends on, if any exists.
        /// If none exist, an empty collection of <c>IDataType</c>.
        /// </returns>
        IEnumerable<IDataType> GetDependentTypes();
    }
}