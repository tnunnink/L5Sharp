using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;
using L5Sharp.Serialization.Data;

namespace L5Sharp.Serialization.Components
{
    /// <summary>
    /// A serializer for a logix <see cref="Connection"/> component.
    /// </summary>
    public class ConnectionSerializer : IL5XSerializer<Connection>
    {
        private static readonly XName ElementName = L5XElement.Connection.ToString();

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
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.ComponentName();
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
            var inputType = GenerateDataType(element, L5XElement.InputTag.ToString());
            var inputTag = inputType is not null ? new Tag<IDataType>(inputTagName, inputType) : null;

            var outputSuffix = element.Attribute("OutputTagSuffix")?.Value ?? "O";
            var outputTagName = DetermineTagName(element, outputSuffix);
            var outputType = GenerateDataType(element, L5XElement.OutputTag.ToString());
            var outputTag = outputType is not null ? new Tag<IDataType>(outputTagName, outputType) : null;

            return new Connection(name, rpi, type, priority, inputConnectionType, inputProductionTrigger,
                outputRedundantOwner, unicast, eventId, inputTag, outputTag);
        }

        private static IDataType? GenerateDataType(XContainer element, string tagName)
        {
            var serializer = new FormattedDataSerializer();

            var tagElement = element.Descendants(tagName).FirstOrDefault();
            var formattedData = tagElement?.Descendants(L5XElement.Data.ToString())
                .FirstOrDefault(e => e.Attribute(L5XAttribute.Format.ToString())?.Value == TagDataFormat.Decorated.Name);

            return formattedData is not null
                ? serializer.Deserialize(formattedData)
                : null;
        }

        private static string DetermineTagName(XNode element, string suffix)
        {
            var moduleName = element.Ancestors(L5XElement.Module.ToString())
                .FirstOrDefault()?.Attribute(L5XAttribute.Name.ToString())?.Value;
            var parentName = element.Ancestors(L5XElement.Module.ToString())
                .FirstOrDefault()?.Attribute("ParentModule")?.Value;

            var slot = element.Ancestors(L5XElement.Port.ToString())
                .Where(p => !bool.Parse(p.Attribute("Upstream")?.Value!)
                            && p.Attribute("Type")?.Value != "Ethernet"
                            && int.TryParse(p.Attribute("Address")?.Value, out _))
                .Select(p => p.Attribute("Address")?.Value)
                .FirstOrDefault();

            return slot is not null ? $"{parentName}:{slot}:{suffix}" : $"{moduleName}:{suffix}";
        }
    }
}