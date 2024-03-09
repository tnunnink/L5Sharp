namespace L5Sharp.Core;

/// <summary>
/// An enumeration of all <see cref="ComponentClass"/> for a various components. 
/// </summary>
public class ComponentClass : LogixEnum<ComponentClass, string>
{
    private ComponentClass(string name, string value) : base(name, value)
    {
    }
    
    /// <summary>
    /// Represents a <b>Standard</b> <see cref="ComponentClass"/> value.
    /// </summary>
    public static readonly ComponentClass Standard = new(nameof(Standard), nameof(Standard));

    /// <summary>
    /// Represents a <b>Safety</b> <see cref="ConnectionPriority"/> value.
    /// </summary>
    public static readonly ComponentClass Safety = new(nameof(Safety), nameof(Safety));
}