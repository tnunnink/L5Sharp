using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    public interface IDataTypeMember : IMember
    {
        void SetName(string name);
        void SetDescription(string description);
        void SetDataType(IDataType dataType);
        void SetDimensions(Dimensions dimensions);
        void SetRadix(Radix radix);
        void SetExternalAccess(ExternalAccess externalAccess);
    }
}