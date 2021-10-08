using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Enumerations;

namespace L5Sharp.Abstractions
{
    public interface IProgram : IComponent
    {
        ProgramType Type { get; }
        bool TestEdits { get; }
        bool Disabled { get; set; }
        IEnumerable<Tag> Tags { get; }
        IEnumerable<IRoutine> Routines { get; }
        bool HasRoutine(string name);
        bool HasRoutine(RoutineType type);
        IRoutine GetRoutine(string name);
        IEnumerable<T> GetRoutines<T>() where T : IRoutine;
        void AddRoutine(string name, RoutineType type);
        void RemoveRoutine(string name);
        void AddTag(Tag tag);
        void RemoveTag(string name);
    }
}