using System.Collections.Generic;
using System.Linq;
using L5Sharp.Types;

namespace L5Sharp.Extensions
{
    /// <summary>
    /// Extensions for various types implementing <see cref="ILogixType"/> interface.
    /// </summary>
    public static class LogixTypeExtensions
    {
        /// <summary>
        /// Explicitly casts the current <see cref="ILogixType"/> to the specified derived type.
        /// </summary>
        /// <param name="logixType">The </param>
        /// <typeparam name="TLogixType"></typeparam>
        /// <returns></returns>
        public static TLogixType ToType<TLogixType>(this ILogixType logixType) where TLogixType : ILogixType =>
            (TLogixType)logixType;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logixType"></param>
        /// <typeparam name="TLogixType"></typeparam>
        /// <returns></returns>
        public static TLogixType? AsType<TLogixType>(this ILogixType logixType) where TLogixType : class, ILogixType =>
            logixType as TLogixType;
        
        /// <summary>
        /// Returns a new <see cref="ArrayType{TLogixType}"/> of generic <see cref="ILogixType"/> objects.
        /// </summary>
        /// <returns>A <see cref="ArrayType{TLogixType}"/> of generic logix type objects.</returns>
        public static ArrayType<TLogixType> ToArrayType<TLogixType>(this IEnumerable<TLogixType> enumerable)
            where TLogixType : ILogixType => new(enumerable.ToArray());
    }
}