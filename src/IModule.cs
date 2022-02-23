using System.Collections.Generic;
using System.Net;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// Represents a Logix <b>Module</b> component.
    /// </summary>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer> 
    public interface IModule : ILogixComponent
    {
        /// <summary>
        /// Gets the <see cref="CatalogNumber"/> value that uniquely identifies the <see cref="IModule"/>.
        /// </summary>
        /// <remarks>
        /// All modules have a unique catalog number. This value is used by a <see cref="ICatalogService"/> to lookup
        /// modules and materialize a corresponding <see cref="ModuleDefinition"/>. This value will be validated by
        /// Logix upon import of the L5X. 
        /// </remarks>
        CatalogNumber CatalogNumber { get; }

        /// <summary>
        /// Gets the <see cref="Vendor"/> entity of the <see cref="IModule"/>.
        /// </summary>
        Vendor Vendor { get; }

        /// <summary>
        /// Gets the <see cref="ProductType"/> entity of the <see cref="IModule"/>.
        /// </summary>
        ProductType ProductType { get; }

        /// <summary>
        /// Gets the unique product code value of the module.
        /// </summary>
        /// <remarks>
        /// This is a unique value that identifies the module and is assigned by Logix. It can be determined from the
        /// L5X content or from obtaining a <see cref="ModuleDefinition"/> from the <see cref="ModuleCatalog"/> service.
        /// This property is validated by Logix upon import of the L5X. 
        /// </remarks>
        ushort ProductCode { get; }

        /// <summary>
        /// Gets the <see cref="Revision"/> number for the <see cref="IModule"/>.
        /// </summary>
        Revision Revision { get; }

        /// <summary>
        /// Gets a value indicating whether the module is inhibited or disabled.
        /// </summary>
        bool Inhibited { get; }

        /// <summary>
        /// Gets a value indicating whether the module will cause a major fault when faulted.
        /// </summary>
        bool MajorFault { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="IModule"/> has safety features enabled.
        /// </summary>
        bool SafetyEnabled { get; }

        /// <summary>
        /// Gets the <see cref="KeyingState"/> value for the <see cref="IModule"/>.
        /// </summary>
        KeyingState State { get; }

        /// <summary>
        /// Gets the value representing the slot number of the <see cref="IModule"/>.
        /// </summary>
        /// <remarks>
        /// Not all modules will have a slot number, which is why this is a nullable int.
        /// The slot number is effectively the index of the Module on the <see cref="Bus"/>.
        /// </remarks>
        int? Slot { get; }

        /// <summary>
        /// Gets the value of the configured <see cref="IPAddress"/> for the <see cref="Module"/>.
        /// </summary>
        /// <remarks>
        /// Only Ethernet based modules will have an IP Address. If the module does not have 'Ethernet' type port,
        /// then this property will be null.
        /// </remarks>
        IPAddress? IP { get; }

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
        string ParentModule { get; }

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
        int ParentPortId { get; }

        /// <summary>
        /// Gets the collection of <see cref="Port"/> for the <see cref="Module"/>.
        /// </summary>
        /// <remarks>
        /// Each module will have at least one port to define how it is connected within the network. Some modules may
        /// have multiple ports, in which case the additional ports usually define the connection to child modules downstream
        /// of the current device. Use this property when adding new modules in a hierarchical fashion.
        /// </remarks>
        PortCollection Ports { get; }

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
        IReadOnlyCollection<Connection> Connections { get; }

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
        ModuleTags Tags { get; }
    }
}