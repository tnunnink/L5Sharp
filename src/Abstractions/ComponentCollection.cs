using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Exceptions;
using L5Sharp.Utilities;

namespace L5Sharp.Abstractions
{
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

        public ComponentCollection()
        {
            _components = new HashSet<TComponent>(_comparer);
            _names = new List<Func<string>>();
        }

        public ComponentCollection(IEnumerable<TComponent> components) : this()
        {
            AddComponents(components);
        }

        public int Count => _components.Count;

        public bool Contains(string name)
        {
            return !string.IsNullOrEmpty(name) && _components.Any(c => c.Name == name);
        }

        public TComponent Get(string name)
        {
            return !string.IsNullOrEmpty(name) ? _components.SingleOrDefault(c => c.Name == name) : default;
        }

        public TComponent Get(Func<TComponent, bool> predicate)
        {
            return _components.SingleOrDefault(predicate);
        }

        public IEnumerable<TComponent> Find(Func<TComponent, bool> predicate)
        {
            return _components.Where(predicate);
        }

        public IEnumerable<TComponent> Ordered()
        {
            return _components.OrderBy(c => _names.FindIndex(func => func.Invoke() == c.Name));
        }

        public int IndexOf(TComponent component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), $"{typeof(TComponent).Name} can not be null");
            
            return _names.FindIndex(n => n.Invoke() == component.Name);
        }

        public virtual void Add(TComponent component) => AddComponent(component);

        public void AddRange(IEnumerable<TComponent> components) => AddComponents(components);

        public virtual void Insert(int index, TComponent component) => InsertComponent(index, component);

        public virtual void Update(TComponent component) => UpdateComponent(component);

        public virtual void Remove(string name) => RemoveComponent(name);

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
            
            Validate.Name(component.Name);

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
            
            Validate.Name(component.Name);

            if (ContainsComponentName(component.Name))
                throw new ComponentNameCollisionException(component.Name, typeof(TComponent));

            _components.Add(component);
            _names.Insert(index, () => component.Name);
        }

        private void RemoveComponent(string name)
        {
            if (string.IsNullOrEmpty(name)) return;
            if (!ContainsComponentName(name)) return;

            _names.RemoveAt(_names.FindIndex(f => f.Invoke() == name));
            _components.RemoveWhere(c => c.Name == name);
        }

        private void UpdateComponent(TComponent component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), $"{typeof(TComponent).Name} can not be null");
            
            Validate.Name(component.Name);

            if (!ContainsComponentName(component.Name))
                AddComponent(component);

            var index = _names.FindIndex(f => f.Invoke() == component.Name);
            _names[index] = () => component.Name;
            
            _components.RemoveWhere(c => c.Name == component.Name);
            _components.Add(component);
        }
    }
}