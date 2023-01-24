using System;
using System.Linq.Expressions;
using L5Sharp.Types;

namespace L5Sharp.Components
{
    public class Tag<TLogixType> : Tag, ILogixTagMember<TLogixType> where TLogixType : ILogixType
    {
        private readonly TLogixType _logixType;

        public Tag(string name, TLogixType logixType)
        {
            _logixType = logixType;
        }
        
        public ILogixTagMember<TMemberType> Member<TMemberType>(Expression<Func<TLogixType, TMemberType>> memberSelector) where TMemberType : ILogixType
        {
            if (_logixType is not StructureType structureType)
                throw new InvalidOperationException();

            if (memberSelector.Body is not MemberExpression memberExpression)
                throw new ArgumentException();
            
            
            var memberType = memberSelector.Compile().Invoke(_logixType);

            //return new TagMember<TMemberType>(memberType, this, _tag);
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