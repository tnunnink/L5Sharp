using System;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Configurations
{
    public class ProgramConfiguration : IProgramConfiguration
    {
        private readonly Program _program;

        public ProgramConfiguration(string name)
        {
            _program = new Program(name);
        }
        public IProgram Compile()
        {
            return _program;
        }

        public IProgramConfiguration HasDescription(string description)
        {
            _program.SetDescription(description);
            return this;
        }

        public IProgramConfiguration OfType(ProgramType type)
        {
            throw new NotImplementedException();
        }

        public IProgramConfiguration IsDisabled()
        {
            _program.Disable();
            return this;
        }

        public IProgramConfiguration HasTag(string name, IDataType dataType, Action<ITagConfiguration> config = null)
        {
            _program.Tags.Add(name, dataType, config);
            return this;
        }
    }
}