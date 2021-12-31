using System.Collections.Generic;
using L5Sharp.Core;

namespace L5Sharp
{
    /// <summary>
    /// Represents a Structured Text Routine content. 
    /// </summary>
    public interface IStructuredText : ILogixContent, IEnumerable<Line>
    {
        
    }
}