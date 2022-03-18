using System.Collections.Generic;
using L5Sharp.Core;

namespace L5Sharp.Repositories
{
    public interface IRungRepository
    {
        IEnumerable<Rung> InRoutine(TagName tagName);
        
        IEnumerable<Rung> WithTagName(TagName tagName);
    }
}