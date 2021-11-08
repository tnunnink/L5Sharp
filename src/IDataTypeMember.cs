using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    public interface IDataTypeMember<out TDataType> : IMember<TDataType> where TDataType : IDataType
    {
        void SetName(string name);
        void SetDescription(string description);
        void SetDimensions(Dimensions dimensions);
        void SetRadix(Radix radix);
        void SetExternalAccess(ExternalAccess externalAccess);
    }
}