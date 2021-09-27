using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Primitives;

namespace L5Sharp.Builders
{
    public interface IMemberBuilder : IBuilder<Member>
    {
        IMemberBuilder HasType(IDataType dataType);
        IMemberBuilder HasDescription(string description);
        IMemberBuilder HasDimension(ushort dimension);
        IMemberBuilder HasRadix(Radix radix);
        IMemberBuilder HasAccess(ExternalAccess access);
    }
}