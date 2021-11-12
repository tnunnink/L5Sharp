using System;
using L5Sharp.Builders;

namespace L5Sharp
{
    public interface ITags : IComponentCollection<ITag<IDataType>>
    {
        void Add(string name, IDataType dataType, Action<ITagBuilder<IDataType>> builder = null);
    }
}