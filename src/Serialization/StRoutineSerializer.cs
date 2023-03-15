using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization;

/// <summary>
/// A logix serializer that performs serialization of <see cref="StRoutine"/> components.
/// </summary>
public class StRoutineSerializer : ILogixSerializer<StRoutine>
{
    private readonly LineSerializer _lineSerializer = new();

    /// <inheritdoc />
    public XElement Serialize(StRoutine obj)
    {
        Check.NotNull(obj);

        var element = new XElement(L5XName.Routine);

        element.AddValue(obj, r => r.Name);
        element.AddText(obj, r => r.Description);
        element.AddValue(obj, r => r.Type);

        var content = new XElement(L5XName.STContent);
        content.Add(obj.Content.Select(l => _lineSerializer.Serialize(l)));
        element.Add(content);

        return element;
    }

    /// <inheritdoc />
    public StRoutine Deserialize(XElement element)
    {
        Check.NotNull(element);

        return new StRoutine
        {
            Name = element.LogixName(),
            Description = element.LogixDescription(),
            Content = element.Descendants(L5XName.Line).Select(e => _lineSerializer.Deserialize(e)).ToList(),
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