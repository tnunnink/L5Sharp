using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Configurations;
using L5Sharp.Exceptions;

namespace L5Sharp.Abstractions
{
    public class ComponentCollection<TComponent> : IComponentCollection<TComponent> where TComponent : ILogixComponent
    {
        //This is the primary component collection.
        //Using a hashset with custom equality comparer that will maintain the name constraint.
        private readonly HashSet<TComponent> _components;
        //This collection is for preserving the order of items in which the were added/inserted/removed.
        //DataType members specifically need this to maintain the structure size.
        // By using a Func<string> and pointing it to the component name,
        // we can avoid issues when the client updates the component name.  
        private readonly List<Func<string>> _names; 

        public ComponentCollection()
        {
            _components = new HashSet<TComponent>(new ComponentNameComparer<TComponent>());
            _names = new List<Func<string>>();
        }

        public ComponentCollection(int length)
        {
            _components = new HashSet<TComponent>(length, new ComponentNameComparer<TComponent>()); 
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

        public virtual void Add(TComponent component) => AddComponent(component);

        public virtual void AddRange(IEnumerable<TComponent> components) => AddComponents(components);

        public void Add<TConfiguration>(TConfiguration configuration)
            where TConfiguration : IComponentConfiguration<TComponent> => AddComponent(configuration);

        public virtual void Insert(int index, TComponent component) => InsertComponent(index, component);

        public virtual void Update(string name, TComponent component) => UpdateComponent(name, component);

        public void Update<TConfiguration>(string name, TConfiguration configuration)
            where TConfiguration : IComponentConfiguration<TComponent> => UpdateComponent(name, configuration);

        public virtual void Remove(string name) => RemoveComponent(name);

        public IEnumerator GetEnumerator()
        {
            return _components.GetEnumerator();
        }

        IEnumerator<TComponent> IEnumerable<TComponent>.GetEnumerator()
        {
            return _components.GetEnumerator();
        }

        private bool ContainsComponent(string name)
        {
            return _components.Any(c => c.Name == name);
        }

        private void AddComponent(TComponent component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), $"{typeof(TComponent).Name} can not be null");

            if (ContainsComponent(component.Name))
                throw new ComponentNameCollisionException(component.Name, typeof(TComponent));

            _components.Add(component);
            _names.Add(() => component.Name);
        }

        private void AddComponent<TConfiguration>(TConfiguration configuration)
            where TConfiguration : IComponentConfiguration<TComponent>
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var component = configuration.Compile();

            AddComponent(component);
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

            if (ContainsComponent(component.Name))
                throw new ComponentNameCollisionException(component.Name, typeof(TComponent));

            _components.Add(component);
            _names.Insert(index, () => component.Name);
        }

        private void RemoveComponent(string name)
        {
            if (string.IsNullOrEmpty(name)) return;
            if (!ContainsComponent(name)) return;

            _names.RemoveAt(_names.FindIndex(f => f.Invoke() == name));
            _components.RemoveWhere(c => c.Name == name);
        }

        private void UpdateComponent(string name, TComponent component)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Component name can not be null or empty");

            if (component == null)
                throw new ArgumentNullException(nameof(component), $"{typeof(TComponent).Name} can not be null");

            if (!ContainsComponent(name))
                AddComponent(component);

            _names.Remove(() => name);
            _components.RemoveWhere(c => c.Name == name);
            
            _components.Add(component);
            _names.Add(() => component.Name);
        }

        private void UpdateComponent<TConfiguration>(string name, TConfiguration configuration)
            where TConfiguration : IComponentConfiguration<TComponent>
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration),
                    $"Configuration for {typeof(TComponent).Name} can not be null");

            var component = configuration.Compile();

            UpdateComponent(name, component);
        }
    }
}