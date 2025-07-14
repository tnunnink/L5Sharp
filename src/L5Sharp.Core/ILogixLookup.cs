using System;
using System.Collections.Generic;

namespace L5Sharp.Core;

/// <summary>
/// An interface that defines an API for searching an L5X for <see cref="LogixEntity"/> objects having a specified
/// reference path.
/// </summary>
public interface ILogixLookup
{
    /// <summary>
    /// Determines if an element with the specified <see cref="Reference"/> path exists in the L5X.
    /// </summary>
    /// <param name="reference">A <see cref="Reference"/> that identifies a path to a particular element in the L5X.</param>
    /// <returns><c>true</c> if the element with the specified reference exists; otherwise, <c>false</c>.</returns>
    bool Contains(Reference reference);

    /// <summary>
    /// Determines if an element with the <see cref="Reference"/> configured by the provided builder exists in the L5X.
    /// </summary>
    /// <param name="action">A fluent reference builder interface that defines the path to a particular element in the L5X.</param>
    /// <returns><c>true</c> if the element with the specified reference exists; otherwise, <c>false</c>.</returns>
    bool Contains(Action<IReferenceTypeBuilder> action);

    /// <summary>
    /// Gets the element having the specified reference path and returns it as a generic <see cref="LogixEntity"/> object.
    /// </summary>
    /// <param name="reference">The path defining the route to the element in L5X tree. This can be
    /// a simple <c>/Type/Name</c> path or include containing program such as <c>/Program/Type/Name</c>.
    /// For more information see <see cref="Reference"/>.</param>
    /// <returns>
    /// A <see cref="LogixEntity"/> instance having the specified reference path.
    /// If no element with the provided path is found, then this method throws a <see cref="KeyNotFoundException"/>.
    /// </returns>
    /// <exception cref="KeyNotFoundException"> When no element with the specified <paramref name="reference"/> was found.</exception>
    /// <remarks>
    /// Note that this overload requires the type to be specified as part of the reference path since there is no generic type
    /// information provided.
    /// </remarks>
    LogixEntity Get(Reference reference);

    /// <summary>
    /// Retrieves a component of the specified type and name from the L5X within an optional program scope.
    /// </summary>
    /// <param name="name">The name of the component to retrieve.</param>
    /// <param name="program">The optional name of the program containing the component or null to search globally.</param>
    /// <typeparam name="TComponent">The type of the component to retrieve, which must derive from <see cref="LogixComponent"/>.</typeparam>
    /// <returns>The retrieved component of type <typeparamref name="TComponent"/>.</returns>
    /// <remarks>
    /// This overload will build the reference to the component using the specified type, name, and optional
    /// container program.
    /// </remarks>
    TComponent Get<TComponent>(string name, string? program = null) where TComponent : LogixComponent;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="number"></param>
    /// <param name="program"></param>
    /// <param name="routine"></param>
    /// <typeparam name="TCode"></typeparam>
    /// <returns></returns>
    TCode Get<TCode>(int number, string program, string routine) where TCode : LogixCode;

    /// <summary>
    /// Gets the element having the specified reference path and returns it as a generic <see cref="LogixEntity"/> object.
    /// </summary>
    /// <param name="action">A fluent builder action that configures the reference path to retrieve.</param>
    /// <returns>
    /// A <see cref="LogixEntity"/> instance of the specified type having the specified reference path.
    /// If no element with the provided path is found, then this method throws a <see cref="KeyNotFoundException"/>.
    /// </returns>
    /// <exception cref="KeyNotFoundException">When no element having the configured reference was found.</exception>
    /// <remarks>
    /// <para>
    /// <c>Get</c> methods imply you know the element at the provided path exists. If this is not the case, use one of the
    /// <c>TryGet</c> overloads.
    /// </para>
    /// </remarks>
    LogixEntity Get(Action<IReferenceTypeBuilder> action);

    /// <summary>
    /// Gets the element having the specified reference path and returns it as the specified type.
    /// </summary>
    /// <param name="action">A fluent builder action that configures the reference path to retrieve.</param>
    /// <typeparam name="TEntity">The element type to find and return.</typeparam>
    /// <returns>
    /// A <see cref="LogixEntity"/> instance of the specified type having the specified reference path.
    /// If no element with the provided path is found, then this method throws a <see cref="KeyNotFoundException"/>.
    /// </returns>
    /// <exception cref="KeyNotFoundException">When no element having the configured reference was found.</exception>
    /// <exception cref="InvalidCastException">When the element can't be cast to the type specified by <c>TEntity</c>.</exception>
    /// <remarks>
    /// <para>
    /// <c>Get</c> methods imply you know the element at the provided path exists. If this is not the case, use one of the
    /// <c>TryGet</c> overloads.
    /// </para>
    /// </remarks>
    TEntity Get<TEntity>(Action<IReferenceLocationBuilder> action) where TEntity : LogixEntity;

    /// <summary>
    /// Tries to get an element with the specified reference path and returns the result.
    /// </summary>
    /// <param name="reference">The <see cref="Reference"/> path defining the route to the element in L5X tree.</param>
    /// <param name="entity">The <see cref="LogixEntity"/> object that was found if an element with the provided reference exists.
    /// If not found, then this value will be null.</param>
    /// <returns>
    /// <c>true</c> if the element with the specified reference was found; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// <c>TryGet</c> methods imply you don't know the element at the provided path exists, as it will check and then
    /// return the result. This is in contrast to the <c>Get</c> overloads, which will throw an exception if not found.
    /// </remarks>
    bool TryGet(Reference reference, out LogixEntity entity);

    /// <summary>
    /// Attempts to retrieve the specified component by name, returning a value that indicates whether the operation succeeded.
    /// </summary>
    /// <param name="name">The name of the component to retrieve.</param>
    /// <param name="compoenent">When this method returns, contains the component of type <typeparamref name="TComponent"/> if found; otherwise, the default value for this type.</param>
    /// <typeparam name="TComponent">The type of the component to retrieve, which must be a <see cref="LogixComponent"/>.</typeparam>
    /// <returns><c>true</c> if the component with the specified name exists and is successfully retrieved; otherwise, <c>false</c>.</returns>
    bool TryGet<TComponent>(string name, out TComponent compoenent) where TComponent : LogixComponent;

    /// <summary>
    /// Attempts to retrieve a <typeparamref name="TComponent"/> from the L5X based on the specified name and program.
    /// </summary>
    /// <param name="name">The name of the component to retrieve.</param>
    /// <param name="program">The program in which the component resides.</param>
    /// <param name="component">When this method returns, contains the component if found; otherwise, the default value for the type of the <paramref name="component"/> parameter.</param>
    /// <typeparam name="TComponent">The type of the component to retrieve, which must inherit from <see cref="LogixComponent"/>.</typeparam>
    /// <returns><c>true</c> if the component was found; otherwise, <c>false</c>.</returns>
    bool TryGet<TComponent>(string name, string program, out TComponent component) where TComponent : LogixComponent;

    /// <summary>
    /// Attempts to retrieve an instance of the specified <typeparamref name="TCode"/> type associated with the
    /// given number, program, and routine.
    /// </summary>
    /// <typeparam name="TCode">The type of <see cref="LogixCode"/> to retrieve.</typeparam>
    /// <param name="number">The unique numerical identifier for the desired code instance.</param>
    /// <param name="program">The name of the program containing the code instance.</param>
    /// <param name="routine">The name of the routine containing the code instance.</param>
    /// <param name="code">When this method returns, contains the retrieved <typeparamref name="TCode"/> instance
    /// if the search was successful; otherwise, <c>null</c>.</param>
    /// <returns><c>true</c> if the code instance was successfully retrieved; otherwise, <c>false</c>.</returns>
    bool TryGet<TCode>(int number, string program, string routine, out TCode code) where TCode : LogixCode;

    /// <summary>
    /// Tries to get an element with the specified reference path and returns the result.
    /// </summary>
    /// <param name="action">A fluent reference builder interface that defines the path to a particular element in the L5X.</param>
    /// <param name="entity">The <see cref="LogixEntity"/> object that was found if an element with the provided reference exists.
    /// If not found, then this value will be null.</param>
    /// <returns>
    /// <c>true</c> if the element with the specified reference was found; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// <c>TryGet</c> methods imply you don't know the element at the provided path exists, as it will check and then
    /// return the result. This is in contrast to the <c>Get</c> overloads, which will throw an exception if not found.
    /// </para>
    /// </remarks>
    bool TryGet(Action<IReferenceTypeBuilder> action, out LogixEntity entity);

    /// <summary>
    /// Tries to get an element with the specified reference path and returns the result.
    /// </summary>
    /// <param name="action">A fluent reference builder interface that defines the path to a particular element in the L5X.</param>
    /// <param name="entity">The <see cref="LogixEntity"/> object of the specified typ that was found if an
    /// element with the provided reference exists. If not found, then this value will be null.</param>
    /// <typeparam name="TEntity">The element type to find and return.</typeparam>
    /// <returns>
    /// <c>true</c> if the element with the specified reference was found; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// <c>TryGet</c> methods imply you don't know the element at the provided path exists, as it will check and then
    /// return the result. This is in contrast to the <c>Get</c> overloads, which will throw an exception if not found.
    /// </para>
    /// </remarks>
    bool TryGet<TEntity>(Action<IReferenceLocationBuilder> action, out TEntity entity) where TEntity : LogixEntity;
}