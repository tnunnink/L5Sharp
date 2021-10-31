using L5Sharp.Abstractions;
using L5Sharp.Enums;

namespace L5Sharp
{
    public interface IProgram : ILogixComponent
    {
        ProgramType Type { get; }
        bool TestEdits { get; }
        bool Disabled { get; }
        void Enable();
        void Disable();
        IComponentCollection<ITag> Tags { get; }
        IComponentCollection<IRoutine> Routines { get; }
        
        /*bool HasRoutine(string name);
        bool HasRoutine(RoutineType type);
        IRoutine GetRoutine(string name);
        T GetRoutine<T>(string name) where T : IRoutine;
        IEnumerable<T> GetRoutines<T>() where T : IRoutine;
        void AddRoutine(string name, RoutineType type);
        void RemoveRoutine(string name);
        ITag GetTag(string name);
        void AddTag(string name, IDataType dataType);
        void RemoveTag(string name);*/
    }
}