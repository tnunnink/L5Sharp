using System;
using System.Collections.Generic;

namespace L5Sharp.Abstractions
{
    public class ComponentCache<T> : IComponentCache<T> where T : IComponent
    {
        private readonly Dictionary<string, T> _cache = new Dictionary<string, T>(StringComparer.OrdinalIgnoreCase);

        public T GetOrCreate(string name, Func<T> create)
        {
            if (_cache.ContainsKey(name)) return _cache[name];
            
            var component = create();
            _cache.Add(component.Name, component);
            return component;
        }
    }
}