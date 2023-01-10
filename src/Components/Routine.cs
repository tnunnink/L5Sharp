using L5Sharp.Enums;

namespace L5Sharp.Core
{
    public abstract class Routine
    {
        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description { get; }

        /// <inheritdoc />
        public RoutineType Type { get; }
    }
}