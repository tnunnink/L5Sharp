using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using L5Sharp.Serialization.Data;

namespace L5Sharp.Types;

/// <summary>
/// A <see cref="ILogixType"/> that represents a complex structure containing members of different types.
/// </summary>
/// <remarks>
/// <para>
/// This type is a building block for all <c>Predefined</c> data types. Inherit from this class to create custom
/// user defined data types that can be used to create in memory representation of the tags for those types.
/// </para>
/// <para>You can either provided members vis the protected constructor, or define the members as properties of the
/// sub class, in which case <see cref="StructureType"/> will generate <see cref="Members"/> using reflection.</para>
/// </remarks>
[LogixSerializer(typeof(StructureSerializer))]
public class StructureType : ILogixType
{
    private List<Member>? _members;

    /// <summary>
    /// Creates a new <see cref="StructureType"/> instance.
    /// </summary>
    /// <param name="name">The name of the type.</param>
    /// <exception cref="ArgumentNullException">name is null.</exception>
    protected StructureType(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    /// <summary>
    /// Creates a new <see cref="StructureType"/> instance.
    /// </summary>
    /// <param name="name">The name of the type.</param>
    /// <param name="members">The collection of <see cref="Member"/> that make up the type.</param>
    /// <exception cref="ArgumentNullException"><c>name</c> or <c>members</c> is null.</exception>
    public StructureType(string name, IEnumerable<Member> members)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        _members = members is not null ? members.ToList() : throw new ArgumentNullException(nameof(members));
    }

    /// <inheritdoc />
    public string Name { get; }

    /// <inheritdoc />
    public virtual DataTypeFamily Family => DataTypeFamily.None;

    /// <inheritdoc />
    public virtual DataTypeClass Class => DataTypeClass.Unknown;

    /// <summary>
    /// The collection of <see cref="Member"/> objects that compose the structure of the logix type.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="Member"/> objects.</returns>
    public IEnumerable<Member> Members => _members ??= FindMembers().ToList();

    /// <inheritdoc />
    public override string ToString() => Name;

    private IEnumerable<Member> FindMembers()
    {
        return GetType().GetProperties()
            .Where(p => typeof(ILogixType).IsAssignableFrom(p.PropertyType))
            .Select(p => new Member(p.Name, (ILogixType)p.GetValue(this)));
    }
}