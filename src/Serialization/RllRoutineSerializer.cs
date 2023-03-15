using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A logix serializer that performs serialization of <see cref="RllRoutine"/> components.
    /// </summary>
    public class RllRoutineSerializer : ILogixSerializer<RllRoutine>
    {
        private readonly RungSerializer _rungSerializer = new();
        
        /// <inheritdoc />
        public XElement Serialize(RllRoutine obj)
        {
            Check.NotNull(obj);

            var element = new XElement(L5XName.Routine);

            element.AddValue(obj, r => r.Name);
            element.AddText(obj, r => r.Description);
            element.AddValue(obj, r => r.Type);

            var content = new XElement(L5XName.RLLContent);
            content.Add(obj.Content.Select(r => _rungSerializer.Serialize(r)));
            element.Add(content);

            return element;
        }

        /// <inheritdoc />
        public RllRoutine Deserialize(XElement element)
        {
            Check.NotNull(element);

            return new RllRoutine
            {
                Name = element.LogixName(),
                Description = element.LogixDescription(),
                Content = element.Descendants(L5XName.Rung).Select(r => _rungSerializer.Deserialize(r)).ToList(),
                Scope = element.Ancestors(L5XName.Program).Any() ? Scope.Program
                    : element.Ancestors(L5XName.AddOnInstructionDefinition).Any() ? Scope.Instruction
                    : Scope.Controller,
                Container = element.Ancestors(L5XName.Program).Any()
                    ? element.Ancestors(L5XName.Program).FirstOrDefault()?.LogixName() ?? string.Empty
                    : element.Ancestors(L5XName.AddOnInstructionDefinition).Any()
                        ? element.Ancestors(L5XName.AddOnInstructionDefinition).FirstOrDefault()?.LogixName() ??
                          string.Empty
                        : element.Ancestors(L5XName.Controller).FirstOrDefault()?.LogixName() ?? string.Empty
            };
        }
    }
}