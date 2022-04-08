using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using L5Sharp.Builders;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public class Module : IModule
    {
        /// <summary>
        /// Serialization constructor
        /// </summary>
        internal Module(string name, string? description, CatalogNumber catalogNumber,
            Vendor vendor, ProductType productType, ushort productCode, Revision revision,
            string? parentModule, int parentPortId, KeyingState? state,
            bool inhibited, bool majorFault, bool safetyEnabled, ITag<IDataType> config,
            IList<Port> ports, IList<Connection> connections, ICollection<IModule> modules)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
            CatalogNumber = catalogNumber;
            Vendor = vendor;
            ProductType = productType;
            ProductCode = productCode;
            Revision = revision;
            ParentModule = parentModule ?? string.Empty;
            ParentPortId = parentPortId;
            State = state ?? KeyingState.CompatibleModule;
            Inhibited = inhibited;
            MajorFault = majorFault;
            SafetyEnabled = safetyEnabled;
            Ports = new PortCollection(ports);
            Connections = new ReadOnlyCollection<Connection>(connections);
            Tags = new ModuleTagCollection(config, Connections);
            Bus = new BusCollection(Ports, this, modules);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">The name of the module. Must adhere to logix naming constraints.</param>
        /// <param name="catalogNumber">The catalog number of the module.</param>
        /// <param name="vendor">The <see cref="Vendor"/> of the module.</param>
        /// <param name="productType">The <see cref="Core.ProductType"/></param>
        /// <param name="productCode"></param>
        /// <param name="ports"></param>
        /// <param name="revision"></param>
        /// <param name="parentModule"></param>
        /// <param name="parentPortId"></param>
        /// <param name="state"></param>
        /// <param name="inhibited"></param>
        /// <param name="majorFault"></param>
        /// <param name="safetyEnabled"></param>
        /// <param name="description"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Module(ComponentName name, CatalogNumber catalogNumber, Vendor vendor,
            ProductType productType, ushort productCode, IList<Port> ports, Revision revision,
            string? parentModule = null, int parentPortId = default, KeyingState? state = null,
            bool inhibited = default, bool majorFault = default, bool safetyEnabled = default,
            string? description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
            CatalogNumber = catalogNumber ?? throw new ArgumentNullException(nameof(catalogNumber));
            Vendor = vendor;
            ProductType = productType;
            ProductCode = productCode;
            Revision = revision;
            ParentModule = parentModule ?? string.Empty;
            ParentPortId = parentPortId;
            State = state ?? KeyingState.CompatibleModule;
            Inhibited = inhibited;
            MajorFault = majorFault;
            SafetyEnabled = safetyEnabled;
            Ports = new PortCollection(ports);
            Connections = new List<Connection>().AsReadOnly();
            Tags = new ModuleTagCollection();
            Bus = new BusCollection(Ports, this);
        }

        /// <summary>
        /// Creates a new <see cref="Module"/> object with the provided name, module definition, and parent parameters.
        /// </summary>
        /// <param name="name">The name of the module.</param>
        /// <param name="definition">The <see cref="ModuleDefinition"/> containing the information on the
        /// module to create. Users may provide a custom definition to create the module instance,
        /// or use the <see cref="ModuleCatalog"/> to obtain one from the service provider. Note that if the definition has
        /// invalid parameters, importing the module may fail as Logix will validate the parameters.</param>
        /// <param name="parentModule">The name of the module that is the parent of the module to create.</param>
        /// <param name="parentPortId">The port id of the module that is the parent of the current module.</param>
        /// <param name="description">The description of the module.</param>
        /// <exception cref="ArgumentNullException">definition is null.</exception>
        /// <remarks>
        /// This constructor provides a flexible way to create modules in circumstances where the module definition is not
        /// known by the default <see cref="ModuleCatalog"/>, or the user wants more control over what is being instantiated.
        /// Note that this constructor will not add the module to parent bus, since there is no strong reference to it,
        /// and therefore this constructor will create an "orphaned" module. To create modules in a hierarchical fashion,
        /// use the <see cref="Bus"/> property containing the API for adding new child modules.
        /// </remarks>
        public Module(ComponentName name, ModuleDefinition definition,
            string? parentModule = null, int parentPortId = default, string? description = null)
        {
            if (definition is null)
                throw new ArgumentNullException(nameof(definition));

            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
            CatalogNumber = definition.CatalogNumber;
            Vendor = definition.Vendor;
            ProductType = definition.ProductType;
            ProductCode = definition.ProductCode;
            Revision = definition.Revisions.Max();
            ParentModule = parentModule ?? string.Empty;
            ParentPortId = parentPortId;
            State = KeyingState.CompatibleModule;
            Ports = new PortCollection(definition.Ports.ToList());
            Connections = new List<Connection>().AsReadOnly();
            Tags = new ModuleTagCollection();
            Bus = new BusCollection(Ports, this);
        }

        /// <summary>
        /// Creates a new <see cref="Module"/> object with the provided name and catalog number.
        /// </summary>
        /// <param name="name">The name of the Module.</param>
        /// <param name="catalogNumber">The catalog number of the Module to create.</param>
        /// <param name="slot">The slot number for which the Module should be assigned.
        /// Will default to '0' and attempt to apply this address to the non-Ethernet type port,
        /// if one exists for the specified catalog number.</param>
        /// <param name="ipAddress">The IP address of the module if applicable. Will only be set on modules that have
        /// a Ethernet type port available. If not provided will default to <see cref="IPAddress.Any"/>.</param>
        /// <param name="description">The description of the Module. If not provided, will default to empty string.</param>
        /// <param name="catalogService">
        /// The service provider that will be used to lookup the definition for the specified catalog number.
        /// By default, this constructor will use the <see cref="ModuleCatalog"/> implementation, which relies on the local
        /// Rockwell installed catalog service file. Therefore, this code may fail when called from a machine without the
        /// necessary software installed. You may optionally provide a custom implementation of the <see cref="ICatalogService"/>
        /// to allow this constructor to build a valid Module instance.
        /// </param>
        /// <param name="parentModule">The name of the parent module. If not provided, will default to empty.</param>
        /// <param name="parentPortId">The id of the parent module port to which the new module is connected.
        /// If not provided, will default to 0.</param>
        /// <exception cref="ArgumentNullException"><c>name</c> or <c>catalogNumber</c> is null.</exception>
        /// <exception cref="ModuleNotFoundException">A module with <c></c> could not be found.</exception>
        /// <remarks>
        /// This constructor provides a simple way to create modules by specifying, at minimum, a name and catalog number.
        /// If the <c>catalogService</c> parameter is not overriden with a custom implementation, then the default <see cref="ModuleCatalog"/>
        /// will be used to lookup the the module definition for the provided catalog number. You can also optionally provide
        /// a slot and IP address to let the constructor configure ports as necessary. You may also specify the parent module
        /// and port id in order to set this module's place in the module tree hierarchy. Note that this constructor does not
        /// add the module to the parent bus since there is not way to reference it. To add child modules in a hierarchical
        /// fashion, use the <see cref="Ports"/> local downstream port of the parent module.
        /// </remarks>
        public Module(ComponentName name, CatalogNumber catalogNumber, byte slot = default, IPAddress? ipAddress = null,
            string? description = null, string? parentModule = null, int parentPortId = default,
            ICatalogService? catalogService = null)
        {
            catalogService ??= new ModuleCatalog();
            var definition = catalogService.Lookup(catalogNumber).ConfigureBackplane(slot, ipAddress);

            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
            CatalogNumber = definition.CatalogNumber;
            Vendor = definition.Vendor;
            ProductType = definition.ProductType;
            ProductCode = definition.ProductCode;
            Revision = definition.Revisions.Max();
            ParentModule = parentModule ?? string.Empty;
            ParentPortId = parentPortId;
            State = KeyingState.CompatibleModule;
            Ports = new PortCollection(definition.Ports.ToList());
            Connections = new List<Connection>().AsReadOnly();
            Tags = new ModuleTagCollection();
            Bus = new BusCollection(Ports, this);
        }

        /// <summary>
        /// Creates a new <see cref="Module"/> object with the provided name and catalog number. This constructor is meant
        /// to be used for creating module's with an upstream 'Ethernet' port, or modules that are connected over a network
        /// as opposed to a chassis.
        /// </summary>
        /// <param name="name">The name of the module.</param>
        /// <param name="catalogNumber">The catalog number of the module to create.</param>
        /// <param name="ipAddress">The IP of the module's "Ethernet' port. If not provided</param>
        /// <param name="slot"></param>
        /// <param name="description">The description of the module. If not provided, will default to empty string.</param>
        /// <param name="catalogService">
        /// The service provider that will be used to lookup the definition for the specified catalog number.
        /// By default, this constructor will use the <see cref="ModuleCatalog"/> implementation, which relies on the local
        /// Rockwell installed catalog service file. Therefore, this code may fail when called from a machine without the
        /// necessary software installed. You may optionally provide a custom implementation of the <see cref="ICatalogService"/>
        /// to allow this constructor to build a valid Module instance.
        /// </param>
        /// <exception cref="ArgumentNullException"><c>name</c> or <c>catalogNumber</c> is null.</exception>
        /// <exception cref="ModuleNotFoundException">A module with <c></c> could not be found.</exception>
        /// <remarks>
        /// This constructor provides a simple way to create modules by specifying, at minimum, a name and catalog number,
        /// and IP address.
        /// If the <c>catalogService</c> parameter is not overriden with a custom implementation, then the default <see cref="ModuleCatalog"/>
        /// will be used to lookup the the module definition for the provided catalog number. Note that this constructor does not
        /// add the module to the parent bus since there is not reference to it. To add child modules in a hierarchical
        /// fashion in memory, use the <see cref="Ports"/> local downstream port of the parent module.
        /// </remarks>
        public Module(ComponentName name, CatalogNumber catalogNumber, IPAddress ipAddress, byte slot = default,
            string? description = null, ICatalogService? catalogService = null)
        {
            catalogService ??= new ModuleCatalog();
            var definition = catalogService.Lookup(catalogNumber).ConfigureEthernet(ipAddress, slot);

            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
            CatalogNumber = definition.CatalogNumber;
            Vendor = definition.Vendor;
            ProductType = definition.ProductType;
            ProductCode = definition.ProductCode;
            Revision = definition.Revisions.Max();
            ParentModule = string.Empty;
            State = KeyingState.CompatibleModule;
            Ports = new PortCollection(definition.Ports.ToList());
            Connections = new List<Connection>().AsReadOnly();
            Tags = new ModuleTagCollection();
            Bus = new BusCollection(Ports, this);
        }

        /// <summary>
        /// Creates a new <see cref="IModule"/> using the fluent <see cref="IModuleBuilder"/> API.
        /// </summary>
        /// <param name="name">The name of the module to create.</param>
        /// <returns>A <see cref="IModuleCatalogNumberStep"/> requiring the catalog number of the module to create.</returns>
        public static IModuleCatalogNumberStep Build(ComponentName name) => new ModuleBuilder(name);

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description { get; }

        /// <inheritdoc />
        public CatalogNumber CatalogNumber { get; }

        /// <inheritdoc />
        public Vendor Vendor { get; }

        /// <inheritdoc />
        public ProductType ProductType { get; }

        /// <inheritdoc />
        public ushort ProductCode { get; }

        /// <inheritdoc />
        public Revision Revision { get; }

        /// <inheritdoc />
        public bool Inhibited { get; }

        /// <inheritdoc />
        public bool MajorFault { get; }

        /// <inheritdoc />
        public bool SafetyEnabled { get; }

        /// <inheritdoc />
        public KeyingState State { get; }

        /// <inheritdoc />
        public int Slot => byte.TryParse(Ports.Backplane()?.Address, out var slot) ? slot : default;

        /// <inheritdoc />
        public IPAddress IP =>
            IPAddress.TryParse(Ports.Ethernet()?.Address ?? string.Empty, out var ip) ? ip : IPAddress.Any;

        /// <inheritdoc />
        public string ParentModule { get; }

        /// <inheritdoc />
        public int ParentPortId { get; }

        /// <inheritdoc />
        public PortCollection Ports { get; }

        /// <inheritdoc />
        public IReadOnlyCollection<Connection> Connections { get; }

        /// <inheritdoc />
        public ModuleTagCollection Tags { get; }

        /// <inheritdoc />
        public BusCollection Bus { get; }
    }
}