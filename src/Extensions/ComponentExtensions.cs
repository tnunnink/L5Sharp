using L5Sharp.Serialization;

namespace L5Sharp.Extensions;

/// <summary>
/// Extension methods for <see cref="ILogixComponent"/> type objects.
/// </summary>
public static class ComponentExtensions
{
    /// <summary>
    /// Performs a deep clone of the current logix component and returns a new instance with same values.
    /// </summary>
    /// <param name="component">The component object to clone.</param>
    /// <typeparam name="TComponent">The logix component type.</typeparam>
    /// <returns>A new <see cref="ILogixComponent"/> of the specified type with same property values.</returns>
    public static TComponent Clone<TComponent>(this TComponent component) where TComponent : ILogixComponent
    {
        var element = LogixSerializer.Serialize(component);
        return LogixSerializer.Deserialize<TComponent>(element);
    }
}