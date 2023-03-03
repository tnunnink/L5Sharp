using System;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using L5Sharp.Serialization.Data;
using L5Sharp.Types;

namespace L5Sharp.Core
{
    /// <summary>
    /// ..
    /// </summary>
    /// <remarks>
    /// <para>
    /// Members are used to define the structure of an <see cref="StructureType"/>.
    /// Since each member holds a strongly typed reference to it's data type,
    /// the structure forms a hierarchical tree of nested members and types. The member's <see cref="Dimensions"/>,
    /// <see cref="Radix"/>, and <see cref="ExternalAccess"/> properties defined the configuration for a given member.
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
        /// Creates a new default <see cref="Member"/> object.
        /// </summary>
        public Member()
        {
        }
        
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
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The logix type of the <see cref="Member"/> instance.
        /// </summary>
        /// <returns>A <see cref="ILogixType"/> representing the member data type.</returns>
        public ILogixType DataType { get; set; } = Logix.Null;
    }
}