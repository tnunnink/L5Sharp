using System;
using L5Sharp.Primitives;

namespace L5Sharp.Builders
{
    public class ProgramBuilder : IProgramBuilder
    {
        private readonly Program _program;

        public ProgramBuilder(string name)
        {
            _program = new Program(name);
        }
        
        public Program Build()
        {
            return _program;
        }

        public void WithRoutine(string name, Action<IRoutineBuilder> builder)
        {
            throw new System.NotImplementedException();
        }
    }
}