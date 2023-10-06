using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;
using L5Sharp.Utilities;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp.Types.Predefined;

/// <summary>
/// A predefined or built in data type used with message instructions. Note that the members of this type resemble those
/// observed from exported L5X, and not that of the predefined data type.
/// </summary>
public sealed class MESSAGE : StructureType
{
    /// <summary>
    /// Creates a new <see cref="MESSAGE"/> data type instance.
    /// </summary>
    public MESSAGE() : base(nameof(MESSAGE))
    {
        MessageType = new STRING();
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
    public MESSAGE(XElement element) : base(nameof(MESSAGE))
    {
        if (element is null) throw new ArgumentNullException(nameof(element));

        MessageType = element.Attribute(nameof(MessageType))?.Value.Parse<STRING>() ?? new STRING();
        RequestedLength = element.Attribute(nameof(RequestedLength))?.Value.Parse<INT>() ?? new INT();
        ConnectedFlag = element.Attribute(nameof(ConnectedFlag))?.Value.Parse<INT>() ?? new INT();
        ConnectionPath = element.Attribute(nameof(ConnectionPath))?.Value.Parse<STRING>() ?? new STRING();
        CommTypeCode = element.Attribute(nameof(CommTypeCode))?.Value.Parse<INT>() ?? new INT();
        ServiceCode = element.Attribute(nameof(ServiceCode))?.Value.Parse<INT>() ?? new INT();
        ObjectType = element.Attribute(nameof(ObjectType))?.Value.Parse<INT>() ?? new INT();
        TargetObject = element.Attribute(nameof(TargetObject))?.Value.Parse<INT>() ?? new INT();
        AttributeNumber = element.Attribute(nameof(AttributeNumber))?.Value.Parse<INT>() ?? new INT();
        LocalIndex = element.Attribute(nameof(LocalIndex))?.Value.Parse<INT>() ?? new INT();
        DestinationTag = element.Attribute(nameof(DestinationTag))?.Value.Parse<STRING>() ?? new STRING();
        CacheConnections = element.Attribute(nameof(CacheConnections))?.Value.Parse<BOOL>() ?? new BOOL();
        LargePacketUsage = element.Attribute(nameof(LargePacketUsage))?.Value.Parse<BOOL>() ?? new BOOL();
    }

    /// <inheritdoc />
    public override DataTypeClass Class => DataTypeClass.Predefined;

    /// <inheritdoc />
    public override XElement Serialize()
    {
        var element = new XElement(L5XName.MessageParameters);
        element.Add(Members.Select(m => new XAttribute(m.Name, m.DataType)));
        return element;
    }

    /// <summary>
    /// Gets the <see cref="MessageType"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>  /// <summary>
    /// Gets the <see cref="MessageType"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public STRING MessageType
    {
        get => GetMember<STRING>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="RequestedLength"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT RequestedLength
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ConnectedFlag"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT ConnectedFlag
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ConnectionPath"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public STRING ConnectionPath
    {
        get => GetMember<STRING>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="CommTypeCode"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT CommTypeCode
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ServiceCode"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT ServiceCode
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ObjectType"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT ObjectType
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="TargetObject"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT TargetObject
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="AttributeNumber"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT AttributeNumber
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LocalIndex"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT LocalIndex
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="DestinationTag"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public STRING DestinationTag
    {
        get => GetMember<STRING>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="CacheConnections"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public BOOL CacheConnections
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="LargePacketUsage"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public BOOL LargePacketUsage
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}