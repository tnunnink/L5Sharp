namespace L5Sharp.Utilities
{
    internal interface IComponentCache
    {
        bool HasComponent(string name);
        void Clear();
    }
    
    internal interface IComponentCache<T> : IComponentCache where T : ILogixComponent
    {
        void Add(T component);
        T Get(string name);
    }
}