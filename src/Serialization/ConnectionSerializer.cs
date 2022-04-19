using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization
{
    internal class ConnectionSerializer : L5XSerializer<Connection>
    {
        private readonly L5XContent? _document;
        private static readonly XName ElementName = L5XName.Connection;

        private InputTagSerializer InputTagSerializer => _document is not null
            ? _document.Serializers.Get<InputTagSerializer>()
            : new InputTagSerializer(_document);
        
        private OutputTagSerializer OutputTagSerializer => _document is not null
            ? _document.Serializers.Get<OutputTagSerializer>()
            : new OutputTagSerializer(_document);

        public ConnectionSerializer(L5XContent? document = null)
        {
            _document = document;
        }

        public override XElement Serialize(Connection component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.Add(new XAttribute(L5XName.Name, component.Name));
            element.Add(new XAttribute(L5XName.RPI, component.Rpi));
            element.Add(new XAttribute(L5XName.InputCxnPoint, component.InputCxnPoint));
            element.Add(new XAttribute(L5XName.InputSize, component.InputSize));
            element.Add(new XAttribute(L5XName.OutputCxnPoint, component.OutputCxnPoint));
            element.Add(new XAttribute(L5XName.OutputSize, component.OutputSize));
            element.Add(new XAttribute(L5XName.Type, component.Type));
            element.Add(new XAttribute(L5XName.Priority, component.Priority));
            element.Add(new XAttribute(L5XName.InputConnectionType, component.InputConnectionType));
            element.Add(
                new XAttribute(L5XName.InputProductionTrigger, component.InputProductionTrigger));
            element.Add(new XAttribute(L5XName.OutputRedundantOwner, component.OutputRedundantOwner));
            element.Add(new XAttribute(L5XName.EventId, component.EventId));
            element.Add(new XAttribute(L5XName.InputTagSuffix, component.InputTagSuffix));
            element.Add(new XAttribute(L5XName.OutputTagSuffix, component.OutputTagSuffix));

            if (component.Input is not null)
            {
                var inputTag = InputTagSerializer.Serialize(component.Input);
                element.Add(inputTag);
            }

            if (component.Output is not null)
            {
                var outputTag = OutputTagSerializer.Serialize(component.Output);
                element.Add(outputTag);
            }

            return element;
        }

        /// <inheritdoc />
        public override  Connection Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.ComponentName();
            var rpi = element.Attribute(L5XName.RPI)?.Value.Parse<int>() ?? default;
            var type = element.Attribute(L5XName.Type)?.Value.Parse<ConnectionType>();
            var inputCxnPoint = element.Attribute(L5XName.InputCxnPoint)
                ?.Value.Parse<ushort>() ?? default;
            var inputSize = element.Attribute(L5XName.InputSize)
                ?.Value.Parse<ushort>() ?? default;
            var outputCxnPoint = element.Attribute(L5XName.OutputCxnPoint)
                ?.Value.Parse<ushort>() ?? default;
            var outputSize = element.Attribute(L5XName.OutputSize)
                ?.Value.Parse<ushort>() ?? default;
            var priority = element.Attribute(L5XName.Priority)?.Value.Parse<ConnectionPriority>();
            var inputConnectionType = element.Attribute(L5XName.InputConnectionType)
                ?.Value.Parse<TransmissionType>();
            var inputProductionTrigger = element.Attribute(L5XName.InputProductionTrigger)
                ?.Value.Parse<ProductionTrigger>();
            var outputRedundantOwner = element.Attribute(L5XName.OutputRedundantOwner)
                ?.Value.Parse<bool>() ?? default;
            var unicast = element.Attribute(L5XName.Unicast)?.Value.Parse<bool>() ?? default;
            var eventId = element.Attribute(L5XName.EventId)?.Value.Parse<int>() ?? default;
            var inputSuffix = element.Attribute(L5XName.InputTagSuffix)?.Value ?? "I";
            var outputSuffix = element.Attribute(L5XName.OutputTagSuffix)?.Value ?? "O";
            
            var inputTagElement = element.Descendants(L5XName.InputTag).FirstOrDefault();
            var inputTag = inputTagElement is not null ? InputTagSerializer.Deserialize(inputTagElement) : null;

            var outputTagElement = element.Descendants(L5XName.OutputTag).FirstOrDefault();
            var outputTag = outputTagElement is not null ? OutputTagSerializer.Deserialize(outputTagElement) : null;

            return new Connection(name, rpi, type, inputCxnPoint, inputSize, outputCxnPoint, outputSize,
                priority, inputConnectionType, inputProductionTrigger, outputRedundantOwner, unicast, eventId,
                inputSuffix, outputSuffix, inputTag, outputTag);
        }
    }
}