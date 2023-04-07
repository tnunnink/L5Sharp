using System.Collections.Generic;
using System.Linq;
using System.Net;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Rockwell;
using L5Sharp.Serialization;

namespace L5Sharp.Components;

/// <summary>
/// A logix <c>Module</c> component. Contains the properties that comprise the L5X Module element.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[LogixSerializer(typeof(ModuleSerializer))]
public class Module : ILogixComponent
{
    /// <inheritdoc />
    public string Name { get; set; } = string.Empty;

    /// <inheritdoc />
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The catalog number uniquely identifies the module. This is a rockwell defined convention.
    /// </summary>
    /// <value>A <see cref="string"/> value containing the catalog number.</value>
    public string CatalogNumber { get; set; } = string.Empty;

    /// <summary>
    /// The vendor or manufacturer of the module.
    /// </summary>
    /// <value>A <see cref="Core.Vendor"/> entity that contains the id and name of the vendor.</value>
    /// <remarks>
    /// All modules have a vendor representing the manufacturer of the module.
    /// This value can be retrieved as part of the <see cref="CatalogEntry"/> object obtained using a
    /// <see cref="ICatalogService"/> for catalog lookup. When deserializing from L5X file, typically only the vendor
    /// id is available on the module element.
    /// </remarks>
    public Vendor Vendor { get; set; } = Vendor.Unknown;

    /// <summary>
    /// The product type of the module, representing a category of the module.
    /// </summary>
    /// <remarks>
    /// All modules have a product type representing the product category of the module.
    /// This value can be retrieved as part of the <see cref="CatalogEntry"/> object obtained using a
    /// <see cref="ICatalogService"/> for catalog lookup.
    /// This value will be validated by Logix upon import of the L5X. 
    /// </remarks>
    public ProductType ProductType { get; set; } = ProductType.Unknown;

    /// <summary>
    /// The unique product code value of the module.
    /// </summary>
    /// <remarks>
    /// This is a unique value that identifies the module and is assigned by Logix.
    /// This value can be retrieved as part of the <see cref="CatalogEntry"/> object obtained using a
    /// <see cref="ICatalogService"/> for catalog lookup, or when deserializing from an L5X file.
    /// </remarks>
    public ushort ProductCode { get; set; }

    /// <summary>
    /// The revision number or hardware version of the module.
    /// </summary>
    /// <value>A <see cref="Core.Revision"/> object representing the major and minor version.</value>
    /// <remarks>
    /// All modules must have a specified revision number.
    /// </remarks>
    public Revision Revision { get; set; } = new();

    /// <summary>
    /// The name of the parent module, or module that the current module is connected to upstream.
    /// This specifies how the module is connected within the module tree.
    /// </summary>
    /// <value>A <see cref="string"/> representing the parent module name. Default is an empty string.</value>
    public string ParentModule { get; set; } = string.Empty;

    /// <summary>
    /// The port id of the parent module that the current module is connected to.
    /// This specified how the module is connected within the module tree.
    /// </summary>
    /// <value>A <see cref="int"/> representing the id of the parent port. Default is zero.</value>
    public int ParentPortId { get; set; }

    /// <summary>
    /// An indication of whether the module is inhibited or disabled.
    /// </summary>
    public bool Inhibited { get; set; }

    /// <summary>
    /// An indication of whether the module the module will cause a major fault when faulted.
    /// </summary>
    public bool MajorFault { get; set; }

    /// <summary>
    /// An indication of whether whether the module has safety features enabled.
    /// </summary>
    public bool SafetyEnabled { get; set; }

    /// <summary>
    /// The electronic keying mode of the module.
    /// </summary>
    /// <value>A <see cref="ElectronicKeying"/> enum value representing the mode.</value>
    public ElectronicKeying Keying { get; set; } = ElectronicKeying.CompatibleModule;

    /// <summary>
    /// A collection of <see cref="Port"/> that define the module's connection within the module tree.
    /// </summary>
    public List<Port> Ports { get; set; } = new();

    /// <summary>
    /// A <see cref="Tag"/> containing the configuration data for the module.
    /// </summary>
    public Tag? Config { get; set; }

    /// <summary>
    /// A collection of <see cref="ModuleConnection"/> defining the input and output connection specific to the module.
    /// </summary>
    public List<ModuleConnection> Connections { get; set; } = new();

    /// <summary>
    /// Gets the slot number of the current module if one exists. If the module does not have an slot, returns null.
    /// </summary>
    /// <value>An <see cref="byte"/> representing the slot number of the module.</value>
    /// <remarks>
    /// This is a helper that just looks for an upstream <see cref="Port"/> with a valid slot byte number.
    /// </remarks>
    public byte? Slot => Ports.FirstOrDefault(p => p.Upstream && p.Address.IsSlot)?.Address.ToSlot();

    /// <summary>
    /// Gets the IP address of the current module if one exists. If the module does not have an IP, returns null.
    /// </summary>
    /// <value>An <see cref="IPAddress"/> representing the IP of the module.</value>
    /// <remarks>
    /// This is a helper property that just looks for <see cref="Port"/> for an Ethernet port with a
    /// valid IP address.
    /// </remarks>
    public IPAddress? IP => Ports.FirstOrDefault(p => p is { Type: "Ethernet", Address.IsIPv4: true })?.Address
        .ToIPAddress();

    /// <summary>
    /// Returns a collection of all non-null <see cref="Tag"/> objects for the current Module, including all
    /// input, output, and config tags.
    /// </summary>
    /// <value>An <see cref="IEnumerable{T}"/> containing the base tags for the Module.</value>
    public IEnumerable<Tag> Tags()
    {
        var tags = new List<Tag>();

        if (Config is not null)
            tags.Add(Config);

        foreach (var connection in Connections)
        {
            if (connection.Input is not null)
                tags.Add(connection.Input);

            if (connection.Output is not null)
                tags.Add(connection.Output);
        }

        return tags;
    }
}