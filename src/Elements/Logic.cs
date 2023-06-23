using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Elements;

/*public class Logic : LogixElement<Logic>
{
    private NeutralText _value;
    
    public Logic(NeutralText text)
    {
    }

    public Logic(XElement element)
    {
        if (element is null) throw new ArgumentNullException(nameof(element));

        if (element.Name != L5XName.Line || element.Name != L5XName.Text)
            throw new InvalidOperationException();
        
        _value = new NeutralText(element.Value);

        if (element.Name == L5XName.Text)
        {
            _value =
        }
        _value = element.Name == L5XName.Line
        Number = element.Attribute(L5XName.Number)?.Value.Parse<int>() ??
                 element.Parent?.Attribute(L5XName.Number)?.Value.Parse<int>() ?? default;
        Scope = Scope.FromElement(element);
        Container = element.Ancestors(L5XName.Program).FirstOrDefault()?.LogixName() ?? string.Empty;
        Routine = element.Ancestors(L5XName.Routine).FirstOrDefault()?.LogixName() ?? string.Empty;
    }

    /// <summary>
    /// The number or index of the <see cref="Logic"/> position within it's containing <c>Routine</c>.
    /// </summary>
    /// <value>An <see cref="int"/> representing the index number of the code (rung, line, or sheet).</value>
    /// <remarks></remarks>
    public int Number { get; }

    /// <summary>
    /// The routine name that this <see cref="Logic"/> is contained within.
    /// </summary>
    /// <value>A <see cref="string"/> representing the name of the containing routine.</value>
    /// <remarks>
    /// This is only used in deserialization of a <see cref="Logic"/> component.
    /// This helper property makes it easier to filter code objects, such as rungs, structured text, and sheets.
    /// This property is not serialized back to an L5X file, so setting it effectively does nothing useful.
    /// </remarks>
    public string Routine { get; }
}*/