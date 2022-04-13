using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Querying
{
    /// <summary>
    /// A fluent <see cref="ILogixQuery{TResult}"/> that adds advanced querying for <see cref="IComplexType"/> elements
    /// within the L5X context. 
    /// </summary>
    public class DataTypeQuery : LogixQuery<IComplexType>
    {
        /// <summary>
        /// Creates a new <see cref="DataTypeQuery"/> with the provided source elements to query.
        /// </summary>
        /// <param name="source">An <see cref="IEnumerable{T}"/> containing the source <see cref="XElement"/> objects
        /// to query.</param>
        public DataTypeQuery(IEnumerable<XElement> source) : base(source)
        {
        }

        /// <summary>
        /// Filters the collection to only data types that depend on the specified type name.
        /// </summary>
        /// <param name="typeName">The name of the child data type to match.</param>
        /// <returns>A new <see cref="DataTypeQuery"/> containing the filtered collection.</returns>
        /// <remarks>
        /// This query is similar to <see cref="UsedBy"/>, but instead of getting the data types used by the specified
        /// type, this query gets other types that reference the specified type as a member of it's data type structure,
        /// therefore giving you only types that depend on it.
        /// </remarks>
        public DataTypeQuery DependingOn(string typeName)
        {
            if (typeName is null)
                throw new ArgumentNullException(nameof(typeName));
            
            var results = this.Where(e => e.Descendants(L5XElement.Member.ToString())
                    .Any(c => c.Attribute(L5XAttribute.DataType.ToString())?.Value == typeName));

            return new DataTypeQuery(results);
        }

        /// <summary>
        /// Filters the collection to only data types with the specified <see cref="DataTypeFamily"/> value. 
        /// </summary>
        /// <param name="family">The <see cref="DataTypeFamily"/> value to filter on.</param>
        /// <returns>A new <see cref="DataTypeQuery"/> containing only data types with the specified family.</returns>
        public DataTypeQuery OfFamily(DataTypeFamily family)
        {
            if (family is null)
                throw new ArgumentNullException(nameof(family));

            var results = this.Where(e => e.Attribute(L5XAttribute.Family.ToString())?.Value == family.Value);

            return new DataTypeQuery(results);
        }

        /// <summary>
        /// Filters the collection to only data types that are used by the members of the specified type name. 
        /// </summary>
        /// <param name="typeName">The name of the data type for which to filter member type of.</param>
        /// <returns>A new <see cref="DataTypeQuery"/> containing the data types that are used by the specified
        /// type name.</returns>
        /// <remarks>
        /// This query is similar to <see cref="DependingOn"/>, but instead of getting other data types that reference
        /// the one specified, this query just gets the data types that the specified type references, therefore, giving
        /// you only the types that are used by it. 
        /// </remarks>
        public DataTypeQuery UsedBy(string typeName)
        {
            if (typeName is null)
                throw new ArgumentNullException(nameof(typeName));

            var typeNames = this.FirstOrDefault(e => e.ComponentName() == typeName)
                ?.Descendants(L5XElement.Member.ToString())
                .Select(e => e.Attribute(L5XAttribute.DataType.ToString())?.Value)
                .ToList();

            if (typeNames is null || typeNames.Count == 0)
            {
                return new DataTypeQuery(Enumerable.Empty<XElement>());
            }

            var results = this.Where(e => typeNames.Contains(e.ComponentName()));
            return new DataTypeQuery(results);
        }
    }
}