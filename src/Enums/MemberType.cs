using L5Sharp.Types;

namespace L5Sharp.Enums
{
    /// <summary>
    /// An enumeration of members types, which indicate whether a given <see cref="Member"/> is a value,
    /// array, structure, or string data type member.
    /// </summary>
    public class MemberType : LogixEnum<MemberType, int>
    {
        private MemberType(string name, int value) : base(name, value)
        {
        }

        /// <summary>
        /// Gets the <see cref="MemberType"/> from a given <see cref="ILogixType"/> object.
        /// </summary>
        /// <param name="type">The <see cref="ILogixType"/> instance use to determine the member type.</param>
        /// <returns>A <see cref="MemberType"/> enum based on the provided logix type.</returns>
        public static MemberType FromType(ILogixType? type)
        {
            return type switch
            {
                AtomicType => ValueMember,
                ILogixArray<ILogixType> => ArrayMember,
                StringType => StringMember,
                StructureType => StructureMember,
                _ => Unknown
            };
        }

        /// <summary>
        /// Represents a value <see cref="MemberType"/>, meaning the member data type is an <see cref="AtomicType"/>.
        /// </summary>
        public static readonly MemberType Unknown = new(nameof(Unknown), -1);

        /// <summary>
        /// Represents a value <see cref="MemberType"/>, meaning the member data type is an <see cref="AtomicType"/>.
        /// </summary>
        public static readonly MemberType ValueMember = new(nameof(ValueMember), 0);
        
        /// <summary>
        /// Represents a array <see cref="MemberType"/>, meaning the member has non empty dimensions.
        /// </summary>
        public static readonly MemberType ArrayMember = new(nameof(ArrayMember), 1);
        
        /// <summary>
        /// Represents a structure <see cref="MemberType"/>, meaning the member data type is an <see cref="StructureType"/>.
        /// </summary>
        public static readonly MemberType StructureMember = new(nameof(StructureMember), 2);
        
        /// <summary>
        /// Represents a structure <see cref="MemberType"/>, meaning the member data type is an <see cref="StringType"/>.
        /// </summary>
        public static readonly MemberType StringMember = new(nameof(StringMember), 3);
    }
}