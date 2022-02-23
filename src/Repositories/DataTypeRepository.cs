using System.Linq;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Repositories
{
    internal class DataTypeRepository : Repository<IUserDefined>
    {
        public DataTypeRepository(L5XContext context) : base(context)
        {
        }

        public override void Add(IUserDefined component)
        {
            base.Add(component);
            
            var dependents = component.GetDependentTypes()
                .Where(t => t.Class == DataTypeClass.User)
                .Cast<IUserDefined>();

            foreach (var dependent in dependents)
                base.Add(dependent);
        }

        public override void Update(IUserDefined component)
        {
            base.Update(component);
            
            var dependents = component.GetDependentTypes()
                .Where(t => t.Class == DataTypeClass.User)
                .Cast<IUserDefined>();

            foreach (var dependent in dependents)
                base.Update(dependent);
        }
    }
}