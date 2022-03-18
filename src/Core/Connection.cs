using System;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <summary>
    /// A component of a <see cref="Module"/> that represents the properties and data of the connection to the field device.
    /// </summary>
    public sealed class Connection
    {
        /// <summary>
        /// Creates a new <see cref="Connection"/> object with the provided parameters.
        /// </summary>
        /// <param name="name">The name of the connection.</param>
        /// <param name="rpi">the request packet interval of the connection (ms).</param>
        /// <param name="type">The <see cref="ConnectionType"/></param>
        /// <param name="inputCxnPoint"></param>
        /// <param name="inputSize"></param>
        /// <param name="outputCxnPoint"></param>
        /// <param name="outputSize"></param>
        /// <param name="priority"></param>
        /// <param name="inputConnectionType"></param>
        /// <param name="inputProductionTrigger"></param>
        /// <param name="outputRedundantOwner"></param>
        /// <param name="unicast"></param>
        /// <param name="eventId"></param>
        /// /// <param name="inputTagSuffix"></param>
        /// <param name="outputTagSuffix"></param>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <exception cref="ArgumentNullException">name is null.</exception>
        public Connection(string name, int rpi, ConnectionType? type,
            ushort inputCxnPoint = default, ushort inputSize = default,
            ushort outputCxnPoint = default, ushort outputSize = default,
            ConnectionPriority? priority = null,
            TransmissionType? inputConnectionType = null,
            ProductionTrigger? inputProductionTrigger = null, 
            bool outputRedundantOwner = false,
            bool unicast = false, int eventId = 0,
            string? inputTagSuffix = null, string? outputTagSuffix = null,
            ITag<IDataType>? input = null, ITag<IDataType>? output = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Rpi = rpi;
            InputCxnPoint = inputCxnPoint;
            InputSize = inputSize;
            OutputCxnPoint = outputCxnPoint;
            OutputSize = outputSize;
            Type = type ?? ConnectionType.Unknown;
            Priority = priority ?? ConnectionPriority.Scheduled;
            InputConnectionType = inputConnectionType ?? TransmissionType.Multicast;
            InputProductionTrigger = inputProductionTrigger ?? ProductionTrigger.Cyclic;
            OutputRedundantOwner = outputRedundantOwner;
            Unicast = unicast;
            EventId = eventId;
            InputTagSuffix = inputTagSuffix ?? "I";
            OutputTagSuffix = outputTagSuffix ?? "O";
            Input = input;
            Output = output;
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
        /// Gets the input connection point for the primary <see cref="Connection"/>.
        /// </summary>
        public ushort InputCxnPoint { get; }
        
        /// <summary>
        /// Gets the input size for the <see cref="Connection"/>.
        /// </summary>
        public ushort InputSize { get; }
        
        /// <summary>
        /// Gets the output connection point for the primary <see cref="Connection"/>.
        /// </summary>
        public ushort OutputCxnPoint { get; }
        
        /// <summary>
        /// Gets the output size for the <see cref="Connection"/>.
        /// </summary>
        public ushort OutputSize { get; }

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

        /// <summary>
        /// Gets the suffix for <see cref="Input"/> tag. 
        /// </summary>
        public string InputTagSuffix { get; }
        
        /// <summary>
        /// Gets the suffix for <see cref="Output"/> tag.
        /// </summary>
        public string OutputTagSuffix { get; }

        /// <summary>
        /// Gets the Tag that represents the input channel data for the <see cref="Connection"/>.
        /// </summary>
        public ITag<IDataType>? Input { get; }

        /// <summary>
        /// Gets the Tag that represents the output channel data for the <see cref="Connection"/>.
        /// </summary>
        public ITag<IDataType>? Output { get; }
    }
}