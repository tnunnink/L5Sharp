using L5Sharp.Enums;

namespace L5Sharp.Components;

/// <summary>
/// A logix <c>Trend</c> component. Contains the properties that comprise the L5X Trend element.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public class Trend : ILogixComponent
{
    /// <inheritdoc />
    public string Name { get; set; } = string.Empty;

    /// <inheritdoc />
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Specify how often trending tags are collected in milliseconds (1 msec...30 minutes).
    /// </summary>
    public int SamplePeriod { get; set; } = 1;

    /// <summary>
    /// Specifies the maximum number of captures allowed (1...100).
    /// </summary>
    public int NumberOfCaptures { get; set; }

    /// <summary>
    /// Define how the capture size is specified.
    /// </summary>
    /// <value>A <see cref="CaptureSize"/> representing the capture size option.</value>
    public CaptureSizeType CaptureSizeType { get; set; } = CaptureSizeType.Samples;

    /// <summary>
    /// Specify the number of samples for each capture.
    /// </summary>
    /// <remarks>
    /// <b>Rockwell:</b> The maximum number of samples is 2-hours worth of data samples or 1000 samples,
    /// whichever is greater. If the CaptureSizeType is Samples, the range is 1...(2 hours/SamplePeriod) or 1000 samples,
    /// whichever is greater. If the CaptureSizeType is TimePeriod, the range is SamplePeriod...2 hours or
    /// (SamplePeriod * 1000), whichever is greater
    /// </remarks>
    public int CaptureSize { get; set; }
}