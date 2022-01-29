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


        /// <inheritdoc />
        public int Count => _components.Count;

        /// <inheritdoc />
        public virtual void Add(TComponent component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            if (_components.ContainsKey(component.Name))
                throw new ComponentNameCollisionException(component.Name, typeof(TComponent));

            _components.Add(component.Name, component);
        }

        /// <inheritdoc />
        public void Clear() => _components.Clear();

        /// <inheritdoc />
        public bool Contains(ComponentName name) => name is not null
            ? _components.ContainsKey(name)
            : throw new ArgumentNullException(nameof(name));

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