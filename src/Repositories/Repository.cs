using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;
using L5Sharp.L5X;
using L5Sharp.Serialization;

namespace L5Sharp.Repositories
{
    /// <summary>
    /// Generic abstract implementation of the repository using the <see cref="L5XContext"/>.
    /// </summary>
    /// <typeparam name="TComponent">The type of logix component the repository represents.</typeparam>
    public abstract class Repository<TComponent> : IRepository<TComponent> where TComponent : ILogixComponent
    {
        /*/// <summary>
        /// The current L5X context for the repository.
        /// </summary>
        protected readonly L5XContext Context;*/

        /// <summary>
        /// The current L5X serializer for the typed component. 
        /// </summary>
        protected readonly IL5XSerializer<TComponent> Serializer;

        /// <summary>
        /// The current L5X containing element for the component repository. 
        /// </summary>
        protected XElement Container;

        /// <summary>
        /// The current L5X containing element for the component repository. 
        /// </summary>
        protected IEnumerable<XElement> Components;

        /// <summary>
        /// Creates a new instance of the <see cref="Repository{TComponent}"/> with the provided context.
        /// </summary>
        /// <param name="context"></param>
        protected Repository(L5XContext context)
        {
            Serializer = context.Serializer.GetFor<TComponent>();
            Container = context.L5X.GetContainer<TComponent>();
            Components = Container.Descendants(L5XNames.GetComponentName<TComponent>())
                .Where(e => e.Attribute(L5XAttribute.Name.ToString()) is not null);
        }

        /// <inheritdoc />
        public virtual void Add(TComponent component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            if (Contains(component.Name))
                throw new ComponentNameCollisionException(component.Name, component.GetType());

            var element = Serializer.Serialize(component);

            Container.Add(element);
        }

        /// <inheritdoc />
        public virtual bool Contains(ComponentName name) =>
            Components.Any(x => x.ComponentName() == name);

        /// <inheritdoc />
        public virtual TComponent? Find(Expression<Func<TComponent, bool>> predicate)
        {
            var filter = predicate.ToXExpression();

            var element = Components.FirstOrDefault(filter);

            return element is not null ? Serializer.Deserialize(element) : default;
        }

        /// <inheritdoc />
        public TComponent? Find(ComponentName name)
        {
            var element = Components.FirstOrDefault(x => x.ComponentName() == name);
            return element is not null ? Serializer.Deserialize(element) : default;
        }

        /// <inheritdoc />
        public virtual IEnumerable<TComponent> FindAll(Expression<Func<TComponent, bool>> predicate)
        {
            var filter = predicate.ToXExpression();

            var elements = Components.Where(filter);

            return elements.Select(e => Serializer.Deserialize(e));
        }

        /// <inheritdoc />
        public virtual TComponent Get(ComponentName name)
        {
            var element = Components.SingleOrDefault(x => x.ComponentName() == name);

            if (element is null)
                throw new ComponentNotFoundException(name, typeof(TComponent));

            return Serializer.Deserialize(element);
        }

        /// <inheritdoc />
        public virtual IEnumerable<TComponent> GetAll() => Components.Select(e => Serializer.Deserialize(e));

        /// <inheritdoc />
        public IEnumerable<string> Names() => Components.Select(x => x.ComponentName());

        /// <inheritdoc />
        public virtual void Remove(ComponentName name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            Components.FirstOrDefault(x => x.ComponentName() == name)?.Remove();
        }

        /// <inheritdoc />
        public virtual void Update(TComponent component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = Serializer.Serialize(component);

            var current = Components.FirstOrDefault(x => x.ComponentName() == component.Name);

            if (current is not null)
            {
                current.ReplaceWith(element);
                return;
            }

            Container.Add(element);
        }
    }
}