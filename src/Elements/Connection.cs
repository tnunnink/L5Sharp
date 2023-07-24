using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Enums;

namespace L5Sharp.Elements;

/// <summary>
/// A component of a <see cref="Module"/> that represents the properties and data of the connection to the field device.
/// </summary>
public sealed class Connection : LogixElement
{
    /// <summary>
    /// Creates a new <see cref="Connection"/> with default values.
    /// </summary>
    public Connection()
    {
        Name = string.Empty;
        Type = ConnectionType.Unknown;
        Priority = ConnectionPriority.Scheduled;
        InputConnectionType = TransmissionType.Multicast;
        InputProductionTrigger = ProductionTrigger.Cyclic;
    }

    /// <summary>
    /// Creates a new <see cref="Connection"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public Connection(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Gets the name of the <see cref="Connection"/> component.
    /// </summary>
    public string Name
    {
        get => GetRequiredValue<string>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the value of the Request Packet Interval for the <see cref="Connection"/>. 
    /// </summary>
    public int RPI
    {
        get => GetRequiredValue<int>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the input connection point for the primary <see cref="Connection"/>.
    /// </summary>
    public ushort InputCxnPoint
    {
        get => GetValue<ushort>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the input size for the <see cref="Connection"/>.
    /// </summary>
    public ushort InputSize
    {
        get => GetValue<ushort>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the output connection point for the primary <see cref="Connection"/>.
    /// </summary>
    public ushort OutputCxnPoint
    {
        get => GetValue<ushort>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the output size for the <see cref="Connection"/>.
    /// </summary>
    public ushort OutputSize
    {
        get => GetValue<ushort>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="Enums.ConnectionType"/> value for the <see cref="Connection"/>.
    /// </summary>
    public ConnectionType? Type
    {
        get => GetValue<ConnectionType>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="Enums.ConnectionPriority"/> value for the <see cref="Connection"/>.
    /// </summary>
    public ConnectionPriority? Priority
    {
        get => GetValue<ConnectionPriority>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="Enums.TransmissionType"/> value for the <see cref="Connection"/>.
    /// </summary>
    public TransmissionType? InputConnectionType
    {
        get => GetValue<TransmissionType>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Connection"/> output is a redundant owner.
    /// </summary>
    public bool OutputRedundantOwner
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="Enums.ProductionTrigger"/> value for the <see cref="Connection"/>.
    /// </summary>
    public ProductionTrigger? InputProductionTrigger
    {
        get => GetValue<ProductionTrigger>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the value indicating whether the EtherNet/IP connection is unicast.
    /// </summary>
    public bool Unicast
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the value of the Event ID used in conjunction with an event task for the <see cref="Connection"/>.
    /// </summary>
    public int? EventId
    {
        get => GetValue<int>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the suffix for <see cref="Input"/> tag. 
    /// </summary>
    public string InputTagSuffix
    {
        get => GetValue<string>() ?? "I";
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the suffix for <see cref="Output"/> tag.
    /// </summary>
    public string OutputTagSuffix
    {
        get => GetValue<string>() ?? "O";
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the Tag that represents the input channel data for the <see cref="Connection"/>.
    /// </summary>
    public Tag? Input
    {
        get
        {
            var element = Element.Descendants(L5XName.InputTag).FirstOrDefault();
            return element is null ? default : new Tag(element) { Name = element.ModuleTagName(InputTagSuffix) };
        }
    }

    /// <summary>
    /// Gets the Tag that represents the output channel data for the <see cref="Connection"/>.
    /// </summary>
    public Tag? Output
    {
        get
        {
            var element = Element.Descendants(L5XName.OutputTag).FirstOrDefault();
            return element is null ? default : new Tag(element) { Name = element.ModuleTagName(OutputTagSuffix) };
        }
    }
}