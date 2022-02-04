using System.Net;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// Represents a Logix <see cref="IModule"/> component.
    /// </summary>
    public interface IModule : ILogixComponent
    {
        /// <summary>
        /// Gets the string catalog number of the <see cref="IModule"/>.
        /// </summary>
        CatalogNumber CatalogNumber { get; }

        /// <summary>
        /// Gets the vendor number of the <see cref="IModule"/>.
        /// </summary>
        Vendor Vendor { get; }
        
        /// <summary>
        /// Gets the product type number of the <see cref="IModule"/> 
        /// </summary>
        /// <value>
        /// An unsigned 16 bit integer that represents an ID for the <see cref="ProductType"/>.
        /// </value>
        ProductType ProductType { get; }
        
        /// <summary>
        /// Gets the product code of the <see cref="IModule"/>.
        /// </summary>
        ushort ProductCode { get; }
        
        /// <summary>
        /// Gets the <see cref="Revision"/> number for the <see cref="IModule"/>.
        /// </summary>
        Revision Revision { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="IModule"/> is inhibited or disabled.
        /// </summary>
        bool Inhibited { get; }
        
        /// <summary>
        /// Gets a value indicating whether the <see cref="IModule"/> will cause a major fault when faulted.
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
        /// Gets the value representing the slot of the <see cref="IModule"/>.
        /// </summary>
        /// <remarks>
        /// Not all modules will have a slot number, which is why this is a nullable int.
        /// The slot number is effectively the index of the Module on the <see cref="Bus"/>.
        /// This value is serialized from the L5X port element of the Module.
        /// </remarks>
        int? Slot { get; }
        
        /// <summary>
        /// Gets the value of the configured <see cref="IPAddress"/> for the <see cref="IModule"/>.
        /// </summary>
        /// <remarks>
        /// Only Ethernet based modules will have an IP Address. If the module does not have ethernet port then this
        /// property will be null.
        /// </remarks>
        IPAddress? IP { get; }

        /// <summary>
        /// Gets the parent <see cref="Port"/> of the upstream Module that the current <see cref="IModule"/> is connected.
        /// </summary>
        /// <remarks>
        /// The L5X hierarchy relies on knowing the parent relation for each Module in the context. This implementation
        /// relies on getting a strong reference to the upstream Port/Module to help maintain the state of the hierarchy.
        /// This allows child modules can be validated and configured as they are added or created.
        /// Note that this member is set internally when adding child modules or deserializing modules from the L5X.
        /// If a Module is orphaned or the parent is undetermined, this property will be null.
        /// </remarks>
        Port? ParentPort { get; }
        
        /// <summary>
        /// Gets the collection of <see cref="Ports"/> for the <see cref="IModule"/>.
        /// </summary>
        /// <remarks>
        /// Each Module will have at least one port to define how it is connected withing the network. Some Modules may
        /// have multiple Ports, in which case the additional ports usually define the connection to child modules downstream
        /// of the current device. This property is mostly informational and used internally to maintain the hierarchical
        /// structure of the IO tree.
        /// </remarks>
        PortCollection Ports { get; }
        
        /// <summary>
        /// Gets the <see cref="Core.Connection"/> object containing configuration for the <see cref="IModule"/> connection.
        /// </summary>
        Connection? Connection { get; }

        /// <summary>
        /// Gets the config <see cref="ITag{TDataType}"/> for the <see cref="IModule"/>.
        /// </summary>
        ITag<IComplexType>? Config { get; }
        
        /// <summary>
        /// Gets the <see cref="ITag{TDataType}"/> that represents the input channel data for the <see cref="Connection"/>.
        /// </summary>
        public ITag<IDataType>? Input { get; }

        /// <summary>
        /// Gets the <see cref="ITag{TDataType}"/> that represents the output channel data for the <see cref="Connection"/>.
        /// </summary>
        public ITag<IDataType>? Output { get; }
    }
}