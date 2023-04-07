using L5Sharp.Components;

namespace L5Sharp.Enums;

/// <summary>
/// Represents an enumeration of all <see cref="TransmissionType"/> options for a given <see cref="ModuleConnection"/>.
/// </summary>
public sealed class TransmissionType : LogixEnum<TransmissionType, string>
{
    private TransmissionType(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents a Null <see cref="TransmissionType"/> value.
    /// </summary>
    public static readonly TransmissionType Null = new(nameof(Null), nameof(Null));
        
    /// <summary>
    /// Represents a Multicast <see cref="TransmissionType"/> value.
    /// </summary>
    public static readonly TransmissionType Multicast = new(nameof(Multicast), nameof(Multicast));
        
    /// <summary>
    /// Represents a Unicast <see cref="TransmissionType"/> value.
    /// </summary>
    public static readonly TransmissionType Unicast = new(nameof(Unicast), nameof(Unicast));
}