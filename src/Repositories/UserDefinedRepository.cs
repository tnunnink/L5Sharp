using System.Linq;
using System.Runtime.CompilerServices;
using L5Sharp.Enums;
using L5Sharp.Extensions;

[assembly: InternalsVisibleTo("L5Sharp.Repositories.Tests")]

namespace L5Sharp.Repositories
{
    internal class UserDefinedRepository : Repository<IUserDefined>
    {
        public UserDefinedRepository(LogixContext context) : base(context)
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