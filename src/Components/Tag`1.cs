using System;
using L5Sharp.Types;

namespace L5Sharp.Components
{
    public class Tag<TLogixType> : Tag, ILogixTagMember<TLogixType> where TLogixType : ILogixType
    {
        public ILogixTagMember<TMemberType> Member<TMemberType>(Func<TLogixType, TMemberType> memberSelector)
            where TMemberType : ILogixType
        {
            throw new NotImplementedException();
        }

        public TAtomicType GetValue<TAtomicType>(Func<TLogixType, TAtomicType> memberSelector) where TAtomicType : AtomicType
        {
            throw new NotImplementedException();
        }

        public void SetValue<TAtomicType>(Func<TLogixType, TAtomicType> memberSelector, TAtomicType value) where TAtomicType : AtomicType
        {
            throw new NotImplementedException();
        }
    }
}