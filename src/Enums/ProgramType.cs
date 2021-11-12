using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    public abstract class ProgramType : SmartEnum<ProgramType, string>
    {
        private ProgramType(string name, string value) : base(name, value)
        {
        }

        public static readonly ProgramType Normal = new NormalType();
        public static readonly ProgramType EquipmentPhase = new EquipmentPhaseType();

        private class NormalType : ProgramType
        {
            public NormalType() : base(nameof(Normal), nameof(Normal))
            {
            }
        }

        private class EquipmentPhaseType : ProgramType
        {
            public EquipmentPhaseType() : base(nameof(EquipmentPhase), nameof(EquipmentPhase))
            {
            }
        }
    }
}