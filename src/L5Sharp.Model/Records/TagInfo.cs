using L5Sharp.Core;

namespace L5Sharp.Model;

/// <summary>
/// A record structure for the <see cref="Tag"/> component.
/// </summary>
public record TagInfo()
{
    public TagInfo(Tag tag) : this()
    {
        Name = tag.TagName;
        Description = tag.Description;
        DataType = tag.DataType;
        Dimensions = tag.Dimensions.ToString();
        Radix = tag.Radix.Name;
        Access = tag.ExternalAccess?.Name ?? ExternalAccess.ReadWrite;
        Value = tag.Value is AtomicData atomic ? atomic.ToValue() : null;
        Type = tag.TagType?.Name ?? TagType.Base;
        Usage = tag.Usage?.Name ?? TagUsage.Normal;
        Class = tag.Class?.Name;
        Constant = tag.Constant ?? false;
        Alias = tag.AliasFor;
        Scope = tag.Scope.Level.Name;
        Container = tag.Scope.Container;
        Members = tag.Members().Select(x => new TagInfo(x)).ToArray();
    }

    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; } = string.Empty;
    public string DataType { get; init; } = string.Empty;
    public string Dimensions { get; init; } = Core.Dimensions.Empty.ToString();
    public string Radix { get; init; } = Core.Radix.Decimal;
    public string Access { get; init; } = ExternalAccess.ReadWrite;
    public ValueType? Value { get; init; }
    public string Type { get; init; } = TagType.Base;
    public string Usage { get; init; } = TagUsage.Normal;
    public string? Class { get; init; }
    public bool Constant { get; init; }
    public string? Alias { get; init; }
    public string Scope { get; init; } = ScopeLevel.None;
    public string Container { get; init; } = string.Empty;
    public TagInfo[] Members { get; init; } = [];

    /// <summary>
    /// Defines an implicit conversion from a <see cref="Tag"/> object to a <see cref="TagInfo"/> object.
    /// </summary>
    /// <param name="tag">The <see cref="Tag"/> instance to be converted.</param>
    /// <returns>A new <see cref="TagInfo"/> object containing the converted program information.</returns>
    public static implicit operator TagInfo(Tag tag) => new(tag);
}