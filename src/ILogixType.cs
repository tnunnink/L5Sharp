using System.Collections.Generic;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp
{
    /// <summary>
    /// A base interface for all <c>Logix</c> built-in data type classes. 
    /// </summary>
    /// <remarks>
    /// <see cref="ILogixType"/> is a construct that contains or represents the actual data that make up a given Logix
    /// data type or tag structure. There are several different base types that correspond to the tag value, structure,
    /// and array members of an L5X file.
    /// </remarks>
    /// <seealso cref="AtomicType"/>
    /// <seealso cref="StructureType"/>
    /// <seealso cref="ArrayType{TLogixType}"/>
    /// <seealso cref="StringType"/>
    public interface ILogixType
    {
        /// <summary>
        /// The name of the <c>Logix</c> type.
        /// </summary>
        /// <value>A <see cref="string"/> name identifying the logix type.</value>
        public string Name { get; }
        
        /// <summary>
        /// The family of the <c>Logix</c> type.
        /// </summary>
        /// <value>
        /// A <see cref="DataTypeFamily"/> option indicating the family for which the current type belongs.
        /// This is just string for string types and none for all others.
        /// </value>
        DataTypeFamily Family { get; }

        /// <summary>
        /// The class of the <c>Logix</c> type.
        /// </summary>
        /// <value>
        /// A <see cref="DataTypeClass"/> option indicating the class for which the current data type belongs.
        /// </value>
        DataTypeClass Class { get; }
        
        /// <summary>
        /// The collection of <see cref="Member"/> objects that make up the structure of the type.
        /// </summary>
        IEnumerable<Member> Members { get; }
    }
}