using System.Xml.Linq;
using L5Sharp.Enums;

namespace L5Sharp;

/// <summary>
/// A base class for all common logix components that are defined uniquely by name.
/// </summary>
/// <typeparam name="TComponent">The type implementing <see cref="ILogixComponent"/>.</typeparam>
public abstract class LogixComponent<TComponent> : LogixEntity<TComponent>, ILogixComponent
    where TComponent : LogixEntity<TComponent>, ILogixComponent
{
    /// <inheritdoc />
    protected LogixComponent()
    {
    }

    /// <inheritdoc />
    protected LogixComponent(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    public virtual Use? Use
    {
        get => GetValue<Use>();
        set => SetValue(value);
    }

    /// <inheritdoc />
    public string Name
    {
        get => GetValue<string>() ?? string.Empty;
        set => SetValue(value);
    }

    /// <inheritdoc />
    public string Description
    {
        get => GetProperty<string>() ?? string.Empty;
        set => SetProperty(value);
    }
}