using System;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Primitives;
using L5Sharp.Utilities;

namespace L5Sharp.Builders
{
    internal class MemberBuilder : IMemberBuilder
    {
        private readonly Member _member;

        public MemberBuilder(string name, IDataType dataType)
        {
            _member = new Member(name, dataType);
        }

        public Member Build()
        {
            return _member;
        }

        public IMemberBuilder HasType(IDataType dataType)
        {
            if (dataType == null) throw new ArgumentNullException(nameof(dataType));

            _member.DataType = dataType;
            return this;
        }

        public IMemberBuilder HasDescription(string description)
        {
            _member.Description = description ?? string.Empty;
            return this;
        }

        public IMemberBuilder HasDimension(ushort dimension)
        {
            _member.Dimension = dimension;
            return this;
        }

        public IMemberBuilder HasRadix(Radix radix)
        {
            if (radix == null) throw new ArgumentNullException(nameof(radix));
            
            if (!_member.DataType.SupportsRadix(radix))
                Throw.RadixNotSupportedException(radix, _member.DataType);
            
            _member.Radix = radix;
            return this;
        }

        public IMemberBuilder HasAccess(ExternalAccess access)
        {
            if (access == null) throw new ArgumentNullException(nameof(access));
            
            _member.ExternalAccess = access;
            return this;
        }
    }
}