using System.Collections.Generic;
using L5Sharp.Abstractions;

namespace L5Sharp.Repositories
{
    public interface IReadOnlyRepository<out T> where T : ILogixComponent
    {
        bool Exists(string name);
        T Get(string name);
        IEnumerable<T> GetAll();
    }
}