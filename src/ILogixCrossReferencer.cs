using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Utilities;

namespace L5Sharp;

public interface ILogixCrossReferencer
{
    IEnumerable<TagReference> Tag(TagName tagName, TagNameComparer? comparer = null);
    
    IEnumerable<TagReference> Tag(TagName tagName, SearchDepth depth, TagNameComparer? comparer = null);

    ILookup<TagName,IEnumerable<TagReference>> Tags(IEnumerable<TagName> tagNames);
    
    ILookup<TagName,IEnumerable<TagReference>> Tags(IEnumerable<TagName> tagNames, SearchDepth depth);
}