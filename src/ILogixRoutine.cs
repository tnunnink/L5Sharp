using System;
using System.Collections.Generic;

namespace L5Sharp;

/// <summary>
/// A base interface for <c>Routine</c> component which defines the members for interacting with the collection of
/// <see cref="ILogixCode"/> for the routine. 
/// </summary>
public interface ILogixRoutine : ILogixComponent, ILogixScoped, IList<ILogixCode>
{
    /// <summary>
    /// Adds the content of the specified collection to the end of the <see cref="ILogixRoutine"/>
    /// </summary>
    /// <param name="content">The collection whose elements should be added to the end of the <see cref="ILogixRoutine"/>.</param>
    /// <exception cref="ArgumentException"></exception>
    public void AddRange(IEnumerable<ILogixCode> content);
}