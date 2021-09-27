using System;
using L5Sharp.Primitives;

namespace L5Sharp.Builders
{
    public interface IProgramBuilder : IBuilder<Program>
    {
        void WithRoutine(string name, Action<IRoutineBuilder> builder);
    }
}