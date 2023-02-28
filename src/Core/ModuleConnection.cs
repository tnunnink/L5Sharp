using L5Sharp.Attributes;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Serialization;

namespace L5Sharp.Core
{
    /// <summary>
    /// A component of a <see cref="Module"/> that represents the properties and data of the connection to the field device.
    /// </summary>
    [LogixSerializer(typeof(ModuleConnectionSerializer))]
    public sealed class ModuleConnection
    {
        /// <summary>
        /// Gets the name of the <see cref="ModuleConnection"/> component.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets the value of the Request Packet Interval for the <see cref="ModuleConnection"/>. 
        /// </summary>
        public int Rpi { get; set; } = default;

        /// <summary>
        /// Gets the input connection point for the primary <see cref="ModuleConnection"/>.
        /// </summary>
        public ushort InputCxnPoint { get; set; } = default;

        /// <summary>
        /// Gets the input size for the <see cref="ModuleConnection"/>.
        /// </summary>
        public ushort InputSize { get; set; } = default;

        /// <summary>
        /// Gets the output connection point for the primary <see cref="ModuleConnection"/>.
        /// </summary>
        public ushort OutputCxnPoint { get; set; } = default;

        /// <summary>
        /// Gets the output size for the <see cref="ModuleConnection"/>.
        /// </summary>
        public ushort OutputSize { get; set; } = default;

        /// <summary>
        /// Gets the <see cref="Enums.ConnectionType"/> value for the <see cref="ModuleConnection"/>.
        /// </summary>
        public ConnectionType Type { get; set; } = ConnectionType.Unknown;

        /// <summary>
        /// Gets the <see cref="Enums.ConnectionPriority"/> value for the <see cref="ModuleConnection"/>.
        /// </summary>
        public ConnectionPriority Priority { get; set; } = ConnectionPriority.Scheduled;

        /// <summary>
        /// Gets the <see cref="Enums.TransmissionType"/> value for the <see cref="ModuleConnection"/>.
        /// </summary>
        public TransmissionType InputConnectionType { get; set; } = TransmissionType.Multicast;

        /// <summary>
        /// Gets a value indicating whether the <see cref="ModuleConnection"/> output is a redundant owner.
        /// </summary>
        public bool OutputRedundantOwner { get; set; } = default;

        /// <summary>
        /// Gets the <see cref="Enums.ProductionTrigger"/> value for the <see cref="ModuleConnection"/>.
        /// </summary>
        public ProductionTrigger InputProductionTrigger { get; set; } = ProductionTrigger.Cyclic;

        /// <summary>
        /// Gets the value indicating whether the EtherNet/IP connection is unicast.
        /// </summary>
        public bool Unicast { get; set; } = default;

        /// <summary>
        /// Gets the value of the Event ID used in conjunction with an event task for the <see cref="ModuleConnection"/>.
        /// </summary>
        public int EventId { get; set; } = default;

        /// <summary>
        /// Gets the suffix for <see cref="Input"/> tag. 
        /// </summary>
        public string InputTagSuffix { get; set; } = "I";

        /// <summary>
        /// Gets the suffix for <see cref="Output"/> tag.
        /// </summary>
        public string OutputTagSuffix { get; set; } = "O";

        /// <summary>
        /// Gets the Tag that represents the input channel data for the <see cref="ModuleConnection"/>.
        /// </summary>
        public Tag? Input { get; set; } = default;

        /// <summary>
        /// Gets the Tag that represents the output channel data for the <see cref="ModuleConnection"/>.
        /// </summary>
        public Tag? Output { get; set; } = default;
    }
}