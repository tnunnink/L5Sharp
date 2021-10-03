using System.Collections.Generic;
using L5Sharp.Abstractions;

namespace L5Sharp.Repositories
{
    public interface IRepository
    {
    }

    public interface IRepository<T> : IRepository where T : IComponent
    {
        IEnumerable<T> All();
        T Get(string name);
        void Add(T component);
        void Remove(T component);
        void Update(T component);
    }
}