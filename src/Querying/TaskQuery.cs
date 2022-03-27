using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.L5X;
using L5Sharp.Serialization;

namespace L5Sharp.Querying
{
    internal class TaskQuery : ComponentQuery<ITask>, ITaskQuery
    {
        public TaskQuery(IEnumerable<XElement> elements, IL5XSerializer<ITask> serializer) 
            : base(elements, serializer)
        {
        }
        
        public ITaskQuery WithProgram(ComponentName program)
        {
            var results = Elements.Where(e =>
                e.Descendants(L5XElement.ScheduledProgram.ToString())
                    .Select(c => c.ComponentName()).Contains(program.ToString()));

            return new TaskQuery(results, Serializer);
        }
    }
}