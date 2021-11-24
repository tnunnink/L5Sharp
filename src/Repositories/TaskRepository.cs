using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

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
        public bool Contains(string name)
        {
            return _context.L5X.Tasks.Descendants(LogixNames.Task).Any(e => e.GetName() == name);
        }

        /// <inheritdoc />
        public ITask Get(string name)
        {
            var element = _context.L5X.Tasks.Descendants(LogixNames.Task).SingleOrDefault(e => e.GetName() == name);
            return _context.Serializer.Deserialize<ITask>(element);
        }

        /// <inheritdoc />
        public IEnumerable<ITask> GetAll()
        {
            var elements = _context.L5X.Tasks.Descendants(LogixNames.Task);
            return elements.Select(e => _context.Serializer.Deserialize<ITask>(e));
        }
        
        public IEnumerable<ITask> Find(Expression<Func<ITask, bool>> predicate)
        {
            var filter = predicate.ToElementFunc();

            var elements = _context.L5X.Tasks.Descendants(LogixNames.Task).Where(filter);

            return elements.Select(e => _context.Serializer.Deserialize<ITask>(e));
        }

        /// <inheritdoc />
        public void Add(ITask component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            if (Contains(component.Name))
                throw new ComponentNameCollisionException(component.Name, component.GetType());
            
            var element = _context.Serializer.Serialize(component);
            
            _context.L5X.Tasks.Add(element);
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