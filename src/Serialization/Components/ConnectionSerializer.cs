using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization.Components
{
    internal class ConnectionSerializer : L5XSerializer<Connection>
    {
        private readonly L5XDocument? _document;
        private static readonly XName ElementName = L5XElement.Connection.ToString();

        private InputTagSerializer InputTagSerializer => _document is not null
            ? _document.Serializers.Get<InputTagSerializer>()
            : new InputTagSerializer(_document);
        
        private OutputTagSerializer OutputTagSerializer => _document is not null
            ? _document.Serializers.Get<OutputTagSerializer>()
            : new OutputTagSerializer(_document);

        public ConnectionSerializer(L5XDocument? document = null)
        {
            _document = document;
        }

        public override XElement Serialize(Connection component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.Add(new XAttribute(L5XAttribute.Name.ToString(), component.Name));
            element.Add(new XAttribute(L5XAttribute.RPI.ToString(), component.Rpi));
            element.Add(new XAttribute(L5XAttribute.InputCxnPoint.ToString(), component.InputCxnPoint));
            element.Add(new XAttribute(L5XAttribute.InputSize.ToString(), component.InputSize));
            element.Add(new XAttribute(L5XAttribute.OutputCxnPoint.ToString(), component.OutputCxnPoint));
            element.Add(new XAttribute(L5XAttribute.OutputSize.ToString(), component.OutputSize));
            element.Add(new XAttribute(L5XAttribute.Type.ToString(), component.Type));
            element.Add(new XAttribute(L5XAttribute.Priority.ToString(), component.Priority));
            element.Add(new XAttribute(L5XAttribute.InputConnectionType.ToString(), component.InputConnectionType));
            element.Add(
                new XAttribute(L5XAttribute.InputProductionTrigger.ToString(), component.InputProductionTrigger));
            element.Add(new XAttribute(L5XAttribute.OutputRedundantOwner.ToString(), component.OutputRedundantOwner));
            element.Add(new XAttribute(L5XAttribute.EventID.ToString(), component.EventId));
            element.Add(new XAttribute(L5XAttribute.InputTagSuffix.ToString(), component.InputTagSuffix));
            element.Add(new XAttribute(L5XAttribute.OutputTagSuffix.ToString(), component.OutputTagSuffix));

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
            var rpi = element.Attribute(L5XAttribute.RPI.ToString())?.Value.Parse<int>() ?? default;
            var type = element.Attribute(L5XAttribute.Type.ToString())?.Value.Parse<ConnectionType>();
            var inputCxnPoint = element.Attribute(L5XAttribute.InputCxnPoint.ToString())
                ?.Value.Parse<ushort>() ?? default;
            var inputSize = element.Attribute(L5XAttribute.InputSize.ToString())
                ?.Value.Parse<ushort>() ?? default;
            var outputCxnPoint = element.Attribute(L5XAttribute.OutputCxnPoint.ToString())
                ?.Value.Parse<ushort>() ?? default;
            var outputSize = element.Attribute(L5XAttribute.OutputSize.ToString())
                ?.Value.Parse<ushort>() ?? default;
            var priority = element.Attribute(L5XAttribute.Priority.ToString())?.Value.Parse<ConnectionPriority>();
            var inputConnectionType = element.Attribute(L5XAttribute.InputConnectionType.ToString())
                ?.Value.Parse<TransmissionType>();
            var inputProductionTrigger = element.Attribute(L5XAttribute.InputProductionTrigger.ToString())
                ?.Value.Parse<ProductionTrigger>();
            var outputRedundantOwner = element.Attribute(L5XAttribute.OutputRedundantOwner.ToString())
                ?.Value.Parse<bool>() ?? default;
            var unicast = element.Attribute(L5XAttribute.Unicast.ToString())?.Value.Parse<bool>() ?? default;
            var eventId = element.Attribute(L5XAttribute.EventID.ToString())?.Value.Parse<int>() ?? default;
            var inputSuffix = element.Attribute(L5XAttribute.InputTagSuffix.ToString())?.Value ?? "I";
            var outputSuffix = element.Attribute(L5XAttribute.OutputTagSuffix.ToString())?.Value ?? "O";
            
            var inputTagElement = element.Descendants(L5XElement.InputTag.ToString()).FirstOrDefault();
            var inputTag = inputTagElement is not null ? InputTagSerializer.Deserialize(inputTagElement) : null;

            var outputTagElement = element.Descendants(L5XElement.OutputTag.ToString()).FirstOrDefault();
            var outputTag = outputTagElement is not null ? OutputTagSerializer.Deserialize(outputTagElement) : null;

            return new Connection(name, rpi, type, inputCxnPoint, inputSize, outputCxnPoint, outputSize,
                priority, inputConnectionType, inputProductionTrigger, outputRedundantOwner, unicast, eventId,
                inputSuffix, outputSuffix, inputTag, outputTag);
        }
    }
}