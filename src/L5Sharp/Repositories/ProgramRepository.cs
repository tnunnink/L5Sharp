using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Enumerations;

namespace L5Sharp.Repositories
{
    public class ProgramRepository : IProgramRepository
    {
        public IEnumerable<Program> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Program Get(string name)
        {
            throw new System.NotImplementedException();
        }

        public void Add(Program component)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(Program component)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Program component)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Program> GetByTask(Task task)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Program> GetByType(ProgramType type)
        {
            throw new System.NotImplementedException();
        }
    }
}