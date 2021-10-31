using System;
using L5Sharp.Configurations;

namespace L5Sharp
{
    public interface ITasks : IComponentCollection<ITask>
    {
        void Add(string name, Action<ITaskConfiguration> config = null);
    }
}