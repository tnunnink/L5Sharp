using System.Collections.Generic;

namespace L5Sharp.Abstractions
{
    public interface IReadOnlyRepository<out T> : IRepository where T : IComponent
    {
        IEnumerable<T> GetAll();
        T Get(string name);
    }
}