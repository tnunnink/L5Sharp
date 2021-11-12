using System;
using L5Sharp.Builders;

namespace L5Sharp
{
    public interface ITasks : IComponentCollection<ITask>
    {
        void Add(Action<ITaskBuilder> builder);
    }
}