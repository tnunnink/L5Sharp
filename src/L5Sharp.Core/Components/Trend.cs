using System.Xml.Linq;
using L5Sharp.Common;
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
    /// <value>A <see cref="CaptureSize"/> representing the capture size option. Type Samples, TimePeriod, or NoLimit.</value>
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
        get => GetValue<TagName>();
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
        get => GetValue<AtomicType>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify a target tag if the StartTriggerTargetType is <c>TargetTag</c>.
    /// </summary>
    /// <remarks>The tag must be one of the pen names.</remarks>
    public TagName? StartTriggerTargetTag1
    {
        get => GetValue<TagName>();
        set => SetValue(value);
    }
    
    /// <summary>
    /// Specify a logical operation (AND or OR) that is performed on StartTriggerXXX1 and StartTriggerXXX2.
    /// </summary>
    /// <remarks>StartTriggerXXX1 consists of StartTriggerTag1, StartTriggerOperation1, StartTriggerTargetType1, and 
    /// StartTriggerTargetValue1 or StartTriggerTargetTag1. StartTriggerXXX2 consists of StartTriggerTag2, 
    /// StartTriggerOperation2, StartTriggerTargetType2, and StartTriggerTargetValue2 or StartTriggerTargetTag2.</remarks>
    public Operator? StartTriggerLogicalOperation
    {
        get => GetValue<Operator>();
        set => SetValue(value);
    }
    
    /// <summary>
    /// Specify the tag name of the second start trigger. The name must be one of the pen names.
    /// </summary>
    public TagName? StartTriggerTag2
    {
        get => GetValue<TagName>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify the operation that is applied on <see cref="StartTriggerTag2"/>,
    /// and <see cref="StartTriggerTargetValue2"/> or <see cref="StartTriggerTargetValue2"/>.
    /// </summary>
    public TriggerOperation? StartTriggerOperation2
    {
        get => GetValue<TriggerOperation>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify the type of the first start trigger target.
    /// </summary>
    /// <remarks>
    /// If you type TargetValue, <see cref="StartTriggerTargetValue2"/> is expected.
    /// Otherwise, <see cref="StartTriggerTargetTag2"/> is expected.
    /// </remarks>
    public TriggerTargetType? StartTriggerTargetType2
    {
        get => GetValue<TriggerTargetType>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify a target value if the <see cref="StartTriggerTargetType2"/> is <c>TargetValue</c>.
    /// </summary>
    /// <remarks>Type a binary, octal, decimal, or hexadecimal integer number or type a floating point number.</remarks>
    public AtomicType? StartTriggerTargetValue2
    {
        get => GetValue<AtomicType>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify a target tag if the StartTriggerTargetType is <c>TargetTag</c>.
    /// </summary>
    /// <remarks>The tag must be one of the pen names.</remarks>
    public TagName? StartTriggerTargetTag2
    {
        get => GetValue<TagName>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify the number of pre-samples (0...1000) if the PreSampleType is Samples. Specify a time period 
    /// (0...(SamplePeriod ∗ 1000)) that covers pre-samples if the PreSampleType is TimePeriod
    /// </summary>
    public int? PreSamples
    {
        get => GetValue<int>();
        set => SetValue(value);
    }
    
    /// <summary>
    /// Specify the type of the stop trigger
    /// </summary>
    /// <value>A <see cref="TriggerType"/> representing the value NoTrigger or Event Trigger.</value>
    public TriggerType? StopTriggerType
    {
        get => GetValue<TriggerType>();
        set => SetValue(value);
    }
    
    /// <summary>
    /// Specify the tag name of the first start trigger. The name must be one of the pen names.
    /// </summary>
    public TagName? StopTriggerTag1
    {
        get => GetValue<TagName>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify the operation that is applied on <see cref="StopTriggerTag1"/>,
    /// and <see cref="StopTriggerTargetValue1"/> or <see cref="StopTriggerTargetTag1"/>.
    /// </summary>
    public TriggerOperation? StopTriggerOperation1
    {
        get => GetValue<TriggerOperation>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify the type of the first start trigger target.
    /// </summary>
    /// <remarks>
    /// If you type TargetValue, <see cref="StopTriggerTargetValue1"/> is expected.
    /// Otherwise, <see cref="StopTriggerTargetTag1"/> is expected.
    /// </remarks>
    public TriggerTargetType? StopTriggerTargetType1
    {
        get => GetValue<TriggerTargetType>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify a target value if the <see cref="StopTriggerTargetType1"/> is <c>TargetValue</c>.
    /// </summary>
    /// <remarks>Type a binary, octal, decimal, or hexadecimal integer number or type a floating point number.</remarks>
    public AtomicType? StopTriggerTargetValue1
    {
        get => GetValue<AtomicType>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify a target tag if the StopTriggerTargetType is <c>TargetTag</c>.
    /// </summary>
    /// <remarks>The tag must be one of the pen names.</remarks>
    public TagName? StopTriggerTargetTag1
    {
        get => GetValue<TagName>();
        set => SetValue(value);
    }
    
    /// <summary>
    /// Specify a logical operation (AND or OR) that is performed on StopTriggerXXX1 and StopTriggerXXX2.
    /// </summary>
    /// <remarks>StopTriggerXXX1 consists of StopTriggerTag1, StopTriggerOperation1, StopTriggerTargetType1, and 
    /// StopTriggerTargetValue1 or StopTriggerTargetTag1. StopTriggerXXX2 consists of StopTriggerTag2, 
    /// StopTriggerOperation2, StopTriggerTargetType2, and StopTriggerTargetValue2 or StopTriggerTargetTag2.</remarks>
    public Operator? StopTriggerLogicalOperation
    {
        get => GetValue<Operator>();
        set => SetValue(value);
    }
    
    /// <summary>
    /// Specify the tag name of the second start trigger. The name must be one of the pen names.
    /// </summary>
    public TagName? StopTriggerTag2
    {
        get => GetValue<TagName>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify the operation that is applied on <see cref="StopTriggerTag2"/>,
    /// and <see cref="StopTriggerTargetValue2"/> or <see cref="StopTriggerTargetValue2"/>.
    /// </summary>
    public TriggerOperation? StopTriggerOperation2
    {
        get => GetValue<TriggerOperation>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify the type of the first start trigger target.
    /// </summary>
    /// <remarks>
    /// If you type TargetValue, <see cref="StopTriggerTargetValue2"/> is expected.
    /// Otherwise, <see cref="StopTriggerTargetTag2"/> is expected.
    /// </remarks>
    public TriggerTargetType? StopTriggerTargetType2
    {
        get => GetValue<TriggerTargetType>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify a target value if the <see cref="StopTriggerTargetType2"/> is <c>TargetValue</c>.
    /// </summary>
    /// <remarks>Type a binary, octal, decimal, or hexadecimal integer number or type a floating point number.</remarks>
    public AtomicType? StopTriggerTargetValue2
    {
        get => GetValue<AtomicType>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify a target tag if the StopTriggerTargetType is <c>TargetTag</c>.
    /// </summary>
    /// <remarks>The tag must be one of the pen names.</remarks>
    public TagName? StopTriggerTargetTag2
    {
        get => GetValue<TagName>();
        set => SetValue(value);
    }

    /// <summary>
    /// The collection of <see cref="Pen"/> components the <c>Trend</c> is configured with.
    /// </summary>
    /// <remarks>Only supports up to 8 pens per trend.</remarks>
    public LogixContainer<Pen> Pens
    {
        get => GetContainer<Pen>();
        set => SetContainer(value);
    }
}