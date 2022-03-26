using L5Sharp.Core;
using L5Sharp.Creators;
using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// Represents a generic Logix member component that is a named instance of a specified <see cref="IDataType"/>
    /// that is a child of a given <see cref="IComplexType"/> or <see cref="ITag{TDataType}"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Members are used to define the structure of an <see cref="IComplexType"/>.
    /// Since each member holds a generic type reference to it's data type,
    /// the structure forms a hierarchical tree of nested members and types. The member's <see cref="Dimensions"/>,
    /// <see cref="Radix"/>, and <see cref="ExternalAccess"/> properties defined the configuration for a given member.
    /// Use the static factory <see cref="Member"/> class to create instances of IMember.
    /// </para>
    /// </remarks>
    /// <typeparam name="TDataType">The <see cref="IDataType"/> of the member.</typeparam>
    /// <seealso cref="ITagMember{TDataType}"/>
    /// <seealso cref="IParameter{TDataType}"/>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer>
    public interface IMember<out TDataType> : ILogixComponent where TDataType : IDataType
    {
        /// <summary>
        /// Gets the <see cref="IDataType"/> instance of the current member.
        /// </summary>
        /// <value>
        /// An instance of a <see cref="IDataType"/> that represents the type of the current member instance.
        /// Since each member contains a reference to it's type, a data type member hierarchy is formed.
        /// </value>
        TDataType DataType { get; }

        /// <summary>
        /// Gets the <see cref="Core.Dimensions"/> value of the current member.
        /// </summary>
        /// <remarks>
        /// Member dimensions can only be single dimensional. Any attempt to may a multidimensional member should fail.
        /// </remarks>
        Dimensions Dimensions { get; }

        /// <summary>
        /// Gets the <see cref="Enums.Radix"/> value of the current member.
        /// </summary>
        /// <value>
        /// For members of type <see cref="IAtomicType{T}"/>, returns the configured radix for that type.
        /// All members of type <see cref="IComplexType"/> return <see cref="Enums.Radix.Null"/>. 
        /// </value>
        Radix Radix { get; }

        /// <summary>
        /// Gets the <see cref="Enums.ExternalAccess"/> value of the current member.
        /// </summary>
        ExternalAccess ExternalAccess { get; }
        
        /// <summary>
        /// Gets the <see cref="Enums.MemberType"/> of the member, indicating whether the member is a value, array, structure,
        /// or string member.
        /// </summary>
        MemberType MemberType { get; }

        /// <summary>
        /// Indicates whether the current member instance is a value type member.
        /// </summary>
        /// <returns>
        /// true if the member data type is an <see cref="IAtomicType"/>; otherwise, false.
        /// </returns>
        bool IsValueMember { get; }

        /// <summary>
        /// Indicates whether the current member instance is an array type member.
        /// </summary>
        /// <returns>
        /// true if the member data type is an <see cref="IArrayType{TDataType}"/>; otherwise, false.
        /// </returns>
        bool IsArrayMember { get; }

        /// <summary>
        /// Indicates whether the current member instance is a structure or complex type member.
        /// </summary>
        /// <returns>
        /// true if the member data type is an <see cref="IComplexType"/>; otherwise, false.
        /// </returns>
        bool IsStructureMember { get; }
        
        /// <summary>
        /// Indicates whether the current member instance is a <see cref="IStringType"/>.
        /// </summary>
        /// <returns>
        /// true if the member data type is an <see cref="IStringType"/>; otherwise, false.
        /// </returns>
        bool IsStringMember { get; }
    }
}