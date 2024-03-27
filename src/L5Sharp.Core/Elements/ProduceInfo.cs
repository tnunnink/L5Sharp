using System;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A sub element of the <see cref="Tag"/> component that contains properties or configuration
/// producer tag types.
/// </summary>
public class ProduceInfo : LogixElement
{
    /// <summary>
    /// Creates a new <see cref="ProduceInfo"/> with default values.
    /// </summary>
    public ProduceInfo() : base(L5XName.ProduceInfo)
    {
    }

    /// <summary>
    /// Creates a new <see cref="ProduceInfo"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public ProduceInfo(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Specifies the maximum number of tags that can consume this tag.
    /// </summary>
    /// <value>A <see cref="int"/> representing the number of consumers allowed.</value>
    public int ProduceCount
    {
        get => GetValue<int>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specifies to use IOT instruction to send event trigger information to consumers of the tag.
    /// </summary>
    /// <value><c>true</c> when the tag is configured to send the event trigger; Otherwise, <c>false</c>.</value>
    public bool ProgrammaticallySendEventTrigger
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specifies to enable multiple unicast consumers to consume from the tag.
    /// </summary>
    /// <value><c>true</c> when the tag is configured enable multiple consumers; Otherwise, <c>false</c>.</value>
    public bool UnicastPermitted
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specifies the minimum RPI that a consumer of this tag may request data.
    /// </summary>
    /// <value>A <see cref="double"/> representing the min duration in ms that the tag may be requested.</value>
    public double MinimumRPI
    {
        get => GetValue<double>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specifies the maximum RPI that a consumer of this tag may request data.
    /// </summary>
    /// <value>A <see cref="double"/> representing the max duration in ms that the tag may be requested.</value>
    public double MaximumRPI
    {
        get => GetValue<double>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specifies the RPI to use for an out of range consumer of the tag.
    /// </summary>
    /// <value>A <see cref="double"/> representing the duration in ms that the tag may be requested by out of range consumer.</value>
    public double DefaultRPI
    {
        get => GetValue<double>();
        set => SetValue(value);
    }
}