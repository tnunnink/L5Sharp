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
    internal class ConnectionSerializer : IL5XSerializer<Connection>
    {
        private static readonly XName ElementName = L5XElement.Connection.ToString();
        private readonly FormattedDataSerializer _formattedDataSerializer;

        public ConnectionSerializer()
        {
            _formattedDataSerializer = new FormattedDataSerializer();
        }
        
        public XElement Serialize(Connection component)
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
            element.Add(new XAttribute(L5XAttribute.InputProductionTrigger.ToString(), component.InputProductionTrigger));
            element.Add(new XAttribute(L5XAttribute.OutputRedundantOwner.ToString(), component.OutputRedundantOwner));
            element.Add(new XAttribute(L5XAttribute.EventID.ToString(), component.EventId));
            element.Add(new XAttribute(L5XAttribute.InputTagSuffix.ToString(), component.InputTagSuffix));
            element.Add(new XAttribute(L5XAttribute.OutputTagSuffix.ToString(), component.OutputTagSuffix));

            //todo input and output tags...
            
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
            var rpi = element.Attribute(L5XAttribute.RPI.ToString())?.Value.Parse<int>() ?? default;
            var type = element.Attribute(L5XAttribute.Type.ToString())?.Value.Parse<ConnectionType>();
            var inputCxnPoint = element.Attribute(L5XAttribute.InputCxnPoint.ToString())
                ?.Value.Parse<byte>() ?? default;
            var inputSize = element.Attribute(L5XAttribute.InputSize.ToString())
                ?.Value.Parse<byte>() ?? default;
            var outputCxnPoint = element.Attribute(L5XAttribute.OutputCxnPoint.ToString())
                ?.Value.Parse<byte>() ?? default;
            var outputSize = element.Attribute(L5XAttribute.OutputSize.ToString())
                ?.Value.Parse<byte>() ?? default;
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
            
            var inputTagName = DetermineTagName(element, inputSuffix);
            var inputType = GenerateDataType(element, L5XElement.InputTag.ToString());
            var inputTag = inputType is not null ? new Tag<IDataType>(inputTagName, inputType) : null;
            
            var outputTagName = DetermineTagName(element, outputSuffix);
            var outputType = GenerateDataType(element, L5XElement.OutputTag.ToString());
            var outputTag = outputType is not null ? new Tag<IDataType>(outputTagName, outputType) : null;

            return new Connection(name, rpi, type, inputCxnPoint, inputSize, outputCxnPoint, outputSize, 
                priority, inputConnectionType, inputProductionTrigger, outputRedundantOwner, unicast, eventId,
                inputSuffix, outputSuffix, inputTag, outputTag);
        }

        private IDataType? GenerateDataType(XContainer element, string tagName)
        {
            var tagElement = element.Descendants(tagName).FirstOrDefault();
            var formattedData = tagElement?.Descendants(L5XElement.Data.ToString())
                .FirstOrDefault(e => e.Attribute(L5XAttribute.Format.ToString())?.Value == TagDataFormat.Decorated.Name);

            return formattedData is not null ? _formattedDataSerializer.Deserialize(formattedData) : null;
        }

        private static string DetermineTagName(XNode element, string suffix)
        {
            var moduleName = element.Ancestors(L5XElement.Module.ToString())
                .FirstOrDefault()?.Attribute(L5XAttribute.Name.ToString())?.Value;
            var parentName = element.Ancestors(L5XElement.Module.ToString())
                .FirstOrDefault()?.Attribute(L5XAttribute.ParentModule.ToString())?.Value;

            var slot = element.Ancestors(L5XElement.Port.ToString())
                .Where(p => !bool.Parse(p.Attribute(L5XAttribute.Upstream.ToString())?.Value!)
                            && p.Attribute(L5XAttribute.Type.ToString())?.Value != "Ethernet"
                            && int.TryParse(p.Attribute(L5XAttribute.Address.ToString())?.Value, out _))
                .Select(p => p.Attribute(L5XAttribute.Address.ToString())?.Value)
                .FirstOrDefault();

            return slot is not null ? $"{parentName}:{slot}:{suffix}" : $"{moduleName}:{suffix}";
        }
    }
}