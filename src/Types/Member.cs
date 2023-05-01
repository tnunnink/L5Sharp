using System;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using L5Sharp.Serialization.Data;

namespace L5Sharp.Types;

/// <summary>
/// A component of a <see cref="ILogixType"/> that defines the structure or members of the type. 
/// </summary>
/// <remarks>
/// <para>
/// Members are used to define the structure of other <see cref="ILogixType"/> objects.
/// Since each member holds a strongly typed reference to it's data type,
/// the structure forms a hierarchical tree of nested members and types.
/// </para>
/// <para>
/// This class effectively maps to the DataValueMember, StructureMember, ArrayMember elements of the L5X tag data
/// structures. This class only defines, name and data type, since Dimension, Radix, and ExternalAccess are all either
/// members or the specific <see cref="ILogixType"/> set, or not inherent in the data structure when serialized or
/// deserialized.
/// </para>
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[LogixSerializer(typeof(MemberSerializer))]
public class Member
{
    /// <summary>
    /// Creates a new <see cref="Member"/> object with the provided parameters.
    /// </summary>
    /// <param name="name">The name of the member.</param>
    /// <param name="logixType">The <see cref="ILogixType"/> object representing the member's data type data.</param>
    /// <exception cref="ArgumentNullException">name or datatype are null.</exception>
    public Member(string name, ILogixType logixType)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        DataType = logixType ?? throw new ArgumentNullException(nameof(logixType));
    }

    /// <summary>
    /// The name of the <see cref="Member"/> instance.
    /// </summary>
    /// <returns>A <see cref="string"/> representing the member name.</returns>
    public string Name { get; }

    /// <summary>
    /// The logix type of the <see cref="Member"/> instance.
    /// </summary>
    /// <returns>A <see cref="ILogixType"/> representing the member data type.</returns>
    public ILogixType DataType { get; }
}