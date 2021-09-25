using System.Collections.Generic;
using L5Sharp.Abstractions;
using L5Sharp.Builders;

namespace L5Sharp.Primitives
{
    public class Controller : IController
    {
        private Dictionary<string, DataType> _dataTypes = new Dictionary<string, DataType>();
        
        public Controller()
        {
            
        }

        public IEnumerable<DataType> DataTypes => _dataTypes.Values;
        
        public IControllerBuilder Create()
        {
            return new ControllerBuilder(this);
        }
    }
}