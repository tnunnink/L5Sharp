using System.Collections.Generic;
using L5Sharp.Common;

namespace L5Sharp;

/// <summary>
/// Provides a common interface for Logix elements that can be referenced by other elements.
/// </summary>
public interface ILogixReferencable
{
    /// <summary>
    /// Returns a collection of <see cref="CrossReference"/> objects that indicate either references
    /// in or to the implementing object.
    /// </summary>
    /// <returns>
    /// A <see cref="IEnumerable{T}"/> of <see cref="CrossReference"/> objects with the relevant reference information.
    /// </returns>
    /// <remarks></remarks>
    IEnumerable<CrossReference> References();
}