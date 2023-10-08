using System;

namespace L5Sharp.Common;

/// <summary>
/// A composite key for a logix component that can be used to uniquely identify a component in the L5X file.
/// 
/// </summary>
public readonly struct ComponentKey : IEquatable<ComponentKey>
{
    private readonly string _type;
    private readonly string _scope;
    private readonly string _name;
    
    /// <summary>
    /// Creates a new <see cref="ComponentKey"/> value type with the provided parameters.
    /// </summary>
    /// <param name="type">The logix type the component represents (DataType, Tag, etc.)</param>
    /// <param name="scope">The program or controller scope name of the component.</param>
    /// <param name="name">The base name of the component.</param>
    public ComponentKey(string type, string scope, string name)
    {
        _type = type;
        _scope = scope;
        _name = name;
    }

    /// <summary>
    /// Determines if this key has the specified component name.
    /// </summary>
    /// <param name="name">The name of the component to test.</param>
    /// <returns><c>true</c> if the component key has the provided name; Otherwise, <c>false</c>.</returns>
    public bool HasName(string name) => string.Equals(_name, name, StringComparison.OrdinalIgnoreCase);

    /// <inheritdoc />
    public bool Equals(ComponentKey other) =>
        StringComparer.OrdinalIgnoreCase.Equals(_type, other._type) &&
        StringComparer.OrdinalIgnoreCase.Equals(_scope, other._scope) &&
        StringComparer.OrdinalIgnoreCase.Equals(_name, other._name);

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is ComponentKey other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() =>
        StringComparer.OrdinalIgnoreCase.GetHashCode(_type) ^
        StringComparer.OrdinalIgnoreCase.GetHashCode(_scope) ^
        StringComparer.OrdinalIgnoreCase.GetHashCode(_name);

    /// <inheritdoc />
    public override string ToString() => $"[{_type}]{_scope}:{_name}";
}