using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Configurations
{
    public interface IDataTypeMemberNameConfiguration
    {
        IDataTypeMemberTypeConfiguration WithName(string name);
    }
    
    public interface IDataTypeMemberTypeConfiguration
    {
        IDataTypeMemberConfiguration OfType(IDataType dataType);
    }
    
    public interface IDataTypeMemberConfiguration
    {
        IDataTypeMemberConfiguration WithDimension(Dimensions dimensions);
        IDataTypeMemberConfiguration WithRadix(Radix radix);
        IDataTypeMemberConfiguration WithAccess(ExternalAccess externalAccess);
        IDataTypeMemberConfiguration WithDescription(string description);
    }
}