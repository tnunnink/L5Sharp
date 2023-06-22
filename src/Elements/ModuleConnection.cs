using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Elements;

/// <summary>
/// A component of a <see cref="Module"/> that represents the properties and data of the connection to the field device.
/// </summary>
public sealed class ModuleConnection : LogixElement<ModuleConnection>
{
    /// <inheritdoc />
    public ModuleConnection()
    {
        Name = string.Empty;
        Type = ConnectionType.Unknown;
        Priority = ConnectionPriority.Scheduled;
        InputConnectionType = TransmissionType.Multicast;
        InputProductionTrigger = ProductionTrigger.Cyclic;
    }

    /// <inheritdoc />
    public ModuleConnection(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Gets the name of the <see cref="ModuleConnection"/> component.
    /// </summary>
    public string? Name
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the value of the Request Packet Interval for the <see cref="ModuleConnection"/>. 
    /// </summary>
    public int Rpi
    {
        get => GetValue<int>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the input connection point for the primary <see cref="ModuleConnection"/>.
    /// </summary>
    public ushort InputCxnPoint
    {
        get => GetValue<ushort>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the input size for the <see cref="ModuleConnection"/>.
    /// </summary>
    public ushort InputSize
    {
        get => GetValue<ushort>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the output connection point for the primary <see cref="ModuleConnection"/>.
    /// </summary>
    public ushort OutputCxnPoint
    {
        get => GetValue<ushort>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the output size for the <see cref="ModuleConnection"/>.
    /// </summary>
    public ushort OutputSize
    {
        get => GetValue<ushort>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="Enums.ConnectionType"/> value for the <see cref="ModuleConnection"/>.
    /// </summary>
    public ConnectionType? Type
    {
        get => GetValue<ConnectionType>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="Enums.ConnectionPriority"/> value for the <see cref="ModuleConnection"/>.
    /// </summary>
    public ConnectionPriority? Priority
    {
        get => GetValue<ConnectionPriority>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="Enums.TransmissionType"/> value for the <see cref="ModuleConnection"/>.
    /// </summary>
    public TransmissionType? InputConnectionType
    {
        get => GetValue<TransmissionType>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="ModuleConnection"/> output is a redundant owner.
    /// </summary>
    public bool OutputRedundantOwner
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="Enums.ProductionTrigger"/> value for the <see cref="ModuleConnection"/>.
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
    /// Gets the value of the Event ID used in conjunction with an event task for the <see cref="ModuleConnection"/>.
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
    /// Gets the Tag that represents the input channel data for the <see cref="ModuleConnection"/>.
    /// </summary>
    public Tag? Input
    {
        get
        {
            var element = Element.Descendants(L5XName.InputTag).FirstOrDefault();

            if (element is null) return default;

            return new Tag(element)
            {
                Name = ModuleTagName(element, InputTagSuffix)
            };
        }
    }

    /// <summary>
    /// Gets the Tag that represents the output channel data for the <see cref="ModuleConnection"/>.
    /// </summary>
    public Tag? Output
    {
        get
        {
            var element = Element.Descendants(L5XName.OutputTag).FirstOrDefault();

            if (element is null) return default;

            return new Tag(element)
            {
                Name = ModuleTagName(element, OutputTagSuffix)
            };
        }
    }
    
    /// <summary>
    /// A helper for determining a <see cref="Module"/> tag name for an input, output, or config tag element.
    /// </summary>
    /// <param name="element">The current module tag element.</param>
    /// <param name="suffix">The string suffix to append to the determines tag name. Default is 'C' for config tag.</param>
    /// <returns>A <see cref="string"/> representing the tag name of the module tag.</returns>
    private static string ModuleTagName(XNode element, string suffix = "C")
    {
        var moduleName = element.Ancestors(L5XName.Module)
            .FirstOrDefault()?.Attribute(L5XName.Name)?.Value;

        var parentName = element.Ancestors(L5XName.Module)
            .FirstOrDefault()?.Attribute(L5XName.ParentModule)?.Value;

        var slot = element
            .Ancestors(L5XName.Module)
            .Descendants(L5XName.Port)
            .Where(p => bool.Parse(p.Attribute(L5XName.Upstream)?.Value!)
                        && p.Attribute(L5XName.Type)?.Value != "Ethernet"
                        && int.TryParse(p.Attribute(L5XName.Address)?.Value, out _))
            .Select(p => p.Attribute(L5XName.Address)?.Value)
            .FirstOrDefault();

        return slot is not null ? $"{parentName}:{slot}:{suffix}" : $"{moduleName}:{suffix}";
    }
}