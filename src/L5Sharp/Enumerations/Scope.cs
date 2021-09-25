using Ardalis.SmartEnum;

namespace L5Sharp.Enumerations
{
    public class Scope : SmartEnum<Scope>
    {
        private Scope(string name, int value) : base(name, value)
        {
        }
        
        public static readonly Scope Null = new Scope("NullScope", 0);
        public static readonly Scope Controller = new Scope("ControllerScope", 1);
        public static readonly Scope Program = new Scope("ProgramScope", 2);
        public static readonly Scope Routine = new Scope("RoutineScope", 3);
    }
}