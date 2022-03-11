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
    /// <summary>
    /// Generic abstract implementation of the repository using the <see cref="L5XContext"/>.
    /// </summary>
    /// <typeparam name="TComponent">The type of logix component the repository represents.</typeparam>
    public abstract class Repository<TComponent> : IRepository<TComponent> where TComponent : ILogixComponent
    {
        /// <summary>
        /// The current L5X context for the repository.
        /// </summary>
        protected readonly L5XContext Context;

        /// <summary>
        /// The current L5X serializer for the typed component. 
        /// </summary>
        protected readonly IL5XSerializer<TComponent> Serializer;

        /// <summary>
        /// Creates a new instance of the <see cref="Repository{TComponent}"/> with the provided context.
        /// </summary>
        /// <param name="context"></param>
        protected Repository(L5XContext context)
        {
            Context = context;
            Serializer = context.Serializer.For<TComponent>();
        }

        /// <inheritdoc />
        public virtual void Add(TComponent component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            if (Contains(component.Name))
                throw new ComponentNameCollisionException(component.Name, component.GetType());

            var element = component.Serialize();

            Context.L5X.GetContainer<TComponent>().Add(element);
        }

        /// <inheritdoc />
        public virtual bool Contains(ComponentName name) =>
            Context.L5X.GetComponents<TComponent>().Any(x => x.ComponentName() == name);

        /// <inheritdoc />
        public virtual TComponent? Find(Expression<Func<TComponent, bool>> predicate)
        {
            var filter = predicate.ToXExpression();

            var element = Context.L5X.GetComponents<TComponent>().FirstOrDefault(filter);

            return element is not null ? Serializer.Deserialize(element) : default;
        }

        /// <inheritdoc />
        public TComponent? Find(ComponentName name)
        {
            var element = Context.L5X.GetComponents<TComponent>().FirstOrDefault(x => x.ComponentName() == name);
            return element is not null ? Serializer.Deserialize(element) : default;
        }

        /// <inheritdoc />
        public virtual IEnumerable<TComponent> FindAll(Expression<Func<TComponent, bool>> predicate)
        {
            var filter = predicate.ToXExpression();

            var elements = Context.L5X.GetComponents<TComponent>().Where(filter);

            return elements.Select(e => Serializer.Deserialize(e));
        }

        /// <inheritdoc />
        public virtual TComponent Get(ComponentName name)
        {
            var element = Context.L5X.GetComponents<TComponent>().SingleOrDefault(x => x.ComponentName() == name);

            if (element is null)
                throw new ComponentNotFoundException(name, typeof(TComponent));

            return Serializer.Deserialize(element);
        }

        /// <inheritdoc />
        public virtual IEnumerable<TComponent> GetAll() =>
            Context.L5X.GetComponents<TComponent>().Select(e => Serializer.Deserialize(e));

        /// <inheritdoc />
        public IEnumerable<string> Names()
            => Context.L5X.GetComponents<TComponent>().Select(x => x.ComponentName());

        /// <inheritdoc />
        public virtual void Remove(ComponentName name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));
            
            Context.L5X.GetComponents<TComponent>().FirstOrDefault(x => x.ComponentName() == name)?.Remove();
        }

        /// <inheritdoc />
        public virtual void Update(TComponent component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));
            
            var element = component.Serialize();

            var current = Context.L5X.GetComponents<TComponent>()
                .FirstOrDefault(x => x.ComponentName() == component.Name);

            if (current is not null)
            {
                current.ReplaceWith(element);
                return;
            }
            
            Context.L5X.GetContainer<TComponent>().Add(element);
        }
    }
}