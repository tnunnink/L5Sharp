using System;

namespace L5Sharp.Core
{
    /// <summary>
    /// A component of a <see cref="Module"/> that represents the means for connecting devices on a network or in a chassis.
    /// </summary>
    /// <remarks>
    /// A Port is a component that helps define the structure of the IO tree.
    /// Each port may contain the slot on the chassis or backplane where the <see cref="Module"/> resides,
    /// or the the network address (IP) of the device. Each port may (or may not) have a <see cref="Bus"/> that
    /// contains child modules. Each port is identifiable by the <see cref="Id"/> property. 
    /// </remarks>
    public sealed class Port : IEquatable<Port>
    {
        /// <summary>
        /// Creates a new <see cref="Port"/> with the specified parameters.
        /// </summary>
        /// <param name="id">The number that uniquely identifies the port (typically 1, or 2).</param>
        /// <param name="address">The string value representing the address (slot/ip/node) of the port in the bus.</param>
        /// <param name="type">The string representing the type of port. (e.g. Ethernet, ICP, etc.)</param>
        /// <param name="upstream">A flag that indicates whether the current port connects to device upstream,
        /// or is a port that hosts other modules downstream, or in the </param>
        /// <param name="busSize"></param>
        /// <param name="downstreamOnly"></param>
        public Port(int id, string type, string? address = null, bool upstream = default, byte busSize = default,
            bool downstreamOnly = default)
        {
            Id = id;
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Address = address ?? string.Empty;
            Upstream = upstream;
            BusSize = busSize;
            DownstreamOnly = downstreamOnly;
        }

        public IModule Module { get; }

        /// <summary>
        /// Gets the Id of the <see cref="Port"/>.
        /// </summary>
        /// <remarks>
        /// All Modules have at least one port. Each is identified by the Id property. Typically Modules will have one
        /// or two ports with Ids '1' and '2', respectively.
        /// </remarks>
        public int Id { get; }

        /// <summary>
        /// Gets the value that represents the <see cref="Port"/> type.
        /// </summary>
        /// <remarks>
        /// This value appears to be specific to the product. Ports with IP will have 'Network' for their type. This
        /// property is required for importing a <see cref="Module"/> correctly.
        /// </remarks>
        public string Type { get; }

        /// <summary>
        /// Gets the address of the <see cref="Port"/>.
        /// </summary>
        /// <remarks>
        /// The Address of a port represents the slot or IP address for the port. This value is used in the
        /// <see cref="Module"/> to determine the slot and IP properties. All ports must have an Address.
        /// </remarks>
        public string Address { get; }

        /// <summary>
        /// Gets the value indicating whether the there are devices upstream of the current <see cref="Port"/>.
        /// </summary>
        /// <remarks>
        /// From examining the L5X examples, the upstream flag seems to indicate that the port is one connected to a
        /// parent module living 'upstream' of the current device. Non-upstream ports seem to indicate they contain
        /// child modules, or connect to devices living 'downstream' of the current Module. This property must be set
        /// correctly to be able to import L5X.
        /// </remarks>
        public bool Upstream { get; }

        /// <summary>
        /// Gets the <see cref="Bus"/> for the <see cref="Port"/> object.
        /// </summary>
        /// <remarks>
        /// A port's bus represents the chassis or network on which Modules are accessible to the port.
        /// Only downstream modules will have a valid Bus. 
        /// </remarks>
        public byte BusSize { get; }
        
        /// <summary>
        /// Gets a value indicating whether the port allows upstream connection, or if it is a downstream only port.
        /// </summary>
        public bool DownstreamOnly { get; }

        /// <summary>
        /// Returns a new <see cref="Port"/> with the same properties and the current instance and updated
        /// slot, upstream, and bus size parameters.
        /// </summary>
        /// <param name="address">The address (slot/IP/node) of the new port.</param>
        /// <param name="upstream">Value indicating whether the port is upstream - meaning connected to a device
        /// upstream.</param>
        /// <param name="busSize">The max capacity of the port's bus.</param>
        /// <remarks>
        /// Provides a simple way to generate a new <c>Port</c> object with updated properties, while maintaining the same
        /// id and type. This is mostly used for creation of new modules after obtaining a <see cref="ModuleDefinition"/>.
        /// </remarks>
        public Port Configure(string address, bool upstream = default, byte busSize = default)
        {
            if (DownstreamOnly && upstream)
                throw new InvalidOperationException(
                    "Not able to configure as upstream port. The current port is configured as a downstream only port.");

            return new Port(Id, Type, address, upstream, busSize);
        }

        /// <inheritdoc />
        public bool Equals(Port? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as Port);

        /// <inheritdoc />
        public override int GetHashCode() => Id;

        /// <summary>
        /// Determines if the provided objects are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are equal; otherwise, false.</returns>
        public static bool operator ==(Port? left, Port? right) => Equals(left, right);

        /// <summary>
        /// Determines if the provided objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are not equal; otherwise, false.</returns>
        public static bool operator !=(Port? left, Port? right) => !Equals(left, right);
    }
}