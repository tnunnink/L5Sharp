using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Configurations
{
    public interface IMemberNameConfiguration
    {
        IMemberTypeConfiguration WithName(string name);
    }
    
    public interface IMemberTypeConfiguration
    {
        IMemberConfiguration OfType(IDataType dataType);
    }
    
    public interface IMemberConfiguration
    {
        IMemberConfiguration WithDimension(Dimensions dimensions);
        IMemberConfiguration WithRadix(Radix radix);
        IMemberConfiguration WithAccess(ExternalAccess externalAccess);
        IMemberConfiguration WithDescription(string description);
    }
}