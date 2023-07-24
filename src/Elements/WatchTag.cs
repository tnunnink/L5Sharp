using System;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Core;

namespace L5Sharp.Elements;

/// <summary>
/// A tag element within a <see cref="WatchList"/> component.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public sealed class WatchTag : LogixElement
{
    /// <summary>
    /// Creates a new <see cref="WatchTag"/> with default values.
    /// </summary>
    public WatchTag()
    {
        Specifier = TagName.Empty;
        Scope = string.Empty;
    }

    /// <summary>
    /// Creates a new <see cref="WatchTag"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public WatchTag(XElement element) : base(element)
    {
    }
    
    /// <summary>
    /// Specify the tag or part of a tag to watch.
    /// </summary>
    /// <value>A <see cref="string"/> representing the tag specifier.</value>
    public TagName Specifier
    {
        get => GetRequiredValue<TagName>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Specify the name of program, equipment phase, or Add-On Instruction that contains the watch tag.
    /// </summary>
    /// <value>A <see cref="string"/> representing the scope of the tag. If controller scope set to empty.</value>
    public string Scope
    {
        get => GetRequiredValue<string>();
        set => SetRequiredValue(value);
    }
}