using System;
using System.Collections.Generic;
using L5Sharp.Configurations;

namespace L5Sharp
{
    public interface IComponentCollection<TComponent> : IEnumerable<TComponent> where TComponent : ILogixComponent
    {
        bool Contains(string name);
        TComponent Get(string name);
        TComponent Get(Func<TComponent, bool> predicate);
        IEnumerable<TComponent> Find(Func<TComponent, bool> predicate);
        IEnumerable<TComponent> Ordered();
        void Add(TComponent component);
        void AddRange(IEnumerable<TComponent> components);

        void Add<TConfiguration>(TConfiguration configuration)
            where TConfiguration : IComponentConfiguration<TComponent>;

        void Insert(int index, TComponent component);
        void Remove(string name);
    }
}