namespace L5Sharp.Core;

/// <summary>
/// Defines an interface for building and configuring references within a defined reference path.
/// Represents a builder pattern implementation that facilitates fluent configuration of references
/// with specified types, modules, or associated configurations.
/// </summary>
public interface IReferenceTypeBuilder : IReferenceBuilder
{
    /// <summary>
    /// Configures the reference to the specified <see cref="ReferenceType"/> within the current reference path.
    /// This method updates the reference type based on the provided type argument, enabling further customization.
    /// </summary>
    /// <param name="type">The <see cref="ReferenceType"/> to associate with the reference being built.</param>
    /// <returns>The updated <see cref="IReferenceLocationBuilder"/> instance to allow for further configuration.</returns>
    IReferenceLocationBuilder Type(ReferenceType type);

    /// <summary>
    /// Configures the reference to the specified DataType within the current reference path.
    /// </summary>
    /// <param name="name">The name of the DataType to associate with the reference being built.</param>
    /// <returns>The updated <see cref="IReferenceScopeBuilder"/> instance to allow for further configuration.</returns>
    IReferenceScopeBuilder DataType(string name);

    /// <summary>
    /// Configures the reference to the specified Module within the current reference path.
    /// </summary>
    /// <param name="name">The name of the Module to associate with the reference being built.</param>
    /// <returns>The updated <see cref="IReferenceScopeBuilder"/> instance to allow for further configuration.</returns>
    IReferenceScopeBuilder Module(string name);

    /// <summary>
    /// Configures the reference to the specified AOI within the current reference path.
    /// </summary>
    /// <param name="name">The name of the AOI to associate with the reference being built.</param>
    /// <returns>The updated <see cref="IReferenceScopeBuilder"/> instance to allow for further configuration.</returns>
    IReferenceScopeBuilder Aoi(string name);

    /// <summary>
    /// Configures the reference to the specified Program within the current reference path.
    /// </summary>
    /// <param name="name">The name of the Program to associate with the reference being built.</param>
    /// <returns>The updated <see cref="IReferenceScopeBuilder"/> instance to allow for further configuration.</returns>
    IReferenceScopeBuilder Program(string name);

    /// <summary>
    /// Configures the reference to the specified Task within the current reference path.
    /// </summary>
    /// <param name="name">The name of the Task to associate with the reference being built.</param>
    /// <returns>The updated <see cref="IReferenceScopeBuilder"/> instance to allow for further configuration.</returns>
    IReferenceScopeBuilder Task(string name);

    /// <summary>
    /// Configures the reference to the specified Tag within the current reference path.
    /// </summary>
    /// <param name="name">The name of the Tag to associate with the reference being built.</param>
    /// <returns>The updated <see cref="IReferenceScopeBuilder"/> instance to allow for further configuration.</returns>
    IReferenceScopeBuilder Tag(string name);

    /// <summary>
    /// Configures the reference to the specified Routine within the current reference path.
    /// </summary>
    /// <param name="name">The name of the Routine to associate with the reference being built.</param>
    /// <returns>The updated <see cref="IReferenceScopeBuilder"/> instance to allow for further configuration.</returns>
    IReferenceScopeBuilder Routine(string name);

    /// <summary>
    /// Configures the reference to the specified Rung within the current reference path.
    /// </summary>
    /// <param name="number">The number of the Rung to associate with the reference being built.</param>
    /// <returns>The updated <see cref="IReferenceScopeBuilder"/> instance to allow for further configuration.</returns>
    IReferenceScopeBuilder Rung(int number);

    /// <summary>
    /// Configures the reference to the specified Line within the current reference path.
    /// </summary>
    /// <param name="number">The number of the Line to associate with the reference being built.</param>
    /// <returns>The updated <see cref="IReferenceScopeBuilder"/> instance to allow for further configuration.</returns>
    IReferenceScopeBuilder Line(int number);

    /// <summary>
    /// Configures the reference to the specified Sheet within the current reference path.
    /// </summary>
    /// <param name="number">The number of the Line to associate with the reference being built.</param>
    /// <returns>The updated <see cref="IReferenceScopeBuilder"/> instance to allow for further configuration.</returns>
    IReferenceScopeBuilder Sheet(int number);
}