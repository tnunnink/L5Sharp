using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Connection
    {
        internal Connection(string name, int rpi, ConnectionType type, ConnectionPriority priority,
            TransmissionType inputConnectionType, bool outputRedundantOwner, ProductionTrigger inputProductionTrigger,
            string connectionPath, string inputSuffix, string outputSuffix, byte inputCxnPoint, byte inputSize,
            byte outputCxnPoint, byte outputSize, bool unicast, int eventId)
        {
            Name = name;
            Rpi = rpi;
            Type = type;
            Priority = priority;
            InputConnectionType = inputConnectionType;
            OutputRedundantOwner = outputRedundantOwner;
            InputProductionTrigger = inputProductionTrigger;
            ConnectionPath = connectionPath;
            InputSuffix = inputSuffix;
            OutputSuffix = outputSuffix;
            InputCxnPoint = inputCxnPoint;
            InputSize = inputSize;
            OutputCxnPoint = outputCxnPoint;
            OutputSize = outputSize;
            Unicast = unicast;
            EventId = eventId;
        }
        
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
        public string ConnectionPath { get;  }
        public string InputSuffix { get;  }
        public string OutputSuffix { get;  }
        public byte InputCxnPoint { get;  }
        public byte InputSize { get;  }
        public byte OutputCxnPoint { get;  }
        public byte OutputSize { get;  }
        public bool Unicast { get;  }
        public int EventId { get; }
    }
}