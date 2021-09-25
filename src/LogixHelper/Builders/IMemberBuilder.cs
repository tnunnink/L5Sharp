using LogixHelper.Abstractions;
using LogixHelper.Enumerations;
using LogixHelper.Primitives;

namespace LogixHelper.Builders
{
    public interface IMemberBuilder : IBuilder<DataTypeMember>
    {
        IMemberBuilder WithDataType(IDataType dataType);
        IMemberBuilder WithDescription(string description);
        IMemberBuilder WithDimension(short dimension);
        IMemberBuilder WithRadix(Radix radix);
        IMemberBuilder WithAccess(ExternalAccess access);
    }
}