using LogixHelper.Abstractions;
using LogixHelper.Enumerations;

namespace LogixHelper.Builders
{
    public interface IMemberBuilder
    {
        IMemberBuilder WithDataType(IDataType dataType);
        IMemberBuilder WithDescription(string description);
        IMemberBuilder WithDimension(short dimension);
        IMemberBuilder WithRadix(Radix radix);
        IMemberBuilder WithAccess(ExternalAccess access);
    }
}