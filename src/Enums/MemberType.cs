using Ardalis.SmartEnum;
using L5Sharp.Core;
using L5Sharp.Types;

namespace L5Sharp.Enums
{
    /// <summary>
    /// An enumeration of members types, which indicate whether a given <see cref="Member"/> is a value,
    /// array, structure, or string data type member.
    /// </summary>
    public class MemberType : SmartEnum<MemberType>
    {
        private MemberType(string name, int value) : base(name, value)
        {
        }

        /// <summary>
        /// Returns the <see cref="MemberType"/> of the provided <see cref="Member"/> instance.
        /// </summary>
        /// <param name="member"></param>
        /// <returns>A <see cref="MemberType"/> value based on the derived type of dataType.</returns>
        /// <remarks>
        /// </remarks>
        public static MemberType FromMember(Member member)
        {
            if (member.Dimensions.IsMultiDimensional)
                return ArrayMember;

            if (member.DataType.Family == DataTypeFamily.String)
                return StringMember;
            
            return member.DataType switch
            {
                AtomicType => ValueMember,
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
        /// Represents a structure <see cref="MemberType"/>, meaning the member data type is an <see cref=""/>.
        /// </summary>
        public static readonly MemberType StringMember = new(nameof(StringMember), 3);
    }
}