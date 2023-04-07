using System.Collections.Generic;
using L5Sharp.Serialization;

namespace L5Sharp.Components;

/// <summary>
/// A logix <c>QuickWatchList</c> component. Contains the properties that comprise the L5X QuickWatchList element.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[LogixSerializer(typeof(QuickWatchListSerializer))]
public class QuickWatchList : List<WatchTag> , ILogixComponent
{
    /// <inheritdoc />
    public QuickWatchList()
    {
    }

    /// <inheritdoc />
    public QuickWatchList(int capacity) : base(capacity)
    {
    }

    /// <inheritdoc />
    public QuickWatchList(IEnumerable<WatchTag> watchTags) : base(watchTags)
    {
    }
    
    /// <inheritdoc />
    public string Name { get; set; } = string.Empty;

    /// <inheritdoc />
    public string Description => string.Empty;
}