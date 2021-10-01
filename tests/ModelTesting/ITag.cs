using System;
using System.Collections.Generic;

namespace ModelTesting
{
    public interface ITag<out T> : IMember where T : IPredefined
    {
        T Instance { get; }
    }
    
    public class Tag : IMember
    {
        public string Name { get; }
        public DataTypeBase DataType { get; }
        public int Dimension { get; }
        public string Radix { get; }
        public string ExternalAccess { get; }
        public string Description { get; }
    }

    public class Tag<T> : Tag where T : DataTypeBase, new()
    {
        private Tag(string name, T type)
        {
            Instance = type;
        }

        public Tag(string name) : this(name, new T())
        {
        }
        
        public T Instance { get; }
    }
}