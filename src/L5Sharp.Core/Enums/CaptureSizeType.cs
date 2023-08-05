using L5Sharp.Components;

namespace L5Sharp.Enums;

/// <summary>
/// An enumeration of all <see cref="CaptureSizeType"/> for a Logix <see cref="Trend"/>. 
/// </summary>
public class CaptureSizeType : LogixEnum<CaptureSizeType, string>
{
    private CaptureSizeType(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents a <b>Samples</b> <see cref="CaptureSizeType"/>.
    /// </summary>
    public static readonly CaptureSizeType Samples = new(nameof(Samples), nameof(Samples));

    /// <summary>
    /// Represents a <b>TimePeriod</b> <see cref="CaptureSizeType"/>.
    /// </summary>
    public static readonly CaptureSizeType TimePeriod = new(nameof(TimePeriod), nameof(TimePeriod));

    /// <summary>
    /// Represents a <b>NoLimit</b> <see cref="CaptureSizeType"/>.
    /// </summary>
    public static readonly CaptureSizeType NoLimit = new(nameof(NoLimit), nameof(NoLimit));
}