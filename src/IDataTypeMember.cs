using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    public interface IDataTypeMember<out TDataType> : IMember<TDataType> where TDataType : IDataType
    {
        void SetName(string name);
        void SetDescription(string description);
        //void SetDataType(IDataType dataType); todo this should probably be immutable? if it wasn't, it would not longer be of TDataType and would need to return new instance
        void SetDimensions(Dimensions dimensions);
        void SetRadix(Radix radix);
        void SetExternalAccess(ExternalAccess externalAccess);
    }
}