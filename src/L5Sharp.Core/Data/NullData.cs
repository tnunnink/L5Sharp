using System.Collections.Generic;
using System.Linq;

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
    public override IEnumerable<Member> Members => Enumerable.Empty<Member>();

    /// <summary>
    /// Gets the singleton instance of the <see cref="NullData"/> logix type.
    /// </summary>
    public static readonly NullData Instance = Singleton;
}