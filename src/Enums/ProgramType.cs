using L5Sharp.Components;

namespace L5Sharp.Enums;

/// <summary>
/// Provides an enumeration of all Logix <see cref="ProgramType"/> options or a given <see cref="Program"/> component.
/// </summary>
public class ProgramType : LogixEnum<ProgramType, string>
{
    private ProgramType(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents a Normal <see cref="ProgramType"/> value.
    /// </summary>
    public static readonly ProgramType Normal = new(nameof(Normal), nameof(Normal));
        
    /// <summary>
    /// Represents a EquipmentPhase <see cref="ProgramType"/> value.
    /// </summary>
    public static readonly ProgramType EquipmentPhase = new(nameof(EquipmentPhase), nameof(EquipmentPhase));
}