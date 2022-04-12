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
    /// <inheritdoc cref="L5Sharp.Querying.ITaskQuery" />
    public class TaskQuery : LogixQuery<ITask>, ITaskQuery
    {
        /// <summary>
        /// Creates a new <see cref="TaskQuery"/> with the provided source element to operate over.
        /// </summary>
        /// <param name="source">An <see cref="IEnumerable{T}"/> containing the source <see cref="XElement"/> objects
        /// to query.</param>
        public TaskQuery(IEnumerable<XElement> source) : base(source)
        {
        }

        /// <inheritdoc />
        public ITaskQuery OfType(TaskType type)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            var results = this.Where(e => e.Attribute(L5XAttribute.Type.ToString())?.Value == type.Value);

            return new TaskQuery(results);
        }

        /// <inheritdoc />
        public ITaskQuery WithRate(Predicate<ScanRate> predicate)
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

        /// <inheritdoc />
        public ITaskQuery ForProgram(ComponentName program)
        {
            if (program is null)
                throw new ArgumentNullException(nameof(program));

            var results = this.Where(e =>
            {
                var programs = e.Descendants(L5XElement.ScheduledProgram.ToString()).Select(c => c.ComponentName());
                return programs.Contains(program.ToString());
            });
            
            return new TaskQuery(results);
        }
    }
}