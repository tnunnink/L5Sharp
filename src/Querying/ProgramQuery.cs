using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Querying
{
    /// <summary>
    /// A fluent <see cref="ILogixQuery{TResult}"/> that adds advanced querying for <see cref="IProgram"/> elements
    /// within the L5X context.  
    /// </summary>
    public class ProgramQuery : LogixQuery<IProgram>
    {
        /// <summary>
        /// Creates a new <see cref="ProgramQuery"/> with the provided source element to query.
        /// </summary>
        /// <param name="source">An <see cref="IEnumerable{T}"/> containing the source <see cref="XElement"/> objects
        /// to query.</param>
        public ProgramQuery(IEnumerable<XElement> source) : base(source)
        {
        }

        /// <summary>
        /// Filters the collection to include only programs that are scheduled in the specified task name.
        /// </summary>
        /// <param name="taskName">The name of the task that the programs are scheduled in.</param>
        /// <returns>A new <see cref="ProgramQuery"/> containing only programs in the specified task.</returns>
        /// <exception cref="ArgumentNullException">taskName is null.</exception>
        public ProgramQuery InTask(ComponentName taskName)
        {
            if (taskName is null)
                throw new ArgumentNullException(nameof(taskName));

            var programNames = this.Ancestors(L5XName.Controller)
                .FirstOrDefault()
                ?.Descendants(L5XName.Task)
                .FirstOrDefault(t => t.ComponentName() == taskName)
                ?.Descendants(L5XName.ScheduledProgram)
                .Select(e => e.ComponentName())
                .ToList();

            if (programNames is null)
            {
                return new ProgramQuery(Enumerable.Empty<XElement>());
            }

            var results = this.Where(e => programNames.Contains(e.ComponentName()));

            return new ProgramQuery(results);
        }
    }
}