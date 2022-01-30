using System;

namespace L5Sharp.Core
{
    /// <summary>
    /// A component of a <see cref="IModule"/> that represents a connection of the device. to other devices on the network.
    /// </summary>
    /// <remarks>
    /// A Port is a component that helps define the structure of the IO tree.
    /// The Port may be the slot on the chassis or backplane where the <see cref="IModule"/> resides.
    /// This may also be the network address (IP) of the device. Each Port may also have a <see cref="Bus"/> that
    /// contains child Modules. Each Port is identifiable by the <see cref="Id"/> property. 
    /// </remarks>
    public sealed class Port
    {
        /// <summary>
        /// Creates a new <see cref="Port"/> object with the provided parameters.
        /// </summary>
        /// <param name="id">The unique int that identifies the Port.</param>
        /// <param name="address">The address of the Port. This is typically an IP address or an int slot number.</param>
        /// <param name="type">The type of the Port. Ethernet for ports with IP address.</param>
        /// <param name="upstream">true indicates that this Port is a connection to a Module upstream.
        /// false indicates that this Port servers as a connection for child modules, and therefore has a non null Bus.</param>
        /// <param name="bus">The <see cref="Bus"/> of the Port represents the backplane or set of child devices reachable on the network.</param>
        /// <exception cref="ArgumentNullException">address or type are null.</exception>
        public Port(int id, string address, string type, bool upstream = false, Bus? bus = null)
        {
            Id = id;
            Address = address ?? throw  new ArgumentNullException(nameof(address));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Upstream = upstream;
            Bus = bus;
        }

        /// <summary>
        /// Gets the Id of the <see cref="Port"/>.
        /// </summary>
        /// <remarks>
        /// All Modules have at least one port. Each is identified by the Id property. Typically Modules will have one
        /// or two ports with Ids '1' and '2', respectively.
        /// </remarks>
        public int Id { get; }

        /// <summary>
        /// Gets the address of the <see cref="Port"/>.
        /// </summary>
        /// <remarks>
        /// The Address of a port represents the slot or IP address for the port. This value is used in the
        /// <see cref="IModule"/> to determine the slot and IP properties. All ports must have an Address.
        /// </remarks>
        public string Address { get; }

        /// <summary>
        /// Gets the value that represents the <see cref="Port"/> type.
        /// </summary>
        /// <remarks>
        /// This value appears to be specific to the product. Ports with IP will have 'Network' for their type. This
        /// property is required for importing a <see cref="IModule"/> correctly.
        /// </remarks>
        public string Type { get; }

        /// <summary>
        /// Gets the value indicating whether the there are devices upstream of the current <see cref="Port"/>.
        /// </summary>
        /// <remarks>
        /// From examining the L5X examples, the upstream flag seems to indicate that the port is one connected to a
        /// parent module living 'upstream' of the current device. Non-upstream ports seem to indicate they contain
        /// child modules, or connect to devices living 'downstream' of the current Module.
        /// </remarks>
        public bool Upstream { get; }

        /// <summary>
        /// Gets the <see cref="Bus"/> for the <see cref="Port"/> object.
        /// </summary>
        /// <remarks>
        /// A port's bus represents the backplane or chassis on which Modules are accessible to the port.
        /// Some Modules may not have a Bus, therefore making this property nullable. 
        /// </remarks>
        public Bus? Bus { get; }
    }
}