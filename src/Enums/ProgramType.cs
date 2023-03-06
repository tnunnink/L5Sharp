using L5Sharp.Components;

namespace L5Sharp.Enums
{
    /// <summary>
    /// Provides an enumeration of all Logix <see cref="ProgramType"/> options or a given <see cref="Program"/> component.
    /// </summary>
    public abstract class ProgramType : LogixEnum<ProgramType, string>
    {
        private ProgramType(string name, string value) : base(name, value)
        {
        }

        /// <summary>
        /// Represents a Normal <see cref="ProgramType"/> value.
        /// </summary>
        public static readonly ProgramType Normal = new NormalType();
        
        /// <summary>
        /// Represents a EquipmentPhase <see cref="ProgramType"/> value.
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