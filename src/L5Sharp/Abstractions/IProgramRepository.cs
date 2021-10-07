using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Enumerations;

namespace L5Sharp.Abstractions
{
    public interface IProgramRepository : IRepository<Program>
    {
        IEnumerable<Program> GetByTask(Task task);
        IEnumerable<Program> GetByType(ProgramType type);
    }
}