using System;
using L5Sharp.Builders;

namespace L5Sharp
{
    public interface IParameters : IComponentCollection<IParameter<IDataType>>
    {
        void Add(string name, IDataType dataType, Action<IParameterBuilder<IDataType>> builder = null);
    }
}