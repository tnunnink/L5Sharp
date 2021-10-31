using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Configurations
{
    public interface IDataTypeMemberConfiguration : IComponentConfiguration<IDataTypeMember>
    {
        IDataTypeMemberConfiguration HasDataType(IDataType dataType);
        IDataTypeMemberConfiguration HasDescription(string description);
        IDataTypeMemberConfiguration HasDimension(Dimensions dimensions);
        IDataTypeMemberConfiguration HasRadix(Radix radix);
        IDataTypeMemberConfiguration HasAccess(ExternalAccess externalAccess);
    }
}