using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents a Module component of the L5X file.
    /// </summary>
    public class Module : ILogixComponent
    {
        /// <summary>
        /// Serialization constructor
        /// </summary>
        internal Module(string name, string? description, CatalogNumber catalogNumber,
            Vendor vendor, ProductType productType, ushort productCode, Revision revision,
            IEnumerable<PortDefinition> ports, string? parentModule, int parentPortId,
            KeyingState? state, bool inhibited, bool majorFault, bool safetyEnabled,
            ITag<IDataType>? config, IEnumerable<Connection> connections)
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
            Ports = new PortCollection(this, ports);
            Connections = new List<Connection>(connections).AsReadOnly();
            Tags = new ModuleTags(config, Connections);
        }

        /// <summary>
        /// Creates a new <see cref="Module"/> object with the provided name, module definition and parent parameters.
        /// </summary>
        /// <param name="name">The name of the Module.</param>
        /// <param name="definition">The <see cref="ModuleDefinition"/> containing the information on the
        /// Module to create. Users may provide a custom definition to create the Module instance,
        /// or use the <see cref="LogixCatalog"/> to obtain one from the service provider. Note that if the definition has
        /// invalid parameters, importing the module may fail as Logix will validate the parameters.</param>
        /// <param name="parentModule">The name of the module that is the parent of the Module to create.</param>
        /// <param name="parentPortId">The port id of the module that is the parent of the current module.</param>
        /// <param name="description">The description of the module.</param>
        /// <exception cref="ArgumentNullException">definition is null.</exception>
        /// <remarks>
        /// This constructor provides a flexible way to create modules in circumstances where the module definition is not
        /// known by the default <see cref="LogixCatalog"/>, or the user wants more control over what is being instantiated.
        /// Note that this constructor will not add the module to parent bus, since there is no strong reference to it,
        /// and therefore this constructor will create an orphaned module. To create modules in a hierarchical fashion,
        /// use the <see cref="Ports"/> local port to add new child modules.
        /// </remarks>
        public Module(ComponentName name, ModuleDefinition definition,
            string? parentModule = null, int parentPortId = default, string? description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;

            if (definition is null)
                throw new ArgumentNullException(nameof(definition));

            CatalogNumber = definition.CatalogNumber;
            Vendor = definition.Vendor;
            ProductType = definition.ProductType;
            ProductCode = definition.ProductCode;
            Revision = definition.Revisions.Max();
            ParentModule = parentModule ?? string.Empty;
            ParentPortId = parentPortId;
            State = KeyingState.CompatibleModule;
            Ports = new PortCollection(this, definition.Ports);
            Connections = new List<Connection>().AsReadOnly();
            Tags = new ModuleTags();
        }

        /// <summary>
        /// Creates a new <see cref="Module"/> object with the provided name and catalog number.
        /// </summary>
        /// <param name="name">The name of the Module.</param>
        /// <param name="catalogNumber">The catalog number of the Module to create.</param>
        /// <param name="slot">The slot number for which the Module should be assigned.
        /// Will default to '0' and attempt to apply this address to the non-Ethernet type port,
        /// if one exists for the specified catalog number.</param>
        /// <param name="ipAddress"></param>
        /// <param name="description">The description of the Module. If not provided, will default to empty string.</param>
        /// <param name="catalogService">
        /// The service provider that will be used to lookup the definition for the specified catalog number.
        /// By default, this constructor will use the <see cref="LogixCatalog"/> implementation, which relies on the local
        /// Rockwell installed catalog service file. Therefore, this code may fail when called from a machine without the
        /// necessary software installed. You may optionally provide a custom implementation of the <see cref="ICatalogService"/>
        /// to allow this constructor to build a valid Module instance.
        /// </param>
        /// <exception cref="ArgumentNullException"><c>name</c> or <c>catalogNumber</c> is null.</exception>
        /// <exception cref="ModuleNotFoundException">A module with <c></c> could not be found.</exception>
        /// <remarks>
        /// This constructor provides a simple way to create modules by specifying, at minimum, a name and catalog number.
        /// If the <c>catalogService</c> parameter is not overriden with a custom implementation, then the default <see cref="LogixCatalog"/>
        /// will be used to lookup the the module definition for the provided catalog number. You can also optionally provide
        /// a slot and IP address to let the constructor configure ports as necessary. You may also specify the parent module
        /// and port id in order to set this module's place in the module tree hierarchy. Note that this constructor does not
        /// add the module to the parent bus since there is not way to reference it. To add child modules in a hierarchical
        /// fashion, use the <see cref="Ports"/> local downstream port of the parent module.
        /// </remarks>
        public Module(ComponentName name, CatalogNumber catalogNumber, byte slot = default, IPAddress? ipAddress = null,
            string? description = null, ICatalogService? catalogService = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;

            catalogService ??= new LogixCatalog();

            var definition = catalogService.Lookup(catalogNumber);

            definition.ConfigurePorts(slot, ipAddress);

            CatalogNumber = definition.CatalogNumber;
            Vendor = definition.Vendor;
            ProductType = definition.ProductType;
            ProductCode = definition.ProductCode;
            Revision = definition.Revisions.Max();
            ParentModule = string.Empty;
            State = KeyingState.CompatibleModule;
            Ports = new PortCollection(this, definition.Ports);
            Connections = new List<Connection>().AsReadOnly();
            Tags = new ModuleTags();
        }

        /// <summary>
        /// Creates a new <see cref="Module"/> object with the provided name and catalog number. This constructor is meant
        /// to be used for creating module's with an upstream 'Ethernet' port, or modules that are connected over a netwrok
        /// as opposed to a chassis.
        /// </summary>
        /// <param name="name">The name of the module.</param>
        /// <param name="catalogNumber">The catalog number of the module to create.</param>
        /// <param name="ipAddress">The IP of the module's "Ethernet' port. If not provided</param>
        /// <param name="slot"></param>
        /// <param name="description">The description of the module. If not provided, will default to empty string.</param>
        /// <param name="catalogService">
        /// The service provider that will be used to lookup the definition for the specified catalog number.
        /// By default, this constructor will use the <see cref="LogixCatalog"/> implementation, which relies on the local
        /// Rockwell installed catalog service file. Therefore, this code may fail when called from a machine without the
        /// necessary software installed. You may optionally provide a custom implementation of the <see cref="ICatalogService"/>
        /// to allow this constructor to build a valid Module instance.
        /// </param>
        /// <exception cref="ArgumentNullException"><c>name</c> or <c>catalogNumber</c> is null.</exception>
        /// <exception cref="ModuleNotFoundException">A module with <c></c> could not be found.</exception>
        /// <remarks>
        /// This constructor provides a simple way to create modules by specifying, at minimum, a name and catalog number,
        /// and IP address.
        /// If the <c>catalogService</c> parameter is not overriden with a custom implementation, then the default <see cref="LogixCatalog"/>
        /// will be used to lookup the the module definition for the provided catalog number. Note that this constructor does not
        /// add the module to the parent bus since there is not reference to it. To add child modules in a hierarchical
        /// fashion in memory, use the <see cref="Ports"/> local downstream port of the parent module.
        /// </remarks>
        public Module(ComponentName name, CatalogNumber catalogNumber, IPAddress ipAddress, byte slot = default,
            string? description = null,
            ICatalogService? catalogService = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;

            catalogService ??= new LogixCatalog();

            var definition = catalogService.Lookup(catalogNumber);
            definition.ConfigurePorts(ipAddress, slot);

            CatalogNumber = definition.CatalogNumber;
            Vendor = definition.Vendor;
            ProductType = definition.ProductType;
            ProductCode = definition.ProductCode;
            Revision = definition.Revisions.Max();
            ParentModule = string.Empty;
            State = KeyingState.CompatibleModule;
            Ports = new PortCollection(this, definition.Ports);
            Connections = new List<Connection>().AsReadOnly();
            Tags = new ModuleTags();
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description { get; }

        /// <summary>
        /// Gets the <see cref="CatalogNumber"/> value that uniquely identifies the module.
        /// </summary>
        /// <remarks>
        /// All modules have a unique catalog number. This value is used by a <see cref="ICatalogService"/> to lookup
        /// modules and materialize a corresponding <see cref="ModuleDefinition"/>. This value will be validated by
        /// Logix upon import of the L5X. 
        /// </remarks>
        public CatalogNumber CatalogNumber { get; }

        /// <summary>
        /// Gets the <see cref="Vendor"/> entity of the module.
        /// </summary>
        public Vendor Vendor { get; }

        /// <summary>
        /// Gets the <see cref="ProductType"/> entity of the module.
        /// </summary>
        public ProductType ProductType { get; }

        /// <summary>
        /// Gets the unique product code value of the module.
        /// </summary>
        /// <remarks>
        /// This is a unique value that identifies the module and is assigned by Logix. It can be determined from the
        /// L5X content or from obtaining a <see cref="ModuleDefinition"/> from the <see cref="LogixCatalog"/> service.
        /// This property is validated by Logix upon import of the L5X. 
        /// </remarks>
        public ushort ProductCode { get; }

        /// <summary>
        /// Gets the <see cref="Revision"/> number for the module.
        /// </summary>
        public Revision Revision { get; }

        /// <summary>
        /// Gets a value indicating whether the module is inhibited or disabled.
        /// </summary>
        public bool Inhibited { get; }

        /// <summary>
        /// Gets a value indicating whether the module will cause a major fault when faulted.
        /// </summary>
        public bool MajorFault { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Module"/> has safety features enabled.
        /// </summary>
        public bool SafetyEnabled { get; }

        /// <summary>
        /// Gets the <see cref="KeyingState"/> value for the <see cref="Module"/>.
        /// </summary>
        public KeyingState State { get; }

        /// <summary>
        /// Gets the value representing the slot number of the module.
        /// </summary>
        /// <remarks>
        /// Not all modules will have a slot number, which is why this is a nullable int.
        /// The slot number is effectively the index of the Module on the <see cref="Bus"/>.
        /// </remarks>
        public int? Slot => Ports.GetSlotAddress();

        /// <summary>
        /// Gets the value of the configured <see cref="IPAddress"/> for the <see cref="Module"/>.
        /// </summary>
        /// <remarks>
        /// Only Ethernet based modules will have an IP Address. If the module does not have 'Ethernet' type port,
        /// then this property will be null.
        /// </remarks>
        public IPAddress? IP => Ports.GetIPAddress();

        /// <summary>
        /// Gets the name of the parent module that this module is connected to upstream.
        /// </summary>
        /// <remarks>
        /// The Logix hierarchy relies on knowing the parent module relation for each module in the context.
        /// Note that this member is set internally when adding child modules or deserializing modules from the L5X.
        /// If this property is empty or undetermined, the child will be considered orphaned. An orphaned module can
        /// still be imported into a Logix project. If this property is set to a valid upstream module, then it will be
        /// validated by Logix upon import, and may fail if the parent state does not meet certain requirements.
        /// </remarks>
        public string ParentModule { get; }

        /// <summary>
        /// Gets the id of the parent module port that this module is connected to upstream.
        /// </summary>
        /// <remarks>
        /// The Logix hierarchy relies on knowing the parent port relation for each module in the context.
        /// Note that this member is set internally when adding child modules or deserializing modules from the L5X.
        /// If this property is '0', the child will be considered orphaned. An orphaned module can
        /// still be imported into a Logix project. If this property is set to a valid upstream port, then it will be
        /// validated by Logix upon import, and may fail if the parent state does not meet certain requirements.
        /// </remarks>
        public int ParentPortId { get; }

        /// <summary>
        /// Gets the collection of <see cref="Port"/> for the <see cref="Module"/>.
        /// </summary>
        /// <remarks>
        /// Each module will have at least one port to define how it is connected within the network. Some modules may
        /// have multiple ports, in which case the additional ports usually define the connection to child modules downstream
        /// of the current device. Use this property when adding new modules in a hierarchical fashion.
        /// </remarks>
        public PortCollection Ports { get; }

        /// <summary>
        /// Gets the collection of <see cref="Core.Connection"/> objects containing configuration for the module's connections
        /// for field devices.
        /// </summary>
        /// <remarks>
        /// Most modules appear to have a single connection object containing configuration parameters and input/output tags.
        /// Note that some modules many not contain a connection object. Also, these objects are only initialized when
        /// deserializing L5X content. There is no known way of obtaining connection information for a given module other
        /// than from the L5X. 
        /// </remarks>
        public IReadOnlyCollection<Connection> Connections { get; }

        /// <summary>
        /// Gets a custom collection of <see cref="ITag{TDataType}"/> instances for the current <see cref="Module"/>.
        /// </summary>
        /// <remarks>
        /// This collection aggregates the config, input, and output tags for a given module. Each module may or may not
        /// have a given tag (config, input, output) based on the type of module it is.
        /// These tags represent the controller tags created for the module in Logix when adding a module to the IO tree.
        /// This collection is only populated when deserializing L5X content,
        /// and will be empty when creating a module in memory.
        /// This is because there is no known way (yet) to obtain the data types necessary for creating the tags each
        /// module would require.
        /// </remarks>
        public ModuleTags Tags { get; }
    }
}