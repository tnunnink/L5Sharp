using L5Sharp.Abstractions;

namespace L5Sharp.Repositories.Abstractions
{
    public interface IRepository
    {
    }

    public interface IRepository<T> : IRepository where T : INamedComponent
    {
        T Get(string name);

        void Add(T component);
        
        void Remove(T component);

        void Update(T component);
    }
}