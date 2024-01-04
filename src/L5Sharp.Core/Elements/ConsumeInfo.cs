using System;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A sub element of the <see cref="Tag"/> component that contains properties or configuration
/// consumer tag types.
/// </summary>
public class ConsumeInfo : LogixElement
{
    /// <summary>
    /// Creates a new <see cref="ConsumeInfo"/> with default values.
    /// </summary>
    public ConsumeInfo()
    {
    }

    /// <summary>
    /// Creates a new <see cref="ConsumeInfo"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public ConsumeInfo(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Specifies a remote device containing the data to consume.
    /// </summary>
    /// <value>A <see cref="string"/> representing the name of the remote device from which to consume data.</value>
    public string Producer
    {
        get => GetRequiredValue<string>();
        set => SetValue(value);
    }
    
    /// <summary>
    /// Specifies a remote tag name to consume.
    /// </summary>
    /// <value>A <see cref="TagName"/> representing the name of the remote tag to consume.</value>
    public TagName RemoteTag
    {
        get => GetRequiredValue<TagName>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specifies the remote instance of which to consume data.
    /// </summary>
    /// <value>A <see cref="int"/> representing the remote instance.</value>
    /// <remarks>This property appears to be serialized but is not available through the logix designer or documented.
    /// Will leave it here for know but am unsure what this does.</remarks>
    public int RemoteInstance
    {
        get => GetValue<int>();
        set => SetValue(value);
    }
    
    /// <summary>
    /// Specifies the amount of time between updates of data from the remote controller.
    /// </summary>
    /// <value>A <see cref="double"/> representing the request packet interval.</value>
    public double RPI
    {
        get => GetValue<double>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specifies to enable unicast connections from consumer of the tag.
    /// </summary>
    /// <value><c>true</c> when the tag is configured enable unicast; Otherwise, <c>false</c>.</value>
    public bool Unicast
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }
}