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
        ParentModPortId = 0;
        Inhibited = false;
        MajorFault = false;
        SafetyEnabled = false;
        Keying = ElectronicKeying.CompatibleModule;
        Ports = [];
    }

    /// <inheritdoc />
    // ReSharper disable once MemberCanBePrivate.Global - Deserialization constructor.
    public Module(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new <see cref="Module"/> initialized with the provided name and optional parameters.
    /// </summary>
    /// <param name="name">The name of the Module.</param>
    /// <param name="catalogNumber">The optional catalog number for the Module. Will default to empty string.</param>
    /// <param name="revision">The optional <see cref="Revision"/> number for the Module. Will default to 1.0.</param>
    public Module(string name, string? catalogNumber = null, Revision? revision = null) : this()
    {
        Element.SetAttributeValue(L5XName.Name, name);
        CatalogNumber = catalogNumber ?? string.Empty;
        Revision = revision ?? new Revision();
    }

    /// <inheritdoc />
    /// <remarks>
    /// Module components don't all have a name attribute (e.g., VFD peripheral modules). For this reason,
    /// the name property is overriden to not be a required field for this component type. If the name is not found,
    /// this property returns an empty string.
    /// </remarks>
    public override string Name
    {
        get => GetValue<string>() ?? string.Empty;
        set => SetValue(value);
    }

    /// <summary>
    /// Specify the catalog number that uniquely identifies the module. This is a rockwell defined convention
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
            return major.HasValue && minor.HasValue ? new Revision(major.Value, minor.Value) : null;
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
    /// Ports define how a module's peripherals are connected to other modules, forming the network or tree of
    /// devices used to communicate with a controller and field equipment. Ports must have a unique id, a type,
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

    /// <summary>
    /// Gets the slot number of the current module if one exists. If the module does not have a slot, it returns null.
    /// </summary>
    /// <value>An <see cref="byte"/> representing the slot number of the module.</value>
    /// <remarks>
    /// This property is not directly part of the L5X structure but is a helper to make accessing the slot number simple.
    /// This property looks for an upstream <see cref="Port"/> with a valid slot byte number.
    /// </remarks>
    public byte? Slot
    {
        get => Ports.FirstOrDefault(p => p is { IsEthernet: false })?.Address?.ToSlot();
        set => SetAddress(value?.ToString());
    }

    /// <summary>
    /// Gets or sets the IP address of this module if an ethernet type port exists.
    /// If the module does not have an ethernet type port, then it returns null or will throw an exception.
    /// </summary>
    /// <value>An <see cref="IPAddress"/> representing the IP of the module.</value>
    /// <remarks>
    /// This property is not directly part of the L5X structure but is a helper to make accessing the IP simple.
    /// This property looks for an Ethernet type <see cref="Port"/> with a valid IPv4 address.
    /// </remarks>
    public IPAddress? IP
    {
        get => Ports.FirstOrDefault(p => p is { IsEthernet: true })?.Address?.ToIPAddress();
        set => SetAddress(value?.ToString(), true);
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
    public Module? Parent => GetParent();

    /// <summary>
    /// Gets the child modules of this module component defined in the current L5X content. 
    /// </summary>
    /// <returns>
    /// A <see cref="IEnumerable{T}"/> of <see cref="Module"/> components that have the <c>ParentModule</c> property
    /// configured as the name of this module.
    /// </returns>
    /// <remarks>
    /// The L5X structure serializes modules in a flat list with each element having properties
    /// (ParentModule, ParentModPortId) defining the parent/child IO tree relationship. This requires the module
    /// is attached to the L5X content. In-memory created modules will inherently return an empty collection,
    /// as there is no L5X structure to introspect.
    /// </remarks>
    public IEnumerable<Module> Modules
    {
        get
        {
            return Element.Parent?.Elements()
                .Where(m => m.Attribute(L5XName.ParentModule)?.Value == Name)
                .Select(e => new Module(e)) ?? [];
        }
    }

    /// <summary>
    /// Returns a collection of all non-null <see cref="Tag"/> objects for the current Module, including all
    /// config, input, and output tags.
    /// </summary>
    /// <value>An <see cref="IEnumerable{T}"/> containing the base tags for the Module.</value>
    /// <remarks>
    /// Since module tags are nested within different layers of complex types, it can be challenging to just
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
    /// Connects the provided child module to the current module and tries to add it to the underlying L5X if possible.
    /// </summary>
    /// <param name="child">The child module to connect.</param>
    /// <exception cref="ArgumentNullException">Thrown when the child module is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when this module contains no downstream port with a type that
    /// matches a port of the provided child -OR- when multiple matching downstream ports are found.</exception>
    /// <remarks>
    /// The intention of this method is to help with setting the parent and port properties of a child module
    /// so that it will be imported correctly into Studio 5K. To do this, it will try to find a downstream port of the current
    /// module that has a type matching one of the child ports. If the match is successful, then the child module
    /// is configured using the current module as the parent. This method will also attempt to add the serialized child
    /// to the underlying L5X content if possible.
    /// </remarks>
    public void Connect(Module child)
    {
        if (child is null)
            throw new ArgumentNullException(nameof(child));

        ConnectModule(child);
        TryAddModule(child);
    }

    /// <summary>
    /// Creates and connects a child module to the current module using the specified catalog number and configuration action.
    /// Also tries to add the child to the underlying L5X if possible
    /// </summary>
    /// <param name="catalogNumber">The catalog number of the child module to be created and connected.</param>
    /// <param name="config">An action to configure the child module after it is connected.</param>
    /// <returns>A new configured <see cref="Module"/> instnace "connected" to this parent module.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the child module is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when this module contains no downstream port with a type that
    /// matches a port of the provided child -OR- when multiple matching downstream ports are found.</exception>
    /// <remarks>
    /// The intention of this method is to help with both generating and configuring the parent and port properties of
    /// a child module so that it will be imported correctly into Studio 5K. This method uses the internal
    /// <see cref="ModuleCatalog"/> to generate the child instance. Then it will try to find a downstream port of the current
    /// module that has a type matching one of the child ports. If the match is successful, then the child module
    /// is configured using the current module as the parent. This method will also attempt to add the serialized child
    /// to the underlying L5X content if possible.
    /// </remarks>
    public Module Connect(string catalogNumber, Action<Module> config)
    {
        if (config is null) throw new ArgumentNullException(nameof(config));
        var child = CreateFromCatalog(string.Empty, catalogNumber);
        ConnectModule(child);
        config.Invoke(child);
        TryAddModule(child);
        return child;
    }

    /// <summary>
    /// Creates and connects a child module to the current module using the provided L5X template file, catalog number,
    /// and configuration action. Also tries to add the child to the underlying L5X if possible
    /// </summary>
    /// <param name="filePath">The path to the L5X which contains the module to create.</param>
    /// <param name="catalogNumber">The catalog number of the child module to be created and connected.</param>
    /// <param name="config">An action to configure the child module after it is connected.</param>
    /// <returns>A new configured <see cref="Module"/> instnace "connected" to this parent module.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="filePath"/>,
    /// <paramref name="catalogNumber"/>, or <paramref name="config"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when this module contains no downstream port with a type that
    /// matches a port of the provided child -OR- when multiple matching downstream ports are found.</exception>
    /// <remarks>
    /// The intention of this method is to help with both generating and configuring the parent and port properties of
    /// a child module so that it will be imported correctly into Studio 5K. This method uses the provided L5X
    /// to generate the child instance. Then it will try to find a downstream port of the current
    /// module that has a type matching one of the child ports. If the match is successful, then the child module
    /// is configured using the current module as the parent. This method will also attempt to add the serialized child
    /// to the underlying L5X content if possible.
    /// </remarks>
    public Module Connect(string filePath, string catalogNumber, Action<Module> config)
    {
        if (config is null) throw new ArgumentNullException(nameof(config));
        var child = CreateFromFile(filePath, catalogNumber);
        ConnectModule(child);
        config.Invoke(child);
        TryAddModule(child);
        return child;
    }

    /// <summary>
    /// Creates a new <see cref="Module"/> instance using the specified name and catalog number.
    /// </summary>
    /// <param name="name">The name of the module to create.</param>
    /// <param name="catalogNumber">The catalog number of the module to create.</param>
    /// <returns>A new configured <see cref="Module"/> instance.</returns>
    /// <remarks>
    /// This factory method uses the <see cref="ModuleCatalog"/> to find the specified catalog number.
    /// If the machine does not have Studio 5K installed or the specified module was not found, then this factory method
    /// will throw an exception. If found, then the module is generated using the catalog data and the provided module name.
    /// This method is for generating modules without needing to know the exact product type, code, vendor, revision,
    /// and port configurations. This will not generate any connection or tag data for the module,
    /// but in most cases that should be built when imported.
    /// </remarks>
    public static Module Create(string name, string catalogNumber)
    {
        var module = CreateFromCatalog(name, catalogNumber);
        return module;
    }

    /// <summary>
    /// Creates a new <see cref="Module"/> instance using the specified catalog number and configuration delegate.
    /// </summary>
    /// <param name="catalogNumber">The catalog number of the module to create.</param>
    /// <param name="config">An action to configure the properties of the created module.</param>
    /// <returns>A new configured <see cref="Module"/> instance.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="catalogNumber"/> or <paramref name="config"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown if no module with the specified catalog number is found in the module catalog.</exception>
    /// <remarks>
    /// This factory method uses the <see cref="ModuleCatalog"/> to find the specified catalog number.
    /// If the machine does not have Studio 5K installed or the specified module was not found, then this factory method
    /// will throw an exception. If found, then the module is generated using the catalog data and the provided config
    /// delegate. This method is for generating and configuring modules without needing to know the exact
    /// product type, code, vendor, revision, and port configurations. This will not generate any connection or
    /// tag data for the module, but in most cases that should be built when imported.
    /// </remarks>
    public static Module Create(string catalogNumber, Action<Module> config)
    {
        if (config is null) throw new ArgumentNullException(nameof(config));

        var module = CreateFromCatalog(string.Empty, catalogNumber);
        config.Invoke(module);
        return module;
    }

    /// <summary>
    /// Creates and configures a <see cref="Module"/> instance by cloning an existing module from the given file
    /// and applying the specified configuration.
    /// </summary>
    /// <param name="filePath">The path to the L5X file containing the module to be cloned.</param>
    /// <param name="catalogNumber">The catalog number of the module to be cloned.</param>
    /// <param name="config">An action delegate that applies configurations to the cloned module.</param>
    /// <returns>A new <see cref="Module"/> instance configured based on the provided action delegate.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="filePath"/>, <paramref name="catalogNumber"/>, or <paramref name="config"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown if no module with the specified catalog number is found in the provided file.</exception>
    /// <remarks>
    /// This factory method exists to assist with creating new module instances from existing L5X content.
    /// There are some module types that we can't generate all the required XML content for using the
    /// <see cref="ModuleCatalog"/>), specifically safety enabled modules that have the 'SafetyScript' element.
    /// This method encapsulates a common workflow of copying modules from existing known content. 
    /// </remarks>
    public static Module Create(string filePath, string catalogNumber, Action<Module> config)
    {
        if (config is null) throw new ArgumentNullException(nameof(config));

        var module = CreateFromFile(filePath, catalogNumber);
        config.Invoke(module);
        return module;
    }

    /// <summary>
    /// Creates a new <see cref="Module"/> instance that represents the local controller module for a project.
    /// </summary>
    /// <param name="processor">The processor catalog number for the module.</param>
    /// <param name="revision">The software revision of the controller.</param>
    /// <returns>A new <see cref="Module"/> representing a default local controller module instance.</returns>
    public static Module Local(string processor, Revision? revision = null)
    {
        var module = CreateFromCatalog(nameof(Local), processor);

        //These are the default self-referential properties for the local controller module instance.
        module.ParentModule = nameof(Local);
        module.ParentModPortId = 1;

        if (revision is not null)
        {
            module.Revision = revision;
        }

        return module;
    }

    #region Internals

    /// <summary>
    /// Gets the parent module to this module using the current Element to traverse the XML structure. 
    /// </summary>
    private Module? GetParent()
    {
        return Element.Parent?.Elements(L5XName.Module)
            .FirstOrDefault(m => m.LogixName() == ParentModule)
            ?.Deserialize<Module>();
    }

    /// <summary>
    /// Connects a child <see cref="Module"/> to its parent by matching ports.
    /// </summary>
    private void ConnectModule(Module child)
    {
        var ports = Ports.Where(p => !p.Upstream && child.Ports.Any(c => c.Type == p.Type)).ToArray();

        switch (ports.Length)
        {
            case 0:
                throw new InvalidOperationException(
                    $"No matching downstream port type is available for '{Name}'.");
            case > 1:
                throw new InvalidOperationException(
                    $"Multiple matching downstream ports types are available for on '{Name}'.");
            default:
                child.ConfigureParent(this, ports[0]);
                break;
        }
    }

    /// <summary>
    /// Configures the properties of this module to form a connection to the provided parent module and port.
    /// </summary>
    /// <param name="parent">The parent module for which to form a connection.</param>
    /// <param name="port">The parent port for which to form a connection.</param>
    private void ConfigureParent(Module parent, Port port)
    {
        ParentModule = parent.Name;
        ParentModPortId = port.Id;

        var match = Ports.FirstOrDefault(p => p.Type == port.Type);

        if (match is not null)
        {
            match.Address = match.IsEthernet ? match.Address : parent.NextSlot();
            match.Upstream = true;
            return;
        }

        Ports.Add(new Port
        {
            Id = Ports.Count + 1,
            Type = port.Type,
            Address = port.IsEthernet ? Address.NewIP() : parent.NextSlot(),
            Upstream = true
        });
    }

    /// <summary>
    /// Attempts to add the specified child module to the parent element in the hierarchy.
    /// If this module has no parent element, then we will return without error.
    /// </summary>
    private void TryAddModule(Module child)
    {
        Element.Parent?.Add(child.Serialize());
    }

    /// <summary>
    /// Creates a new <see cref="Module"/> instance from the built in <see cref="ModuleCatalog"/> service utility
    /// using the provided name and catalog number.
    /// </summary>
    private static Module CreateFromCatalog(string name, string catalogNumber)
    {
        if (name is null) throw new ArgumentNullException(nameof(name));
        if (catalogNumber is null) throw new ArgumentNullException(nameof(catalogNumber));

        var catalog = new ModuleCatalog();
        var entry = catalog.Lookup(catalogNumber);

        var ports = entry.Ports.Select(p => new Port
        {
            Id = p.Number,
            Type = p.Type,
            Address = p.Type == "Ethernet" ? Address.NewIP() : Address.NewSlot(),
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

        return module;
    }

    /// <summary>
    /// Creates a new <see cref="Module"/> instance from a provided L5X file and catalog number.
    /// </summary>
    private static Module CreateFromFile(string fileName, string catalogNumber)
    {
        if (fileName is null) throw new ArgumentNullException(nameof(fileName));
        if (catalogNumber is null) throw new ArgumentNullException(nameof(catalogNumber));

        var file = L5X.Load(fileName);
        var template = file.Query<Module>().FirstOrDefault(m => m.CatalogNumber == catalogNumber);

        if (template is null)
        {
            throw new InvalidOperationException(
                $"No module with CatalogNumber '{catalogNumber}' found in '{fileName}'.");
        }

        return template.Clone();
    }

    /// <summary>
    /// Gets the next largest slot number for the current module based on the slot numbers of all other
    /// child modules with this same parent module. 
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

        return next.HasValue ? Address.NewSlot(next.Value) : Address.NewSlot();
    }

    /// <summary>
    /// Sets the address (Slot or IP) of the module given the provided value and flag indicating whether this is an
    /// ethernet port or not.
    /// </summary>
    private void SetAddress(string? value, bool isEthernet = false)
    {
        var address = Address.TryParse(value);

        var port = Ports.FirstOrDefault(p => Equals(p.IsEthernet, isEthernet));

        if (port is null)
            throw new InvalidOperationException($"No supporting port found for address: '{value}'");

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