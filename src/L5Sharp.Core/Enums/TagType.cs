using L5Sharp.Components;

namespace L5Sharp.Enums;

/// <summary>
/// Represents an enumeration of Logix <see cref="TagType"/> options for a given <see cref="Tag"/>.
/// </summary>
public class TagType : LogixEnum<TagType, string>
{
    private TagType(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents a Base <see cref="TagType"/> value.
    /// </summary>
    public static readonly TagType Base = new(nameof(Base), nameof(Base));
        
    /// <summary>
    /// Represents a Alias <see cref="TagType"/> value.
    /// </summary>
    public static readonly TagType Alias = new(nameof(Alias), nameof(Alias));
        
    /// <summary>
    /// Represents a Produced <see cref="TagType"/> value.
    /// </summary>
    public static readonly TagType Produced = new(nameof(Produced), nameof(Produced));
        
    /// <summary>
    /// Represents a Consumed <see cref="TagType"/> value.
    /// </summary>
    public static readonly TagType Consumed = new(nameof(Consumed), nameof(Consumed));
}