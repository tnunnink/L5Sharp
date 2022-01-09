using System;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents a Logix Module Connection component.
    /// </summary>
    public sealed class Connection
    {
        internal Connection(string name, int rpi, ConnectionType? type, 
            ConnectionPriority? priority = null,
            TransmissionType? inputConnectionType = null,
            ProductionTrigger? inputProductionTrigger = null, 
            bool outputRedundantOwner = false, 
            string? inputSuffix = null, string? outputSuffix = null,
            bool unicast = false, int eventId = 0,
            ITag<IDataType>? inputTag = null, ITag<IDataType>? outputTag = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Rpi = rpi;
            Type = type ?? ConnectionType.Unknown;
            Priority = priority ?? ConnectionPriority.Scheduled;
            InputConnectionType = inputConnectionType ?? TransmissionType.Multicast;
            InputProductionTrigger = inputProductionTrigger ?? ProductionTrigger.Cyclic;
            OutputRedundantOwner = outputRedundantOwner;
            InputSuffix = inputSuffix ?? string.Empty;
            OutputSuffix = outputSuffix ?? string.Empty;;
            Unicast = unicast;
            EventId = eventId;
            InputTag = inputTag;
            OutputTag = outputTag;
        }

        /// <summary>
        /// Creates a new <see cref="Connection"/> object with the provided name, RPI, and <see cref="ConnectionType"/>.
        /// </summary>
        /// <param name="name">The name of the connection.</param>
        /// <param name="rpi">The request packet interval of the connection.</param>
        /// <param name="type">The type of the connection.</param>
        public Connection(string name, int rpi, ConnectionType type) : 
            this(name, rpi, type, ConnectionPriority.Scheduled, TransmissionType.Multicast, ProductionTrigger.Cyclic,
                false, "I", "O")
        {
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
        /// Gets value of the input suffix for the <see cref="Connection"/>.
        /// </summary>
        public string InputSuffix { get; }
        
        /// <summary>
        /// Gets value of the output suffix for the <see cref=""/>.
        /// </summary>
        public string OutputSuffix { get; }
        
        /// <summary>
        /// Gets the value indicating whether the EtherNet/IP connection is unicast.
        /// </summary>
        public bool Unicast { get; }
        
        /// <summary>
        /// Gets the value of the Event ID used in conjunction with an event task for the <see cref="Connection"/>.
        /// </summary>
        public int EventId { get; }
        
        /// <summary>
        /// Gets the <see cref="ITag{TDataType}"/> that represents the input channel data for the <see cref="Connection"/>.
        /// </summary>
        public ITag<IDataType>? InputTag { get; }

        /// <summary>
        /// Gets the <see cref="ITag{TDataType}"/> that represents the output channel data for the <see cref="Connection"/>.
        /// </summary>
        public ITag<IDataType>? OutputTag { get; }
    }
}