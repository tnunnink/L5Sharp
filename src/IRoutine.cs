using L5Sharp.Enums;

namespace L5Sharp
{
    public interface IRoutine : ILogixComponent
    {
        RoutineType Type { get; }
    }
}