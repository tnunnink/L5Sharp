using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Connection
    {
        /// <summary>
        /// Gets the name of the <see cref="Connection"/>
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the value of the RPI (Request Packet Interval) for the <see cref="Connection"/>. 
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
        public TransmissionType InputConnectionType { get; }
        public bool OutputRedundantOwner { get; }
        public ProductionTrigger InputProductionTrigger { get; }
        public string InputSuffix { get;  }
        public string OutputSuffix { get;  }
        public bool Unicast { get;  }
        public int EventId { get; }
    }
}