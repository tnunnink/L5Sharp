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
    }

    /// <inheritdoc />
    public MessageData(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    public override void Update(LogixData data)
    {
        if (data is not MessageData other)
            throw new ArgumentException("Can not update message data with type");

        MessageType = other.MessageType;
    }

    /// <summary>
    /// Gets the <see cref="MessageType"/> value of the <see cref="MESSAGE"/> data object.
    /// </summary>
    public MessageType? MessageType
    {
        get => GetValue(MessageType.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="RequestedLength"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public int? RequestedLength
    {
        get => GetValue(int.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ConnectedFlag"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public bool? ConnectedFlag
    {
        get => GetValue(bool.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ConnectionPath"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public string? ConnectionPath
    {
        get => GetValue();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="CommTypeCode"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT? CommTypeCode
    {
        get => GetValue(INT.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ServiceCode"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT? ServiceCode
    {
        get => GetValue(INT.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ObjectType"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT? ObjectType
    {
        get => GetValue(INT.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="TargetObject"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT? TargetObject
    {
        get => GetValue(INT.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="AttributeNumber"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT? AttributeNumber
    {
        get => GetValue(INT.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LocalIndex"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT? LocalIndex
    {
        get => GetValue(INT.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="DestinationTag"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public STRING? DestinationTag
    {
        get => GetValue() ?? new STRING();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="CacheConnections"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public BOOL CacheConnections
    {
        get => GetValue(BOOL.Parse) ?? false;
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LargePacketUsage"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public BOOL LargePacketUsage
    {
        get => GetValue(BOOL.Parse) ?? false;
        set => SetValue(value);
    }
}