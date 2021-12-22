using System;
using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Exceptions;

namespace L5Sharp.Abstractions
{
    
    public class ComponentCollection<TComponent> : Components<TComponent>, IComponentCollection<TComponent>
        where TComponent : ILogixComponent
    {
        /// <summary>
        /// Creates a new instance of <see cref="ComponentCollection{TComponent}"/> for the specified
        /// <see cref="ILogixComponent"/> with the provided collection of components. 
        /// </summary>
        /// <param name="components">A collection of components to initialize the collection with.</param>
        internal ComponentCollection(IEnumerable<TComponent>? components = null) 
            : base(components)
        {
        }
        
        /// <inheritdoc />
        public void Clear()
        {
            Collection.Clear();
        }

        /// <inheritdoc />
        public virtual void Add(TComponent component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            if (Collection.ContainsKey(component.Name))
                throw new ComponentNameCollisionException(component.Name, typeof(TComponent));

            Collection.Add(component.Name, component);
        }

        /// <inheritdoc />
        public virtual void AddRange(IEnumerable<TComponent> components)
        {
            if (components is null)
                throw new ArgumentNullException(nameof(components));
            
            foreach (var component in components)
            {
                if (Collection.ContainsKey(component.Name))
                    throw new ComponentNameCollisionException(component.Name, component.GetType());

                Collection.Add(component.Name, component);
            }
        }

        /// <inheritdoc />
        public virtual void Update(TComponent component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            if (!Collection.ContainsKey(component.Name))
                Add(component);

            Collection[component.Name] = component;
        }

        /// <inheritdoc />
        public virtual void Remove(ComponentName name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException();

            if (!Collection.ContainsKey(name))
                return;

            Collection.Remove(name);
        }

        /// <inheritdoc />
        public virtual void Replace(ComponentName name, TComponent component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            if (!Collection.ContainsKey(name))
                return;

            Collection.Remove(name);
            
            if (Collection.ContainsKey(component.Name))
                throw new ComponentNameCollisionException(component.Name, typeof(TComponent));
            
            Collection.Add(component.Name, component);
        }
    }
}