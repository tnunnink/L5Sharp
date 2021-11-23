using System.Collections.Generic;
using System.Linq;
using L5Sharp.Extensions;

namespace L5Sharp.Repositories
{
    /// <inheritdoc />
    internal class TaskRepository : ITaskRepository
    {
        private readonly LogixContext _context;

        /// <summary>
        /// Creates a new instance of the repository with the provided context.
        /// </summary>
        /// <param name="context"></param>
        public TaskRepository(LogixContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public bool Exists(string name)
        {
            return _context.L5X.Tasks.Elements(LogixNames.Task).Any(e => e.GetName() == name);
        }

        /// <inheritdoc />
        public ITask Get(string name)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<ITask> GetAll()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public void Add(ITask component)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public void Remove(ITask component)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public void Update(ITask component)
        {
            throw new System.NotImplementedException();
        }
    }
}