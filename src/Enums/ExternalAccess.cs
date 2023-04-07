using System;

namespace L5Sharp.Enums;

/// <summary>
/// An enumeration of all Logix <see cref="ExternalAccess"/> options.
/// <remarks>
/// <see cref="ExternalAccess"/> is a Logix setting that determines the ability to read from or write to a given component.
/// </remarks>
/// </summary>
public sealed class ExternalAccess : LogixEnum<ExternalAccess, int>
{
    private ExternalAccess(string name, int value) : base(name, value)
    {
    }

    /// <summary>
    /// Gets the most restrictive of the two provided <see cref="ExternalAccess"/> objects.
    /// </summary>
    /// <param name="first">The first <c>ExternalAccess</c> object to compare.</param>
    /// <param name="second">The second <c>ExternalAccess</c> object to compare.</param>
    /// <returns>If first has a value less than second, then will return first;
    /// otherwise, second.</returns>
    /// <exception cref="ArgumentNullException">When either first or second is null.</exception>
    public static ExternalAccess MostRestrictive(ExternalAccess first, ExternalAccess second)
    {
        if (first is null)
            throw new ArgumentNullException(nameof(first));
            
        if (second is null)
            throw new ArgumentNullException(nameof(second));

        return first.Value < second.Value ? first : second;
    }

    /// <summary>
    /// Represents no read or write <see cref="ExternalAccess"/>.
    /// </summary>
    public static readonly ExternalAccess None = new(nameof(None), 0);

    /// <summary>
    /// Represents read but not write <see cref="ExternalAccess"/>.
    /// </summary>
    public static readonly ExternalAccess ReadOnly = new("Read Only", 1);

    /// <summary>
    /// Represents read and write <see cref="ExternalAccess"/>.
    /// </summary>
    public static readonly ExternalAccess ReadWrite = new("Read/Write", 2);
}