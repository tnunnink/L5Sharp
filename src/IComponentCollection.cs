using System;
using System.Collections.Generic;

namespace L5Sharp
{
    public interface IComponentCollection<TComponent> : IEnumerable<TComponent> where TComponent : ILogixComponent
    {
        int Count { get; }
        bool Contains(string name);
        TComponent Get(string name);
        TComponent Get(Func<TComponent, bool> predicate);
        IEnumerable<TComponent> Find(Func<TComponent, bool> predicate);
        IEnumerable<TComponent> Ordered();
        int IndexOf(TComponent component);
        void Add(TComponent component);
        void AddRange(IEnumerable<TComponent> components);
        void Insert(int index, TComponent component);
        void Update(TComponent component);
        void Remove(string name);
    }
}