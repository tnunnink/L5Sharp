using L5Sharp.Types.Predefined;

namespace L5Sharp.Enums;

/// <summary>
/// Represents the types of data formats produced by a L5X file.
/// </summary>
public class DataFormat : LogixEnum<DataFormat, string>
{
    private DataFormat(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents a common verbose formatted data structure. 
    /// </summary>
    public static readonly DataFormat Decorated = new(nameof(Decorated), nameof(Decorated));
        
    /// <summary>
    /// Represents String formatted data structure.
    /// </summary>
    public static readonly DataFormat String = new(nameof(String), nameof(String));
        
    /// <summary>
    /// Represents Alarm formatted data structure.
    /// </summary>
    public static readonly DataFormat Alarm = new(nameof(Alarm), nameof(Alarm));
        
    /// <summary>
    /// Represents Message formatted data structure.
    /// </summary>
    public static readonly DataFormat Message = new(nameof(Message), nameof(Message));
        
    /// <summary>
    /// Represents L5K formatted data structure.
    /// </summary>
    public static readonly DataFormat L5K = new(nameof(L5K), nameof(L5K));
}