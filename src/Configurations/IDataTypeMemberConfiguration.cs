using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Configurations
{
    public interface IDataTypeMemberConfiguration : IComponentConfiguration<IDataTypeMember<IDataType>>
    {
        IDataTypeMemberConfiguration HasDescription(string description);
        IDataTypeMemberConfiguration OfType(IDataType dataType);
        IDataTypeMemberConfiguration WithDimension(Dimensions dimensions);
        IDataTypeMemberConfiguration WithRadix(Radix radix);
        IDataTypeMemberConfiguration WithAccess(ExternalAccess externalAccess);
    }
}