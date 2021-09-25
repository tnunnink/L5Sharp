using LogixHelper.Abstractions;
using LogixHelper.Enumerations;
using LogixHelper.Primitives;
using LogixHelper.Utilities;

namespace LogixHelper.Builders
{
    public class MemberBuilder : IMemberBuilder
    {
        private readonly DataTypeMember _member;

        public MemberBuilder(DataTypeMember member)
        {
            _member = member;
        }

        public IMemberBuilder WithDataType(IDataType dataType)
        {
            _member.DataType = dataType;
            return this;
        }

        public IMemberBuilder WithDescription(string description)
        {
            _member.Description = description;
            return this;
        }

        public IMemberBuilder WithDimension(short dimension)
        {
            _member.Dimension = dimension;
            return this;
        }

        public IMemberBuilder WithRadix(Radix radix)
        {
            //todo need to validate radix based on current datatype
            _member.Radix = radix;
            return this;
        }

        public IMemberBuilder WithAccess(ExternalAccess access)
        {
            _member.ExternalAccess = access;
            return this;
        }
    }
}