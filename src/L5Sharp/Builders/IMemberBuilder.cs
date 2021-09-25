using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Primitives;

namespace L5Sharp.Builders
{
    public interface IMemberBuilder : IBuilder<DataTypeMember>
    {
        IMemberBuilder WithDataType(IDataType dataType);
        IMemberBuilder WithDescription(string description);
        IMemberBuilder WithDimension(ushort dimension);
        IMemberBuilder WithRadix(Radix radix);
        IMemberBuilder WithAccess(ExternalAccess access);
    }
}