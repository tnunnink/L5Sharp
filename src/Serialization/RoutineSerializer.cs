using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization;

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
            
        if (obj.Type == RoutineType.Rll)
            element.Add(_rllSerializer.Serialize(obj.As<Rung>()));
            
        if (obj.Type == RoutineType.St)
            element.Add(_stlSerializer.Serialize(obj.As<Line>()));

        return element;
    }

    /// <inheritdoc />
    public Routine Deserialize(XElement element)
    {
        Check.NotNull(element);
            
        var routine = new Routine
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
            
        IEnumerable<ILogixCode>? content = default;
            
        if (routine.Type == RoutineType.Rll && element.Element(L5XName.RLLContent) is not null)
            content = _rllSerializer.Deserialize(element.Element(L5XName.RLLContent)!);
            
        if (routine.Type == RoutineType.St && element.Element(L5XName.STContent) is not null)
            content = _stlSerializer.Deserialize(element.Element(L5XName.STContent)!);

        if (content is not null)
            routine.AddRange(content);
            
        return routine;
    }
}