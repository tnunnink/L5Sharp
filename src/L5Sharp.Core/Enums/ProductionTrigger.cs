using L5Sharp.Components;
using L5Sharp.Elements;

namespace L5Sharp.Enums;

/// <summary>
/// An enumeration of all Logix <see cref="ProductionTrigger"/> options for a given <see cref="Connection"/>.
/// </summary>
public class ProductionTrigger : LogixEnum<ProductionTrigger, string>
{
    private ProductionTrigger(string name, string value) : base(name, value)
    {
    }
        
    /// <summary>
    /// Represents a Cyclic <see cref="ProductionTrigger"/> value.
    /// </summary>
    public static readonly ProductionTrigger Cyclic = new(nameof(Cyclic), nameof(Cyclic));
        
    /// <summary>
    /// Represents a COS <see cref="ProductionTrigger"/> value.
    /// </summary>
    public static readonly ProductionTrigger Cos = new(nameof(Cos), nameof(Cos));
        
    /// <summary>
    /// Represents a Application <see cref="ProductionTrigger"/> value.
    /// </summary>
    public static readonly ProductionTrigger Application = new(nameof(Application), nameof(Application));
}