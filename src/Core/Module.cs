using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public class Module : IModule
    {
        private readonly Dictionary<Port, Bus> _buses;

        /// <summary>
        /// Creates a new <see cref="Module"/> object with the provided module properties.
        /// </summary>
        /// <param name="name">The component name of the module.</param>
        /// <param name="catalogNumber">The catalog number that identifies the module.</param>
        /// <param name="vendor">The vendor who supplies/produces the module.
        /// Will default to <see cref="Core.ProductType.Unknown"/>.</param>
        /// <param name="productType">The product type of the module.</param>
        /// <param name="productCode">The unique id that identifies the module product.</param>
        /// <param name="revision">The revision number at which the module is configured.</param>
        /// <param name="ports">The set of ports available for connections on the module.</param>
        /// <param name="parentModule">The name of the module that is the parent to this module in the IO tree.</param>
        /// <param name="parentPortId">The ID number of the parent port for which the module is connected.</param>
        /// <param name="keying">The configured keying state of the module.</param>
        /// <param name="inhibited">The value indicating whether the module is inhibited.</param>
        /// <param name="majorFault">The value indicating whether the module will cause a major fault.</param>
        /// <param name="safetyEnabled">The value indicating whether the module has safety features enabled.</param>
        /// <param name="config">The configuration tag containing the configuration of the module.</param>
        /// <param name="connections">The set of of field connection objects for the module.</param>
        /// <param name="modules">The collection of child modules that are downstream of the module.
        /// These modules will be added to the appropriate port bus of the module.</param>
        /// <param name="description">The description of the module.</param>
        /// <exception cref="ArgumentNullException">name is null.</exception>
        public Module(ComponentName name, CatalogNumber catalogNumber,
            Vendor? vendor = null, ProductType? productType = null, ushort productCode = default,
            Revision? revision = null, IList<Port>? ports = null,
            string? parentModule = null, int parentPortId = default,
            ElectronicKeying? keying = null,
            bool inhibited = default, bool majorFault = default, bool safetyEnabled = default,
            ITag<IDataType>? config = null, IList<Connection>? connections = null,
            ICollection<IModule>? modules = null,
            string? description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            CatalogNumber = catalogNumber;
            Vendor = vendor ?? Vendor.Unknown;
            ProductType = productType ?? ProductType.Unknown;
            ProductCode = productCode;
            Revision = revision ?? new Revision();
            ParentModule = parentModule ?? string.Empty;
            ParentPortId = parentPortId;
            Keying = keying ?? ElectronicKeying.CompatibleModule;
            Inhibited = inhibited;
            MajorFault = majorFault;
            SafetyEnabled = safetyEnabled;
            Ports = ports is not null
                ? new PortCollection(ports)
                : new PortCollection(new List<Port> { new(1, "ICP") });
            Config = config;
            Connections = connections is not null
                ? new ReadOnlyCollection<Connection>(connections)
                : new List<Connection>().AsReadOnly();
            Description = description ?? string.Empty;
            _buses = InitializeBuses(modules);
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
        /// <param name="keying"></param>
        /// <param name="inhibited"></param>
        /// <param name="majorFault"></param>
        /// <param name="safetyEnabled"></param>
        /// <exception cref="ArgumentNullException">definition is null.</exception>
        /// <remarks>
        /// This constructor provides a flexible way to create modules in circumstances where the module definition is not
        /// known by the default <see cref="ModuleCatalog"/>, or the user wants more control over what is being instantiated.
        /// Note that this constructor will not add the module to parent bus, since there is no strong reference to it,
        /// and therefore this constructor will create an "orphaned" module. To create modules in a hierarchical fashion,
        /// use the <see cref="Bus"/> property containing the API for adding new child modules.
        /// </remarks>
        public Module(ComponentName name, ModuleDefinition definition,
            string? parentModule = null, int parentPortId = default, ElectronicKeying? keying = null,
            bool inhibited = default, bool majorFault = default, bool safetyEnabled = default,
            string? description = null)
        {
            if (definition is null)
                throw new ArgumentNullException(nameof(definition));

            Name = name ?? throw new ArgumentNullException(nameof(name));
            CatalogNumber = definition.CatalogNumber;
            Vendor = definition.Vendor;
            ProductType = definition.ProductType;
            ProductCode = definition.ProductCode;
            Revision = definition.Revision;
            ParentModule = parentModule ?? string.Empty;
            ParentPortId = parentPortId;
            Inhibited = inhibited;
            MajorFault = majorFault;
            SafetyEnabled = safetyEnabled;
            Keying = keying ?? ElectronicKeying.CompatibleModule;
            Ports = new PortCollection(definition.Ports.ToList());
            Connections = new List<Connection>().AsReadOnly();
            Description = description ?? definition.Description;
            _buses = InitializeBuses();
        }

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
        public ElectronicKeying Keying { get; }

        /// <inheritdoc />
        public int Slot => Ports.Backplane is not null && Ports.Backplane.Address.IsSlot
            ? Ports.Backplane.Address.ToSlot()
            : default;

        /// <inheritdoc />
        public IPAddress IP => Ports.Ethernet is not null && Ports.Ethernet.Address.IsIPv4
            ? Ports.Ethernet.Address.ToIPAddress()
            : IPAddress.None;

        /// <inheritdoc />
        public string ParentModule { get; }

        /// <inheritdoc />
        public int ParentPortId { get; }

        /// <inheritdoc />
        public PortCollection Ports { get; }

        /// <inheritdoc />
        public ITag<IDataType>? Config { get; }

        /// <inheritdoc />
        public ITag<IDataType>? Input => Connections.FirstOrDefault()?.Input;
        
        /// <inheritdoc />
        public ITag<IDataType>? Output => Connections.FirstOrDefault()?.Output;

        /// <inheritdoc />
        public IReadOnlyCollection<Connection> Connections { get; }

        /// <inheritdoc />
        public Bus? Backplane => Ports.Backplane is not null && !Ports.Backplane.Upstream
            ? _buses[Ports.Backplane]
            : default;

        /// <inheritdoc />
        public Bus? Ethernet => Ports.Ethernet is not null && !Ports.Ethernet.Upstream
            ? _buses[Ports.Ethernet]
            : default;

        /// <inheritdoc />
        public IEnumerable<IModule> Modules => _buses.SelectMany(b => b.Value);

        /// <inheritdoc />
        public IEnumerable<ITag<IDataType>> Tags
        {
            get
            {
                var tags = new List<ITag<IDataType>>();

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

        /// <inheritdoc />
        public IEnumerable<Bus> Buses() => _buses.Values;

        /// <inheritdoc />
        public Bus Bus(int portId)
        {
            var port = Ports[portId];

            if (port is null)
                throw new ArgumentNullException();

            return _buses[port];
        }

        private Dictionary<Port, Bus> InitializeBuses(ICollection<IModule>? children = null)
        {
            var buses = new Dictionary<Port, Bus>();

            foreach (var port in Ports.Downstream)
            {
                var bus = new Bus(this, port);

                if (children is not null)
                    bus.AddMany(children.Where(c => c.ParentPortId == port.Id));

                buses.Add(port, bus);
            }

            return buses;
        }
    }
}