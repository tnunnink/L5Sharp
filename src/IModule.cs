using System.Collections.Generic;
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
        /// Gets the value of the parent Module that the <see cref="IModule"/> is connected to.
        /// </summary>
        /// <remarks>
        /// This property is needed by the L5X to determine the hierarchical structure of the IO tree. This is property
        /// is set internally when configuring or serializing modules to and from the L5X. 
        /// </remarks>
        string ParentModule { get; }
        
        /// <summary>
        /// Gets the value of the parent Module Port id that the <see cref="IModule"/> is connected to.
        /// </summary>
        /// <remarks>
        /// This property is needed by the L5X to determine the hierarchical structure of the IO tree. This is property
        /// is set internally when configuring or serializing modules to and from the L5X. 
        /// </remarks>
        int ParentModPortId { get; }
        
        /// <summary>
        /// Gets the <see cref="Core.Connection"/> object containing configuration for the <see cref="IModule"/> connection.
        /// </summary>
        Connection? Connection { get; }
        
        /// <summary>
        /// Gets the <see cref="Bus"/> or backplane instance of the <see cref="IModule"/>.
        /// </summary>
        /// <remarks>
        /// The <see cref="Bus"/> represents the collection of child modules for the current Module object. Use this
        /// property and it's members to add and/or remove a <see cref="IModule"/> from the local chassis of the module. 
        /// </remarks>
        Bus? Bus { get; }
        
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