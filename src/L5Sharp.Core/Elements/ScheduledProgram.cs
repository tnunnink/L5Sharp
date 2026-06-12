using System;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents a program scheduled to a task in a Logix controller.
/// </summary>
[LogixElement(L5XName.ScheduledProgram)]
public class ScheduledProgram : LogixObject<ScheduledProgram>
{
    /// <summary>
    /// Creates a new <see cref="ScheduledProgram"/> with the specified name.
    /// </summary>
    /// <param name="name">The name of the scheduled program.</param>
    public ScheduledProgram(string name) : base(L5XName.ScheduledProgram)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));

        Element.SetAttributeValue(L5XName.Name, name);
    }

    /// <summary>
    /// Creates a new <see cref="ScheduledProgram"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    public ScheduledProgram(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Gets or sets the name of the scheduled program.
    /// </summary>
    public string Name => GetRequiredValue();

    /// <summary>
    /// Determines whether the specified object is equal to the current <see cref="ScheduledProgram"/>.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;

        return obj switch
        {
            ScheduledProgram other => Name.IsEquivalent(other.Name),
            string name => Name.IsEquivalent(name),
            _ => false
        };
    }

    /// <summary>
    /// Returns the hash code for this <see cref="ScheduledProgram"/>.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        return StringComparer.OrdinalIgnoreCase.GetHashCode(Name);
    }

    /// <summary>
    /// Determines whether two <see cref="ScheduledProgram"/> instances are equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns>true if the instances are equal; otherwise, false.</returns>
    public static bool operator ==(ScheduledProgram? left, ScheduledProgram? right) => Equals(left, right);

    /// <summary>
    /// Determines whether two <see cref="ScheduledProgram"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns>true if the instances are not equal; otherwise, false.</returns>
    public static bool operator !=(ScheduledProgram? left, ScheduledProgram? right) => !Equals(left, right);

    /// <summary>
    /// Defines an implicit conversion from a <see cref="string"/> to a <see cref="ScheduledProgram"/>.
    /// </summary>
    /// <param name="name">The name of the scheduled program to create.</param>
    /// <returns>A new instance of <see cref="ScheduledProgram"/> initialized with the provided name.</returns>
    public static implicit operator ScheduledProgram(string name) => new(name);
}