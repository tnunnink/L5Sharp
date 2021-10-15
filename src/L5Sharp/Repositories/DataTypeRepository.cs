using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

[assembly: InternalsVisibleTo("L5Sharp.Repositories.Tests")]

namespace L5Sharp.Repositories
{
    internal class DataTypeRepository : Repository<IDataType>, IDataTypeRepository
    {
        public DataTypeRepository(LogixContext context) : base(context)
        {
        }

        public override IDataType Get(string name)
        {
            return Predefined.ContainsType(name) ? Predefined.ParseType(name) : base.Get(name);
        }

        public override void Add(IDataType component)
        {
            if (Container.Contains<IDataType>(component.Name))
                Throw.ComponentNameCollisionException(component.Name, typeof(IDataType));

            var element = component.Serialize();
            
            var dependents = component.GetDependentTypes()
                .Where(t => t.Class == DataTypeClass.User)
                .Select(t => t.Serialize());

            Container.Add(element);
            Container.Add(dependents);
        }

        public override void Remove(IDataType component)
        {
            throw new System.NotImplementedException();
        }

        public override void Update(IDataType component)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IDataType> WithMemberType(IDataType dataType)
        {
            return Container.Descendants(LogixNames.Components.Member)
                .Where(x => x.GetValue<IMember, IDataType, string>(m => m.DataType, s => s) == dataType.Name)
                .Select(x => Factory.Create(x.Parent));
        }
    }
}