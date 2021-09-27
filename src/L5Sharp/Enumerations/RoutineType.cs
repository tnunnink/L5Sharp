using Ardalis.SmartEnum;

namespace L5Sharp.Enumerations
{
    public class RoutineType : SmartEnum<RoutineType>
    {
        public RoutineType(string name, int value) : base(name, value)
        {
        }

        public static readonly RoutineType Typeless = new RoutineType("Typeless", 0);
        public static readonly RoutineType Ladder = new RoutineType("RLL", 0);
        public static readonly RoutineType FunctionBlock = new RoutineType("FBD", 0);
        public static readonly RoutineType Sfc = new RoutineType("SFC", 0);
        public static readonly RoutineType StructuredText = new RoutineType("ST", 0);
        public static readonly RoutineType External = new RoutineType("External", 0);
        public static readonly RoutineType Encrypted = new RoutineType("Encrypted", 0);
    }
}