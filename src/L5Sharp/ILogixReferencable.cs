using System.Collections.Generic;

namespace L5Sharp;

/// <summary>
/// Provides a common interface for Logix elements that can be referenced by other elements.
/// </summary>
public interface ILogixReferencable
{
    /// <summary>
    /// Returns a collection of <see cref="LogixReference"/> objects that indicate either references
    /// in or to the implementing object.
    /// </summary>
    /// <returns>
    /// A <see cref="IEnumerable{T}"/> of <see cref="LogixReference"/> objects with the relevant reference information.
    /// </returns>
    /// <remarks></remarks>
    IEnumerable<LogixReference> References();
}