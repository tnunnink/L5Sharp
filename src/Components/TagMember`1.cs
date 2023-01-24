using System;
using System.Linq.Expressions;
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
        public ILogixTagMember<TMemberType> Member<TMemberType>(Expression<Func<TLogixType, TMemberType>> memberSelector)
            where TMemberType : ILogixType
        {
            if (_member.DataType is not TLogixType logixType)
                throw new ArgumentException();
            
            if (_member.DataType is not StructureType structureType)
                throw new InvalidOperationException();

            if (memberSelector is not MemberExpression memberExpression)
                throw new ArgumentException();
            
            
            
            var memberType = memberSelector.Compile().Invoke(logixType);

            //return new TagMember<TMemberType>(memberType, this, _tag);
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public TAtomicType GetValue<TAtomicType>(Func<TLogixType, TAtomicType> memberSelector) 
            where TAtomicType : AtomicType
        {
            if (_member.DataType is not TLogixType logixType)
                throw new ArgumentException();
            
            return memberSelector.Invoke(logixType);
        }

        /// <inheritdoc />
        public void SetValue<TAtomicType>(Func<TLogixType, TAtomicType> memberSelector, TAtomicType value) 
            where TAtomicType : AtomicType
        {
            if (_member.DataType is not TLogixType logixType)
                throw new ArgumentException();
            
            var atomic = memberSelector.Invoke(logixType);

            atomic = value;
        }
    }
}