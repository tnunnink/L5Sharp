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
        ITags Tags { get; }
        IComponentCollection<IRoutine> Routines { get; }
    }
}