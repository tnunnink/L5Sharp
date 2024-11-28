using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;


namespace L5Sharp.Core;

/// <summary>
/// A logix <c>Module</c> component. Contains the properties that comprise the L5X Module element.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public class Module : LogixComponent<Module>
{
    /// <inheritdoc />
    protected override List<string> ElementOrder =>
    [
        L5XName.Description,
        L5XName.EKey,
        L5XName.Ports,
        L5XName.Communications,
        L5XName.ExtendedProperties
    ];

    /// <inheritdoc />
    public Module() : base(L5XName.Module)
    {
        CatalogNumber = string.Empty;
        Revision = new Revision();
        Vendor = Vendor.Rockwell;
        ProductType = ProductType.Unknown;
        ParentModule = string.Empty;
        ParentModPortId = default;
        Inhibited = false;
        MajorFault = false;
        SafetyEnabled = false;
        Keying = ElectronicKeying.CompatibleModule;
        Ports = [];
    }

    /// <inheritdoc />
    public Module(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new <see cref="Module"/> initialized with the provided name and optional parameters.
    /// </summary>
    /// <param name="name">The name of the Module.</param>
    /// <param name="catalogNumber">The optional catalog number for the Module. Will default to empty string.</param>
    /// <param name="revision">The optional <see cref="Revision"/> number for the Module. Will default to 1.0.</param>
    public Module(string name, string? catalogNumber = default, Revision? revision = default) : this()
    {
        Element.SetAttributeValue(L5XName.Name, name);
        CatalogNumber = catalogNumber ?? string.Empty;
        Revision = revision ?? new Revision();
    }

    /// <inheritdoc />
    /// <remarks>
    /// Module components don't all have a name attribute (e.g. VFD peripheral modules). For this reason,
    /// the name property is overriden to not be a required field for this component type. If the name is not found,
    /// this property returns an empty string.
    /// </remarks>
    public override string Name
    {
        get => GetValue<string>() ?? string.Empty;
        set => SetValue(value);
    }

    /// <summary>
    /// Specify the catalog number that uniquely identifies the module. This is a rockwell defined convention,
    /// and represents the identity of the module type.
    /// </summary>
    /// <value>A <see cref="string"/> value containing the catalog number.</value>
    public string? CatalogNumber
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// The vendor or manufacturer of the module.
    /// </summary>
    /// <value>A <see cref="Core.Vendor"/> entity that contains the id and name of the vendor.</value>
    /// <remarks>
    /// All modules have a vendor representing the manufacturer of the module.
    /// </remarks>
    public Vendor? Vendor
    {
        get => GetValue<Vendor>();
        set => SetValue(value?.Id);
    }

    /// <summary>
    /// The product type of the module, representing a category of the module.
    /// </summary>
    /// <remarks>
    /// All modules have a product type representing the product category of the module.
    /// </remarks>
    public ProductType? ProductType
    {
        get => GetValue<ProductType>();
        set => SetValue(value?.Id);
    }

    /// <summary>
    /// The unique product code value of the module.
    /// </summary>
    /// <remarks>
    /// This is a unique value that identifies the module and is assigned by Logix.
    /// </remarks>
    public ushort ProductCode
    {
        get => GetValue<ushort>();
        set => SetValue(value);
    }

    /// <summary>
    /// The revision number or hardware version of the module.
    /// </summary>
    /// <value>A <see cref="Core.Revision"/> object representing the major and minor version.</value>
    /// <remarks>
    /// All modules must have a specified revision number.
    /// </remarks>
    public Revision? Revision
    {
        get
        {
            var major = Element.Attribute(L5XName.Major)?.Value.Parse<ushort>();
            var minor = Element.Attribute(L5XName.Minor)?.Value.Parse<ushort>();
            return major.HasValue && minor.HasValue ? new Revision(major.Value, minor.Value) : default;
        }
        set
        {
            Element.SetAttributeValue(L5XName.Major, value?.Major);
            Element.SetAttributeValue(L5XName.Minor, value?.Minor);
        }
    }

    /// <summary>
    /// The name of the parent module, or module that the current module is connected to upstream.
    /// This specifies how the module is connected within the module tree.
    /// </summary>
    /// <value>A <see cref="string"/> representing the parent module name. Default is an empty string.</value>
    public string? ParentModule
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// The port id of the parent module that the current module is connected to.
    /// This specified how the module is connected within the module tree.
    /// </summary>
    /// <value>A <see cref="int"/> representing the id of the parent port. Default is zero.</value>
    public int ParentModPortId
    {
        get => GetValue<int>();
        set => SetValue(value);
    }

    /// <summary>
    /// An indication of whether the module is inhibited or disabled.
    /// </summary>
    public bool Inhibited
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// An indication of whether the module will cause a major fault when faulted.
    /// </summary>
    public bool MajorFault
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// An indication of whether the module has safety features enabled.
    /// </summary>
    public bool SafetyEnabled
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// The electronic keying mode of the module.
    /// </summary>
    /// <value>A <see cref="ElectronicKeying"/> enum value representing the mode.</value>
    public ElectronicKeying? Keying
    {
        get => Element.Element(L5XName.EKey)?.Attribute(L5XName.State)?.Value.Parse<ElectronicKeying>();
        set
        {
            if (value is null)
            {
                Element.Element(L5XName.EKey)?.Remove();
                return;
            }

            if (Element.Element(L5XName.EKey) is null)
            {
                Element.Add(new XElement(L5XName.EKey, new XAttribute(L5XName.State, value)));
                return;
            }

            Element.Element(L5XName.EKey)!.SetAttributeValue(L5XName.State, value);
        }
    }

    /// <summary>
    /// A collection of <see cref="Port"/> elements that define the module's connection within the module tree.
    /// </summary>
    /// <value>A <see cref="LogixContainer{TElement}"/> of <see cref="Port"/> objects.</value>
    /// <remarks>
    /// Ports define how a module's peripherals are connected to other module's, forming the network or tree of
    /// devices used to communicated with a controller and field equipment. Ports must have a unique id, a type,
    /// and address.
    /// </remarks>
    public LogixContainer<Port> Ports
    {
        get => GetContainer<Port>();
        set => SetContainer(value);
    }

    /// <summary>
    /// 
    /// </summary>
    public Communications? Communications
    {
        get => GetComplex<Communications>();
        set => SetComplex(value);
    }

    //Properties or methods extending the functionality of a Module component.

    #region Extensions

    /// <summary>
    /// Gets the slot number of the current module if one exists. If the module does not have an slot, returns null.
    /// </summary>
    /// <value>An <see cref="byte"/> representing the slot number of the module.</value>
    /// <remarks>
    /// This property is not directly part of the L5X structure, but is a helper to make accessing the slot number simple.
    /// This property looks for an upstream <see cref="Port"/> with a valid slot byte number.
    /// </remarks>
    public byte? Slot => Ports.FirstOrDefault(p => p is { Upstream: true, Address.IsSlot: true })?.Address.ToSlot();

    /// <summary>
    /// Gets or sets the IP address of this module if one exists. If the module does not have an IP, returns null.
    /// </summary>
    /// <value>An <see cref="IPAddress"/> representing the IP of the module.</value>
    /// <remarks>
    /// This property is not directly part of the L5X structure, but is a helper to make accessing the IP simple.
    /// This property looks for an Ethernet type <see cref="Port"/> with a valid IPv4 address.
    /// </remarks>
    public IPAddress? IP
    {
        get
        {
            var port = Ports.FirstOrDefault(p => p is { IsEthernet: true });
            return port?.Address.ToIPAddress();
        }
        set
        {
            var port = Ports.FirstOrDefault(p => p is { IsEthernet: true });

            if (port is null)
                throw new InvalidOperationException("No Ethernet type port is configured for this Module.");

            port.Address = Address.IP(value?.ToString());
        }
    }

    /// <summary>
    /// Gets the parent module of this module component defined in the current L5X document.
    /// </summary>
    /// <returns>A <see cref="Module"/> representing the parent of this module if it exists; otherwise, <c>null</c>.</returns>
    /// <remarks>
    /// The L5X structure serializes modules in a flat list with each element having properties (ParentModule, ParentModPortId)
    /// defining the parent/child IO tree relationship. It would be nice to navigate this hierarchy programatically,
    /// hence the reason for this extension. Of course, this requires the module is attached to the L5X content.
    /// In-memory created modules will inherently return a null object, as there is no L5X structure to introspect.
    /// </remarks>
    public Module? Parent
    {
        get
        {
            var parent = Element.Parent?.Elements().FirstOrDefault(m => m.LogixName() == ParentModule);
            return parent is not null ? new Module(parent) : default;
        }
    }

    /// <summary>
    /// Gets the child modules of this module component defined in the current L5X content. 
    /// </summary>
    /// <returns>
    /// A <see cref="IEnumerable{T}"/> of <see cref="Module"/> components that have the <c>ParentModule</c> property
    /// configured as the name of this module.
    /// </returns>
    /// <remarks>
    /// The L5X structure serializes modules in a flat list with each element having properties (ParentModule, ParentModPortId)
    /// defining the parent/child IO tree relationship. It would be nice to navigate this hierarchy programatically,
    /// hence the reason for this extension method. Of course, this requires the module is attached to the L5X content.
    /// In-memory created modules will inherently return an empty collection, as there is no L5X structure to introspect.
    /// </remarks>
    public IEnumerable<Module> Modules
    {
        get
        {
            return Element.Parent?.Elements().Where(m => m.Attribute(L5XName.ParentModule)?.Value == Name)
                       .Select(e => new Module(e))
                   ?? [];
        }
    }

    /// <summary>
    /// Returns a collection of all non-null <see cref="Tag"/> objects for the current Module, including all
    /// config, input, and output tags.
    /// </summary>
    /// <value>An <see cref="IEnumerable{T}"/> containing the base tags for the Module.</value>
    /// <remarks>
    /// Since module tags are nested within different layers of complex types, it can be difficult to just
    /// get a single list of all module tags. This extension makes that easy by sifting through the object and returning
    /// a flat list containing all non-null config, input, and output tags defined for the <see cref="Module"/> component.
    /// </remarks>
    public IEnumerable<Tag> Tags
    {
        get
        {
            var tags = new List<Tag>();

            if (Communications is null) return tags;

            if (Communications.ConfigTag is not null)
                tags.Add(Communications.ConfigTag);

            foreach (var connection in Communications.Connections)
            {
                if (connection.InputTag is not null)
                    tags.Add(connection.InputTag);

                if (connection.OutputTag is not null)
                    tags.Add(connection.OutputTag);
            }

            return tags;
        }
    }

    /// <summary>
    /// Returns the module's config tag object contained in the communications element.
    /// </summary>
    /// <returns>A <see cref="Tag"/> containing the module's config tag data.</returns>
    /// <remarks>This is a simple helper to make accessing the module config data more concise.</remarks>
    public Tag? Config => Communications?.ConfigTag;

    /// <summary>
    /// Adds a child module to the current module object by updating the parent module properties, configuring the
    /// child module upstream port, and adding the component to the underlying L5X content.
    /// </summary>
    /// <param name="child">The module to add.</param>
    /// <param name="address">The optional <see cref="Address"/> (slot or IP) of the module to add.</param>
    /// <exception cref="InvalidOperationException"><c>parent</c> does not have a downstream port for which to connect
    /// child modules.</exception>
    /// <remarks>
    /// This extension gives us an easy way to add modules hierarchically to the underlying L5X content.
    /// If the parent module is attached to a L5X content file, this will add child module.
    /// </remarks>
    public void Add(Module child, Address? address = default)
    {
        var parentPort = Ports.FirstOrDefault(p => !p.Upstream);

        if (parentPort is null)
            throw new InvalidOperationException(
                $"The module '{Name}' does not have a port for downstream module connections.");

        address = parentPort.IsEthernet switch
        {
            true when address is null => Address.IP(),
            false when address is null => NextSlot(),
            _ => address
        };

        var childPort = new Port { Id = 1, Type = parentPort.Type, Address = address, Upstream = true };

        child.ParentModule = Name;
        child.ParentModPortId = parentPort.Id;
        child.Ports.Add(childPort);

        var container = Element.Parent;

        if (container is null)
            throw new InvalidOperationException($"The module '{Name}' is not attached to and L5X content file.");

        container.Add(child.Serialize());
    }

    /// <summary>
    /// Creates a new <see cref="Module"/> with the provided name and catalog number.
    /// </summary>
    /// <param name="name">The name of the module</param>
    /// <param name="catalogNumber">The catalog number to look up a catalog entry for.</param>
    /// <param name="address">The optional <see cref="Address"/> defining the slot or IP address of the device.</param>
    /// <returns>A new <see cref="Module"/> object initialized with data return by the catalog service.</returns>
    /// <exception cref="InvalidOperationException">The module catalog service could not load the installed catalog
    /// database file -or- catalog number does not exist in the catalog database.</exception>
    /// <exception cref="ArgumentException"><c>catalogNumber</c> is null or empty.</exception>
    /// <remarks>
    /// This factory method uses the <see cref="ModuleCatalog"/> service to lookup info for the specified
    /// catalog number. If RSLogix is not installed on the current environment, this will throw an exception.
    /// </remarks>
    public static Module Create(string name, string catalogNumber, Address? address = null)
    {
        var catalog = new ModuleCatalog();
        var entry = catalog.Lookup(catalogNumber);

        var ports = entry.Ports.Select(p => new Port
        {
            Id = p.Number,
            Type = p.Type,
            Upstream = !p.DownstreamOnly
        });

        var module = new Module
        {
            Name = name,
            CatalogNumber = entry.CatalogNumber,
            Revision = entry.Revisions.Max(),
            Vendor = entry.Vendor,
            ProductType = entry.ProductType,
            ProductCode = entry.ProductCode,
            Ports = new LogixContainer<Port>(ports),
            Description = entry.Description,
            SafetyEnabled = entry.Categories.Contains("Safety")
        };

        if (address is not null)
            module.SetAddress(address);

        return module;
    }

    /// <summary>
    /// Creates a new <see cref="Module"/> instance that represents the local controller module for a project.
    /// </summary>
    /// <param name="processor">The processor catalog number for the module.</param>
    /// <param name="revision">The software revision of the controller.</param>
    /// <returns>A new <see cref="Module"/> representing a default local controller module instance.</returns>
    public static Module Local(string processor, Revision? revision = default)
    {
        var catalog = new ModuleCatalog();
        var entry = catalog.Lookup(processor);

        var ports = entry.Ports.Select(p => new Port
        {
            Id = p.Number,
            Type = p.Type,
            Address = p.Type == "Ethernet" ? Address.IP() : Address.Slot(),
            Upstream = !p.DownstreamOnly
        });

        return new Module
        {
            Name = "Local",
            CatalogNumber = entry.CatalogNumber,
            Vendor = entry.Vendor,
            ProductType = entry.ProductType,
            ProductCode = entry.ProductCode,
            Revision = revision ?? entry.Revisions.Max(),
            ParentModule = "Local",
            ParentModPortId = 1,
            MajorFault = true,
            Keying = ElectronicKeying.Disabled,
            Ports = new LogixContainer<Port>(ports),
            SafetyEnabled = entry.Categories.Contains("Safety")
        };
    }

    /// <summary>
    /// Gets the next largest slot number for the current module based on the slot numbers of all other
    /// child modules with ths same parent module. 
    /// </summary>
    private Address NextSlot()
    {
        var children = Element.Parent?.Elements()
            .Where(m => m.Attribute(L5XName.ParentModule)?.Value == Name);

        var next = children?.Select(c => c.Descendants(L5XName.Port)
                .FirstOrDefault(p => p.Attribute(L5XName.Upstream)?.Value.Parse<bool>() is true &&
                                     byte.TryParse(p.Attribute(L5XName.Address)?.Value, out _))
                ?.Attribute(L5XName.Address)?.Value.Parse<byte>())
            .OrderByDescending(b => b)
            .FirstOrDefault();

        return next.HasValue ? Address.Slot(next.Value) : Address.Slot();
    }

    /// <summary>
    /// Sets the address (slot or IP) of the <see cref="Module"/> instance to the provided value.
    /// </summary>
    /// <param name="address">The <see cref="Address"/> to configure this module with.</param>
    /// <remarks>
    /// The address in this case can be either a slot number or IP address. This helper method will find the first
    /// matching upstream port and set the address property if found. If not found, this method will throw an exception.
    /// </remarks>
    private void SetAddress(Address address)
    {
        if (address is null)
            throw new ArgumentNullException(nameof(address));

        var port = Ports.Where(p => p.Upstream).FirstOrDefault(p => p.IsEthernet == address.IsIPv4);

        if (port is null)
            throw new InvalidOperationException("Failed to set address. No matching upstream port is configured.");

        port.Address = address;
    }

    #endregion
}

/// <summary>
/// Extensions methods for a single <see cref="Module"/> or collection of <see cref="Module"/> components.
/// </summary>
public static class ModuleExtensions
{
    /// <summary>
    /// Gets the <c>Local</c> module or module that represents the controller of the module collection.
    /// </summary>
    /// <param name="modules">A collection of modules.</param>
    /// <returns>A single <see cref="Module"/> which is named local if found; Otherwise, <c>null</c>.</returns>
    /// <remarks>This is a helper to concisely get the controller or root local module from the module collection.</remarks>
    public static Module? Local(this IEnumerable<Module> modules) => modules.SingleOrDefault(m => m.Name == "Local");
}