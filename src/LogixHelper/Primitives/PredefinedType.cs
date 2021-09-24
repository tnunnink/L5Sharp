using System.Collections.Generic;
using System.Linq;
using LogixHelper.Enumerations;

namespace LogixHelper.Primitives
{
    public class PredefinedType
    {
        private readonly DataType _dataType;

        internal PredefinedType(DataType dataType)
        {
            _dataType = dataType;
        }
        
        internal PredefinedType(string name)
        {
            _dataType = new DataType(name, DataTypeFamily.None, DataTypeClass.ProductDefined);
        }

        public string Name => _dataType.Name;
        public DataTypeFamily Family => _dataType.Family;
        public DataTypeClass Class => _dataType.Class;
        public IEnumerable<PredefinedTypeMember> Members => _dataType.Members.Select(x => new PredefinedTypeMember(x));
    }
}