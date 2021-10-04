using Ardalis.SmartEnum;

namespace L5Sharp.Enumerations
{
    public class ProgramType : SmartEnum<ProgramType>
    {
        private ProgramType(string name, int value) : base(name, value)
        {
        }

        public static readonly ProgramType Typeless = new ProgramType("Typeless", 0);
        public static readonly ProgramType Normal = new ProgramType("Normal", 1);
        public static readonly ProgramType EquipmentPhase = new ProgramType("EquipmentPhase", 2);
    }
}