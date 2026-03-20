using System;
using System.Collections.Generic;

namespace L5Sharp.Core;

/// <summary>
/// Represents a null <see cref="LogixData"/> implementation, or a type that is neither atomic, structure, array,
/// nor string.
/// </summary>
/// <remarks>This would be the default for any tag that has no data type defined.</remarks>
public sealed class NullData : LogixData
{
    private static readonly NullData Singleton = new();

    private NullData() : base(L5XName.Data)
    {
    }

    /// <inheritdoc />
    public override string Name => "NULL";

    /// <inheritdoc />
    public override IEnumerable<LogixMember> Members => [];

    /// <inheritdoc />
    public override byte[] ToBytes() => [];

    /// <inheritdoc />
    public override void UpdateData(LogixData data) =>
        throw new InvalidOperationException("Can not update data for the null data instance.");

    /// <summary>
    /// Gets the singleton instance of the <see cref="NullData"/> logix type.
    /// </summary>
    public static readonly NullData Instance = Singleton;
}