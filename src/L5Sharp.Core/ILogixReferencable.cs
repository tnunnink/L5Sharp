using System.Collections.Generic;

namespace L5Sharp.Core;

/// <summary>
/// Provides a common interface for Logix elements that can be referenced by or refer to other elements. Implementing
/// this interface is signing the class up to determine it's references. 
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