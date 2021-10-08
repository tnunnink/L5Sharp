using L5Sharp.Enums;

namespace L5Sharp.Abstractions
{
    public interface IRoutine : IComponent
    {
        public RoutineType Type { get; }
    }
}