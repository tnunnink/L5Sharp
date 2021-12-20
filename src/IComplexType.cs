using System;
using System.Collections.Generic;

namespace L5Sharp
{
    /// <summary>
    /// Represents an <c>IDataType</c> that has <c>IMember</c> collection.
    /// </summary>
    /// <remarks>
    /// This type is the basis for forming complex structure of nested members and data types.
    /// All non atomic types derive from <c>IComplexType</c>. 
    /// </remarks>
    public interface IComplexType : IDataType
    {
        /// <summary>
        /// Gets the collection of <c>IMember</c> objects for the current <c>IComplexType</c>.
        /// </summary>
        /// <remarks>
        /// This property represents the immediate child <c>IMember</c> instances of the current <c>ComplexType</c>,
        /// and not the flattened collection of nested <c>IMember</c> objects.
        /// To get a flat collection of nested <c>IMember</c> objects, use <see cref="GetMembers"/>.  
        /// </remarks>
        IEnumerable<IMember<IDataType>> Members { get; }

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
        IMember<IDataType>? GetMember(string name);

        /// <summary>
        /// Gets all <c>IMember</c> objects, including nested members, for the current <c>IComplexType</c>.
        /// </summary>
        /// <remarks>
        /// This method will recursively traverse the nested hierarchy of data type members to get a deep collection
        /// of member instance for the current <c>IComplexType</c>.
        /// For immediate child objects only, use <see cref="Members"/> property.
        /// </remarks>
        /// <returns>
        /// A collection of <c>IMember</c> objects that represent all immediate and nested child members of the current
        /// <c>IComplexType</c> if any exists.
        /// If none exist, an empty collection of <c>IMember</c>.
        /// </returns>
        IEnumerable<IMember<IDataType>> GetMembers();

        /// <summary>
        /// Gets all <c>IMember</c> names, including nested members, for the current <c>IComplexType</c>.
        /// </summary>
        /// <remarks>
        /// This method will recursively traverse the nested hierarchy of data type members to get a deep collection
        /// of names for each <c>IMember</c> object.
        /// </remarks>
        /// <returns>A collection of string names.</returns>
        IEnumerable<string> GetMemberNames();

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