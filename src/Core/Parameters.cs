using System;
using System.Collections.Generic;
using L5Sharp.Abstractions;
using L5Sharp.Builders;

namespace L5Sharp.Core
{
    public class Parameters : ComponentCollection<IParameter<IDataType>>, IParameters
    {
        public Parameters(IAddOnDefined addOnDefined, IEnumerable<IParameter<IDataType>> parameters = null)
        {
            
        }
        
        public void Add(string name, IDataType dataType, Action<IParameterBuilder<IDataType>> builder = null)
        {
            throw new NotImplementedException();
        }
    }
}