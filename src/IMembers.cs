using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    public interface IMembers : IComponentCollection<IMember<IDataType>>
    {
        void UpdateDataType(string name, IDataType dataType);
        void UpdateDimensions(string name, Dimensions dimensions);
        void UpdateAccess(string name, ExternalAccess externalAccess);
    }
}