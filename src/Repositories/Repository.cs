using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using L5Sharp.Core;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;

namespace L5Sharp.Repositories
{
    /// <summary>
    /// Generic abstract implementation of the read only repository.
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    internal abstract class Repository<TComponent> : IRepository<TComponent> where TComponent : ILogixComponent
    {
        /// <summary>
        /// The current logix context for the repository.
        /// </summary>
        protected readonly LogixContext Context;

        /// <summary>
        /// Creates a new instance of the <see cref="Repository{TComponent}"/> with the provided context.
        /// </summary>
        /// <param name="context"></param>
        protected Repository(LogixContext context)
        {
            Context = context;
        }

        /// <inheritdoc />
        public virtual bool Contains(string name)
        {
            return Context.L5X.GetComponents<TComponent>().Any(x => x.GetName() == name);
        }

        /// <inheritdoc />
        public virtual TComponent Get(string name)
        {
            var element = Context.L5X.GetComponents<TComponent>().FirstOrDefault(x => x.GetName() == name);
            return Context.Serializer.Deserialize<TComponent>(element);
        }

        /// <inheritdoc />
        public virtual IEnumerable<TComponent> GetAll()
        {
            var elements = Context.L5X.GetComponents<TComponent>();
            return elements.Select(e => Context.Serializer.Deserialize<TComponent>(e));
        }

        /// <inheritdoc />
        public virtual TComponent GetFirst(Expression<Func<TComponent, bool>> predicate)
        {
            var filter = predicate.ToXExpression();
            var element = Context.L5X.GetComponents<TComponent>().FirstOrDefault(filter);
            return Context.Serializer.Deserialize<TComponent>(element);
        }

        /// <inheritdoc />
        public virtual IEnumerable<TComponent> Find(Expression<Func<TComponent, bool>> predicate)
        {
            var filter = predicate.ToXExpression();
            var elements = Context.L5X.GetComponents<TComponent>().Where(filter);
            return elements.Select(e => Context.Serializer.Deserialize<TComponent>(e));
        }

        public virtual void Add(TComponent component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            if (Contains(component.Name))
                throw new ComponentNameCollisionException(component.Name, component.GetType());
            
            var element = Context.Serializer.Serialize(component);
            
            Context.L5X.GetContainer<TComponent>().Add(element);
        }

        public virtual void Update(TComponent component)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(ComponentName name)
        {
            Context.L5X.GetComponents<TComponent>().FirstOrDefault(x => x.GetName() == name)?.Remove();
        }
    }
}