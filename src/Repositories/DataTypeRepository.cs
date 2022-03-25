using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Querying;
using L5Sharp.Serialization;

namespace L5Sharp.Repositories
{
    internal class DataTypeRepository : ComponentRepository<IComplexType>, IDataTypeRepository
    {
        public DataTypeRepository(IEnumerable<XElement> elements, IL5XSerializer<IComplexType> serializer) 
            : base(elements, serializer)
        {
        }

        public override void Add(IComplexType component)
        {
            base.Add(component);
            
            var dependents = component.GetDependentTypes()
                .Where(t => t.Class == DataTypeClass.User)
                .Cast<IUserDefined>();

            foreach (var dependent in dependents)
                base.Add(dependent);
        }

        public override void Update(IComplexType component)
        {
            base.Update(component);
            
            var dependents = component.GetDependentTypes()
                .Where(t => t.Class == DataTypeClass.User)
                .Cast<IUserDefined>();

            foreach (var dependent in dependents)
                base.Update(dependent);
        }

        public IDataTypeQuery DependingOn(string typeName)
        {
            throw new System.NotImplementedException();
        }
    }
}