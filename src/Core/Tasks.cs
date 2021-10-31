using System;
using L5Sharp.Abstractions;
using L5Sharp.Configurations;

namespace L5Sharp.Core
{
    public class Tasks : ComponentCollection<ITask>, ITasks
    {
        public void Add(string name, Action<ITaskConfiguration> config = null)
        {
            var configuration = new TaskConfiguration(name);
            config?.Invoke(configuration);
            Add(configuration);
        }
    }
}