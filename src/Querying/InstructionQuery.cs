using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Extensions;
using L5Sharp.L5X;
using L5Sharp.Serialization;

namespace L5Sharp.Querying
{
    /// <summary>
    /// A fluent <see cref="ILogixQuery{TResult}"/> that adds advanced querying for <see cref="IAddOnInstruction"/>
    /// elements within the L5X context.  
    /// </summary>
    public class InstructionQuery : LogixQuery<IAddOnInstruction>
    {
        /// <summary>
        /// Creates a new <see cref="TaskQuery"/> with the provided source element to query.
        /// </summary>
        /// <param name="source">An <see cref="IEnumerable{T}"/> containing the source <see cref="XElement"/> objects
        /// to query.</param>
        public InstructionQuery(IEnumerable<XElement> source) : base(source)
        {
        }

        /// <summary>
        /// Filters the collection to include only instructions that are referenced by a tag in the current context.
        /// </summary>
        /// <returns>A new <see cref="InstructionQuery"/> containing only reference instructions.</returns>
        public InstructionQuery IsReferenced()
        {
            var tagTypes = this.Ancestors(L5XName.RSLogix5000Content).First()
                .Descendants(L5XName.Tag)
                .Select(t => t.DataTypeName());

            var results = this.Where(e => tagTypes.Contains(e.ComponentName()));

            return new InstructionQuery(results);
        }
    }
}