using System;
using System.Collections.Generic;

namespace L5Sharp.Core;

/// <summary>
/// An interface that defines an API for searching an L5X for <see cref="LogixObject"/>
/// having a specified name or scope.
/// </summary>
public interface ILogixLookup
{
    /// <summary>
    /// Determines if an element with the specified <see cref="Scope"/> path exists in the L5X.
    /// </summary>
    /// <param name="scope">A <see cref="Scope"/> that identifies a path to a particular element in the L5X.</param>
    /// <returns><c>true</c> if the element with the specified scope exists; otherwise, <c>false</c>.</returns>
    bool Contains(Scope scope);

    /// <summary>
    /// Determines if an element with the <see cref="Scope"/> configured by the provided builder exists in the L5X.
    /// </summary>
    /// <param name="builder">A fluent scope builder interface that defines the path to a particular element in the L5X.</param>
    /// <returns><c>true</c> if the element with the specified scope exists; otherwise, <c>false</c>.</returns>
    bool Contains(Func<IScopeBuilder, Scope> builder);

    /// <summary>
    /// Finds all elements having the type and name specified by the provided <see cref="Scope"/> path.
    /// </summary>
    /// <param name="scope">A fluent scope builder function that generates a specific type/name scope to search for.</param>
    /// <returns>
    /// A collection of all elements found in the L5X having the specified type and name.
    /// This includes controller, program, and routine scoped elements.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="scope"/> is nul.</exception>
    IEnumerable<LogixObject> Find(Scope scope);

    /// <summary>
    /// Finds all elements having the specified type and name across all scopes.
    /// </summary>
    /// <param name="scope">The component name or code number that identifies the element to find.</param>
    /// <typeparam name="TObject">The type to find.</typeparam>
    /// <returns>
    /// A collection of all elements found in the L5X having the specified type and name.
    /// This includes controller, program, and routine scoped elements.
    /// </returns>
    IEnumerable<TObject> Find<TObject>(Scope scope) where TObject : LogixObject;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scope"></param>
    /// <returns></returns>
    LogixObject Get(Scope scope);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scope"></param>
    /// <returns></returns>
    TObject Get<TObject>(Scope scope) where TObject : LogixObject;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    LogixObject Get(Func<IScopeBuilder, Scope> builder);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    TObject Get<TObject>(Func<IScopeBuilder, Scope> builder) where TObject : LogixObject;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scope"></param>
    /// <param name="element"></param>
    /// <returns></returns>
    bool TryGet(Scope scope, out LogixObject element);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scope"></param>
    /// <param name="element"></param>
    /// <returns></returns>
    bool TryGet<TObject>(Scope scope, out TObject element) where TObject : LogixObject;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="element"></param>
    /// <returns></returns>
    bool TryGet(Func<IScopeBuilder, Scope> builder, out LogixObject element);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="element"></param>
    /// <returns></returns>
    bool TryGet<TObject>(Func<IScopeBuilder, Scope> builder, out TObject element) where TObject : LogixObject;

    /// <summary>
    /// Retrieves a collection of <see cref="CrossReference"/> objects that reference the specified component name.
    /// </summary>
    /// <param name="name">The name of the component to retrieve references for.</param>
    /// <returns>A collection of <see cref="CrossReference"/> objects that reference the specified <paramref name="name"/>.</returns>
    /// <exception cref="ArgumentException">Throw if <paramref name="name"/> is null or empty.</exception>
    /// <remarks>
    /// <para>
    /// A cross-reference object contains information about the type and location of the object that has a reference
    /// to a given component. This library has a built-in mechanism for parsing and indexing both tag and logic references
    /// to various components for efficient lookup.
    /// </para>
    /// </remarks>
    IEnumerable<CrossReference> References(TagName name);
}