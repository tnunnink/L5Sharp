using System.Collections.Generic;
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
        string CatalogNumber { get; }

        /// <summary>
        /// Gets the vendor number of the <see cref="IModule"/>.
        /// </summary>
        ushort Vendor { get; }
        
        /// <summary>
        /// Gets the product type number of the <see cref="IModule"/> 
        /// </summary>
        /// <value>
        /// An unsigned 16 bit integer that represents an ID for the <see cref="ProductType"/>.
        /// </value>
        ushort ProductType { get; }
        
        /// <summary>
        /// Gets the product code of the <see cref="IModule"/>.
        /// </summary>
        ushort ProductCode { get; }
        
        /// <summary>
        /// Gets the <see cref="Revision"/> number for the <see cref="IModule"/>.
        /// </summary>
        Revision Revision { get; }

        /// <summary>
        /// Gets the name of the <see cref="IModule"/> that is the parent of the the current <see cref="IModule"/>
        /// </summary>
        string ParentModule { get; }

        /// <summary>
        /// Gets the port ID value that represents the port for which the current <see cref="IModule"/> is connected.
        /// </summary>
        int ParentModPortId { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="IModule"/> is inhibited.
        /// </summary>
        bool Inhibited { get; }
        
        /// <summary>
        /// Gets a value indicating whether the <see cref="IModule"/> will cause a major fault
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
        /// Gets a collection of <see cref="Port"/> objects for the <see cref="IModule"/>.
        /// </summary>
        IEnumerable<Port> Ports { get; }
        
        /// <summary>
        /// Gets the collection of <see cref="Connection"/> objects for the <see cref="IModule"/>.
        /// </summary>
        IEnumerable<Connection> Connections { get; }
        
        /// <summary>
        /// Gets the config <see cref="ITag{TDataType}"/> for the <see cref="IModule"/>.
        /// </summary>
        ITag<IComplexType> Config { get; }
    }
}