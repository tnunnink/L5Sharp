using System.Linq;
using L5Sharp.Enums;
using L5Sharp.L5X;
using L5Sharp.Querying;

namespace L5Sharp.Repositories
{
    internal class DataTypeRepository : ComponentRepository<IComplexType>, IDataTypeRepository
    {
        public DataTypeRepository(L5XDocument document) : base(document)
        {
        }

        public void AddAll(IComplexType dataType)
        {
            Add(dataType);

            var dependents = dataType.Members.Where(m => m.DataType.Class == DataTypeClass.User);

            foreach (var dependent in dependents)
            {
                if (dependent.DataType is not IComplexType complexType)
                    continue;
                
                AddAll(complexType);
            }
        }
        
        public IDataTypeQuery DependingOn(string typeName) => 
            new DataTypeQuery(Elements, Serializer).DependingOn(typeName);
    }
}