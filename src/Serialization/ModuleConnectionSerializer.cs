using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Serialization.Data;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A logix serializer that performs serialization of <see cref="ModuleConnection"/> components.
    /// </summary>
    public class ModuleConnectionSerializer : ILogixSerializer<ModuleConnection>
    {
        /// <inheritdoc />
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
                
                if (obj.Input.Comments.Any())
                {
                    var comments = new XElement(L5XName.Comments);
                    comments.Add(obj.Input.Comments.Select(c =>
                    {
                        var comment = new XElement(L5XName.Comment);
                        comment.AddValue(c.Key, L5XName.Operand);
                        comment.Add(new XCData(c.Value));
                        return comment;
                    }));

                    input.Add(comments);
                }
                
                if (obj.Input.Units.Any())
                {
                    var units = new XElement(L5XName.EngineeringUnits);
                    units.Add(obj.Input.Units.Select(u =>
                    {
                        var unit = new XElement(L5XName.EngineeringUnit);
                        unit.AddValue(u.Key, L5XName.Operand);
                        unit.Add(new XCData(u.Value));
                        return unit;
                    }));

                    input.Add(units);
                }
                
                input.Add(TagDataSerializer.DecoratedData.Serialize(obj.Input.Data));
                element.Add(input);
            }

            if (obj.Output is not null)
            {
                var output = new XElement(L5XName.OutputTag);
                output.AddValue(obj.Output.ExternalAccess, L5XName.ExternalAccess);
                
                if (obj.Output.Comments.Any())
                {
                    var comments = new XElement(L5XName.Comments);
                    comments.Add(obj.Output.Comments.Select(c =>
                    {
                        var comment = new XElement(L5XName.Comment);
                        comment.AddValue(c.Key, L5XName.Operand);
                        comment.Add(new XCData(c.Value));
                        return comment;
                    }));

                    output.Add(comments);
                }
                
                if (obj.Output.Units.Any())
                {
                    var units = new XElement(L5XName.EngineeringUnits);
                    units.Add(obj.Output.Units.Select(u =>
                    {
                        var unit = new XElement(L5XName.EngineeringUnit);
                        unit.AddValue(u.Key, L5XName.Operand);
                        unit.Add(new XCData(u.Value));
                        return unit;
                    }));

                    output.Add(units);
                }
                
                output.Add(TagDataSerializer.DecoratedData.Serialize(obj.Output.Data));
                element.Add(output);
            }

            return element;
        }

        /// <inheritdoc />
        public ModuleConnection Deserialize(XElement element)
        {
            Check.NotNull(element);

            //first we try to deserialize the connection input and output tags since it is more involved.
            var inputSuffix = element.TryGetValue<string>(L5XName.InputTagSuffix) ?? "I";
            var input = DeserializeTag(element, L5XName.InputTag, inputSuffix);

            var outputSuffix = element.TryGetValue<string>(L5XName.OutputTagSuffix) ?? "O";
            var output = DeserializeTag(element, L5XName.OutputTag, outputSuffix);


            return new ModuleConnection
            {
                Name = element.LogixName(),
                Rpi = element.TryGetValue<int?>(L5XName.RPI) ?? default,
                InputCxnPoint = element.TryGetValue<ushort?>(L5XName.InputCxnPoint) ?? default,
                InputSize = element.TryGetValue<ushort?>(L5XName.InputSize) ?? default,
                OutputCxnPoint = element.TryGetValue<ushort?>(L5XName.OutputCxnPoint) ?? default,
                OutputSize = element.TryGetValue<ushort?>(L5XName.OutputSize) ?? default,
                Type = element.TryGetValue<ConnectionType>(L5XName.Type) ?? ConnectionType.Unknown,
                Priority = element.TryGetValue<ConnectionPriority>(L5XName.Priority) ?? ConnectionPriority.Scheduled,
                InputConnectionType = element.TryGetValue<TransmissionType>(L5XName.InputConnectionType) ??
                                      TransmissionType.Multicast,
                OutputRedundantOwner = element.TryGetValue<bool?>(L5XName.OutputRedundantOwner) ?? default,
                InputProductionTrigger = element.TryGetValue<ProductionTrigger>(L5XName.InputProductionTrigger) ??
                                         ProductionTrigger.Cyclic,
                Unicast = element.TryGetValue<bool?>(L5XName.Unicast) ?? default,
                EventId = element.TryGetValue<int?>(L5XName.EventId) ?? default,
                InputTagSuffix = element.TryGetValue<string>(L5XName.InputTagSuffix) ?? "I",
                OutputTagSuffix = element.TryGetValue<string>(L5XName.OutputTagSuffix) ?? "O",
                Input = input,
                Output = output
            };
        }

        private static Tag? DeserializeTag(XElement element, XName tagName, string suffix)
        {
            var tag = element.Descendants(tagName).FirstOrDefault();
            var data = tag?.Elements(L5XName.Data)
                .FirstOrDefault(e => e.Attribute(L5XName.Format)?.Value == DataFormat.Decorated)
                ?.Elements().First();

            if (tag is null || data is null) return null;

            return new Tag
            {
                Name = tag.ModuleTagName(suffix),
                Data = TagDataSerializer.DecoratedData.Deserialize(data),
                ExternalAccess = element.TryGetValue<ExternalAccess>(L5XName.ExternalAccess) ?? ExternalAccess.ReadWrite,
                Comments = element.Descendants(L5XName.Comment)
                    .ToDictionary(
                        k => k.GetValue<string>(L5XName.Operand),
                        e => e.Value),
                Units = element.Descendants(L5XName.EngineeringUnit)
                    .ToDictionary(
                        k => k.GetValue<string>(L5XName.Operand),
                        e => e.Value)
                
            };
        }
    }
}