using System;
using System.Collections.Generic;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Types;

/// <summary>
/// A static factory for <see cref="AtomicType"/> objects.
/// </summary>
public static class Atomic
{
    private static readonly Dictionary<string, Func<string, AtomicType>> Atomics = new()
    {
        { nameof(BOOL), BOOL.Parse },
        { "BIT", BOOL.Parse },
        { nameof(SINT), SINT.Parse },
        { nameof(INT), INT.Parse },
        { nameof(DINT), DINT.Parse },
        { nameof(LINT), LINT.Parse },
        { nameof(REAL), REAL.Parse },
        { nameof(USINT), USINT.Parse },
        { nameof(UINT), UINT.Parse },
        { nameof(UDINT), UDINT.Parse },
        { nameof(ULINT), ULINT.Parse }
    };

    /// <summary>
    /// Parses the provided string value into an atomic type value.
    /// </summary>
    /// <param name="value">The value of the atomic type.</param>
    /// <returns>A <see cref="AtomicType"/> representing the value and format of the provided value.</returns>
    /// <exception cref="FormatException"><c>value</c> does not have a valid format to be parsed as an atomic type.</exception>
    public static AtomicType Parse(string value) => Radix.Infer(value).Parse(value);

    /// <summary>
    /// Parses the provided string value into the atomic type value specified by name.
    /// </summary>
    /// <param name="name">The name of the atomic type.</param>
    /// <param name="value">The value of the atomic type.</param>
    /// <returns>A <see cref="AtomicType"/> representing the value and format of the provided value.</returns>
    /// <exception cref="InvalidOperationException"><c>name</c> does not represent a valid atomic type.</exception>
    /// <exception cref="FormatException"><c>value</c> does not have a valid format to be parsed as the specified atomic type.</exception>
    public static AtomicType Parse(string name, string value)
    {
        if (!Atomics.ContainsKey(name))
            throw new InvalidOperationException($"The type name '{name}' is not a valid {typeof(AtomicType)}");

        return Atomics[name].Invoke(value);
    }

    /// <summary>
    /// Returns indication to whether the provided type name is the name of an atomic type.
    /// </summary>
    /// <param name="name">The type name to test.</param>
    /// <returns><c>true</c> if <c>name</c> is the name of any atomic type; otherwise, <c>false</c>.</returns>
    public static bool IsAtomic(string name) => Atomics.ContainsKey(name);
}