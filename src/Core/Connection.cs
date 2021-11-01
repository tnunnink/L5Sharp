using System.Collections.Generic;
using L5Sharp.Abstractions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    public interface IConnection
    {
        string Name { get; }
        int Rpi { get; set; }
        ConnectionType Type { get; set; }
        ConnectionPriority Priority { get; set; }
        TransmissionType InputConnectionType { get; set; }
        ProductionTrigger InputProductionTrigger { get; set; }
        bool OutputRedundantOwner { get; set; }
        byte InputSize { get; set; }
        byte OutputSize { get; set; }
        string InputSuffix { get; set; }
        string OutputSuffix { get; set; }
        bool Unicast { get; set; }
        int EventId { get; set; }
    }

    public class Connection : IConnection
    {
        public string Name { get; }
        public int Rpi { get; set; }
        public ConnectionType Type { get; set; }
        public ConnectionPriority Priority { get; set; }
        public TransmissionType InputConnectionType { get; set; }
        public bool OutputRedundantOwner { get; set; }
        public ProductionTrigger InputProductionTrigger { get; set; }
        public string ConnectionPath { get; set; }
        public string InputSuffix { get; set; }
        public string OutputSuffix { get; set; }
        public byte InputCxnPoint { get; set; }
        public byte InputSize { get; set; }
        public byte OutputCxnPoint { get; set; }
        public byte OutputSize { get; set; }
        public bool Unicast { get; set; }
        public int EventId { get; set; }
    }
}