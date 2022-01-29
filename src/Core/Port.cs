using System;

namespace L5Sharp.Core
{
    /// <summary>
    /// A component of a <see cref="IModule"/> that represents the connection location of the device.
    /// the device.
    /// </summary>
    /// <remarks>
    /// The Port may be the slot on the chassis or backplane where the <see cref="IModule"/> resides.
    /// This may also be the network address (IP) of the device. Each Port may also have a <see cref="Bus"/> that
    /// contains child Modules. Each Port is identifiable by the <see cref="Id"/> property. 
    /// </remarks>
    public sealed class Port : IEquatable<Port>
    {
        internal Port(int id, string address, string type, bool upstream = false, Bus? bus = null)
        {
            Id = id;
            Address = address;
            Type = type;
            Upstream = upstream;
            Bus = bus;
        }

        /// <summary>
        /// Gets the Id of the <see cref="Port"/>.
        /// </summary>
        /// <remarks>
        /// All Modules have at least oe port. Each is identified by the Id property. The <see cref="Port"/> equality
        /// members are defined using the Id value. This value is internally generated.
        /// </remarks>
        public int Id { get; }

        /// <summary>
        /// Gets the address of the <see cref="Port"/>.
        /// </summary>
        /// <remarks>
        /// The address of a port represents the slot or IP address for the port. This value is used in the
        /// <see cref="IModule"/> to determine the slot and IP properties. This value is assigned when creating the
        /// Module component. 
        /// </remarks>
        public string Address { get; }

        /// <summary>
        /// Gets the value that represents the <see cref="Port"/> type.
        /// </summary>
        /// <remarks>
        /// This value appears to be specific to the product. Ports with IP will have 'Network' for their value.
        /// </remarks>
        public string Type { get; }

        /// <summary>
        /// Gets the value indicating whether the there are devices upstream of the current <see cref="Port"/>.
        /// </summary>
        public bool Upstream { get; }

        /// <summary>
        /// Gets the <see cref="Bus"/> for the <see cref="Port"/> object.
        /// </summary>
        /// <remarks>
        /// A port's bus represents the backplane or chassis on which Modules accessible to the port.
        /// Some Modules may not have a Bus, therefor making this property nullable. 
        /// </remarks>
        public Bus? Bus { get; }

        /// <inheritdoc />
        public bool Equals(Port? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is Port other && Equals(other);

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