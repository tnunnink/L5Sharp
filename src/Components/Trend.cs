using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Components;

/// <summary>
/// A logix <c>Trend</c> component. Contains the properties that comprise the L5X Trend element.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public class Trend : LogixComponent<Trend>
{
    /// <inheritdoc />
    public Trend()
    {
        SamplePeriod = 1;
    }

    /// <inheritdoc />
    public Trend(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Specify how often trending tags are collected in milliseconds (1 msec...30 minutes).
    /// </summary>
    public int SamplePeriod
    {
        get => GetValue<int>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specifies the maximum number of captures allowed (1...100).
    /// </summary>
    public int NumberOfCaptures
    {
        get => GetValue<int>();
        set => SetValue(value);
    }

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

    /// <summary>
    /// Specify the type of the start trigger
    /// </summary>
    /// <returns></returns>
    public TriggerType? StartTriggerType
    {
        get => GetValue<TriggerType>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify the tag name of the first start trigger. The name must be one of the pen names.
    /// </summary>
    public TagName? StartTriggerTag1
    {
        get => GetValue<string>()?.ToTagName();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify the operation that is applied on <see cref="StartTriggerTag1"/>,
    /// and <see cref="StartTriggerTargetValue1"/> or <see cref="StartTriggerTargetTag1"/>.
    /// </summary>
    public TriggerOperation? StartTriggerOperation1
    {
        get => GetValue<TriggerOperation>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify the type of the first start trigger target.
    /// </summary>
    /// <remarks>
    /// If you type TargetValue, <see cref="StartTriggerTargetValue1"/> is expected.
    /// Otherwise, <see cref="StartTriggerTargetTag1"/> is expected.
    /// </remarks>
    public TriggerTargetType? StartTriggerTargetType1
    {
        get => GetValue<TriggerTargetType>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify a target value if the <see cref="StartTriggerTargetType1"/> is <c>TargetValue</c>.
    /// </summary>
    /// <remarks>Type a binary, octal, decimal, or hexadecimal integer number or type a floating point number.</remarks>
    public AtomicType? StartTriggerTargetValue1
    {
        get
        {
            var value = GetValue<string>();
            return value is not null ? Atomic.Parse(value) : default; 
        }
        set => SetValue(value);
    }

    /// <summary>
    /// Specify a target tag if the StartTriggerTargetType is <c>TargetTag</c>.
    /// </summary>
    /// <remarks>The tag must be one of the pen names.</remarks>
    public TagName? StartTriggerTargetTag1
    {
        get => GetValue<string>()?.ToTagName();
        set => SetValue(value);
    }
}