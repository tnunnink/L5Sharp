using System;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <summary>
    /// A set or properties for a given <see cref="IModule"/> connection. 
    /// </summary>
    public sealed class Connection
    {
        internal Connection(string name, int rpi, ConnectionType? type, 
            ConnectionPriority? priority = null,
            TransmissionType? inputConnectionType = null,
            ProductionTrigger? inputProductionTrigger = null, 
            bool outputRedundantOwner = false,
            bool unicast = false, int eventId = 0)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Rpi = rpi;
            Type = type ?? ConnectionType.Unknown;
            Priority = priority ?? ConnectionPriority.Scheduled;
            InputConnectionType = inputConnectionType ?? TransmissionType.Multicast;
            InputProductionTrigger = inputProductionTrigger ?? ProductionTrigger.Cyclic;
            OutputRedundantOwner = outputRedundantOwner;
            Unicast = unicast;
            EventId = eventId;
        }

        /// <summary>
        /// Gets the name of the <see cref="Connection"/> component.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the value of the Request Packet Interval for the <see cref="Connection"/>. 
        /// </summary>
        public int Rpi { get; }

        /// <summary>
        /// Gets the <see cref="Enums.ConnectionType"/> value for the <see cref="Connection"/>.
        /// </summary>
        public ConnectionType Type { get; }

        /// <summary>
        /// Gets the <see cref="Enums.ConnectionPriority"/> value for the <see cref="Connection"/>.
        /// </summary>
        public ConnectionPriority Priority { get; }

        /// <summary>
        /// Gets the <see cref="Enums.TransmissionType"/> value for the <see cref="Connection"/>.
        /// </summary>
        public TransmissionType InputConnectionType { get; }
        
        /// <summary>
        /// Gets a value indicating whether the <see cref="Connection"/> output is a redundant owner.
        /// </summary>
        public bool OutputRedundantOwner { get; }
        
        /// <summary>
        /// Gets the <see cref="Enums.ProductionTrigger"/> value for the <see cref="Connection"/>.
        /// </summary>
        public ProductionTrigger InputProductionTrigger { get; }

        /// <summary>
        /// Gets the value indicating whether the EtherNet/IP connection is unicast.
        /// </summary>
        public bool Unicast { get; }
        
        /// <summary>
        /// Gets the value of the Event ID used in conjunction with an event task for the <see cref="Connection"/>.
        /// </summary>
        public int EventId { get; }
    }
}