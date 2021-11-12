using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Repositories
{
    internal class ProgramRepository : IProgramRepository
    {
        private readonly LogixContext _context;

        public ProgramRepository(LogixContext context)
        {
            _context = context;
        }

        public bool Exists(string name)
        {
            return _context.L5X.Programs.Descendants().Any(x => x.GetName() == name);
        }

        public IProgram Get(string name)
        {
            var element = _context.L5X.Programs.Descendants().SingleOrDefault(x => x.GetName() == name);
            return null;
        }

        public IEnumerable<Program> GetByTask(ITask task)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Program> GetByType(ProgramType type)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IProgram> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Add(IProgram component)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(IProgram component)
        {
            throw new System.NotImplementedException();
        }

        public void Update(IProgram component)
        {
            throw new System.NotImplementedException();
        }
    }
}