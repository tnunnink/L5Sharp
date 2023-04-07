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
        private readonly RllSerializer _rllSerializer = new();
        private readonly StSerializer _stlSerializer = new();
        
        /// <inheritdoc />
        public XElement Serialize(Routine obj)
        {
            Check.NotNull(obj);

            var element = new XElement(L5XName.Routine);

            element.AddValue(obj, r => r.Name);
            element.AddText(obj, r => r.Description);
            element.AddValue(obj, r => r.Type);

            switch (obj.Content)
            {
                case Rll rll:
                    element.Add(_rllSerializer.Serialize(rll));
                    break;
                case St st:
                    element.Add(_stlSerializer.Serialize(st));
                    break;
            }

            return element;
        }

        /// <inheritdoc />
        public Routine Deserialize(XElement element)
        {
            Check.NotNull(element);

            var type = element.GetValue<RoutineType>(L5XName.Type);
            
            ILogixCode? content = default;
            
            if (type == RoutineType.Rll && element.Element(L5XName.RLLContent) is not null)
                content = _rllSerializer.Deserialize(element.Element(L5XName.RLLContent)!);
            
            if (type == RoutineType.St && element.Element(L5XName.STContent) is not null)
                content = _stlSerializer.Deserialize(element.Element(L5XName.STContent)!);

            return new Routine
            {
                Name = element.LogixName(),
                Description = element.LogixDescription(),
                Scope = element.Ancestors(L5XName.Program).Any() ? Scope.Program
                    : element.Ancestors(L5XName.AddOnInstructionDefinition).Any() ? Scope.Instruction
                    : Scope.Controller,
                Container = element.Ancestors(L5XName.Program).Any()
                    ? element.Ancestors(L5XName.Program).FirstOrDefault()?.LogixName() ?? string.Empty
                    : element.Ancestors(L5XName.AddOnInstructionDefinition).Any()
                        ? element.Ancestors(L5XName.AddOnInstructionDefinition).FirstOrDefault()?.LogixName() ??
                          string.Empty
                        : element.Ancestors(L5XName.Controller).FirstOrDefault()?.LogixName() ?? string.Empty,
                Content = content
            };
        }
    }
}