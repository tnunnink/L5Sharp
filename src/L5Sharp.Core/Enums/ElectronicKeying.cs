namespace L5Sharp.Core;

/// <summary>
/// An enumeration of all <see cref="ElectronicKeying"/> values for a given Logix <see cref="Module"/>
/// </summary>
public class ElectronicKeying : LogixEnum<ElectronicKeying, string>
{
    private ElectronicKeying(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents the ExactMatch <see cref="ElectronicKeying"/> value.
    /// </summary>
    public static readonly ElectronicKeying ExactMatch = new(nameof(ExactMatch), nameof(ExactMatch));
        
    /// <summary>
    /// Represents the <c>CompatibleModule</c> <see cref="ElectronicKeying"/> value.
    /// </summary>
    public static readonly ElectronicKeying CompatibleModule = new(nameof(CompatibleModule), nameof(CompatibleModule));
        
    /// <summary>
    /// Represents the <c>Disabled</c> <see cref="ElectronicKeying"/> value.
    /// </summary>
    public static readonly ElectronicKeying Disabled = new(nameof(Disabled), nameof(Disabled));
        
    /// <summary>
    /// Represents the <c>Custom</c> <see cref="ElectronicKeying"/> value.
    /// </summary>
    public static readonly ElectronicKeying Custom = new(nameof(Custom), nameof(Custom));
}