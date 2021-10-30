namespace L5Sharp.Repositories
{
    public interface IRepository<T> : IReadOnlyRepository<T> where T : ILogixComponent
    {
        void Add(T component);
        void Remove(T component);
        void Update(T component);
    }
}