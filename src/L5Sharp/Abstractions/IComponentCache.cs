using System;

namespace L5Sharp.Abstractions
{
    public interface IComponentCache
    {
        
    }
    
    public interface IComponentCache<T> : IComponentCache where T : IComponent
    {
        T GetOrCreate(string name, Func<T> create);
    }
}