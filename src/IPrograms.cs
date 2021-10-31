using System;
using L5Sharp.Configurations;

namespace L5Sharp
{
    public interface IPrograms : IComponentCollection<IProgram>
    {
        void Add(string name, Action<IProgramConfiguration> config = null);
    }
}