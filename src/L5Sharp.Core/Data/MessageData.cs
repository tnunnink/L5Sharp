using System;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// 
/// </summary>
[LogixElement(L5XName.MessageParameters)]
[LogixData(TypeName)]
public class MessageData : LogixData
{
    private const string TypeName = "MESSAGE";

    /// <inheritdoc />
    public MessageData() : base(L5XName.MessageParameters)
    {
        MessageType = MessageType.Unconfigured;
        RequestedLength = 0;
        ConnectedFlag = 0;
        ConnectionPath = string.Empty;
        CommTypeCode = 0;
        ServiceCode = Radix.Hex.Format(0);
        ObjectType = Radix.Hex.Format(0);
        TargetObject = 0;
        AttributeNumber = Radix.Hex.Format(0);
        LocalIndex = 0;
    }

    /// <inheritdoc />
    public MessageData(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    public override string Name => TypeName;

    /// <inheritdoc />
    public override void UpdateData(LogixData data)
    {
        if (data is null)
            throw new ArgumentNullException(nameof(data));

        if (data is not MessageData message)
            throw new ArgumentException($"Can not update message data with type: '{data.GetType()}'");

        Element.SetAttributeValue(nameof(MessageType), message.MessageType);
        Element.SetAttributeValue(nameof(RequestedLength), message.RequestedLength);
        Element.SetAttributeValue(nameof(ConnectedFlag), message.ConnectedFlag);
        Element.SetAttributeValue(nameof(ConnectionPath), message.ConnectionPath);
        Element.SetAttributeValue(nameof(CommTypeCode), message.CommTypeCode);
        Element.SetAttributeValue(nameof(ServiceCode), message.ServiceCode);
        Element.SetAttributeValue(nameof(ObjectType), message.ObjectType);
        Element.SetAttributeValue(nameof(TargetObject), message.TargetObject);
        Element.SetAttributeValue(nameof(AttributeNumber), message.AttributeNumber);
        Element.SetAttributeValue(nameof(LocalIndex), message.LocalIndex);
        Element.SetAttributeValue(nameof(DestinationTag), message.DestinationTag);
        Element.SetAttributeValue(nameof(CacheConnections), message.CacheConnections);
        Element.SetAttributeValue(nameof(LargePacketUsage), message.LargePacketUsage);
    }

    /// <summary>
    /// Gets or sets the <see cref="MessageType"/> value for the <see cref="MessageData"/> object.
    /// </summary>
    public MessageType? MessageType
    {
        get => GetValue(MessageType.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets or sets the <see cref="RequestedLength"/> value for the <see cref="MessageData"/> object.
    /// </summary>
    public int? RequestedLength
    {
        get => GetValue(int.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets or sets the <see cref="ConnectedFlag"/> value for the <see cref="MessageData"/> object.
    /// </summary>
    public int? ConnectedFlag
    {
        get => GetValue(int.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets or sets the <see cref="ConnectionPath"/> value for the <see cref="MessageData"/> object.
    /// </summary>
    public string? ConnectionPath
    {
        get => GetValue();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets or sets the <see cref="CommTypeCode"/> value for the <see cref="MessageData"/> object.
    /// </summary>
    public int? CommTypeCode
    {
        get => GetValue(int.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets or sets the <see cref="ServiceCode"/> value for the <see cref="MessageData"/> object.
    /// </summary>
    public INT? ServiceCode
    {
        get => GetValue(INT.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets or sets the <see cref="ObjectType"/> value for the <see cref="MessageData"/> object.
    /// </summary>
    public INT? ObjectType
    {
        get => GetValue(INT.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets or sets the <see cref="TargetObject"/> value for the <see cref="MessageData"/> object.
    /// </summary>
    public int? TargetObject
    {
        get => GetValue(int.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets or sets the <see cref="AttributeNumber"/> value for the <see cref="MessageData"/> object.
    /// </summary>
    public INT? AttributeNumber
    {
        get => GetValue(INT.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets or sets the <see cref="LocalIndex"/> value for the <see cref="MessageData"/> object.
    /// </summary>
    public int? LocalIndex
    {
        get => GetValue(int.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets or sets the <see cref="DestinationTag"/> value for the <see cref="MessageData"/> object.
    /// </summary>
    public TagName? DestinationTag
    {
        get => GetValue() ?? new STRING();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets or sets the <see cref="CacheConnections"/> value for the <see cref="MessageData"/> object.
    /// </summary>
    public bool? CacheConnections
    {
        get => GetOptionalBool();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets or sets the <see cref="LargePacketUsage"/> value for the <see cref="MessageData"/> object.
    /// </summary>
    public bool? LargePacketUsage
    {
        get => GetOptionalBool();
        set => SetValue(value);
    }
}