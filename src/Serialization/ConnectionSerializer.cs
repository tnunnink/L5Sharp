using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Helpers;

namespace L5Sharp.Serialization
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
            element.AddAttribute(component, c => c.Name);
            element.AddAttribute(component, c => c.Rpi);
            element.AddAttribute(component, c => c.Type);
            element.AddAttribute(component, c => c.Priority);
            element.AddAttribute(component, c => c.InputConnectionType);
            element.AddAttribute(component, c => c.InputProductionTrigger);
            element.AddAttribute(component, c => c.OutputRedundantOwner);
            element.AddAttribute(component, c => c.EventId);

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
            var eventId = element.GetAttribute<Connection, int>(c => c.EventId);
            var unicast = element.GetAttribute<Connection, bool>(c => c.Unicast);

            var inputSuffix = element.Attribute("InputTagSuffix")?.Value ?? "I";
            var inputTagName = DetermineTagName(element, inputSuffix);
            var inputType = GenerateDataType(element, LogixNames.InputTag);
            var inputTag = inputType is not null ? new Tag<IDataType>(inputTagName, inputType) : null;

            var outputSuffix = element.Attribute("OutputTagSuffix")?.Value ?? "O";
            var outputTagName = DetermineTagName(element, outputSuffix);
            var outputType = GenerateDataType(element, LogixNames.OutputTag);
            var outputTag = outputType is not null ? new Tag<IDataType>(outputTagName, outputType) : null;

            return new Connection(name, rpi, type, priority, inputConnectionType, inputProductionTrigger,
                outputRedundantOwner, unicast, eventId, inputTag, outputTag);
        }

        private static IDataType? GenerateDataType(XContainer element, string tagName)
        {
            var serializer = new FormattedDataSerializer();

            var tagElement = element.Descendants(tagName).FirstOrDefault();
            var formattedData = tagElement?.Descendants(LogixNames.Data)
                .FirstOrDefault(e => e.Attribute("Format")?.Value == TagDataFormat.Decorated.Name);

            return formattedData is not null
                ? serializer.Deserialize(formattedData)
                : null;
        }

        private static string DetermineTagName(XNode element, string suffix)
        {
            var moduleName = element.Ancestors(LogixNames.Module).FirstOrDefault()?.Attribute("Name")?.Value;
            var parentName = element.Ancestors(LogixNames.Module).FirstOrDefault()?.Attribute("ParentModule")?.Value;

            var slot = element.Ancestors(LogixNames.Port)
                .Where(p => !bool.Parse(p.Attribute("Upstream")?.Value!)
                            && p.Attribute("Type")?.Value != "Ethernet"
                            && int.TryParse(p.Attribute("Address")?.Value, out _))
                .Select(p => p.Attribute("Address")?.Value)
                .FirstOrDefault();

            return slot is not null ? $"{parentName}:{slot}:{suffix}" : $"{moduleName}:{suffix}";
        }
    }
}