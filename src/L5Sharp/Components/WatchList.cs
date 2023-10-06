using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Elements;
using L5Sharp.Utilities;

namespace L5Sharp.Components;

/// <summary>
/// A logix <c>WatchList</c> component. Contains the properties that comprise the L5X QuickWatchList element.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[L5XType(L5XName.QuickWatchList)]
public class WatchList : LogixComponent<WatchList>
{
    /// <inheritdoc />
    public WatchList() : base(new XElement(L5XName.QuickWatchList))
    {
    }

    /// <inheritdoc />
    public WatchList(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Accesses a <see cref="WatchTag"/> at the specified index of the <see cref="WatchList"/>.
    /// </summary>
    /// <param name="index">The zer0-based index of the <see cref="WatchTag"/>.</param>
    public WatchTag this[int index]
    {
        get => new(Element.Elements(L5XName.WatchTag).ElementAt(index));
        set => Element.Elements(L5XName.WatchTag).ElementAt(index).ReplaceWith(value.Serialize());
    }

    /// <summary>
    /// Accesses a <see cref="WatchTag"/> of the <see cref="WatchList"/> with the specifier tag name.
    /// </summary>
    /// <param name="specifier">The tag specifier of the <see cref="WatchTag"/> to access.</param>
    public WatchTag this[string specifier]
    {
        get
        {
            var tag = Element.Elements(L5XName.WatchTag)
                .FirstOrDefault(e => e.Attribute(L5XName.Specifier)?.Value == specifier);
            return tag is not null ? new WatchTag(tag) : throw Element.L5XError(specifier!);
        }
        set
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            
            var tag = Element.Elements(L5XName.WatchTag)
                .FirstOrDefault(e => e.Attribute(L5XName.Specifier)?.Value == specifier);

            if (tag is null)
            {
                Element.Add(value.Serialize());
                return;
            }
            
            tag.ReplaceWith(value.Serialize());
        }
    }

    /// <summary>
    /// Adds a <see cref="WatchTag"/> to the end of the <see cref="WatchList"/>.
    /// </summary>
    /// <param name="tag">The tag to add.</param>
    /// <exception cref="ArgumentNullException"><c>tag</c> is null.</exception>
    public void Add(WatchTag tag)
    {
        if (tag is null) throw new ArgumentNullException(nameof(tag));
        Element.Add(tag.Serialize());
    }

    /// <summary>
    /// Removes the first <see cref="WatchTag"/> found in the <see cref="WatchList"/> with the provided specifier name.
    /// </summary>
    /// <param name="specifier">The tag specifier to remove.</param>
    public void Remove(string specifier)
    {
        if (string.IsNullOrEmpty(specifier)) throw new ArgumentNullException(nameof(specifier));
        
        Element.Elements(L5XName.WatchTag)
            .FirstOrDefault(e => e.Attribute(L5XName.Specifier)?.Value == specifier)?.Remove();
    }
}