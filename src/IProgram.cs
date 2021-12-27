using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp
{
    public interface IProgram : ILogixComponent
    {
        ProgramType Type { get; }
        bool TestEdits { get; }
        bool Disabled { get; }
        IEnumerable<ITag<IDataType>> Tags { get; }
        IEnumerable<IRoutine> Routines { get; }
    }
}