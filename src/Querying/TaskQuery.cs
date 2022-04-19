using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Querying
{
    /// <summary>
    /// A fluent <see cref="ILogixQuery{TResult}"/> that adds advanced querying for <see cref="ITask"/> elements
    /// within the L5X context.  
    /// </summary>
    public class TaskQuery : LogixQuery<ITask>
    {
        /// <summary>
        /// Creates a new <see cref="TaskQuery"/> with the provided source element to query.
        /// </summary>
        /// <param name="source">An <see cref="IEnumerable{T}"/> containing the source <see cref="XElement"/> objects
        /// to query.</param>
        public TaskQuery(IEnumerable<XElement> source) : base(source)
        {
        }

        /// <summary>
        /// Filters the source collection to include only tasks with the specified program scheduled to it.
        /// </summary>
        /// <param name="type">The program name for which to filter the task collection.</param>
        /// <returns>A new <see cref="TaskQuery"/> with the filtered element collection.</returns>
        public TaskQuery OfType(TaskType type)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            var results = this.Where(e => e.Attribute(L5XName.Type)?.Value == type.Value);

            return new TaskQuery(results);
        }
        
        /// <summary>
        /// Filters the collection to include only tasks that satisfy the <see cref="ScanRate"/> predicate condition.
        /// </summary>
        /// <param name="predicate">A predicate condition for which to filter task components.</param>
        /// <returns>A new <see cref="TaskQuery"/> with the filtered element collection.</returns>
        /// <exception cref="ArgumentNullException">predicate is null.</exception>
        public TaskQuery WithRate(Predicate<ScanRate> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            var results = this.Where(e =>
            {
                var rate = e.Attribute("Rate")?.Value;
                return rate is not null && predicate.Invoke(rate.Parse<ScanRate>());
            });

            return new TaskQuery(results);
        }

        /// <summary>
        /// Filters the source collection to include only tasks with the specified program scheduled to it.
        /// </summary>
        /// <param name="program">The program name for which to filter the task collection.</param>
        /// <returns>A new <see cref="TaskQuery"/> with the filtered element collection.</returns>
        public TaskQuery ForProgram(ComponentName program)
        {
            if (program is null)
                throw new ArgumentNullException(nameof(program));

            var results = this.Where(e =>
            {
                var programs = e.Descendants(L5XName.ScheduledProgram).Select(c => c.ComponentName());
                return programs.Contains(program.ToString());
            });
            
            return new TaskQuery(results);
        }
    }
}