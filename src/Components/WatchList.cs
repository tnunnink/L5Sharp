using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;
using L5Sharp.Core;
using L5Sharp.Elements;

namespace L5Sharp.Components;

/// <summary>
/// A logix <c>WatchList</c> component. Contains the properties that comprise the L5X QuickWatchList element.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[XmlType(L5XName.QuickWatchList)]
public class WatchList : LogixComponent<WatchList>
{
    /// <inheritdoc />
    public WatchList()
    {
    }

    /// <inheritdoc />
    public WatchList(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WatchList"/> that contains tags copied from the provided collection.
    /// </summary>
    /// <param name="watchTags">The collection of <see cref="WatchTag"/> to initialize the list with.</param>
    public WatchList(IEnumerable<WatchTag> watchTags)
    {
    }
}