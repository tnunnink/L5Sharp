using System;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A tag element within a <see cref="WatchList"/> component.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public class WatchTag : LogixElement
{
    /// <summary>
    /// Creates a new <see cref="WatchTag"/> with default values.
    /// </summary>
    public WatchTag()
    {
        Specifier = TagName.Empty;
        ScopeName = string.Empty;
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
    /// <remarks>
    /// This property internally (in the L5X) is named Scope, but we are renaming it to ScopeName to avoid
    /// the property name issues. I wanted Scope to remain the same at the logix element level since more types will
    /// make use of that property.
    /// </remarks>
    public string ScopeName
    {
        get => GetRequiredValue<string>(nameof(Scope));
        set => SetRequiredValue(value, nameof(Scope));
    }
}