using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using L5Sharp.Extensions;

namespace L5Sharp.Repositories
{
    internal class TaskRepository : IReadOnlyRepository<ITask>
    {
        private readonly LogixContext _context;

        public TaskRepository(LogixContext context)
        {
            _context = context;
        }

        public bool Contains(string name) => _context.L5X.GetComponents<ITask>().Any(t => t.GetComponentName() == name);

        public ITask? Get(string name)
        {
            var task = _context.L5X.GetComponents<ITask>().FirstOrDefault(t => t.GetComponentName() == name);

            return task is not null ? _context.Serializer.Deserialize<ITask>(task) : null;
        }

        public IEnumerable<ITask> GetAll()
        {
            var tasks = _context.L5X.GetComponents<ITask>();

            return tasks.Select(e => _context.Serializer.Deserialize<ITask>(e));
        }

        public ITask? Find(Expression<Func<ITask, bool>> predicate)
        {
            var filter = predicate.ToXExpression();
            var task = _context.L5X.GetComponents<ITask>().FirstOrDefault(filter);
            return task is not null ? _context.Serializer.Deserialize<ITask>(task) : null;
        }

        public IEnumerable<ITask> FindAll(Expression<Func<ITask, bool>> predicate)
        {
            var filter = predicate.ToXExpression();
            var tasks = _context.L5X.GetComponents<ITask>().Where(filter);
            return tasks.Select(t => _context.Serializer.Deserialize<ITask>(t));
        }
    }
}