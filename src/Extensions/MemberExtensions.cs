namespace L5Sharp.Extensions
{
    public static class MemberExtensions
    {
        /// <summary>
        /// Determines if the current member is a dimensionless value type
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static bool IsValueMember(this IMember<IDataType> member)
        {
            return member.DataType is IAtomic && member.Dimensions.AreEmpty;
        }

        /// <summary>
        /// Determines if the current member is an array of value types.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static bool IsArrayValueMember(this IMember<IDataType> member)
        {
            return member.DataType is IAtomic && !member.Dimensions.AreEmpty;
        }
        
        /// <summary>
        /// Determines if the current member is an array of complex types.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static bool IsArrayStructureMember(this IMember<IDataType> member)
        {
            return !(member.DataType is IAtomic) && !member.Dimensions.AreEmpty;
        }
        
        /// <summary>
        /// Determines if the current member is a dimensionless comples type 
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static bool IsStructureMember(this IMember<IDataType> member)
        {
            return !(member.DataType is IAtomic) && member.Dimensions.AreEmpty;
        }
    }
}