using System;

namespace L5Sharp.Core;

/// <summary>
/// Defines a fluent interface for building and configuring <see cref="Tag"/> instances.
/// </summary>
public interface ITagBuilder
{
    /// <summary>
    /// Configures the tag as an alias that references another tag.
    /// </summary>
    /// <param name="alias">The <see cref="TagName"/> of the tag to be aliased.</param>
    /// <returns>The current <see cref="ITagBuilder"/> instance.</returns>
    ITagBuilder AliasFor(TagName alias);

    /// <summary>
    /// Sets the data value for the tag being constructed.
    /// </summary>
    /// <param name="value">The <see cref="LogixData"/> instance to assign as the tag's value.</param>
    /// <returns>The current <see cref="ITagBuilder"/> instance.</returns>
    ITagBuilder WithValue(LogixData value);

    /// <summary>
    /// Creates and sets a new instance of the specified data type for the tag being constructed.
    /// </summary>
    /// <typeparam name="TData">The type of <see cref="LogixData"/> to instantiate and assign.</typeparam>
    /// <returns>The current <see cref="ITagBuilder"/> instance.</returns>
    ITagBuilder WithValue<TData>() where TData : LogixData, new();

    /// <summary>
    /// Creates a new instance of the specified data type, configures it using the provided action, and sets it as the tag's value.
    /// </summary>
    /// <param name="config">An action delegate to configure the newly created data instance.</param>
    /// <typeparam name="TData">The type of <see cref="LogixData"/> to instantiate, configure, and assign.</typeparam>
    /// <returns>The current <see cref="ITagBuilder"/> instance.</returns>
    ITagBuilder WithValue<TData>(Action<TData> config) where TData : LogixData, new();

    /// <summary>
    /// Sets the description for the tag being constructed.
    /// </summary>
    /// <param name="description">A textual description to be assigned to the tag.</param>
    /// <returns>The current <see cref="ITagBuilder"/> instance.</returns>
    ITagBuilder WithDescription(string description);

    /// <summary>
    /// Sets the access level for the tag being constructed.
    /// </summary>
    /// <param name="access">The <see cref="Access"/> instance to assign as the tag's access level.</param>
    /// <returns>The current <see cref="ITagBuilder"/> instance.</returns>
    ITagBuilder WithAccess(Access access);

    /// <summary>
    /// Sets the external access level of the tag to read/write, allowing both read and write operations from external sources.
    /// </summary>
    /// <returns>The current <see cref="ITagBuilder"/> instance.</returns>
    ITagBuilder ReadWrite();

    /// <summary>
    /// Sets the external access level of the tag to read-only, allowing only read operations from external sources.
    /// </summary>
    /// <returns>The current <see cref="ITagBuilder"/> instance.</returns>
    ITagBuilder ReadOnly();

    /// <summary>
    /// Sets the usage type for the tag being constructed.
    /// </summary>
    /// <param name="usage">The <see cref="TagUsage"/> value indicating how the tag will be used (e.g., Input, Output, InOut).</param>
    /// <returns>The current <see cref="ITagBuilder"/> instance.</returns>
    ITagBuilder WithUsage(TagUsage usage);
    
    /// <summary>
    /// Sets the tag usage to <see cref="TagUsage.Normal"/>.
    /// </summary>
    /// <returns>The current <see cref="ITagBuilder"/> instance.</returns>
    ITagBuilder Normal();
    
    /// <summary>
    /// Sets the tag usage to <see cref="TagUsage.Local"/>.
    /// </summary>
    /// <returns>The current <see cref="ITagBuilder"/> instance.</returns>
    ITagBuilder Local();
    
    /// <summary>
    /// Sets the tag usage to <see cref="TagUsage.Public"/>.
    /// </summary>
    /// <returns>The current <see cref="ITagBuilder"/> instance.</returns>
    ITagBuilder Public();
    
    /// <summary>
    /// Sets the tag usage to <see cref="TagUsage.Input"/>.
    /// </summary>
    /// <returns>The current <see cref="ITagBuilder"/> instance.</returns>
    ITagBuilder Input();
    
    /// <summary>
    /// Sets the tag usage to <see cref="TagUsage.Output"/>.
    /// </summary>
    /// <returns>The current <see cref="ITagBuilder"/> instance.</returns>
    ITagBuilder Output();
    
    /// <summary>
    /// Sets the tag usage to <see cref="TagUsage.InOut"/>.
    /// </summary>
    /// <returns>The current <see cref="ITagBuilder"/> instance.</returns>
    ITagBuilder InOut();
    
    /// <summary>
    /// Sets the tag usage to <see cref="TagUsage.Static"/>.
    /// </summary>
    /// <returns>The current <see cref="ITagBuilder"/> instance.</returns>
    ITagBuilder Static();

    /// <summary>
    /// Marks the current tag as a constant.
    /// </summary>
    /// <returns>The current <see cref="ITagBuilder"/> instance.</returns>
    ITagBuilder Constant();

    /// <summary>
    /// Configures the tag to have producer information.
    /// </summary>
    /// <param name="config">The action to configure the producer information.</param>
    /// <returns>The current <see cref="ITagBuilder"/> instance.</returns>
    ITagBuilder Produces(Action<ITagProducerBuilder> config);

    /// <summary>
    /// Configures the tag to consume data.
    /// </summary>
    /// <param name="config">A delegate that defines the consumer configuration.</param>
    /// <returns>The current <see cref="ITagBuilder"/> instance.</returns>
    ITagBuilder Consumes(Action<ITagConsumerBuilder> config);

    /// <summary>
    /// Constructs and returns a configured <see cref="Tag"/> instance based on the provided builder configuration.
    /// </summary>
    /// <returns>A fully configured <see cref="Tag"/> instance.</returns>
    Tag Build();
}