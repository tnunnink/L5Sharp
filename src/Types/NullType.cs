using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;

namespace L5Sharp.Types;

/// <summary>
/// Represents a null <see cref="ILogixType"/> implementation, or a type that is neither atomic, structure, array,
/// or string.
/// </summary>
/// <remarks>This would be the default for any tag that has not data type defined.</remarks>
// ReSharper disable once InconsistentNaming
public sealed class NullType : ILogixType
{
    private static readonly NullType Null = new();
        
    private NullType()
    {
    }

    /// <inheritdoc />
    public string Name => "NULL";

    /// <inheritdoc />
    public DataTypeFamily Family => DataTypeFamily.None;

    /// <inheritdoc />
    public DataTypeClass Class => DataTypeClass.Unknown;

    /// <inheritdoc />
    public IEnumerable<Member> Members => Enumerable.Empty<Member>();

    /// <summary>
    /// Gets the singleton instance of the <see cref="NullType"/> logix type.
    /// </summary>
    public static readonly NullType Instance = Null;
}