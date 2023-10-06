using System;
using L5Sharp.Components;

namespace L5Sharp.Enums;

/// <summary>
/// Represents an enumeration of all <see cref="TriggerOperation"/> options for a given <see cref="Trend"/>.
/// </summary>
public class TriggerOperation : LogixEnum<TriggerOperation, int>
{
    private TriggerOperation(string name, int value) : base(name, value)
    {
    }

    /// <summary>
    /// Parses a string number into the corresponding <see cref="TriggerOperation"/>.
    /// </summary>
    /// <param name="value">The string integer number to parse.</param>
    /// <returns>A <see cref="TriggerOperation"/> value.</returns>
    /// <exception cref="FormatException"><c>value</c> is not a parsable integer.</exception>
    public static TriggerOperation Parse(string value)
    {
        if (!int.TryParse(value, out var number))
            throw new FormatException($"Could not parse Trigger Operation value {value}");

        return FromValue(number);
    }

    /// <summary>
    /// Represents a ExactEqual <see cref="TriggerOperation"/> value.
    /// </summary>
    public static readonly TriggerOperation ExactEqual = new(nameof(ExactEqual), 0);

    /// <summary>
    /// Represents a TriggerLevel <see cref="TriggerOperation"/> value.
    /// </summary>
    public static readonly TriggerOperation TriggerLevel = new(nameof(TriggerLevel), 1);

    /// <summary>
    /// Represents a NotEqual <see cref="TriggerOperation"/> value.
    /// </summary>
    public static readonly TriggerOperation NotEqual = new(nameof(NotEqual), 2);
    
    /// <summary>
    /// Represents a LessThan <see cref="TriggerOperation"/> value.
    /// </summary>
    public static readonly TriggerOperation LessThan = new(nameof(LessThan), 3);
    
    /// <summary>
    /// Represents a GreaterThan <see cref="TriggerOperation"/> value.
    /// </summary>
    public static readonly TriggerOperation GreaterThan = new(nameof(GreaterThan), 4);
    
    /// <summary>
    /// Represents a LessThanOrEqualTo <see cref="TriggerOperation"/> value.
    /// </summary>
    public static readonly TriggerOperation LessThanOrEqualTo = new(nameof(LessThanOrEqualTo), 5);
    
    /// <summary>
    /// Represents a GreaterThanOrEqualTo <see cref="TriggerOperation"/> value.
    /// </summary>
    public static readonly TriggerOperation GreaterThanOrEqualTo = new(nameof(GreaterThanOrEqualTo), 6);
    
    /// <summary>
    /// Represents a PositiveSlope <see cref="TriggerOperation"/> value.
    /// </summary>
    public static readonly TriggerOperation PositiveSlope = new(nameof(PositiveSlope), 7);
    
    /// <summary>
    /// Represents a NegativeSlope <see cref="TriggerOperation"/> value.
    /// </summary>
    public static readonly TriggerOperation NegativeSlope = new(nameof(NegativeSlope), 8);
    
    /// <summary>
    /// Represents a BitwiseOR <see cref="TriggerOperation"/> value.
    /// </summary>
    public static readonly TriggerOperation BitwiseOR = new(nameof(BitwiseOR), 9);
    
    /// <summary>
    /// Represents a BitwiseOR <see cref="TriggerOperation"/> value.
    /// </summary>
    public static readonly TriggerOperation NotBitwiseOR = new(nameof(NotBitwiseOR), 10);
    
    /// <summary>
    /// Represents a BitwiseAND <see cref="TriggerOperation"/> value.
    /// </summary>
    public static readonly TriggerOperation BitwiseAND = new(nameof(BitwiseAND), 11);
    
    /// <summary>
    /// Represents a BitwiseAND <see cref="TriggerOperation"/> value.
    /// </summary>
    public static readonly TriggerOperation NotBitwiseAND = new(nameof(NotBitwiseAND), 12);
    
    /// <summary>
    /// Represents a BitwiseXOR <see cref="TriggerOperation"/> value.
    /// </summary>
    public static readonly TriggerOperation BitwiseXOR = new(nameof(BitwiseXOR), 13);
    
    /// <summary>
    /// Represents a BitwiseXOR <see cref="TriggerOperation"/> value.
    /// </summary>
    public static readonly TriggerOperation NotBitwiseXOR = new(nameof(NotBitwiseXOR), 14);
}