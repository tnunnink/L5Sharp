using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using L5Sharp.Core;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;

namespace L5Sharp.Repositories
{
    internal class TaskRepository : IReadOnlyRepository<ITask>
    {
        private readonly L5XContext _context;

        public TaskRepository(L5XContext context)
        {
            _context = context;
        }

        public bool Contains(ComponentName name) =>
            _context.L5X.GetComponents<ITask>().Any(t => t.ComponentName() == name);

        public ITask? Find(Expression<Func<ITask, bool>> predicate) =>
            _context.L5X.GetComponents<ITask>().FirstOrDefault(predicate.ToXExpression())?.Deserialize<ITask>();

        public ITask? Find(ComponentName name) =>
            _context.L5X.GetComponents<ITask>().FirstOrDefault(x => x.ComponentName() == name)?.Deserialize<ITask>();

        public IEnumerable<ITask> FindAll(Expression<Func<ITask, bool>> predicate) =>
            _context.L5X.GetComponents<ITask>().Where(predicate.ToXExpression()).Select(e => e.Deserialize<ITask>());

        public ITask Get(ComponentName name)
        {
            var count = _context.L5X.GetComponents<ITask>().Count(x => x.ComponentName() == name);

            return count switch
            {
                0 => throw new ComponentNotFoundException(name, typeof(ITask)),
                > 1 => throw new InvalidOperationException(
                    $"The provided component name '{name}' has more than one instance in the current context."),
                _ => _context.L5X.GetComponents<ITask>()
                    .Single(x => x.ComponentName() == name)
                    .Deserialize<ITask>()
            };
        }

        public IEnumerable<ITask> GetAll() => _context.L5X.GetComponents<ITask>().Select(t => t.Deserialize<ITask>());

        public IEnumerable<string> Names() => _context.L5X.GetComponents<ITask>().Select(x => x.ComponentName());
    }
}