using L5Sharp.Enumerations;

namespace L5Sharp.Abstractions
{
    public interface IRoutine : IComponent
    {
        public RoutineType Type { get; }
    }
}