using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A component of a <see cref="Module"/> that represents the properties and data of the connection to the field device.
/// </summary>
[LogixElement(L5XName.Connection)]
public class Connection : LogixObject<Connection>
{
    /// <inheritdoc />
    protected override List<string> ElementOrder =>
    [
        L5XName.InputTag,
        L5XName.OutputTag
    ];

    /// <summary>
    /// Creates a new <see cref="Connection"/> with default values.
    /// </summary>
    public Connection() : base(L5XName.Connection)
    {
        Name = string.Empty;
        RPI = 0;
        Type = ConnectionType.Unknown;
        Priority = ConnectionPriority.Scheduled;
        InputConnectionType = TransmissionType.Multicast;
        InputProductionTrigger = ProductionTrigger.Cyclic;

        //By default, just add the input and output tags. This will let us set data.
        Element.Add(new XElement(L5XName.InputTag, new XAttribute(L5XName.ExternalAccess, Access.ReadWrite)));
        Element.Add(new XElement(L5XName.OutputTag, new XAttribute(L5XName.ExternalAccess, Access.ReadWrite)));
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
        get => GetRequiredValue();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the value of the Request Packet Interval for the <see cref="Connection"/>. 
    /// </summary>
    public int RPI
    {
        get => GetRequiredValue(int.Parse);
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the input connection point for the primary <see cref="Connection"/>.
    /// </summary>
    public ushort InputCxnPoint
    {
        get => GetValue(ushort.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the input size for the <see cref="Connection"/>.
    /// </summary>
    public ushort InputSize
    {
        get => GetValue(ushort.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the output connection point for the primary <see cref="Connection"/>.
    /// </summary>
    public ushort OutputCxnPoint
    {
        get => GetValue(ushort.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the output size for the <see cref="Connection"/>.
    /// </summary>
    public ushort OutputSize
    {
        get => GetValue(ushort.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ConnectionType"/> value for the <see cref="Connection"/>.
    /// </summary>
    public ConnectionType? Type
    {
        get => GetValue(ConnectionType.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ConnectionPriority"/> value for the <see cref="Connection"/>.
    /// </summary>
    public ConnectionPriority? Priority
    {
        get => GetValue(ConnectionPriority.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="TransmissionType"/> value for the <see cref="Connection"/>.
    /// </summary>
    public TransmissionType? InputConnectionType
    {
        get => GetValue(TransmissionType.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Connection"/> output is a redundant owner.
    /// </summary>
    public bool? OutputRedundantOwner
    {
        get => GetOptionalBool();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ProductionTrigger"/> value for the <see cref="Connection"/>.
    /// </summary>
    public ProductionTrigger? InputProductionTrigger
    {
        get => GetValue(ProductionTrigger.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the value indicating whether the EtherNet/IP connection is unicast.
    /// </summary>
    public bool? Unicast
    {
        get => GetOptionalBool();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the value of the Event ID used in conjunction with an event task for the <see cref="Connection"/>.
    /// </summary>
    public int? EventId
    {
        get => GetValue(int.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether an event trigger can be sent programmatically.
    /// </summary>
    /// <remarks>
    /// This property allows controlling the ability to send an event trigger through code rather than relying on predefined configurations.
    /// </remarks>
    public bool? ProgrammaticallySendEventTrigger
    {
        get => GetOptionalBool();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets or sets the path used to define the connection between the module and the field device.
    /// </summary>
    public string? ConnectionPath
    {
        get => GetValue();
        set => SetValue(value);
    }

    /// <summary>
    /// The character used to define the tag name for the <see cref="InputTag"/> element. 
    /// </summary>
    /// <value>A <see cref="string"/> containing the suffix character if it exists; Otherwise, will default as 'I'.</value>
    /// <remarks>
    /// This value is used in determining the module tag name. Not all modules serialize this property
    /// but still use 'I' as the suffix character for input tags. Therefore, we will default to 'I' if not found.
    /// </remarks>
    public string InputTagSuffix
    {
        get => GetValue() ?? "I";
        set => SetValue(value);
    }

    /// <summary>
    /// The character used to define the tag name for the <see cref="OutputTag"/> element.
    /// </summary>
    /// <value>A <see cref="string"/> containing the suffix character if it exists; Otherwise, will default as 'O'.</value>
    /// <remarks>
    /// This value is used in determining the module tag name. Not all modules serialize this property
    /// but still use 'O' as the suffix character for output tags. Therefore, we will default to 'O' if not found.
    /// </remarks>
    public string OutputTagSuffix
    {
        get => GetValue() ?? "O";
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the Tag that represents the input channel data for the <see cref="Connection"/> element.
    /// </summary>
    /// <value>A <see cref="Tag"/> component containing the module defined data structure for the input connection data.</value>
    public Tag? InputTag => GetComplex<Tag>();

    /// <summary>
    /// Gets the Tag that represents the output channel data for the <see cref="Connection"/> element.
    /// </summary>
    /// <value>A <see cref="Tag"/> component containing the module defined data structure for the output connection data.</value>
    public Tag? OutputTag => GetComplex<Tag>();
}