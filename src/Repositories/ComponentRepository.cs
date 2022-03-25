using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;
using L5Sharp.Querying;
using L5Sharp.Serialization;

namespace L5Sharp.Repositories
{
    /// <summary>
    /// Generic abstract implementation of the repository using the <see cref="L5XContext"/>.
    /// </summary>
    /// <typeparam name="TComponent">The type of logix component the repository represents.</typeparam>
    public abstract class ComponentRepository<TComponent> : ComponentQuery<TComponent>, IComponentRepository<TComponent>
        where TComponent : ILogixComponent
    {
        /// <summary>
        /// Creates a new <see cref="ComponentRepository{TComponent}"/> with the provided root container and serializer.
        /// </summary>
        /// <param name="elements">The source collection of elements for which to query and mutate.</param>
        /// <param name="serializer">The serializer instance that can deserialize elements to the specified result type.</param>
        protected ComponentRepository(IEnumerable<XElement> elements, IL5XSerializer<TComponent> serializer)
            : base(elements, serializer)
        {
        }

        /// <inheritdoc />
        public virtual void Add(TComponent component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            if (Elements.Any(e => e.ComponentName() == component.Name))
                throw new ComponentNameCollisionException(component.Name, component.GetType());

            var element = Serializer.Serialize(component);

            Elements.Last().AddAfterSelf(element);
        }


        /// <inheritdoc />
        public virtual void Remove(ComponentName name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            Elements.FirstOrDefault(x => x.ComponentName() == name)?.Remove();
        }

        /// <inheritdoc />
        public virtual void Update(TComponent component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = Serializer.Serialize(component);

            var current = Elements.FirstOrDefault(x => x.ComponentName() == component.Name);

            if (current is not null)
            {
                current.ReplaceWith(element);
                return;
            }

            Elements.Last().AddAfterSelf(element);
        }
    }
}