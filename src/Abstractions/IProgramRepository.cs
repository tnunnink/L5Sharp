using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Abstractions
{
    public interface IProgramRepository : IRepository<IProgram>
    {
        IEnumerable<Program> GetByTask(ITask task);
        IEnumerable<Program> GetByType(ProgramType type);
    }
}