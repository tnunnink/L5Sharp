using L5Sharp.Enums;

namespace L5Sharp
{
    public interface IRoutine : IComponent
    {
        RoutineType Type { get; }
    }
}