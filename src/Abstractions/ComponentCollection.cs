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
        private readonly Dictionary<string, TComponent> _components;
        private readonly List<string> _items;

        public ComponentCollection()
        {
            _components = new Dictionary<string, TComponent>();
            _items = new List<string>();
        }

        public ComponentCollection(int length)
        {
            _components = new Dictionary<string, TComponent>(length);
            _items = new List<string>(length);
        }

        public ComponentCollection(IEnumerable<TComponent> components) : this()
        {
            AddComponents(components);
        }

        public bool Contains(string name)
        {
            return !string.IsNullOrEmpty(name) && _components.ContainsKey(name);
        }

        public TComponent Get(string name)
        {
            return !string.IsNullOrEmpty(name) && _components.ContainsKey(name) ? _components[name] : default;
        }

        public TComponent Get(Func<TComponent, bool> predicate)
        {
            return _components.Values.SingleOrDefault(predicate);
        }

        public IEnumerable<TComponent> Find(Func<TComponent, bool> predicate)
        {
            return _components.Values.Where(predicate);
        }

        public IEnumerable<TComponent> Ordered()
        {
            return _components.Values.OrderBy(c => _items.IndexOf(c.Name));
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
            return _components.Values.GetEnumerator();
        }

        private void AddComponent(TComponent component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), $"{typeof(TComponent).Name} can not be null");

            if (_components.ContainsKey(component.Name))
                throw new ComponentNameCollisionException(component.Name, typeof(TComponent));

            _components.Add(component.Name, component);
            _items.Add(component.Name);
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

            if (_components.ContainsKey(component.Name))
                throw new ComponentNameCollisionException(component.Name, typeof(TComponent));

            _components.Add(component.Name, component);
            _items.Insert(index, component.Name);
        }

        private void RemoveComponent(string name)
        {
            if (string.IsNullOrEmpty(name)) return;
            if (!_components.ContainsKey(name)) return;

            _components.Remove(name);
            _items.Remove(name);
        }

        private void UpdateComponent(string name, TComponent component)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Component name can not be null or empty");

            if (component == null)
                throw new ArgumentNullException(nameof(component), $"{typeof(TComponent).Name} can not be null");

            if (!_components.ContainsKey(name))
                AddComponent(component);

            _components.Remove(name);
            _components.Add(component.Name, component);

            if (name == component.Name) return;

            var index = _items.IndexOf(name);
            if (index == -1)
                throw new InvalidOperationException("This should not happen right?");
            _items[index] = component.Name;
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