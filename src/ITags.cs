using System;
using L5Sharp.Configurations;

namespace L5Sharp
{
    public interface ITags : IComponentCollection<ITag>
    {
        void Add(string name, IDataType dataType, Action<ITagConfiguration> config = null);
    }
}