using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Utilities;

[assembly: InternalsVisibleTo("L5Sharp.Repositories.Tests")]

namespace L5Sharp.Repositories
{
    internal class DataTypeRepository : Repository<IUserDefined>, IDataTypeRepository
    {
        public DataTypeRepository(LogixContext context) : base(context)
        {
        }

        public override void Add(IUserDefined component)
        {
            if (Container.Contains<IDataType>(component.Name))
                throw new ComponentNameCollisionException(component.Name, typeof(IDataType));

            var element = component.Serialize();
            
            var dependents = component.GetDependentTypes()
                .Where(t => t.Class == DataTypeClass.User)
                .Select(t => t.Serialize());

            Container.Add(element);
            Container.Add(dependents);
        }

        public override void Update(IUserDefined component)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IDataType> WithMemberType(IDataType dataType)
        {
            return Container.Descendants(LogixNames.GetComponentName<IMember<IDataType>>())
                .Where(x => x.GetValue<IMember<IDataType>, IDataType, string>(m => m.DataType, s => s) == dataType.Name)
                .Select(x => Factory.Create(x.Parent));
        }
    }
}