using System;

namespace L5Sharp.Core;

/// <summary>
/// Defines a builder interface for configuring and constructing tag properties within the system.
/// </summary>
/// <typeparam name="TBuilder">
/// The type of the builder implementation for method chaining and fluent configuration.
/// </typeparam>
public interface ITagBaseBuilder<out TBuilder> : ITagBuilder
{
    /// <summary>
    /// Sets the description for the tag being constructed.
    /// </summary>
    /// <param name="description">A textual description to be assigned to the tag.</param>
    /// <returns>An instance of the builder for further configuration.</returns>
    TBuilder WithDescription(string description);

    /// <summary>
    /// Sets the external access level for the tag being constructed.
    /// </summary>
    /// <param name="access">The external access level to be assigned, specifying read and write permissions.</param>
    /// <returns>An instance of the builder for further configuration.</returns>
    TBuilder WithAccess(ExternalAccess access);

    /// <summary>
    /// Marks the current atomic tag as a constant, preventing any runtime modifications to its value.
    /// </summary>
    /// <returns>An instance of the builder for further configuration.</returns>
    TBuilder Constant();

    /// <summary>
    /// Configures the atomic tag to have producer information based on the provided action.
    /// </summary>
    /// <param name="action">The action to configure the producer information using an <see cref="ITagProducerBuilder"/> instance.</param>
    /// <returns>An instance of the builder for further configuration.</returns>
    TBuilder Produces(Action<ITagProducerBuilder> action);

    /// <summary>
    /// Configures the tag to consume data according to the specified consumer configuration.
    /// </summary>
    /// <param name="action">A delegate that defines the consumer configuration by using an <see cref="ITagConsumerBuilder"/>.</param>
    /// <returns>An instance of the builder for further configuration.</returns>
    TBuilder Consumes(Action<ITagConsumerBuilder> action);
}