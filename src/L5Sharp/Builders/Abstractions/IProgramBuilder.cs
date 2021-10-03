using System;
using L5Sharp.Core;

namespace L5Sharp.Builders.Abstractions
{
    public interface IProgramBuilder : IBuilder<Program>
    {
        void WithRoutine(string name, Action<IRoutineBuilder> builder);
    }
}