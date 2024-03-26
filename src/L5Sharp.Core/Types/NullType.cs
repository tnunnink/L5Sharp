using System.Collections.Generic;
using System.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents a null <see cref="LogixType"/> implementation, or a type that is neither atomic, structure, array,
/// or string.
/// </summary>
/// <remarks>This would be the default for any tag that has no data type defined.</remarks>
public sealed class NullType : LogixType
{
    private static readonly NullType Singleton = new();

    private NullType()
    {
    }

    /// <inheritdoc />
    public override string Name => "NULL";

    /// <inheritdoc />
    public override IEnumerable<Member> Members => Enumerable.Empty<Member>();

    /// <summary>
    /// Gets the singleton instance of the <see cref="NullType"/> logix type.
    /// </summary>
    public static readonly NullType Instance = Singleton;
}