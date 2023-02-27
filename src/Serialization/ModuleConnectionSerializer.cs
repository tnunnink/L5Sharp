using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    internal class ModuleConnectionSerializer : ILogixSerializer<ModuleConnection>
    {
        private readonly DecoratedDataSerializer _dataSerializer = new();

        public XElement Serialize(ModuleConnection obj)
        {
            Check.NotNull(obj);

            var element = new XElement(L5XName.Connection);
            element.AddValue(obj.Name, L5XName.Name);
            element.AddValue(obj.Rpi, L5XName.RPI);
            element.AddValue(obj.InputCxnPoint, L5XName.InputCxnPoint);
            element.AddValue(obj.InputSize, L5XName.InputSize);
            element.AddValue(obj.OutputCxnPoint, L5XName.OutputCxnPoint);
            element.AddValue(obj.OutputSize, L5XName.OutputSize);
            element.AddValue(obj.Type, L5XName.Type);
            element.AddValue(obj.Priority, L5XName.Priority);
            element.AddValue(obj.InputConnectionType, L5XName.InputConnectionType);
            element.AddValue(obj.InputProductionTrigger, L5XName.InputProductionTrigger);
            element.AddValue(obj.OutputRedundantOwner, L5XName.OutputRedundantOwner);
            element.AddValue(obj.EventId, L5XName.EventId);
            element.AddValue(obj.InputTagSuffix, L5XName.InputTagSuffix);
            element.AddValue(obj.OutputTagSuffix, L5XName.OutputTagSuffix);

            if (obj.Input is not null)
            {
                var input = new XElement(L5XName.InputTag);
                input.AddValue(obj.Input.ExternalAccess, L5XName.ExternalAccess);
                input.Add(_dataSerializer.Serialize(obj.Input.Data));
                element.Add(input);
            }

            if (obj.Output is not null)
            {
                var output = new XElement(L5XName.OutputTag);
                output.AddValue(obj.Output.ExternalAccess, L5XName.ExternalAccess);
                output.Add(_dataSerializer.Serialize(obj.Output.Data));
                element.Add(output);
            }

            return element;
        }

        /// <inheritdoc />
        public ModuleConnection Deserialize(XElement element)
        {
            Check.NotNull(element);

            var input = element.Descendants(L5XName.InputTag).FirstOrDefault()?.Elements(L5XName.Data)
                .FirstOrDefault(e => e.Attribute(L5XName.Format)?.Value == DataFormat.Decorated);

            if (input is not null)
            {
                var inputData = _dataSerializer.Deserialize(input);
            }


            return new ModuleConnection
            {
                Name = element.LogixName(),
                Rpi = element.ValueOrDefault<int?>(L5XName.RPI) ?? default,
                InputCxnPoint = element.ValueOrDefault<ushort?>(L5XName.InputCxnPoint) ?? default,
                InputSize = element.ValueOrDefault<ushort?>(L5XName.InputSize) ?? default,
                OutputCxnPoint = element.ValueOrDefault<ushort?>(L5XName.OutputCxnPoint) ?? default,
                OutputSize = element.ValueOrDefault<ushort?>(L5XName.OutputSize) ?? default,
                Type = element.ValueOrDefault<ConnectionType>(L5XName.Type) ?? ConnectionType.Unknown,
                Priority = element.ValueOrDefault<ConnectionPriority>(L5XName.Priority) ?? ConnectionPriority.Scheduled,
                InputConnectionType = element.ValueOrDefault<TransmissionType>(L5XName.InputConnectionType) ??
                                      TransmissionType.Multicast,
                OutputRedundantOwner = element.ValueOrDefault<bool?>(L5XName.OutputRedundantOwner) ?? default,
                InputProductionTrigger = element.ValueOrDefault<ProductionTrigger>(L5XName.InputConnectionType) ??
                                         ProductionTrigger.Cyclic,
                Unicast = element.ValueOrDefault<bool?>(L5XName.Unicast) ?? default,
                EventId = element.ValueOrDefault<int?>(L5XName.EventId) ?? default,
                InputTagSuffix = element.ValueOrDefault<string>(L5XName.InputTagSuffix) ?? "I",
                OutputTagSuffix = element.ValueOrDefault<string>(L5XName.OutputTagSuffix) ?? "O",
            };
        }
    }
}