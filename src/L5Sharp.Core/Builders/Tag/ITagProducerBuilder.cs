namespace L5Sharp.Core;

/// <summary>
/// Defines a builder for configuring and constructing a tag <see cref="ProduceInfo"/>.
/// </summary>
public interface ITagProducerBuilder
{
    /// <summary>
    /// Configures the builder with the specified maximum number of consumer connected tags.
    /// </summary>
    /// <param name="maximum">The maximum number of tags to be produced.</param>
    /// <returns>An <see cref="ITagProducerBuilder"/> instance configured with the specified maximum count.</returns>
    ITagProducerBuilder WithMaxCount(int maximum);

    /// <summary>
    /// Configures the builder to enable the programmatic sending of event triggers for produced tags.
    /// </summary>
    /// <returns>An <see cref="ITagProducerBuilder"/> instance configured to enable programmatic event triggering.</returns>
    ITagProducerBuilder SendEventTrigger();

    /// <summary>
    /// Configures the builder to allow unicast communication for produced tags.
    /// </summary>
    /// <returns>An <see cref="ITagProducerBuilder"/> instance configured to permit unicast communication.</returns>
    ITagProducerBuilder Unicast();
}