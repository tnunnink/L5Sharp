using System;
using System.Collections.Generic;
using L5Sharp.Exceptions;

namespace L5Sharp.Abstractions
{
    /// <inheritdoc cref="L5Sharp.IComponentsList{TComponent}" />
    public abstract class ComponentList<TComponent> : ComponentCollection<TComponent>, IComponentsList<TComponent>
        where TComponent : ILogixComponent
    {
        /// <summary>
        /// Creates a new instance of <see cref="ComponentList{TComponent}"/> for the specified
        /// <see cref="ILogixComponent"/> with the provided collection of components. 
        /// </summary>
        /// <param name="components">A collection of components to initialize the collection with.</param>
        internal ComponentList(IEnumerable<TComponent>? components = null) 
            : base(components)
        {
        }
        
        /// <inheritdoc />
        public virtual void Clear()
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
        public virtual void Remove(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException();

            if (!Collection.ContainsKey(name))
                return;

            Collection.Remove(name);
        }
    }
}