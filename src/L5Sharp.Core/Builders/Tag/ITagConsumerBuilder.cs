namespace L5Sharp.Core;

/// <summary>
/// Defines a builder for configuring and constructing a tag <see cref="ConsumeInfo"/>.
/// </summary>
public interface ITagConsumerBuilder
{
    /// <summary>
    /// Configures the provider to be used for the tag consumer.
    /// </summary>
    /// <param name="provider">The name of the provider to configure.</param>
    /// <returns>The current instance of <see cref="ITagConsumerBuilder"/> for chaining further configurations.</returns>
    public ITagConsumerBuilder Provider(string provider);

    /// <summary>
    /// Configures the remote tag to be associated with the tag consumer.
    /// </summary>
    /// <param name="remoteTag">The remote tag to configure, represented as an instance of <see cref="TagName"/>.</param>
    /// <returns>The current instance of <see cref="ITagConsumerBuilder"/> for chaining further configurations.</returns>
    public ITagConsumerBuilder RemoteTag(TagName remoteTag);

    /// <summary>
    /// Configures the Requested Packet Interval (RPI) for the tag consumer.
    /// </summary>
    /// <param name="rpi">The RPI value to configure, typically expressed in milliseconds.
    /// If not specified, the default value is 20 ms.</param>
    /// <returns>The current instance of <see cref="ITagConsumerBuilder"/> for chaining further configurations.</returns>
    public ITagConsumerBuilder RPI(double rpi);

    /// <summary>
    /// Configures the tag consumer to use unicast mode for communication.
    /// </summary>
    /// <returns>The current instance of <see cref="ITagConsumerBuilder"/> for chaining further configurations.</returns>
    public ITagConsumerBuilder Unicast();
}