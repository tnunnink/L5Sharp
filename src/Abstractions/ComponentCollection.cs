using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Exceptions;

namespace L5Sharp.Abstractions
{
    /// <inheritdoc />
    public class ComponentCollection<TComponent> : IComponentCollection<TComponent> where TComponent : ILogixComponent
    {
        //Using a hashset with custom equality comparer that will maintain the name constraint and with fast lookup
        private readonly HashSet<TComponent> _components;
        private readonly ComponentNameComparer<TComponent> _comparer = new ComponentNameComparer<TComponent>();

        /// <summary>
        /// Creates a new empty instance of <see cref="ComponentCollection{TComponent}"/> for the specified <see cref="ILogixComponent"/>.
        /// </summary>
        public ComponentCollection()
        {
            _components = new HashSet<TComponent>(_comparer);
        }

        /// <summary>
        /// Creates a new instance of <see cref="ComponentCollection{TComponent}"/> for the specified
        /// <see cref="ILogixComponent"/> with the provided collection of components. 
        /// </summary>
        /// <param name="components">A collection of components to initialize the collection with.</param>
        public ComponentCollection(IEnumerable<TComponent> components) : this()
        {
            AddComponents(components);
        }

        /// <inheritdoc />
        public int Count => _components.Count;

        /// <inheritdoc />
        public bool Contains(ComponentName name)
        {
            return !string.IsNullOrEmpty(name) && _components.Any(c => c.Name == name);
        }

        /// <inheritdoc />
        public bool Contains(Func<TComponent, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return _components.Any(predicate);
        }

        /// <inheritdoc />
        public TComponent Get(ComponentName name)
        {
            return !string.IsNullOrEmpty(name) ? _components.SingleOrDefault(c => c.Name == name) : default;
        }

        /// <inheritdoc />
        public TComponent Get(Func<TComponent, bool> predicate)
        {
            return _components.FirstOrDefault(predicate);
        }

        /// <inheritdoc />
        public IEnumerable<TComponent> Find(Func<TComponent, bool> predicate)
        {
            return _components.Where(predicate);
        }

        /// <inheritdoc />
        public void Clear()
        {
            _components.Clear();
        }

        /// <inheritdoc />
        public virtual void Add(TComponent component) => AddComponent(component);

        /// <inheritdoc />
        public void AddRange(IEnumerable<TComponent> components) => AddComponents(components);

        /// <inheritdoc />
        public virtual void Update(TComponent component) => UpdateComponent(component);

        /// <inheritdoc />
        public virtual void Remove(ComponentName name) => RemoveComponent(name);

        /// <inheritdoc />
        public void Replace(ComponentName name, TComponent component)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerator GetEnumerator()
        {
            return _components.GetEnumerator();
        }

        private bool ContainsComponent(ComponentName name)
        {
            return _components.Any(c => c.Name == name);
        }

        private void AddComponent(TComponent component)
        {
            if (component == null) return;

            if (ContainsComponent(component.Name))
                throw new ComponentNameCollisionException(component.Name, typeof(TComponent));

            _components.Add(component);
        }

        private void AddComponents(IEnumerable<TComponent> components)
        {
            if (components == null) return;

            foreach (var component in components)
                AddComponent(component);
        }

        private void UpdateComponent(TComponent component)
        {
            if (component == null) return;

            if (!ContainsComponent(component.Name))
                _components.Add(component);

            _components.RemoveWhere(c => c.Name == component.Name);
            _components.Add(component);
        }

        private void RemoveComponent(ComponentName name)
        {
            if (name == null) return;
            if (!ContainsComponent(name)) return;
            _components.RemoveWhere(c => c.Name == name);
        }
        
        private void ReplaceComponent(ComponentName name, TComponent component)
        {
            if (name == null) return;
            if (component == null) return;
            
            if (ContainsComponent(name))
                _components.RemoveWhere(c => c.Name == name);
            
            _components.Add(component);
        }

        IEnumerator<TComponent> IEnumerable<TComponent>.GetEnumerator()
        {
            return _components.GetEnumerator();
        }
    }
}