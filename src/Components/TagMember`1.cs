using System;
using L5Sharp.Core;
using L5Sharp.Types;

namespace L5Sharp.Components
{
    public class TagMember<TLogixType> : TagMember, ILogixTagMember<TLogixType> where TLogixType : ILogixType
    {
        internal TagMember(Member member, ILogixTagMember parent, Tag tag) : base(member, parent, tag)
        {
        }

        /// <inheritdoc />
        public ILogixTagMember<TMemberType> Member<TMemberType>(Func<TLogixType, TMemberType> memberSelector) 
            where TMemberType : ILogixType
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public TAtomicType GetValue<TAtomicType>(Func<TLogixType, TAtomicType> memberSelector) 
            where TAtomicType : AtomicType
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void SetValue<TAtomicType>(Func<TLogixType, TAtomicType> memberSelector, TAtomicType value) 
            where TAtomicType : AtomicType
        {
            throw new NotImplementedException();
        }
    }
}