using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    /// <summary>
    /// An enumeration of members types, which indicate whether a given <see cref="IMember{TDataType}"/> is a value,
    /// array, structure, or string data type member.
    /// </summary>
    public class MemberType : SmartEnum<MemberType>
    {
        private MemberType(string name, int value) : base(name, value)
        {
        }

        /// <summary>
        /// Returns the <see cref="MemberType"/> of the provided <see cref="IDataType"/> instance.
        /// </summary>
        /// <param name="dataType">The <see cref="IDataType"/> for which to evaluate the member type.</param>
        /// <returns>A <see cref="MemberType"/> value based on the derived type of dataType.</returns>
        /// <remarks>
        /// Since <see cref="IStringType"/> inherits <see cref="IComplexType"/>, the string type member is evaluated
        /// first. Therefore, if the data type is a string type it will return <see cref="StringMember"/>
        /// (even through it is technically also a <see cref="StructureMember"/>). 
        /// </remarks>
        public static MemberType FromType(IDataType dataType)
        {
            return dataType switch
            {
                IAtomicType => ValueMember,
                IArrayType<IDataType> => ArrayMember,
                IStringType => StringMember,
                IComplexType => StructureMember,
                _ => Unknown
            };
        }
        
        /// <summary>
        /// Represents a value <see cref="MemberType"/>, meaning the member data type is an <see cref="IAtomicType"/>.
        /// </summary>
        public static readonly MemberType Unknown = new(nameof(Unknown), -1);

        /// <summary>
        /// Represents a value <see cref="MemberType"/>, meaning the member data type is an <see cref="IAtomicType"/>.
        /// </summary>
        public static readonly MemberType ValueMember = new(nameof(ValueMember), 0);
        
        /// <summary>
        /// Represents a array <see cref="MemberType"/>, meaning the member data type is an <see cref="IArrayType{T}"/>.
        /// </summary>
        public static readonly MemberType ArrayMember = new(nameof(ArrayMember), 1);
        
        /// <summary>
        /// Represents a structure <see cref="MemberType"/>, meaning the member data type is an <see cref="IComplexType"/>.
        /// </summary>
        public static readonly MemberType StructureMember = new(nameof(StructureMember), 2);
        
        /// <summary>
        /// Represents a structure <see cref="MemberType"/>, meaning the member data type is an <see cref="IStringType"/>.
        /// </summary>
        public static readonly MemberType StringMember = new(nameof(StringMember), 3);
    }
}