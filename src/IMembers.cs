using System;
using L5Sharp.Configurations;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    public interface IMembers : IComponentCollection<IMember<IDataType>>
    {
        void Add(Action<IMemberNameConfiguration> config);
        void Update(string name, Action<IMemberNameConfiguration> config);
        void SetDataType(string name, IDataType dataType);
        void SetDimensions(string name, Dimensions dimensions);
        void SetAccess(string name, ExternalAccess externalAccess);
    }
}