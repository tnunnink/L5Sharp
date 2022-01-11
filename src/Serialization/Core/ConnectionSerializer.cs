using System;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Serialization.Core
{
    /// <summary>
    /// A serializer for a logix <see cref="Connection"/> component.
    /// </summary>
    public class ConnectionSerializer : IXSerializer<Connection>
    {
        private static readonly XName ElementName = LogixNames.Connection;

        /// <inheritdoc />
        public XElement Serialize(Connection component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.AddAttribute(component,c => c.Name);
            element.AddAttribute(component,c => c.Rpi);
            element.AddAttribute(component,c => c.Type);
            element.AddAttribute(component,c => c.Priority);
            element.AddAttribute(component,c => c.InputConnectionType);
            element.AddAttribute(component,c => c.InputProductionTrigger);
            element.AddAttribute(component,c => c.OutputRedundantOwner);
            element.AddAttribute(component,c => c.InputSuffix);
            element.AddAttribute(component,c => c.OutputSuffix);
            element.AddAttribute(component,c => c.EventId);

            return element;
        }

        /// <inheritdoc />
        public Connection Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var name = element.GetComponentName();
            var rpi = element.GetAttribute<Connection, int>(c => c.Rpi);
            var type = element.GetAttribute<Connection, ConnectionType>(c => c.Type) ?? ConnectionType.Unknown;
            var priority = element.GetAttribute<Connection, ConnectionPriority>(c => c.Priority);
            var inputConnectionType = element.GetAttribute<Connection, TransmissionType>(c => c.InputConnectionType);
            var inputProductionTrigger =
                element.GetAttribute<Connection, ProductionTrigger>(c => c.InputProductionTrigger);
            var outputRedundantOwner = element.GetAttribute<Connection, bool>(c => c.OutputRedundantOwner);
            var inputSuffix = element.GetAttribute<Connection, string>(c => c.InputSuffix);
            var outputSuffix = element.GetAttribute<Connection, string>(c => c.OutputSuffix);
            var eventId = element.GetAttribute<Connection, int>(c => c.EventId);
            var unicast = element.GetAttribute<Connection, bool>(c => c.Unicast);

            //These types will be structure types?
            var inputTag = element.Element("InputTag");
            var outputTag = element.Element("OutputTag");

            return new Connection(name, rpi, type, priority,
                inputConnectionType, inputProductionTrigger, outputRedundantOwner, inputSuffix, outputSuffix, unicast,
                eventId);
        }
    }
}