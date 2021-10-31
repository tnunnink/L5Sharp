using System;
using L5Sharp.Enums;

namespace L5Sharp.Configurations
{
    public interface IProgramConfiguration : IComponentConfiguration<IProgram>
    {
        IProgramConfiguration HasDescription(ProgramType type);
        IProgramConfiguration OfType(ProgramType type);
        IProgramConfiguration IsDisabled();
        IProgramConfiguration HasTag(string name, Action<ITagConfiguration> config = null);

    }
}