using System.Collections.Generic;
using LogixHelper.Abstractions;
using LogixHelper.Builders;

namespace LogixHelper.Primitives
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