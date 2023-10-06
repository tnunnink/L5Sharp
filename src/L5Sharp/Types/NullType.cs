using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Types;

/// <summary>
/// Represents a null <see cref="L5Sharp.LogixType"/> implementation, or a type that is neither atomic, structure, array,
/// or string.
/// </summary>
/// <remarks>This would be the default for any tag that has no data type defined.</remarks>
// ReSharper disable once InconsistentNaming
public sealed class NullType : LogixType
{
    private static readonly NullType Singleton = new();

    private NullType()
    {
    }

    /// <inheritdoc />
    public override string Name => "NULL";

    /// <inheritdoc />
    public override DataTypeFamily Family => DataTypeFamily.None;

    /// <inheritdoc />
    public override DataTypeClass Class => DataTypeClass.Unknown;

    /// <inheritdoc />
    public override IEnumerable<LogixMember> Members => Enumerable.Empty<LogixMember>();

    /// <inheritdoc />
    public override XElement Serialize() => new(L5XName.Data, new XAttribute(L5XName.Format, DataFormat.Decorated));

    /// <summary>
    /// Gets the singleton instance of the <see cref="NullType"/> logix type.
    /// </summary>
    public static readonly NullType Instance = Singleton;
}