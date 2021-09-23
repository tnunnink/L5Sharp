using Ardalis.SmartEnum;

namespace LogixHelper
{
    public class Scope : SmartEnum<Scope>
    {
        private Scope(string name, int value) : base(name, value)
        {
        }
        
        public static readonly Scope Null = new Scope("NullScope", 0);
        public static readonly Scope Controller = new Scope("Controller", 1);
        public static readonly Scope Program = new Scope("Program", 2);
        public static readonly Scope Routine = new Scope("Routine", 3);
    }
}