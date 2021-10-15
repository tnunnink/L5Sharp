using System;

namespace L5Sharp.Abstractions
{
    public interface IComponentCache
    {
        
    }
    
    public interface IComponentCache<T> : IComponentCache where T : IComponent
    {
        bool HasComponent(string name);
        void Cache(T component);
        T Get(string name);
    }
}