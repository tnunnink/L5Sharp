using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A logix serializer that performs serialization of <see cref="Routine"/> components.
    /// </summary>
    public class RoutineSerializer : ILogixSerializer<Routine>
    {
        /// <inheritdoc />
        public XElement Serialize(Routine obj)
        {
            Check.NotNull(obj);

            var element = new XElement(L5XName.Routine);

            element.AddValue(obj, r => r.Name);
            element.AddText(obj, r => r.Description);
            element.AddValue(obj, r => r.Type);

            return element;
        }

        /// <inheritdoc />
        public Routine Deserialize(XElement element)
        {
            Check.NotNull(element);

            return new Routine
            {
                Name = element.LogixName(),
                Description = element.LogixDescription(),
                Type = element.GetValue<RoutineType>(L5XName.Type),
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