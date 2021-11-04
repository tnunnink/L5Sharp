using System;
using L5Sharp.Configurations;

namespace L5Sharp
{
    public interface IParameters : IComponentCollection<IParameter>
    {
        void Add(string name, IDataType dataType, Action<IParameterConfiguration> config = null);
    }
}