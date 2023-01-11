using L5Sharp.Enums;

namespace L5Sharp.Components
{
    public abstract class Routine : ILogixScopedComponent
    {
        /// <inheritdoc />
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc />
        public string Description { get; set; } = string.Empty;

        /// <inheritdoc />
        public Scope Scope => Scope.Program;
        
        /// <summary>
        /// 
        /// </summary>
        public abstract RoutineType Type { get; }
    }
}