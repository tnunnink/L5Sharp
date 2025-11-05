using System;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents the Logix EventInfo element, which encapsulates details about an event trigger,
/// associated tag, and timeout configuration.
/// </summary>
public class EventInfo : LogixElement
{
    /// <summary>
    /// Creates a new <see cref="EventInfo"/> with default values.
    /// </summary>
    public EventInfo() : base(L5XName.EventInfo)
    {
        EventTrigger = TaskEventTrigger.ConsumedTag;
    }

    /// <summary>
    /// Creates a new <see cref="EventInfo"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public EventInfo(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The trigger for the event task. Only used for event tasks.
    /// </summary>
    /// <value>
    /// A <see cref="TaskEventTrigger"/> value indicating what triggers the task. Returns <c>null</c> for non-event tasks.
    /// </value>
    public TaskEventTrigger? EventTrigger
    {
        get => GetValue(TaskEventTrigger.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// The tag name that the event task consumes. Only used for event tasks.
    /// </summary>
    /// <value>
    /// A <see cref="TagName"/> value indicating what tag to consume. Returns <c>null</c> for non-event tasks.
    /// </value>
    /// <remarks>Only used for event tasks with a Consumed Tag trigger or a Module Input Data State Change trigger.</remarks>
    public TagName? EventTag
    {
        get => GetValue(TagName.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// The value indicating whether timeouts are enabled for the event task. Only used for event tasks.
    /// </summary>
    /// <value>
    /// If the task is an event type task, <c>true</c> indicating that timeouts are enabled, <c>false</c> to indicate
    /// they are disabled. Returns <c>null</c> for non-event tasks.
    /// </value>
    public bool EnableTimeout
    {
        get => GetValue(bool.Parse);
        set => SetValue(value);
    }
}