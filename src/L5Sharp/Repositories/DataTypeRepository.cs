using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
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
        public DataTypeRepository(XElement context) : base(context)
        {
        }

        public override void Add(IDataType component)
        {
            if (Context.Contains<IDataType>(component.Name))
                Throw.ComponentNameCollisionException(component.Name, typeof(IDataType));

            var element = component.Serialize();
            
            var dependents = component.GetDependentTypes()
                .Where(t => t.Class == DataTypeClass.User)
                .Select(t => t.Serialize());

            Context.Add(element);
            Context.Add(dependents);
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
            return Context.Descendants(LogixNames.Components.Member)
                .Where(x => x.GetDataTypeName() == dataType.Name)
                .Select(x => x.Parent.Deserialize<DataType>());
        }
    }
}