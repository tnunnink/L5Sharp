using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Types;

/// <summary>
/// Represents a null <see cref="L5Sharp.LogixType"/> implementation, or a type that is neither atomic, structure, array,
/// or string.
/// </summary>
/// <remarks>This would be the default for any tag that has not data type defined.</remarks>
// ReSharper disable once InconsistentNaming
public sealed class NullType : L5Sharp.LogixType
{
    private static readonly NullType Null = new();

    private NullType() : base(new XElement(L5XName.Data))
    {
    }

    /// <inheritdoc />
    public override string Name => "NULL";

    /// <inheritdoc />
    public override IEnumerable<Member> Members => Enumerable.Empty<Member>();

    /// <summary>
    /// Gets the singleton instance of the <see cref="NullType"/> logix type.
    /// </summary>
    public static readonly NullType Instance = Null;
}