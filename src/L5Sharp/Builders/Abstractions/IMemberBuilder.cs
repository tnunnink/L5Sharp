using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Enumerations;

namespace L5Sharp.Builders.Abstractions
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