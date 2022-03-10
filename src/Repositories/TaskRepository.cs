using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using L5Sharp.Core;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;
using L5Sharp.Serialization;

namespace L5Sharp.Repositories
{
    internal class TaskRepository : IReadOnlyRepository<ITask>
    {
        private readonly L5XContext _context;
        private readonly IL5XSerializer<ITask> _serializer;

        public TaskRepository(L5XContext context)
        {
            _context = context;
            _serializer = context.Serializers.GetSerializer<ITask>();
        }

        public bool Contains(ComponentName name) =>
            _context.L5X.GetComponents<ITask>().Any(t => t.ComponentName() == name);

        public ITask? Find(Expression<Func<ITask, bool>> predicate) =>
            _serializer.Deserialize(_context.L5X.GetComponents<ITask>().FirstOrDefault(predicate.ToXExpression()));

        public ITask? Find(ComponentName name) =>
            _serializer.Deserialize(_context.L5X.GetComponents<ITask>().FirstOrDefault(x => x.ComponentName() == name));

        public IEnumerable<ITask> FindAll(Expression<Func<ITask, bool>> predicate) =>
            _context.L5X.GetComponents<ITask>().Where(predicate.ToXExpression()).Select(e => _serializer.Deserialize(e));

        public ITask Get(ComponentName name)
        {
            var element = _context.L5X.GetComponents<ITask>().SingleOrDefault(x => x.ComponentName() == name);

            if (element is null)
                throw new ComponentNotFoundException(name, typeof(ITask));

            return _serializer.Deserialize(element);
        }

        public IEnumerable<ITask> GetAll() => _context.L5X.GetComponents<ITask>().Select(e => _serializer.Deserialize(e));

        public IEnumerable<string> Names() => _context.L5X.GetComponents<ITask>().Select(x => x.ComponentName());
    }
}