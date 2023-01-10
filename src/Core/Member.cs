using System;
using L5Sharp.Enums;
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
    public class Member
    {
        /// <summary>
        /// Creates a new <see cref="Member"/> instance with the provided parameters.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="dataType">The member <see cref="ILogixType"/>.</param>
        /// <param name="dimensions">The dimensions of the member.</param>
        /// <param name="radix">the radix format of the member value.</param>
        /// <param name="externalAccess">The external access value of the member.</param>
        /// <param name="description">The description of the member.</param>
        /// <exception cref="ArgumentNullException">name or datatype are null.</exception>
        public Member(string name, ILogixType dataType, Dimensions? dimensions = null, Radix? radix = null,
            ExternalAccess? externalAccess = null, string? description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            DataType = dataType ?? throw new ArgumentNullException(nameof(dataType));
            Dimensions = dimensions ?? Dimensions.Empty;
            Radix = radix ?? Radix.Default(DataType);
            ExternalAccess = externalAccess ?? ExternalAccess.ReadOnly;
            Description = description ?? string.Empty;
        }

        /// <summary>
        /// The name of the <see cref="Member"/> instance.
        /// </summary>
        /// <returns>A <see cref="string"/> representing the member name.</returns>
        public string Name { get; set; }

        /// <summary>
        /// The description of the <see cref="Member"/> instance.
        /// </summary>
        /// <returns>A <see cref="string"/> representing the member description.</returns>
        public string Description { get; set; }

        /// <summary>
        /// The datatype of the <see cref="Member"/> instance.
        /// </summary>
        /// <returns>A <see cref="ILogixType"/> representing the member data type.</returns>
        public ILogixType DataType { get; set; }

        /// <summary>
        /// The dimensions of the <see cref="Member"/> instance.
        /// </summary>
        /// <returns>A <see cref="Core.Dimensions"/> value representing the member array dimensions.</returns>
        public Dimensions Dimensions { get; set; }

        /// <summary>
        /// The radix format of the <see cref="Member"/> instance.
        /// </summary>
        /// <returns>A <see cref="Enums.Radix"/> enum representing the member data format.</returns>
        public Radix Radix { get; set; }

        /// <summary>
        /// The external access of the <see cref="Member"/> instance.
        /// </summary>
        /// <returns>An <see cref="Enums.ExternalAccess"/> enum representing the read/write access of the member.</returns>
        public ExternalAccess ExternalAccess { get; set; }

        /// <summary>
        /// The member type of the <see cref="Member"/> instance.
        /// </summary>
        /// <returns>An <see cref="Enums.MemberType"/> enum representing the type of the member.</returns>
        public MemberType MemberType => MemberType.FromMember(this);

        /// <summary>
        /// A generic static factory method to simplify member construction.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <typeparam name="TLogixType">The <see cref="ILogixType"/> of the member.
        /// Must have default parameterless constructor.</typeparam>
        /// <returns>A new <see cref="Member"/> instance.</returns>
        public static Member Create<TLogixType>(string name) where TLogixType : ILogixType, new() =>
            new(name, new TLogixType());

        /// <summary>
        /// A generic static factory method to simplify member construction.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="radix">The radix format of the member.</param>
        /// <typeparam name="TLogixType">The <see cref="ILogixType"/> of the member.
        /// Must have default parameterless constructor.</typeparam>
        /// <returns>A new <see cref="Member"/> instance.</returns>
        public static Member Create<TLogixType>(string name, Radix radix) where TLogixType : ILogixType, new() =>
            new(name, new TLogixType(), radix: radix);

        /// <summary>
        /// A static generic factory method to simplify member construction.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="dimensions">The dimensions of the member.</param>
        /// <typeparam name="TLogixType">The <see cref="ILogixType"/> of the member.
        /// Must have default parameterless constructor.</typeparam>
        /// <returns>A new <see cref="Member"/> instance.</returns>
        public static Member Create<TLogixType>(string name, Dimensions dimensions)
            where TLogixType : ILogixType, new() => new(name, new TLogixType(), dimensions);
    }
}