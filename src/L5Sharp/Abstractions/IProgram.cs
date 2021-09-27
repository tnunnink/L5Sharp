using System.Collections.Generic;
using L5Sharp.Enumerations;
using L5Sharp.Primitives;

namespace L5Sharp.Abstractions
{
    public interface IProgram
    {
        string Name { get;}
        string Description { get;}
        ProgramType Type { get;}
        string MainRoutineName { get;}
        string FaultRoutineName { get;}
        bool TestEdits { get;}
        bool Disabled { get;}
        bool UseAsFolder { get;}
        IEnumerable<Tag> Tags { get; }
        IEnumerable<Routine> Routines { get;}
    }
}