using System.Collections.Generic;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Repositories
{
    internal class ProgramRepository : Repository<IProgram>, IProgramRepository
    {
        public ProgramRepository(LogixContext context) : base(context)
        {
        }

        public IEnumerable<Program> GetByTask(ITask task)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Program> GetByType(ProgramType type)
        {
            throw new System.NotImplementedException();
        }
    }
}