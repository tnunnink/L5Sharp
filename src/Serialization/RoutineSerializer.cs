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
    private readonly RungSerializer _rungSerializer = new();
    private readonly LineSerializer _lineSerializer = new();

    /// <inheritdoc />
    public XElement Serialize(Routine obj)
    {
        Check.NotNull(obj);

        var element = new XElement(L5XName.Routine);

        element.AddValue(obj, r => r.Name);
        element.AddText(obj, r => r.Description);
        element.AddValue(obj, r => r.Type);

        if (obj.Type == RoutineType.Rll)
        {
            var rll = new XElement(L5XName.RLLContent);
            rll.Add(obj.Content.Cast<Rung>().Select(r => _rungSerializer.Serialize(r)));
            element.Add(rll);
        }
        
        // ReSharper disable once InvertIf
        if (obj.Type == RoutineType.St)
        {
            var st = new XElement(L5XName.STContent);
            st.Add(obj.Content.Cast<Line>().Select(l => _lineSerializer.Serialize(l)));
            element.Add(st);
        }

        return element;
    }

    /// <inheritdoc />
    public Routine Deserialize(XElement element)
    {
        Check.NotNull(element);
        
        var type = element.GetValue<RoutineType>(L5XName.Type);
        
        var content = new List<ILogixCode>();

        if (type == RoutineType.Rll)
            content = element.Descendants(L5XName.Rung).Select(e => _rungSerializer.Deserialize(e))
                .Cast<ILogixCode>().ToList();
        
        if (type == RoutineType.St)
            content = element.Descendants(L5XName.Line).Select(e => _lineSerializer.Deserialize(e))
                .Cast<ILogixCode>().ToList();

        return new Routine
        {
            Name = element.LogixName(),
            Description = element.LogixDescription(),
            Type = type,
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