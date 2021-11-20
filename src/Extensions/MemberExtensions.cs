namespace L5Sharp.Extensions
{
    public static class MemberExtensions
    {
        /// <summary>
        /// Determines if the current <see cref="IMember{TDataType}"/> is a dimensionless value type
        /// </summary>
        /// <param name="member">The current member to inspect</param>
        /// <returns>
        /// <b>True</b> if the current member data type is <see cref="IAtomic"/> and the member dimensions are empty.
        /// Otherwise, <b>True</b>/>
        /// </returns>
        public static bool IsValueMember<TDataType>(this IMember<TDataType> member)
            where TDataType : IDataType
        {
            return member.DataType is IAtomic && member.Dimension.AreEmpty;
        }

        /// <summary>
        /// Determines if the current <see cref="IMember{TDataType}"/> is a dimensionless complex type 
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static bool IsStructureMember<TDataType>(this IMember<TDataType> member)
            where TDataType : IDataType
        {
            return !(member.DataType is IAtomic) && member.Dimension.AreEmpty;
        }

        /// <summary>
        /// Determines if the current <see cref="IMember{TDataType}"/> is an array of value types.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static bool IsArrayValueMember<TDataType>(this IMember<TDataType> member)
            where TDataType : IDataType
        {
            return member.DataType is IAtomic && !member.Dimension.AreEmpty;
        }

        /// <summary>
        /// Determines if the current <see cref="IMember{TDataType}"/> is an array of complex types.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static bool IsArrayStructureMember<TDataType>(this IMember<TDataType> member)
            where TDataType : IDataType
        {
            return !(member.DataType is IAtomic) && !member.Dimension.AreEmpty;
        }

        /// <summary>
        /// Determines if the current <see cref="IMember{TDataType}"/> and element of an array.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static bool IsArrayElement<TDataType>(this IMember<TDataType> member)
            where TDataType : IDataType
        {
            return member is IElement<IDataType>;
        }
    }
}