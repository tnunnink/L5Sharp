using System.Linq;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Repositories
{
    internal class DataTypeRepository : Repository<IUserDefined>
    {
        public DataTypeRepository(LogixContext context) : base(context)
        {
        }

        public override void Add(IUserDefined component)
        {
            base.Add(component);
            
            //We also want to add dependent user defined types.
            var dependents = component.GetDependentTypes()
                .Where(t => t.Class == DataTypeClass.User)
                .Cast<IUserDefined>();

            foreach (var dependent in dependents)
                base.Add(dependent);
        }
    }
}