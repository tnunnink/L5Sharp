using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Exceptions;

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public class ComponentCollection<TComponent> : IComponentCollection<TComponent>
        where TComponent : ILogixComponent
    {
        private readonly Dictionary<string, TComponent> _components;

        /// <summary>
        /// Creates a new empty <see cref="ComponentCollection{TComponent}"/> object. 
        /// </summary>
        public ComponentCollection()
        {
            _components = new Dictionary<string, TComponent>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Creates a new initialized <see cref="ComponentCollection{TComponent}"/> with the provided collection
        /// of <see cref="ILogixComponent"/> object.
        /// </summary>
        /// <param name="components">The collection of components to initialize the collection with.</param>
        /// <exception cref="ArgumentNullException">components is null.</exception>
        /// <remarks>
        /// This constructor will take the the provided component collection and add each valid instance.
        /// If a component is null, or the name is a duplicate (i.e. already added to the collection),
        /// it will be ignored as it is considered invalid.  
        /// </remarks>
        public ComponentCollection(IEnumerable<TComponent>? components) : this()
        {
            if (components is null)
                return;

            foreach (var component in components)
                if (component is not null && !_components.ContainsKey(component.Name))
                    _components.Add(component.Name, component);
        }


        /// <summary>
        /// Gets the number of items in the <see cref="ComponentCollection{TComponent}"/>.
        /// </summary>
        public int Count => _components.Count;

        /// <summary>
        /// Gets value indicating whether the <see cref="ComponentCollection{TComponent}"/> is read-only. 
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Adds the provided component to the <see cref="ComponentCollection{TComponent}"/>.
        /// </summary>
        /// <param name="component">The component to add.</param>
        /// <exception cref="ArgumentNullException">component is null.</exception>
        /// <exception cref="ComponentNameCollisionException">component name is a duplicate.</exception>
        public void Add(TComponent component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            if (_components.ContainsKey(component.Name))
                throw new ComponentNameCollisionException(component.Name, typeof(TComponent));

            _components.Add(component.Name, component);
        }

        /// <summary>
        /// Clears all components from the <see cref="ComponentCollection{TComponent}"/>.
        /// </summary>
        public void Clear() => _components.Clear();

        /// <summary>
        /// Determines if the provided component is contained in the <see cref="ComponentCollection{TComponent}"/>.
        /// </summary>
        /// <param name="component">The component to locate.</param>
        /// <returns>true if the component is not null and is found in the collection; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">component is null.</exception>
        public bool Contains(TComponent component) => component is not null
            ? _components.ContainsKey(component.Name)
            : throw new ArgumentNullException(nameof(component));

        /// <inheritdoc />
        public void CopyTo(TComponent[] array, int arrayIndex) => _components.Values.CopyTo(array, arrayIndex);

        /// <inheritdoc />
        public TComponent? Find(Predicate<TComponent> match) => match is not null
            ? _components.Values.FirstOrDefault(c => match(c))
            : throw new ArgumentNullException(nameof(match));

        /// <inheritdoc />
        public IEnumerable<TComponent> FindAll(Predicate<TComponent> match) => match is not null
            ? _components.Values.Where(c => match(c))
            : throw new ArgumentNullException(nameof(match));

        /// <inheritdoc />
        public TComponent? Get(ComponentName name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            return _components.ContainsKey(name) ? _components[name] : default;
        }

        /// <summary>
        /// Removes the provided component from the <see cref="ComponentCollection{TComponent}"/>.
        /// </summary>
        /// <param name="component">The component to remove.</param>
        /// <returns>true if the component is successfully found and removed; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">component is null.</exception>
        public bool Remove(TComponent component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            return _components.Remove(component.Name);
        }

        /// <inheritdoc />
        public bool Remove(ComponentName name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            return _components.Remove(name);
        }

        /// <inheritdoc />
        public void Upsert(TComponent component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            if (!_components.ContainsKey(component.Name))
            {
                _components.Add(component.Name, component);
                return;
            }

            _components[component.Name] = component;
        }

        /// <inheritdoc />
        public void Upsert(IEnumerable<TComponent> components)
        {
            if (components is null)
                throw new ArgumentNullException(nameof(components));

            foreach (var component in components)
                Upsert(component);
        }

        /// <inheritdoc />
        public IEnumerator<TComponent> GetEnumerator()
        {
            return _components.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}