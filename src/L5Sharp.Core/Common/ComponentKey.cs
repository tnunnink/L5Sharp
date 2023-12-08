using System;

namespace L5Sharp.Core;

/// <summary>
/// A composite key for a logix component that can be used to uniquely identify a component in the L5X file.
/// </summary>
/// <remarks>
/// This is an library construct that is primarily used for indexing and quick lookup of components
/// and their references within a give L5X file. Since components of different types can have the same name,
/// we consider both the component type and name to be the unique identifier.
/// </remarks>
public readonly struct ComponentKey : IEquatable<ComponentKey>
{
    private readonly string _type;
    private readonly string _name;

    /// <summary>
    /// Creates a new <see cref="ComponentKey"/> value type with the provided parameters.
    /// </summary>
    /// <param name="type">The logix type the component represents (DataType, Tag, etc.)</param>
    /// <param name="name">The base name of the component.</param>
    public ComponentKey(string type, string name)
    {
        _type = type ?? throw new ArgumentNullException(nameof(type));
        _name = name ?? throw new ArgumentNullException(nameof(name));
    }

    /// <summary>
    /// Determines if this key has the specified component name.
    /// </summary>
    /// <param name="name">The name of the component to test.</param>
    /// <returns><c>true</c> if the component key has the provided name; Otherwise, <c>false</c>.</returns>
    public bool HasName(string name) => string.Equals(_name, name, StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Determines if this key is of the specified component type name.
    /// </summary>
    /// <param name="type">The component type to check.</param>
    /// <returns><c>true</c> if the component key is of the provided type name; Otherwise, <c>false</c>.</returns>
    public bool IsType(string type) => _type.IsEquivalent(type);

    /// <summary>
    /// Determines if this key is of the specified component type parameter.
    /// </summary>
    /// <typeparam name="TComponent">The component type to check</typeparam>
    /// <returns><c>true</c> if the component key is of the provided type parameter; Otherwise, <c>false</c>.</returns>
    public bool IsType<TComponent>() where TComponent : LogixComponent =>
        _type.IsEquivalent(typeof(TComponent).L5XType());

    /// <inheritdoc />
    public bool Equals(ComponentKey other) =>
        StringComparer.OrdinalIgnoreCase.Equals(_type, other._type) &&
        StringComparer.OrdinalIgnoreCase.Equals(_name, other._name);

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is ComponentKey other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() =>
        StringComparer.OrdinalIgnoreCase.GetHashCode(_type) ^
        StringComparer.OrdinalIgnoreCase.GetHashCode(_name);

    /// <summary>
    /// Determines if two objects are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if the objects have the same type and name property; Otherwise, <c>false</c>.</returns>
    public static bool operator ==(ComponentKey left, ComponentKey right) => Equals(left, right);

    /// <summary>
    /// Determines if two objects are not equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if the objects have the same type and name property; Otherwise, <c>false</c>.</returns>
    public static bool operator !=(ComponentKey left, ComponentKey right) => !Equals(left, right);

    /// <inheritdoc />
    public override string ToString() => $"[{_type}]{_name}";
}