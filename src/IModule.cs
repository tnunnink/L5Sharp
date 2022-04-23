using System.Collections.Generic;
using System.Net;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// Represents a Logix <b>Module</b> component. A module is the basis for defining IO connection to field devices
    /// and other networking equipment.
    /// </summary>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer> 
    public interface IModule : ILogixComponent
    {
        /// <summary>
        /// Gets the <see cref="CatalogNumber"/> value that uniquely identifies the module.
        /// </summary>
        /// <remarks>
        /// All modules have a unique catalog number. This value is used by a <see cref="ICatalogService"/> to lookup
        /// modules and materialize a corresponding <see cref="ModuleDefinition"/>.
        /// This value will be validated by Logix upon import of the L5X.
        /// </remarks>
        CatalogNumber CatalogNumber { get; }

        /// <summary>
        /// Gets the <see cref="Vendor"/> entity of the module.
        /// </summary>
        /// <remarks>
        /// All modules have a vendor representing the manufacturer of the module.
        /// This value can be retrieved as part of the <see cref="ModuleDefinition"/> object obtained using a
        /// <see cref="ICatalogService"/> for catalog lookup.
        /// This value will be validated by Logix upon import of the L5X. 
        /// </remarks>
        Vendor Vendor { get; }

        /// <summary>
        /// Gets the <see cref="ProductType"/> entity of the module.
        /// </summary>
        /// <remarks>
        /// All modules have a product type representing the product category of the module.
        /// This value can be retrieved as part of the <see cref="ModuleDefinition"/> object obtained using a
        /// <see cref="ICatalogService"/> for catalog lookup.
        /// This value will be validated by Logix upon import of the L5X. 
        /// </remarks>
        ProductType ProductType { get; }

        /// <summary>
        /// Gets the unique product code value of the module.
        /// </summary>
        /// <remarks>
        /// This is a unique value that identifies the module and is assigned by Logix.
        /// This value can be retrieved as part of the <see cref="ModuleDefinition"/> object obtained using a
        /// <see cref="ICatalogService"/> for catalog lookup.
        /// This property is validated by Logix upon import of the L5X. 
        /// </remarks>
        ushort ProductCode { get; }

        /// <summary>
        /// Gets the <see cref="Revision"/> number for the module.
        /// </summary>
        /// <remarks>
        /// All modules must have a specified revision number.
        /// This value can be retrieved as part of the <see cref="ModuleDefinition"/> object obtained using a
        /// <see cref="ICatalogService"/> for catalog lookup.
        /// This property is validated by Logix upon import of the L5X. 
        /// </remarks>
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
        /// Gets a value indicating whether the module has safety features enabled.
        /// </summary>
        bool SafetyEnabled { get; }

        /// <summary>
        /// Gets the <see cref="ElectronicKeying"/> value for the module.
        /// </summary>
        ElectronicKeying Keying { get; }

        /// <summary>
        /// Gets the value representing the slot number of the module.
        /// </summary>
        /// <remarks>
        /// Only non-Ethernet type ports will have a byte number representing a slot.
        /// For modules without a slot number, this value will default to zero.
        /// </remarks>
        int Slot { get; }

        /// <summary>
        /// Gets the configured <see cref="IPAddress"/> for the module.
        /// </summary>
        /// <remarks>
        /// Only Ethernet based modules will have an IP Address. If the module does not have 'Ethernet' type port,
        /// then this property will default to <see cref="IPAddress.Any"/>.
        /// </remarks>
        IPAddress IP { get; }

        /// <summary>
        /// Gets the name of the parent module that this module is connected to upstream.
        /// </summary>
        /// <remarks>
        /// The Logix hierarchy relies on knowing the parent module relation for each module in the context.
        /// If this property is empty or undetermined, the child will be considered orphaned. An orphaned module may
        /// still be imported into a Logix project. If this property is set to a valid upstream module, then it will be
        /// validated by Logix upon import, and may fail if the parent state does not meet certain requirements.
        /// </remarks>
        string ParentModule { get; }

        /// <summary>
        /// Gets the id of the parent module port that this module is connected to upstream.
        /// </summary>
        /// <remarks>
        /// The Logix hierarchy relies on knowing the parent port relation for each module in the context.
        /// If this property is '0', the child will be considered orphaned. An orphaned module may
        /// still be imported into a Logix project. If this property is set to a valid upstream port, then it will be
        /// validated by Logix upon import, and may fail if the parent state does not meet certain requirements.
        /// </remarks>
        int ParentPortId { get; }

        /// <summary>
        /// Gets the collection of <see cref="Port"/> for the <see cref="IModule"/>.
        /// </summary>
        /// <remarks>
        /// Each module will have at least one port to define how it is connected within the network. Some modules may
        /// have multiple ports, in which case the additional ports usually define the connection to child modules downstream
        /// of the current device.
        /// </remarks>
        PortCollection Ports { get; }

        /// <summary>
        /// Gets the <see cref="ITag{TDataType}"/> that represents the configuration tag data for the module.
        /// </summary>
        /// <remarks>
        /// This property will be null for modules without config tag. Typically, this tag will contain all the additional
        /// module specific configuration fot scaling, alarms, calibration, etc.
        /// </remarks>
        ITag<IDataType>? Config { get; }
        
        /// <summary>
        /// Gets the <see cref="ITag{TDataType}"/> that represents the input data tag for the module.
        /// </summary>
        ITag<IDataType>? Input { get; }
        
        /// <summary>
        /// Gets the <see cref="ITag{TDataType}"/> that represents the output data tag for the module.
        /// </summary>
        ITag<IDataType>? Output { get; }

        /// <summary>
        /// Gets the collection of <see cref="Core.Connection"/> objects containing configuration for the module's
        /// connections for field devices.
        /// </summary>
        /// <remarks>
        /// Most modules appear to have a single connection object containing configuration parameters and input/output tags.
        /// Note that some modules many not contain a connection object. Also, these objects are only initialized when
        /// deserializing L5X content. There is no known way of obtaining connection information for a given module other
        /// than from the L5X. 
        /// </remarks>
        IReadOnlyCollection<Connection> Connections { get; }
        
        /// <summary>
        /// Gets the <see cref="Core.Bus"/> that represents the module backplane, on which new modules can be
        /// added or created.
        /// </summary>
        /// <remarks>
        /// This property will return null for modules with no backplane bus (i.e. modules with no downstream
        /// non-Ethernet type port).
        /// </remarks>
        Bus? Backplane { get; }
        
        /// <summary>
        /// Gets the <see cref="Core.Bus"/> that represents the module Ethernet network, on which new modules can be
        /// added or created.
        /// </summary>
        /// <remarks>
        /// This property will return null for modules with no Ethernet bus (i.e. modules with not downstream Ethernet
        /// type port).
        /// </remarks>
        Bus? Ethernet { get; }
        
        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> containing all child modules from all downstream ports
        /// of the current module.
        /// </summary>
        IEnumerable<IModule> Modules { get; }

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> containing all tags (Config/Input/Output) that are members of the module.
        /// </summary>
        /// <remarks>
        /// This property simply aggregates the config, input, and output tags for the module for easy of access.
        /// </remarks>
        IEnumerable<ITag<IDataType>> Tags { get; }

        /// <summary>
        /// Gets all <see cref="Core.Bus"/> objects for the module.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> containing all <see cref="Core.Bus"/> objects from the current
        /// module.</returns>
        IEnumerable<Bus> Buses();

        /// <summary>
        /// Gets a <see cref="Core.Bus"/> from the module with the specified port id.
        /// </summary>
        /// <param name="portId"></param>
        /// <returns></returns>
        Bus Bus(int portId);
    }
}