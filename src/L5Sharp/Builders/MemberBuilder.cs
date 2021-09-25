using System;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Primitives;
using L5Sharp.Utilities;

namespace L5Sharp.Builders
{
    public class MemberBuilder : BaseBuilder<DataTypeMember>, IMemberBuilder
    {
        public MemberBuilder(DataTypeMember model) : base(model)
        {
        }

        public IMemberBuilder WithDataType(IDataType dataType)
        {
            if (dataType == null) throw new ArgumentNullException(nameof(dataType));

            Model.DataType = dataType;
            return this;
        }

        public IMemberBuilder WithDescription(string description)
        {
            Model.Description = description ?? string.Empty;
            return this;
        }

        public IMemberBuilder WithDimension(ushort dimension)
        {
            Model.Dimension = dimension;
            return this;
        }

        public IMemberBuilder WithRadix(Radix radix)
        {
            if (radix == null) throw new ArgumentNullException(nameof(radix));
            
            if (!radix.SupportsType(Model.DataType))
                Throw.RadixNotSupportedException(radix, Model.DataType);
            
            Model.Radix = radix;
            return this;
        }

        public IMemberBuilder WithAccess(ExternalAccess access)
        {
            if (access == null) throw new ArgumentNullException(nameof(access));
            
            Model.ExternalAccess = access;
            return this;
        }

    }
}