using System;
using System.Collections.Generic;

namespace L5Sharp.Core;

/// <summary>
/// An interface that defines an API for searching an L5X for <see cref="LogixScoped"/> objects having a specified
/// type, name, and optioanl controller/program/routine scope.
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
    /// <param name="scope">A <see cref="Scope"/> that identifies a path to a particular element in the L5X.</param>
    /// <returns>
    /// A collection of all elements found in the L5X having the specified type and name.
    /// This includes controller, program, and routine scoped elements.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="scope"/> is nul.</exception>
    IEnumerable<LogixScoped> Find(Scope scope);

    /// <summary>
    /// Finds all elements having the specified type and name across all scopes.
    /// </summary>
    /// <param name="scope">A <see cref="Scope"/> that identifies a path to a particular element in the L5X.</param>
    /// <typeparam name="TScoped">The type to find.</typeparam>
    /// <returns>
    /// A collection of all elements found in the L5X having the specified type and name.
    /// This includes controller, program, and routine scoped elements.
    /// </returns>
    IEnumerable<TScoped> Find<TScoped>(Scope scope) where TScoped : LogixScoped;

    /// <summary>
    /// Gets the element having the specified scope path and returns it as a generic <see cref="LogixScoped"/> object.
    /// </summary>
    /// <param name="scope">The path defining the route to the element in L5X tree. This can be
    /// a simple <c>/Type/Name</c> path or include containing program such as <c>/Program/Type/Name</c>.
    /// For more inforamtion see <see cref="Scope"/>.</param>
    /// <returns>
    /// A <see cref="LogixScoped"/> instance having the specified scope path.
    /// If no element with the provided path is found, then this method throws a <see cref="KeyNotFoundException"/>.
    /// </returns>
    /// <exception cref="KeyNotFoundException"> When no element with the specified <paramref name="scope"/> was found.</exception>
    /// <remarks>
    /// <para>
    /// Note that this overload requires the type to be specified as part of the scope path since there is no generic type
    /// information provided.
    /// </para>
    /// <para>
    /// <c>Get</c> methods imply you know the element at the provided path exists. If this is not the case, use one of the
    /// <c>TryGet</c> overloads.
    /// </para>
    /// </remarks>
    LogixScoped Get(Scope scope);

    /// <summary>
    /// Gets the element having the specified scope path and returns it as the specified type.
    /// </summary>
    /// <param name="scope">The path defining the route to the element in L5X tree. This can be
    /// a simple <c>/Type/Name</c> path or include containing program such as <c>/Program/Type/Name</c>.
    /// For more inforamtion see <see cref="Scope"/>.</param>
    /// <typeparam name="TScoped">The element type to find and return.</typeparam>
    /// <returns>
    /// A <see cref="LogixScoped"/> instance of the specified type having the specified scope path.
    /// If no element with the provided path is found, then this method throws a <see cref="KeyNotFoundException"/>.
    /// </returns>
    /// <exception cref="KeyNotFoundException"> When no element with the specified <paramref name="scope"/> was found.</exception>
    /// <exception cref="InvalidCastException"> When the element can't be cast to the type specified by <c>TScoped</c>.</exception>
    /// <remarks>
    /// <para>
    /// If no type is provided as part of the scope, then the type will be infered from <c>TScoped</c>.
    /// </para>
    /// <para>
    /// <c>Get</c> methods imply you know the element at the provided path exists. If this is not the case, use one of the
    /// <c>TryGet</c> overloads.
    /// </para>
    /// </remarks>
    TScoped Get<TScoped>(Scope scope) where TScoped : LogixScoped;

    /// <summary>
    /// Gets the element having the specified scope path and returns it as a generic <see cref="LogixScoped"/> object.
    /// </summary>
    /// <param name="builder">A fluent scope builder interface that defines the path to a particular element in the L5X.</param>
    /// <returns>
    /// A <see cref="LogixScoped"/> instance of the specified type having the specified scope path.
    /// If no element with the provided path is found, then this method throws a <see cref="KeyNotFoundException"/>.
    /// </returns>
    /// <exception cref="KeyNotFoundException">When no element having the configured scope was found.</exception>
    /// <remarks>
    /// <para>
    /// <c>Get</c> methods imply you know the element at the provided path exists. If this is not the case, use one of the
    /// <c>TryGet</c> overloads.
    /// </para>
    /// </remarks>
    LogixScoped Get(Func<IScopeBuilder, Scope> builder);

    /// <summary>
    /// Gets the element having the specified scope path and returns it as the specified type.
    /// </summary>
    /// <param name="builder">A fluent scope builder interface that defines the path to a particular element in the L5X.</param>
    /// <typeparam name="TScoped">The element type to find and return.</typeparam>
    /// <returns>
    /// A <see cref="LogixScoped"/> instance of the specified type having the specified scope path.
    /// If no element with the provided path is found, then this method throws a <see cref="KeyNotFoundException"/>.
    /// </returns>
    /// <exception cref="KeyNotFoundException">When no element having the configured scope was found.</exception>
    /// <exception cref="InvalidCastException">When the element can't be cast to the type specified by <c>TScoped</c>.</exception>
    /// <remarks>
    /// <para>
    /// <c>Get</c> methods imply you know the element at the provided path exists. If this is not the case, use one of the
    /// <c>TryGet</c> overloads.
    /// </para>
    /// </remarks>
    TScoped Get<TScoped>(Func<IScopeBuilder, Scope> builder) where TScoped : LogixScoped;

    /// <summary>
    /// Tries to get an element with the specified scope path and returns the result.
    /// </summary>
    /// <param name="scope">The path defining the route to the element in L5X tree. This can be
    /// a simple <c>/Type/Name</c> path or include containing program such as <c>/Program/Type/Name</c>.
    /// For more inforamtion see <see cref="Scope"/>.</param>
    /// <param name="element">The <see cref="LogixScoped"/> object that was found if an element with the provided scope exists.
    /// If not found, then this value will be null.</param>
    /// <returns>
    /// <c>true</c> if the element with the specified scope was found; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// Note that this overload requires the type to be specified as part of the scope path since there is no generic type
    /// information provided.
    /// </para>
    /// <para>
    /// <c>TryGet</c> methods imply you don't know the element at the provided path exists, as it will check and then
    /// return the result. This is in contrast to the <c>Get</c> overloads, which will throw an exception if not found.
    /// </para>
    /// </remarks>
    bool TryGet(Scope scope, out LogixScoped element);

    /// <summary>
    /// Tries to get an element with the specified scope path and returns the result.
    /// </summary>
    /// <param name="scope">The path defining the route to the element in L5X tree. This can be
    /// a simple <c>/Type/Name</c> path or include containing program such as <c>/Program/Type/Name</c>.
    /// For more inforamtion see <see cref="Scope"/>.</param>
    /// <param name="element">The <see cref="LogixScoped"/> object of the specified typ that was found if an
    /// element with the provided scope exists. If not found, then this value will be null.</param>
    /// <typeparam name="TScoped">The element type to find and return.</typeparam>
    /// <returns>
    /// <c>true</c> if the element with the specified scope was found; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// If no type is provided as part of the scope, then the type will be infered from <c>TScoped</c>.
    /// </para>
    /// <para>
    /// <c>TryGet</c> methods imply you don't know the element at the provided path exists, as it will check and then
    /// return the result. This is in contrast to the <c>Get</c> overloads, which will throw an exception if not found.
    /// </para>
    /// </remarks>
    bool TryGet<TScoped>(Scope scope, out TScoped element) where TScoped : LogixScoped;

    /// <summary>
    /// Tries to get an element with the specified scope path and returns the result.
    /// </summary>
    /// <param name="builder">A fluent scope builder interface that defines the path to a particular element in the L5X.</param>
    /// <param name="element">The <see cref="LogixScoped"/> object that was found if an element with the provided scope exists.
    /// If not found, then this value will be null.</param>
    /// <returns>
    /// <c>true</c> if the element with the specified scope was found; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// <c>TryGet</c> methods imply you don't know the element at the provided path exists, as it will check and then
    /// return the result. This is in contrast to the <c>Get</c> overloads, which will throw an exception if not found.
    /// </para>
    /// </remarks>
    bool TryGet(Func<IScopeBuilder, Scope> builder, out LogixScoped element);

    /// <summary>
    /// Tries to get an element with the specified scope path and returns the result.
    /// </summary>
    /// <param name="builder">A fluent scope builder interface that defines the path to a particular element in the L5X.</param>
    /// <param name="element">The <see cref="LogixScoped"/> object of the specified typ that was found if an
    /// element with the provided scope exists. If not found, then this value will be null.</param>
    /// <typeparam name="TScoped">The element type to find and return.</typeparam>
    /// <returns>
    /// <c>true</c> if the element with the specified scope was found; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// <c>TryGet</c> methods imply you don't know the element at the provided path exists, as it will check and then
    /// return the result. This is in contrast to the <c>Get</c> overloads, which will throw an exception if not found.
    /// </para>
    /// </remarks>
    bool TryGet<TScoped>(Func<IScopeBuilder, Scope> builder, out TScoped element) where TScoped : LogixScoped;

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