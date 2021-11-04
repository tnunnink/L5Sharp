using System;
using L5Sharp.Abstractions;
using L5Sharp.Configurations;

namespace L5Sharp.Core
{
    public class Programs : ComponentCollection<IProgram>, IPrograms
    {
        public void Add(string name, Action<IProgramConfiguration> config = null)
        {
            var configuration = new ProgramConfiguration(name);

            config?.Invoke(configuration);

            Add(configuration);
        }
    }
}