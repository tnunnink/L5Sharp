using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    /// <summary>
    /// Represents an enumeration of all Logix <see cref="Scope"/> options.
    /// </summary>
    public class Scope : SmartEnum<Scope, string>
    {
        private Scope(string name, string value) : base(name, value)
        {
        }
        
        /// <summary>
        /// Represents a Null <see cref="Scope"/> value.
        /// </summary>
        public static readonly Scope Null = new(nameof(Null), "NullScope");
        
        /// <summary>
        /// Represents a Controller <see cref="Scope"/> value.
        /// </summary>
        public static readonly Scope Controller = new(nameof(Controller), "ControllerScope");
        
        /// <summary>
        /// Represents a Program <see cref="Scope"/> value.
        /// </summary>
        public static readonly Scope Program = new(nameof(Program), "ProgramScope");

        /// <summary>
        /// Represents a Routine <see cref="Scope"/> value.
        /// </summary>
        public static readonly Scope Routine = new(nameof(Routine), "RoutineScope");
    }
}