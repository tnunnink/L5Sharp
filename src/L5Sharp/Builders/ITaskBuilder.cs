using System;
using L5Sharp.Primitives;

namespace L5Sharp.Builders
{
    public interface ITaskBuilder : IBuilder<Task>
    {
        void WithProgram(string name, Action<IProgramBuilder> builder);
    }
}