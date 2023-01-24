using System;
using System.Linq.Expressions;
using L5Sharp.Types;

namespace L5Sharp
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TLogixType"></typeparam>
    public interface ILogixTagMember<TLogixType> : ILogixTagMember
        where TLogixType : ILogixType
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberSelector"></param>
        /// <typeparam name="TMemberType"></typeparam>
        /// <returns></returns>
        ILogixTagMember<TMemberType> Member<TMemberType>(Expression<Func<TLogixType, TMemberType>> memberSelector)
            where TMemberType : ILogixType;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberSelector"></param>
        /// <typeparam name="TAtomicType"></typeparam>
        /// <returns></returns>
        TAtomicType GetValue<TAtomicType>(Func<TLogixType, TAtomicType> memberSelector)
            where TAtomicType : AtomicType;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberSelector"></param>
        /// <param name="value"></param>
        /// <typeparam name="TAtomicType"></typeparam>
        void SetValue<TAtomicType>(Func<TLogixType, TAtomicType> memberSelector, TAtomicType value)
            where TAtomicType : AtomicType;
    }
}