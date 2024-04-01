using System.Collections.Generic;
using System.Xml.Linq;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp.Core;

/// <summary>
/// A predefined or built in data type used with message instructions. Note that the members of this type resemble those
/// observed from exported L5X, and not that of the predefined data type.
/// </summary>
public sealed class MESSAGE : StructureType
{
    /// <summary>
    /// Creates a new <see cref="MESSAGE"/> data type instance.
    /// </summary>
    public MESSAGE() : base(new XElement(L5XName.MessageParameters))
    {
        MessageType = MessageType.Unconfigured;
        RequestedLength = new INT();
        ConnectedFlag = new INT();
        ConnectionPath = new STRING();
        CommTypeCode = new INT();
        ServiceCode = new INT(Radix.Hex);
        ObjectType = new INT(Radix.Hex);
        TargetObject = new INT();
        AttributeNumber = new INT(Radix.Hex);
        LocalIndex = new INT();
        DestinationTag = new STRING();
        CacheConnections = new BOOL();
        LargePacketUsage = new BOOL();
    }

    /// <inheritdoc />
    public MESSAGE(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    public override string Name => nameof(MESSAGE);

    /// <inheritdoc />
    public override IEnumerable<Member> Members => GenerateVirtualMembers();

    /// <summary>
    /// Gets the <see cref="MessageType"/> value of the <see cref="MESSAGE"/> data object.
    /// </summary>
    public MessageType MessageType
    {
        get => GetRequiredValue<MessageType>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="RequestedLength"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT RequestedLength
    {
        get => GetRequiredValue<INT>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ConnectedFlag"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT ConnectedFlag
    {
        get => GetRequiredValue<INT>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ConnectionPath"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public STRING ConnectionPath
    {
        get => GetRequiredValue<STRING>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="CommTypeCode"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT CommTypeCode
    {
        get => GetRequiredValue<INT>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ServiceCode"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT ServiceCode
    {
        get => GetRequiredValue<INT>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ObjectType"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT ObjectType
    {
        get => GetRequiredValue<INT>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="TargetObject"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT TargetObject
    {
        get => GetRequiredValue<INT>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="AttributeNumber"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT AttributeNumber
    {
        get => GetRequiredValue<INT>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LocalIndex"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT LocalIndex
    {
        get => GetRequiredValue<INT>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="DestinationTag"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public STRING DestinationTag
    {
        get => GetRequiredValue<STRING>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Gets the <see cref="CacheConnections"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public BOOL CacheConnections
    {
        get => GetValue<BOOL>() ?? false;
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LargePacketUsage"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public BOOL LargePacketUsage
    {
        get => GetValue<BOOL>() ?? false;
        set => SetValue(value);
    }

    private IEnumerable<Member> GenerateVirtualMembers()
    {
        yield return new Member(nameof(MessageType), () => new STRING(MessageType.Value),
            t => { MessageType = MessageType.Parse(t.ToString()); });
        yield return new Member(nameof(RequestedLength), () => RequestedLength, t => { RequestedLength = (INT)t; });
        yield return new Member(nameof(ConnectionPath), () => ConnectionPath,
            t => { ConnectionPath = t.As<STRING>(); });
        yield return new Member(nameof(ConnectedFlag), () => ConnectedFlag, t => { ConnectedFlag = (INT)t; });
        yield return new Member(nameof(CommTypeCode), () => CommTypeCode, t => { CommTypeCode = (INT)t; });
        yield return new Member(nameof(ServiceCode), () => ServiceCode, t => { ServiceCode = (INT)t; });
        yield return new Member(nameof(ObjectType), () => ObjectType, t => { ObjectType = (INT)t; });
        yield return new Member(nameof(TargetObject), () => TargetObject, t => { TargetObject = (INT)t; });
        yield return new Member(nameof(AttributeNumber), () => AttributeNumber, t => { AttributeNumber = (INT)t; });
        yield return new Member(nameof(LocalIndex), () => LocalIndex, t => { LocalIndex = (INT)t; });
        yield return new Member(nameof(DestinationTag), () => DestinationTag, t => { DestinationTag = (STRING)t; });
        yield return new Member(nameof(CacheConnections), () => CacheConnections, t => { CacheConnections = (BOOL)t; });
        yield return new Member(nameof(LargePacketUsage), () => LargePacketUsage, t => { LargePacketUsage = (BOOL)t; });
    }
}