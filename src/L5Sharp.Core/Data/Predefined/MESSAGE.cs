using System.Collections.Generic;
using System.Xml.Linq;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp.Core;

/// <summary>
/// A predefined or built-in data type used with message instructions. Note that the members of this type resemble those
/// observed from exported L5X and not those of the predefined data type.
/// </summary>
[LogixElement(L5XName.MessageParameters)]
[LogixData(nameof(MESSAGE))]
public sealed class MESSAGE : StructureData
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
        ServiceCode = Radix.Hex.Format(0);
        ObjectType = Radix.Hex.Format(0);
        TargetObject = new INT();
        AttributeNumber = Radix.Hex.Format(0);
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
    public override IEnumerable<LogixMember> Members => GenerateVirtualMembers();

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
    public INT? RequestedLength
    {
        get => GetValue(INT.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ConnectedFlag"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT? ConnectedFlag
    {
        get => GetValue(INT.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ConnectionPath"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public STRING? ConnectionPath
    {
        get => GetValue() ?? new STRING();
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

    private IEnumerable<LogixMember> GenerateVirtualMembers()
    {
        var members = new List<LogixMember>();

        foreach (var attribute in Element.Attributes())
        {
            members.Add(new LogixMember(attribute));
        }

        return members;
    }
}