using L5Sharp.Primitives;
using L5Sharp.Repositories.Abstractions;
using L5Sharp.Utilities;

namespace L5Sharp.Repositories
{
    public class DataTypeRepository : IDataTypeRepository
    {
        private readonly L5X _l5X;

        public DataTypeRepository(L5X l5X)
        {
            _l5X = l5X;
        }
        
        public DataType Get(string name)
        {
            return _l5X.Get<DataType>(name);
        }

        public void Add(DataType component)
        {
            if (_l5X.Contains<DataType>(component.Name))
                Throw.NameCollisionException(component.Name, typeof(DataType));
            
            var element = component.Serialize();

            var container = _l5X.GetContainer<DataType>();

            container.Add(element);
        }

        public void Remove(DataType component)
        {
            throw new System.NotImplementedException();
        }

        public void Update(DataType component)
        {
            throw new System.NotImplementedException();
        }
    }
}