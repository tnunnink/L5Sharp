using System;
using L5Sharp.Enums;

namespace L5Sharp.Configurations
{
    public interface IProgramConfiguration : IComponentConfiguration<IProgram>
    {
        IProgramConfiguration HasDescription(string description);
        IProgramConfiguration OfType(ProgramType type);
        IProgramConfiguration IsDisabled();
        IProgramConfiguration HasTag(string name, IDataType dataType, Action<ITagConfiguration> config = null);

    }
}