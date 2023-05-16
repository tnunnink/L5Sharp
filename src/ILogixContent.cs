using System.Collections.Generic;
using L5Sharp.Components;
using L5Sharp.Repositories;
using L5Sharp.Serialization;

namespace L5Sharp;

/// <summary>
/// The primary API for interacting with the L5X content file.
/// </summary>
public interface ILogixContent
{
    /// <summary>
    /// Gets the single <see cref="Components.Controller"/> component in the L5X if onw exists.
    /// </summary>
    /// <returns>A <see cref="Components.Controller"/> object if found; otherwise, null.</returns>
    Controller Controller { get; }

    /// <summary>
    /// Returns a component repository for manipulating <see cref="DataType"/> components in the L5X file.
    /// </summary>
    /// <returns>A <see cref="ILogixComponentRepository{TComponent}"/> for <see cref="DataType"/> components.</returns>
    ILogixComponentRepository<DataType> DataTypes { get; }

    /// <summary>
    /// 
    /// </summary>
    ILogixComponentRepository<AddOnInstruction> Instructions { get; }

    /// <summary>
    /// Returns a component collection for querying and manipulating <see cref="Module"/> components
    /// in the L5X file.
    /// </summary>
    /// <returns>A <see cref="ILogixComponentRepository{TComponent}"/> for <see cref="Module"/> components</returns>
    ILogixComponentRepository<Module> Modules { get; }

    /// <summary>
    /// Returns a component collection for querying and manipulating <see cref="Program"/> components
    /// in the L5X file.
    /// </summary>
    /// <returns>A <see cref="ILogixComponentRepository{TComponent}"/> for <see cref="Program"/> components</returns>
    ILogixComponentRepository<Program> Programs { get; }

    /// <summary>
    /// Returns a scoped component collection for querying and manipulating <see cref="Routine"/> components
    /// in the L5X file.
    /// </summary>
    /// <returns>A <see cref="ILogixComponentRepository{TComponent}"/> for <see cref="Routine"/> components.</returns>
    ILogixScopedRepository<Routine> Routines { get; }

    /// <summary>
    /// Returns a component collection for querying and manipulating controller <see cref="Tag"/> components
    /// in the L5X file.
    /// </summary>
    /// <returns>A <see cref="ILogixComponentRepository{TComponent}"/> for <see cref="Tag"/> components</returns>
    /// <remarks>
    /// This collection returns controller scoped tags only.
    /// To access program specific tags, use <see cref="Tags(string)"/> and specify then program name.
    /// To query all tags in the L5X, use the <see cref="Find{TEntity}"/> method and specify <see cref="Tag"/> as
    /// the entity type.
    /// </remarks>
    ILogixTagRepository Tags { get; }

    /*/// <summary>
    /// Returns a scoped component collection for querying and manipulating <see cref="Routine"/> components
    /// in the L5X file.
    /// </summary>
    /// <returns>A <see cref="ILogixComponentRepository{TComponent}"/> for <see cref="Routine"/> components.</returns>
    ILogixComponentRepository<TRoutine> Routines<TRoutine>(string programName) where TRoutine : Routine;*/

    /// <summary>
    /// Returns a component collection for querying and manipulating <see cref="LogixTask"/> components
    /// in the L5X file.
    /// </summary>
    /// <returns>A <see cref="ILogixComponentRepository{TComponent}"/> for <see cref="LogixTask"/> components.</returns>
    ILogixComponentRepository<LogixTask> Tasks { get; }

    /// <summary>
    /// Returns all entities of the specified type found in the L5X file.
    /// </summary>
    /// <typeparam name="TEntity">The type that has a corresponding <see cref="ILogixSerializer{T}"/> defined and
    /// configured using the  <see cref="LogixSerializerAttribute"/>.</typeparam>
    /// <returns>An <see cref="IEnumerable{T}"/> containing object of the specified type.</returns>
    /// <remarks>This allows the user to query essentially any data structure. All that is needed is a corresponding
    /// <see cref="ILogixSerializer{T}"/> so we know how to materialize the specified type.</remarks>
    IEnumerable<TEntity> Find<TEntity>() where TEntity : class;
}