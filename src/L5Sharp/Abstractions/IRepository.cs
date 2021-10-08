namespace L5Sharp.Abstractions
{
    public interface IRepository
    {
    }

    public interface IRepository<T> : IReadOnlyRepository<T> where T : IComponent
    {
        void Add(T component);
        void Remove(T component);
        void Update(T component);
    }
}