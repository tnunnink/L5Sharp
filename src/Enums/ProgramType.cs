using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    public abstract class ProgramType : SmartEnum<ProgramType, string>
    {
        private ProgramType(string name, string value) : base(name, value)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly ProgramType Normal = new NormalType();
        
        /// <summary>
        /// 
        /// </summary>
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