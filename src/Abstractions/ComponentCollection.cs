using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Exceptions;

namespace L5Sharp.Abstractions
{
    /// <inheritdoc />
    public class ComponentCollection<TComponent> : IComponentCollection<TComponent> where TComponent : ILogixComponent
    {
        private readonly ComponentNameComparer<TComponent> _comparer = new ComponentNameComparer<TComponent>();
        //This is the primary collection storing components
        //Using a hashset with custom equality comparer that will maintain the name constraint and with fast lookup
        private readonly HashSet<TComponent> _components;
        //This collection is for preserving the order of items in which the were added/inserted/removed.
        //DataType members specifically need this to maintain the structure size in RSLogix.
        //By using a Func<string> and pointing it to the component name,
        //we can avoid state issues when the client updates the component name.  
        private readonly List<Func<string>> _names; 

        /// <summary>
        /// Creates a new empty instance of <see cref="ComponentCollection{TComponent}"/> for the specified <see cref="ILogixComponent"/>.
        /// </summary>
        public ComponentCollection()
        {
            _components = new HashSet<TComponent>(_comparer);
            _names = new List<Func<string>>();
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
        public bool Contains(string name)
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
        public TComponent Get(string name)
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
        public IEnumerable<TComponent> Ordered()
        {
            return _components.OrderBy(c => _names.FindIndex(func => func.Invoke() == c.Name));
        }

        /// <inheritdoc />
        public int IndexOf(TComponent component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), $"{typeof(TComponent).Name} can not be null");
            
            return _names.FindIndex(n => n.Invoke() == component.Name);
        }

        /// <inheritdoc />
        public virtual void Add(TComponent component) => AddComponent(component);

        /// <inheritdoc />
        public void AddRange(IEnumerable<TComponent> components) => AddComponents(components);

        /// <inheritdoc />
        public virtual void Insert(int index, TComponent component) => InsertComponent(index, component);

        /// <inheritdoc />
        public virtual void Update(TComponent component) => UpdateComponent(component);

        /// <inheritdoc />
        public virtual void Remove(string name) => RemoveComponent(name);

        /// <inheritdoc />
        public IEnumerator GetEnumerator()
        {
            return _components.GetEnumerator();
        }

        IEnumerator<TComponent> IEnumerable<TComponent>.GetEnumerator()
        {
            return _components.GetEnumerator();
        }

        private bool ContainsComponentName(string name)
        {
            return _components.Any(c => c.Name == name);
        }

        private void AddComponent(TComponent component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), $"{typeof(TComponent).Name} can not be null");

            if (ContainsComponentName(component.Name))
                throw new ComponentNameCollisionException(component.Name, typeof(TComponent));

            _components.Add(component);
            _names.Add(() => component.Name);
        }

        private void AddComponents(IEnumerable<TComponent> components)
        {
            if (components == null)
                throw new ArgumentNullException(nameof(components), "Components can not be null");

            foreach (var component in components)
                AddComponent(component);
        }

        private void InsertComponent(int index, TComponent component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), $"{typeof(TComponent).Name} can not be null");

            if (ContainsComponentName(component.Name))
                throw new ComponentNameCollisionException(component.Name, typeof(TComponent));

            _components.Add(component);
            _names.Insert(index, () => component.Name);
        }

        private void UpdateComponent(TComponent component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), $"{typeof(TComponent).Name} can not be null");

            if (!ContainsComponentName(component.Name))
                AddComponent(component);

            var index = _names.FindIndex(f => f.Invoke() == component.Name);
            _names[index] = () => component.Name;
            
            _components.RemoveWhere(c => c.Name == component.Name);
            _components.Add(component);
        }

        private void RemoveComponent(string name)
        {
            if (string.IsNullOrEmpty(name)) return;
            if (!ContainsComponentName(name)) return;

            _names.RemoveAt(_names.FindIndex(f => f.Invoke() == name));
            _components.RemoveWhere(c => c.Name == name);
        }
    }
}