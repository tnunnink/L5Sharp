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
        private readonly Dictionary<string, TComponent> _components = new Dictionary<string, TComponent>();
        private readonly List<string> _items = new List<string>();

        public ComponentCollection()
        {
        }

        public ComponentCollection(IEnumerable<TComponent> components)
        {
            foreach (var component in components)
            {
                if (_components.ContainsKey(component.Name)) continue;
                _components.Add(component.Name, component);
                _items.Add(component.Name);
            }
        }

        public virtual bool Contains(string name)
        {
            return !string.IsNullOrEmpty(name) && _components.ContainsKey(name);
        }

        public virtual TComponent Get(string name)
        {
            return !string.IsNullOrEmpty(name) && _components.ContainsKey(name) ? _components[name] : default;
        }

        public virtual TComponent Get(Func<TComponent, bool> predicate)
        {
            return _components.Values.SingleOrDefault(predicate);
        }

        public virtual IEnumerable<TComponent> Find(Func<TComponent, bool> predicate)
        {
            return _components.Values.Where(predicate);
        }

        public IEnumerable<TComponent> Ordered()
        {
            return _components.Values.OrderBy(c => _items.IndexOf(c.Name));
        }

        public virtual void Add(TComponent component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), $"{typeof(TComponent).Name} can not be null");

            if (_components.ContainsKey(component.Name))
                throw new ComponentNameCollisionException(component.Name, typeof(TComponent));

            _components.Add(component.Name, component);
            _items.Add(component.Name);
        }

        public void Insert(int index, TComponent component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), $"{typeof(TComponent).Name} can not be null");

            if (_components.ContainsKey(component.Name))
                throw new ComponentNameCollisionException(component.Name, typeof(TComponent));

            _components.Add(component.Name, component);
            _items.Insert(index, component.Name);
        }

        public virtual void Remove(string name)
        {
            if (string.IsNullOrEmpty(name)) return;
            if (!_components.ContainsKey(name)) return;

            _components.Remove(name);
            _items.Remove(name);
        }

        public IEnumerator GetEnumerator()
        {
            return _components.GetEnumerator();
        }

        IEnumerator<TComponent> IEnumerable<TComponent>.GetEnumerator()
        {
            return _components.Values.GetEnumerator();
        }

        protected void Add<TConfiguration>(string name, TConfiguration configuration)
            where TConfiguration : IComponentConfiguration<TComponent>
        {
            configuration.HasName(name);

            var component = configuration.Compile();

            if (component == null)
                throw new ArgumentNullException(nameof(component), $"{typeof(TComponent).Name} can not be null");

            if (_components.ContainsKey(component.Name))
                throw new ComponentNameCollisionException(component.Name, typeof(TComponent));

            _components.Add(component.Name, component);
            _items.Add(component.Name);
        }
    }
}