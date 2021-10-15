using System;
using System.Collections.Generic;
using L5Sharp.Abstractions;

namespace L5Sharp.Utilities
{
    internal class ComponentCache<T> : IComponentCache<T> where T : IComponent
    {
        private readonly Dictionary<string, T> _cache = new Dictionary<string, T>(StringComparer.OrdinalIgnoreCase);

        public bool HasComponent(string name)
        {
            return _cache.ContainsKey(name);
        }

        public void Add(T component)
        {
            if (_cache.ContainsKey(component.Name)) return;
            _cache.Add(component.Name, component);
        }

        public T Get(string name)
        {
            _cache.TryGetValue(name, out var component);
            return component;
        }

        public void Clear()
        {
            _cache.Clear();
        }
    }
}