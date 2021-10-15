using System.Collections.Generic;

namespace L5Sharp.Abstractions
{
    public interface IReadOnlyRepository<out T> where T : IComponent
    {
        bool Exists(string name);
        T Get(string name);
        IEnumerable<T> GetAll();
    }
}