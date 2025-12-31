using System;
using System.Xml.Linq;

// ReSharper disable InconsistentNaming

namespace L5Sharp.Core;

/// <summary>
/// Represents a predefined String Logix data type.
/// </summary>
[LogixData(nameof(STRING_32))]
public sealed class STRING_32 : StringData
{
    /// <inheritdoc />
    public STRING_32(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new empty <see cref="STRING"/> type.
    /// </summary>
    public STRING_32() : base(nameof(STRING_32), string.Empty)
    {
    }

    /// <summary>
    /// Creates a new <see cref="STRING"/> with the provided value.
    /// </summary>
    /// <param name="value">The string value.</param>
    /// <exception cref="ArgumentNullException"><c>value</c> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <c>value</c> length is greater than the predefined Logix string length of 82 characters.
    /// </exception>
    public STRING_32(string value) : base(nameof(STRING_32), value)
    {
    }

    /// <inheritdoc />
    public override int Capacity => 32;
}