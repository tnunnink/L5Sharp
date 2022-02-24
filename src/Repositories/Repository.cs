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
    /// Generic abstract implementation of the repository using the <see cref="L5XContext"/>.
    /// </summary>
    /// <typeparam name="TComponent">The type of logix component the repository represents.</typeparam>
    public abstract class Repository<TComponent> : IRepository<TComponent> where TComponent : ILogixComponent
    {
        /// <summary>
        /// The current logix context for the repository.
        /// </summary>
        protected readonly L5XContext Context;

        /// <summary>
        /// Creates a new instance of the <see cref="Repository{TComponent}"/> with the provided context.
        /// </summary>
        /// <param name="context"></param>
        protected Repository(L5XContext context)
        {
            Context = context;
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
            Context.L5X.GetComponents<TComponent>().Any(x => x.GetComponentName() == name);

        /// <inheritdoc />
        public virtual TComponent? Find(Expression<Func<TComponent, bool>> predicate)
        {
            var filter = predicate.ToXExpression();

            var element = Context.L5X.GetComponents<TComponent>().FirstOrDefault(filter);

            return element is not null ? element.Deserialize<TComponent>() : default;
        }

        /// <inheritdoc />
        public TComponent? Find(ComponentName name)
        {
            var element = Context.L5X.GetComponents<TComponent>().FirstOrDefault(x => x.GetComponentName() == name);
            return element is not null ? element.Deserialize<TComponent>() : default;
        }

        /// <inheritdoc />
        public virtual IEnumerable<TComponent> FindAll(Expression<Func<TComponent, bool>> predicate)
        {
            var filter = predicate.ToXExpression();

            var elements = Context.L5X.GetComponents<TComponent>().Where(filter);

            return elements.Select(e => e.Deserialize<TComponent>());
        }

        /// <inheritdoc />
        public virtual TComponent Get(ComponentName name)
        {
            var count = Context.L5X.GetComponents<TComponent>().Count(x => x.GetComponentName() == name);

            return count switch
            {
                0 => throw new ComponentNotFoundException(name, typeof(TComponent)),
                > 1 => throw new InvalidOperationException(
                    $"The provided component name '{name}' has more than one instance in the current context."),
                _ => Context.L5X.GetComponents<TComponent>()
                    .Single(x => x.GetComponentName() == name)
                    .Deserialize<TComponent>()
            };
        }

        /// <inheritdoc />
        public virtual IEnumerable<TComponent> GetAll() =>
            Context.L5X.GetComponents<TComponent>().Select(e => e.Deserialize<TComponent>());

        /// <inheritdoc />
        public IEnumerable<string> Names()
            => Context.L5X.GetComponents<TComponent>().Select(x => x.GetComponentName());

        /// <inheritdoc />
        public virtual void Remove(ComponentName name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));
            
            Context.L5X.GetComponents<TComponent>().FirstOrDefault(x => x.GetComponentName() == name)?.Remove();
        }

        /// <inheritdoc />
        public virtual void Update(TComponent component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));
            
            var element = component.Serialize();

            var current = Context.L5X.GetComponents<TComponent>()
                .FirstOrDefault(x => x.GetComponentName() == component.Name);

            if (current is not null)
            {
                current.ReplaceWith(element);
                return;
            }
            
            Context.L5X.GetContainer<TComponent>().Add(element);
        }
    }
}