using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;

[assembly: InternalsVisibleTo("L5Sharp.Repositories.Tests")]

namespace L5Sharp.Repositories
{
    internal class UserDefinedRepository : Repository<IUserDefined>, IUserDefinedRepository
    {
        public UserDefinedRepository(LogixContext context) : base(context)
        {
        }

        public IEnumerable<IDataType> WithMemberType(IDataType dataType)
        {
            /*return Container.Descendants(LogixNames.GetComponentName<IMember<IDataType>>())
                .Where(x => x.GetValue<IMember<IDataType>, IDataType, string>(m => m.DataType, s => s) == dataType.Name)
                .Select(x => Factory.Create(x.Parent));*/
            return null;
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