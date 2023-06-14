using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

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
    public MESSAGE() : base(new XElement(L5XName.MessageParameters))
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
    public MESSAGE(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    public override string Name => nameof(MESSAGE);

    /// <inheritdoc />
    public override DataTypeClass Class => DataTypeClass.Predefined;
    
    /// <inheritdoc />
    //todo this wont work for the string type. Do we need a generic logix type parse?
    public override IEnumerable<Member> Members =>
        Element.Attributes().Select(a => new Member(a.Name.ToString(), Atomic.Parse(a.Value))); 

    /// <summary>
    /// Gets the <see cref="MessageType"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>  /// <summary>
    /// Gets the <see cref="MessageType"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public STRING MessageType
    {
        get => GetValue<string>() ?? throw new L5XException(Element);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="RequestedLength"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT RequestedLength
    {
        get => GetValue<INT>() ?? throw new L5XException(Element);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ConnectedFlag"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT ConnectedFlag
    {
        get => GetValue<INT>() ?? throw new L5XException(Element);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ConnectionPath"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public STRING ConnectionPath
    {
        get => GetValue<string>() ?? throw new L5XException(Element);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="CommTypeCode"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT CommTypeCode
    {
        get => GetValue<INT>() ?? throw new L5XException(Element);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ServiceCode"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT ServiceCode
    {
        get => GetValue<INT>() ?? throw new L5XException(Element);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ObjectType"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT ObjectType
    {
        get => GetValue<INT>() ?? throw new L5XException(Element);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="TargetObject"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT TargetObject
    {
        get => GetValue<INT>() ?? throw new L5XException(Element);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="AttributeNumber"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT AttributeNumber
    {
        get => GetValue<INT>() ?? throw new L5XException(Element);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LocalIndex"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public INT LocalIndex
    {
        get => GetValue<INT>() ?? throw new L5XException(Element);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="DestinationTag"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public STRING DestinationTag
    {
        get => GetValue<string>() ?? throw new L5XException(Element);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="CacheConnections"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public BOOL CacheConnections
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LargePacketUsage"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public BOOL LargePacketUsage
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }
}